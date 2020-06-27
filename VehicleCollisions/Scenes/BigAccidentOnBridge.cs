using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;

namespace VehicleCollisions.Scenes
{
    internal class BigAccidentOnBridge : IScene
    {
        public bool HasAdditionalTasks => false;
        
        public Vector3 RandomCoordinates;

        public BigAccidentOnBridge()
        {
            RandomCoordinates = new Vector3(Utilities.Between(510, 807), Utilities.Between(-861, -841),
                Utilities.Between(40, 42));
        }


        private static Random rnd => new Random();
        public string Title => "Big accident on bridge";
        public string Description => "We've received a call about several cars crashing into each other on the bridge.";
        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call about several cars crashing into each other on the bridge.",
                "Manage the traffic, treat the victims and tow away the vehicles."
            };
        }

        // Define the coordinates of the main accident
        public Vector3 Coordinates => new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z);

        // Define the on-scene police cars (if any)
        public PoliceCar[] EmergencyCars => new PoliceCar[]
        {
        };

        // Define the on-scene police officers (if any)
        public PolicePed[] PolicePeds => new PolicePed[]
        {
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new[]
        {
            new CrashedVehicle(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z),
                    Utilities.Between(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(25)
                .ShouldHaveBlip(true)
                .ShouldRandomlyBeDamaged(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f,
                            PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                        .SetHealth(Utilities.Between(0, 200)),
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f,
                            PedUtilities.GetRandomPed(), VehicleSeat.Passenger)
                        .SetHealth(Utilities.Between(0, 200))
                        .ShouldRandomlySpawn(true)
                }),
            new CrashedVehicle(
                    new Vector3(RandomCoordinates.X + Utilities.Between(3, 6),
                        RandomCoordinates.Y + Utilities.Between(3, 6),
                        RandomCoordinates.Z), Utilities.Between(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(0)
                .ShouldHaveBlip(true)
                .ShouldRandomlyBeDamaged(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f,
                            PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                        .SetHealth(0),
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f,
                            PedUtilities.GetRandomPed(), VehicleSeat.Passenger)
                        .SetHealth(Utilities.Between(0, 200))
                        .ShouldRandomlySpawn(true)
                }),
            new CrashedVehicle(
                    new Vector3(RandomCoordinates.X + Utilities.Between(3, 6),
                        RandomCoordinates.Y + Utilities.Between(3, 6),
                        RandomCoordinates.Z), Utilities.Between(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(0)
                .ShouldHaveBlip(true)
                .ShouldRandomlyBeDamaged(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f,
                            PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                        .SetHealth(0),
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f,
                            PedUtilities.GetRandomPed(), VehicleSeat.Passenger)
                        .SetHealth(Utilities.Between(0, 200))
                        .ShouldRandomlySpawn(true)
                })
        };

        public CivilianPed[] CivilianPeds => new CivilianPed[]
        {
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