﻿using System.Threading.Tasks;
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
            new Vector3(-719.87f, -2390.4f, 14.76f),
            new Vector3(428.12f, 297.9f, 103.0f),
            new Vector3(-1073.6f, 389.89f, 68.95f),
            new Vector3(-2166.56f, -337.01f, 13.2f),
            new Vector3(216.62f, -1295.37f, 29.34f),
            new Vector3(-133.14f, -1733.31f, 30.14f),
            new Vector3(-1077.31f, -1476.38f, 5.11f),
            new Vector3(-1405.01f, -911.16f, 11.11f),
            new Vector3(-1559.36f, -336.36f, 46.96f),
            new Vector3(-1074.05f, 400.38f, 69.0f),
            new Vector3(-559.42f, 514.18f, 105.72f),
            new Vector3(209.88f, 198.14f, 105.59f),
            new Vector3(1270.75f, -542.6f, 68.94f),
            new Vector3(915.96f, -2245.27f, 30.54f)
        };

        public Vector3 RandomCoordinates;

        public CarCrashWithBike()
        {
            // Get a random accident
            AccidentIndex = Utilities.Between(0, AccidentLocations.Length);

            // Get the random accident coordinates
            RandomCoordinates = AccidentLocations[AccidentIndex];
        }

        public Vector3 BikeCoordinates => new Vector3(Coordinates.X + Utilities.Between(1, 10),
            Coordinates.Y + Utilities.Between(1, 10),
            Coordinates.Z);

        public bool HasAdditionalTasks => false;

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

        public Vector3 Coordinates => new Vector3(RandomCoordinates.X + Utilities.Between(0, 10),
            RandomCoordinates.Y + Utilities.Between(0, 10), RandomCoordinates.Z);

        public EmergencyCar[] EmergencyCars => new EmergencyCar[]
        {
        };

        public EmergencyPed[] EmergencyPeds => new EmergencyPed[]
        {
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new[]
        {
            new CrashedVehicle(Coordinates, Utilities.Between(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                        .SetHealth(Utilities.Between(0, 200))
                })
                .SetEngineHealth(Utilities.Between(50, 200))
                .ShouldBeDamaged(true)
                .SetDoorsOpen(false, false, false, false, Utilities.RandomBool())
                .ShouldHaveBlip(true),

            new CrashedVehicle(BikeCoordinates, Utilities.Between(0, 360), VehicleUtilities.GetRandomBike())
                .SetEngineHealth(Utilities.Between(0, 200))
                .ShouldHaveBlip(true)
                .ShouldRandomlyBurstTires(true)
                .SetRotation(new[] {0f, 90f, 0f})
        };

        public CivilianPed[] CivilianPeds => new[]
        {
            new CivilianPed(
                    new Vector3(BikeCoordinates.X + Utilities.Between(1, 5),
                        BikeCoordinates.Y + Utilities.Between(1, 5),
                        BikeCoordinates.Z), Utilities.Between(0, 360), PedUtilities.GetRandomPed())
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

        public Task RunAdditionalTasks()
        {
            return null;
        }
    }
}