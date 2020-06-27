using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;

namespace VehicleCollisions.Scenes
{
    internal class HighwayAccident : IScene
    {
        public bool HasAdditionalTasks => false;
        
        public int AccidentIndex;

        public Vector3[] AccidentLocations =
        {
            new Vector3(-165.12f, -1192.85f, 36.92f),
            new Vector3(-395.78f, -955.54f, 37.22f),
            new Vector3(-419.26f, -748.6f, 37.08f),
            new Vector3(-154.8f, -532.45f, 28.83f),
            new Vector3(20.39f, -491.75f, 34.08f),
        };
        
        public Vector3 RandomCoordinates;
        public Vector3 PoliceCarCoordinates;
        
        public HighwayAccident()
        {
            // Get a random accident
            //AccidentIndex = Utilities.Between(0, AccidentLocations.Length);
            AccidentIndex = 4;
            
            // Get the random accident coordinates
            RandomCoordinates = AccidentLocations[AccidentIndex];
            
            // Try to get a location on the road for a cop car, this is not accurate and can place the cop car at a really weird location
            PoliceCarCoordinates =
                World.GetNextPositionOnStreet(new Vector3(RandomCoordinates.X + Utilities.Between(0, 5), RandomCoordinates.Y  + Utilities.Between(0, 5), RandomCoordinates.Z));
        }

        public string Title => "Crash on the highway";
        public string Description => "We've received a call about a crash on the highway.";
        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new string[]
            {
                "We've received a call about a crash on the highway.",
                "Head over there, manage traffic, evaluate the situation and take care of it."
            };
        }

        public Vector3 Coordinates => new Vector3(RandomCoordinates.X + Utilities.Between(0, 5),
            RandomCoordinates.Y + Utilities.Between(0, 5), RandomCoordinates.Z);

        public EmergencyCar[] EmergencyCars => new EmergencyCar[]
        {
            new EmergencyCar(PoliceCarCoordinates, 0f, VehicleUtilities.GetRandomCopCar())
                .SetSirenActive(true)
                .SetSirenSilent(true),
        };

        public PolicePed[] PolicePeds => new PolicePed[]
        {
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new CrashedVehicle[]
        {
            new CrashedVehicle(Coordinates, Utilities.Between(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                        .SetHealth(Utilities.Between(0, 200)),
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Passenger)
                        .ShouldRandomlySpawn(true)
                        .SetHealth(Utilities.Between(0, 200))
                })
                .SetEngineHealth(Utilities.Between(50, 200))
                .ShouldBeDamaged(true)
                .ShouldRandomlyBurstTires(true)
                .SetDoorsOpen(Utilities.RandomBool(), Utilities.RandomBool(), Utilities.RandomBool(), Utilities.RandomBool(), Utilities.RandomBool())
                .ShouldHaveBlip(true),

            new CrashedVehicle(new Vector3(Coordinates.X + Utilities.Between(3,5), Coordinates.Y + Utilities.Between(3,5), Coordinates.Z), Utilities.Between(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                        .SetHealth(Utilities.Between(0, 200)),
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Passenger)
                        .ShouldRandomlySpawn(true)
                        .SetHealth(Utilities.Between(0, 200))
                })
                .SetEngineHealth(Utilities.Between(0, 200))
                .ShouldHaveBlip(true)
                .ShouldBeDamaged(true)
                .ShouldRandomlySpawn(true)
                .ShouldRandomlyBurstTires(true)
                .SetRotation(new[] {0f, Utilities.Between(0, 180), 0f}),
            
            new CrashedVehicle(new Vector3(Coordinates.X + Utilities.Between(3,5), Coordinates.Y + Utilities.Between(3,5), Coordinates.Z), Utilities.Between(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                        .SetHealth(Utilities.Between(0, 200)),
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Passenger)
                        .ShouldRandomlySpawn(true)
                        .SetHealth(Utilities.Between(0, 200))
                })
                .SetEngineHealth(Utilities.Between(0, 200))
                .ShouldHaveBlip(true)
                .ShouldBeDamaged(true)
                .ShouldRandomlySpawn(true)
                .ShouldRandomlyBurstTires(true)
                .SetRotation(new[] {0f, Utilities.Between(0, 180), 0f}),
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