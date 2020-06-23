using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;

namespace VehicleCollisions.Scenes
{
    internal class CarCrashWithBike : IScene
    {
        public int AccidentIndex;

        public Vector3[] AccidentLocations =
        {
            new Vector3(-939.94f, -208.75f, 37.76f),
            new Vector3(-1184.51f, -299.4f, 37.79f),
            new Vector3(-1574.84f, -861.45f, 10.18f),
            new Vector3(-630.67f, -1295.32f, 10.67f),
            new Vector3(-539.57f, -669.69f, 33.24f),
            new Vector3(-442.42f, -519.22f, 24.84f),
            new Vector3(-34.52f, 23.25f, 72f),
            new Vector3(696.62f, 11.58f, 84.19f),
            new Vector3(1070.42f, -404.58f, 67.05f),
            new Vector3(775.96f, -856.99f, 25.56f),
            new Vector3(208.11f, -1136.59f, 29.33f),
            new Vector3(-24.11f, -1620.57f, 29.29f),
            new Vector3(733.44f, -2469.09f, 20.21f),
            new Vector3(-719.87f, -2390.4f, 14.76f)
        };

        public Vector3 RandomCoordinates;

        public CarCrashWithBike()
        {
            // Get a random accident
            AccidentIndex = Utilities.Between(0, AccidentLocations.Length);

            // Get the random accident coordinates
            RandomCoordinates = AccidentLocations[AccidentIndex];
        }

        private static Random rnd => new Random();

        public Vector3 BikeCoordinates => new Vector3(Coordinates.X + rnd.Next(1, 10), Coordinates.Y + rnd.Next(1, 10),
            Coordinates.Z);

        public string Title => "Car crash with bike";
        public string Description => "We've received a call a bike has collided with a car.";
        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call a bike has collided with a car.",
                "Treat the victim and tow away the vehicle and bike."
            };
        }

        public Vector3 Coordinates => new Vector3(RandomCoordinates.X + rnd.Next(0, 10),
            RandomCoordinates.Y + rnd.Next(0, 10), RandomCoordinates.Z);

        public PoliceCar[] PoliceCars => new PoliceCar[]
        {
        };

        public PolicePed[] PolicePeds => new PolicePed[]
        {
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new[]
        {
            new CrashedVehicle(Coordinates, rnd.Next(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                })
                .SetEngineHealth(rnd.Next(50, 200))
                .SetDoorsOpen(false, false, false, false, Utilities.RandomBool())
                .ShouldHaveBlip(true),

            new CrashedVehicle(BikeCoordinates, rnd.Next(0, 360), VehicleUtilities.GetRandomBike())
                .SetEngineHealth(rnd.Next(0, 200))
                .ShouldHaveBlip(true)
                .ShouldRandomlyBurstTires(true)
                .SetRotation(new[] {0f, 90f, 0f})
        };

        public CivilianPed[] CivilianPeds => new[]
        {
            new CivilianPed(
                    new Vector3(BikeCoordinates.X + rnd.Next(1, 5), BikeCoordinates.Y + rnd.Next(1, 5),
                        BikeCoordinates.Z), rnd.Next(0, 360), PedUtilities.GetRandomPed())
                .ShouldHaveBlip(true)
                .SetHealth(0)
        };

        public void Accept()
        {
        }

        public void Start(Ped[] CivilianPeds = null, Vehicle[] CrashedCars = null)
        {
        }

        public void Finish()
        {
        }

        public async Task RunAdditionalTasks()
        {
        }
    }
}