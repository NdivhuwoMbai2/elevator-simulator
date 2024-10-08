﻿using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using elevator_simulator.core.v1.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static Building? Building;
    public static int NumOfElevators;
    public static Elevator? Elevator;
    public static ElevatorType? ElevatorType;
    public static Queue<Request>? ElevatorQueue;
    public static IFloorRequestHandler? FloorRequestHandler;
    public static IElevatorHandler? ElevatorHandler;
    public static IQueueHandler? QueueHandler;
    public static IPassengerHandler? PassengerHandler;
    public static bool isSystemRunning = false;


    /// <summary>
    /// Main routine
    /// </summary>
    /// <param name="args"></param>
    /// <returns>Exit code</returns>
    public static async Task Main(string[] args)
    {
        try
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, services) =>
                {

                    isSystemRunning = true;
                    //setup our DI
                    services.AddSingleton<IFloorRequestHandler, FloorRequestHandler>()
                    .AddSingleton<IQueueHandler, QueueHandler>()
                    .AddSingleton<IElevatorHandler, ElevatorHandler>()
                    .AddSingleton<IPassengerHandler, PassengerHandler>();


                    var serviceProvider = services.BuildServiceProvider();

                    FloorRequestHandler = serviceProvider.GetRequiredService<IFloorRequestHandler>();
                    QueueHandler = serviceProvider.GetRequiredService<IQueueHandler>();
                    ElevatorHandler = serviceProvider.GetRequiredService<IElevatorHandler>();
                    PassengerHandler = serviceProvider.GetRequiredService<IPassengerHandler>();

                    Initialize();

                    SetupBuildingElevators();

                    Task.Run(() => ProcessElevatorSetupRequest());

                })
                .Build();

            await host.RunAsync();
        }
        catch (Exception ex)
        {
            // Note that this should only occur if something went wrong with building Host
            await Console.Error.WriteLineAsync(ex.Message);

        }
    }
    private async static void ProcessElevatorSetupRequest()
    {
        try
        {
            ConsoleKey response;
            do
            {
                Console.WriteLine();
                Console.Write("If you would to request an elevator PRESS L ");
                Console.WriteLine();
                Console.Write("If you like to add elevator type PRESS S  or press any key to exit");
                Console.WriteLine();
                response = Console.ReadKey(false).Key;   // true is intercept key (dont show), false is show
                if (response != ConsoleKey.Enter)
                    Console.WriteLine();
                if (response == ConsoleKey.L)
                {
                    RunElevatorRequest();
                }
                else if (response == ConsoleKey.S)
                {
                    RunElevatorTypeSeup();
                }
            } while (response == ConsoleKey.L && response == ConsoleKey.S);
            Console.WriteLine("done!!!");
        }
        catch (Exception ex)
        {
            await Console.Error.WriteLineAsync(ex.Message);
        }
    }

    private static void RunElevatorTypeSeup()
    {
        Console.WriteLine();
        Console.WriteLine("What is the title of the new elevatortype");
        Console.WriteLine();
        ElevatorType.ElevatorTypes = ElevatorHandler.AddElevatorType(ElevatorType.ElevatorTypes, Console.ReadLine());
    }

    private async static void RunElevatorRequest()
    {
        Console.WriteLine();
        Console.WriteLine("What is your current floor number");
        int currentFloor = IsValidInt(Console.ReadLine());

        Console.WriteLine();
        Console.WriteLine("What floor would like to go to?");
        int eleKey = IsValidInt(Console.ReadLine());

        Console.WriteLine();
        Console.WriteLine("How many passengers?");
        int passengers = IsValidInt(Console.ReadLine());


        await QueueHandler.Add(new Request() { Destination = eleKey, CurrentFloor = currentFloor, NumberOfPassengers = passengers }, ElevatorQueue);

        await ProcessQueues(ElevatorQueue, Building.Elevators);
    }

    private static async Task<Queue<Request>> ProcessQueues(Queue<Request> elevatorQueue, List<Elevator>? elevators)
    {
        while (elevatorQueue.Count > 0)
        {
            //get the closest elevator
            var request = elevatorQueue.FirstOrDefault();
            try
            {
                //search for an elevator with enough space to avoid overload 
                var availableEleList = ElevatorHandler.GetElevatorWithSpace(request, elevators).Result;

                if (availableEleList.Count == 0)
                {
                    Console.WriteLine($"Elevators are out of space for Request at Floor {request.CurrentFloor} going to " +
                        $"{request.Destination} carrying {request.NumberOfPassengers} number of people");
                    elevatorQueue.Dequeue();
                    continue;
                }
                else
                {
                    Elevator elevator = await ElevatorHandler.GetClosestElevator(request.CurrentFloor, availableEleList);

                    //this methed with send the elevator to pickup the passengers
                    elevator = await QueueHandler.SendElevatorToPickup(request, elevator);
                     
                    elevator = await PassengerHandler.Boarding(elevator_simulator.common.Enums.Boarding.In, elevator, request);


                    //SendElevator to the requested destination
                    elevator = await QueueHandler.SendElevatorToDropOff(request, elevator); 

                    elevator = await PassengerHandler.Boarding(elevator_simulator.common.Enums.Boarding.Out, elevator, request);
                    elevatorQueue.Dequeue();
                }
            }
            catch (System.InvalidOperationException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Invalid operation occured while dequeuing");
            }
        }
        Console.WriteLine("No request pending ");
        return await Task.FromResult(elevatorQueue);
    }

    private static void SetupBuildingElevators()
    {
        Console.WriteLine("Welcome to the Elevator Simulator");
        //Building name
        Console.WriteLine("What is the name of your Building?");
        Building = new Building(Console.ReadLine());

        Console.WriteLine("Help me setup the simulator for you");
        Console.WriteLine("===================================");
        Console.WriteLine();
        Console.WriteLine($"How many elevators are in the building {Building.Name} ?");
        NumOfElevators = IsValidInt(Console.ReadLine());

        for (int i = 0; i < NumOfElevators; i++)
        {
            Elevator = new Elevator() { currentFloor = 0 };
            Console.WriteLine($"Elevator number {i}");
            Console.WriteLine("==================");
            Console.WriteLine();
            Console.WriteLine("What is the name of the Elevator (Alias)");
            Elevator.Name = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine($"What type of elevator is elevator number {i} choose from the list below");
            Console.WriteLine($"Choose 1 either 1,2 or 3 e.t.c");
            for (int j = 0; j < ElevatorType.ElevatorTypes.Count; j++)
            {
                Console.WriteLine($"{j} {ElevatorType.ElevatorTypes[j]}");
            }
            int typeOfElevator = isValidElevatorType(Console.ReadLine(), ElevatorType.ElevatorTypes);

            Elevator.ElevatorType = ElevatorType.ElevatorTypes[typeOfElevator];

            Console.WriteLine($"How many floors are in the building?");
            Elevator.TopFloor = IsValidInt(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine($"What is the elevator capacity (10,1 e.t.c)?");
            Elevator.MaximumCapacity = IsValidInt(Console.ReadLine());
            Console.WriteLine();

            Building.Elevators = ElevatorHandler.Add(Elevator, Building.Elevators);
        }
        Console.WriteLine("All Set");
        Console.WriteLine("=======");
        Console.WriteLine();
    }
    private static int isValidElevatorType(string? elevatorType, List<string> elevatorTypes)
    {
        int input;
        while (!Int32.TryParse(elevatorType, out input))
        {
            Console.WriteLine("Not a valid number, try again.");
            elevatorType = Console.ReadLine();
        }
        while (input > elevatorType.Length)
        {

            Console.WriteLine("Not a valid number, elevator type please select 0,1 as shown above try again.");
            input = IsValidInt(Console.ReadLine());
        }
        return input;
    }
    private static int IsValidInt(string? elevatorType)
    {
        int result;
        while (!Int32.TryParse(elevatorType, out result))
        {
            Console.WriteLine("Not a valid number, try again.");
            elevatorType = Console.ReadLine();
        }
        return result;
    }
    private static void Initialize()
    {
        ElevatorType = new ElevatorType();
        ElevatorQueue = new Queue<Request>();
        ElevatorType.ElevatorTypes = ElevatorHandler.LoadElevatorTypes();
    }
}

