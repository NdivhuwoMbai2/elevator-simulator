using elevator_simulator.common.v1.Interfaces;
using elevator_simulator.common.v1.Models;
using elevator_simulator.core.v1.Handlers;
using elevator_simulator.core.v1.Repo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{
    public static Building? Building;
    public static int NumOfElevators;
    public static List<Elevator>? Elevators;
    public static Elevator? Elevator;
    public static ElevatorType ElevatorType;
    public static Queue<Request>? ElevatorQueue;
    public static IFloorRequestHandler FloorRequestHandler;
    public static IElevatorRepository ElevatorRepository;
    public static IQueueHandler QueueHandler;
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
                         .AddSingleton<IQueueHandler, QueueHandler>().
                         AddSingleton<IElevatorRepository, ElevatorRepository>()
                        ;

                    var serviceProvider = services.BuildServiceProvider();

                    FloorRequestHandler = serviceProvider.GetRequiredService<IFloorRequestHandler>();
                    QueueHandler = serviceProvider.GetRequiredService<IQueueHandler>();
                    ElevatorRepository = serviceProvider.GetRequiredService<IElevatorRepository>();

                    Initialize();

                    SetupBuildingElevators();

                    Task.Run(() => ProcessElevatorRequest());

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
    private async static void ProcessElevatorRequest()
    {
        while (isSystemRunning)
        {
            Console.WriteLine("What is your current floor number");
            int currentFloor = IsValidInt(Console.ReadLine());


            Console.WriteLine("What floor would like to go to?");
            int eleKey = IsValidInt(Console.ReadLine());

            Console.WriteLine("How many passengers?");
            int passengers = IsValidInt(Console.ReadLine());


            await QueueHandler.AddToQueue(new Request() { Destination = eleKey, CurrentFloor = currentFloor, NumberOfPassengers = passengers }, ElevatorQueue);

            ProcessQueues(ElevatorQueue, Elevators);
        }
    }

    private static async Task<Request> ProcessQueues(Queue<Request> elevatorQueue, List<Elevator>? elevators)
    {
        while (elevatorQueue.Count > 0)
        {
            //get the closest elevator
            var request = elevatorQueue.FirstOrDefault();
            try
            {
                Elevator elevator = FloorRequestHandler.GetClosestElevator(request.CurrentFloor, elevators);

                //this methed with send the elevator to pickup the passengers
                elevator = await QueueHandler.SendElevatorToPickup(request, elevator);

                elevator = await QueueHandler.PickUpPassengers(elevator, request);


                //SendElevator to the requested destination
                elevator = await QueueHandler.SendElevatorToDropOff(request, elevator);

                elevator = await QueueHandler.DropPassengers(elevator, request);

                return await Task.FromResult(elevatorQueue.Dequeue());
            }
            catch (System.InvalidOperationException ex)
            {

                throw;
            }
        }

        Console.WriteLine("No request pending ");

        return await Task.FromResult(elevatorQueue.Dequeue());
    }

    private static void SetupBuildingElevators()
    {
        Console.WriteLine("Welcome to the Elevator Simulator");
        //Building name
        Console.WriteLine("What is the name of your Building?");
        Building = new Building(Console.ReadLine());

        Console.WriteLine("Help me setup the simulator for you");
        Console.WriteLine("===================================");
        Console.WriteLine($"How many elevators are in the building {Building.Name} ?");
        NumOfElevators = IsValidInt(Console.ReadLine());

        for (int i = 0; i < NumOfElevators; i++)
        {
            Elevator = new Elevator() { currentFloor = 0 };
            Console.WriteLine($"Elevator number {i}");
            Console.WriteLine("==================");
            Console.WriteLine("What is the name of the Elevator (Alias)");
            Elevator.Name = Console.ReadLine();
            Console.WriteLine($"What type of elevator is elevator number {i} choose from the list below");
            Console.WriteLine($"Choose 1 either 1,2 or 3 e.t.c");
            for (int j = 0; j < ElevatorType.ElevatorTypes.Count; j++)
            {
                Console.WriteLine($"{j} {ElevatorType.ElevatorTypes[j]}");
            }
            int typeOfElevator = IsValidInt(Console.ReadLine());

            Elevator.ElevatorType = ElevatorType.ElevatorTypes[typeOfElevator];

            Console.WriteLine($"How many floors are in the building?");

            Elevator.TopFloor = IsValidInt(Console.ReadLine());

            Console.WriteLine($"What is the elevator capacity (10,1 e.t.c)?");

            Elevator.MaximumCapacity = IsValidInt(Console.ReadLine());

            Elevators = ElevatorRepository.AddElevator(Elevator, Elevators);
        }
        Console.WriteLine("All Set");
        Console.WriteLine("=======");
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
        Elevators = new List<Elevator>();
        ElevatorType.ElevatorTypes = ElevatorRepository.LoadElevatorTypes();
    }


}
//number of elevators

//elevator num1
//elevator type
//number of floors

