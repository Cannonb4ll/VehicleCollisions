using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;
using static CitizenFX.Core.UI.Screen;

namespace VehicleCollisions.Scenes
{
    internal class HighwayAccident : IScene
    {
        public int AccidentIndex;

        public Vector3[] AccidentLocations =
        {
            new Vector3(-165.12f, -1192.85f, 36.92f),
            new Vector3(-395.78f, -955.54f, 37.22f),
            new Vector3(-419.26f, -748.6f, 37.08f),
            new Vector3(-154.8f, -532.45f, 28.83f),
            new Vector3(20.39f, -491.75f, 34.08f),
            new Vector3(679.49f, -155.62f, 49.31f),
            new Vector3(419.1f, -557.07f, 28.75f),
            new Vector3(992.14f, 259.94f, 81.08f),
            new Vector3(1309.84f, 598.86f, 80.06f),
            new Vector3(1003.83f, -852.09f, 32.04f),
            new Vector3(615.5f, -2500.8f, 16.97f),
            new Vector3(-750.2f, -2158.7f, 14.35f),
            new Vector3(-612.95f, -1733.07f, 37.43f),
            new Vector3(-1134.74f, -639.98f, 12.03f),
            new Vector3(-1766.77f, -618.17f, 10.89f)
        };

        public Vector3 PoliceCarCoordinates;

        public Vector3 RandomCoordinates;

        public HighwayAccident()
        {
            // Get a random accident
            AccidentIndex = Utilities.Between(0, AccidentLocations.Length);

            // Get the random accident coordinates
            RandomCoordinates = AccidentLocations[AccidentIndex];

            // Try to get a location on the road for a cop car, this is not accurate and can place the cop car at a really weird location
            PoliceCarCoordinates =
                World.GetNextPositionOnStreet(new Vector3(RandomCoordinates.X + Utilities.Between(7, 12),
                    RandomCoordinates.Y + Utilities.Between(7, 12), RandomCoordinates.Z));
        }

        public bool HasAdditionalTasks => false;

        public string Title => "Crash on the highway";
        public string Description => "We've received a call about a crash on the highway.";
        public int ResponseCode => 3;
        public float StartDistance => 25f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call about a crash on the highway.",
                "One of your colleagues is already on-scene but inexperienced with traffic control.",
                "Head over there, manage traffic, evaluate the situation and take care of it."
            };
        }

        public Vector3 Coordinates => new Vector3(RandomCoordinates.X + Utilities.Between(0, 5),
            RandomCoordinates.Y + Utilities.Between(0, 5), RandomCoordinates.Z);

        public EmergencyCar[] EmergencyCars => new[]
        {
            new EmergencyCar(PoliceCarCoordinates, Utilities.Between(0, 360), VehicleUtilities.GetRandomCopCar())
                .SetSirenActive(true)
                .SetSirenSilent(true)
        };

        public EmergencyPed[] EmergencyPeds => new[]
        {
            new EmergencyPed(
                    new Vector3(PoliceCarCoordinates.X + Utilities.Between(1, 3),
                        PoliceCarCoordinates.Y + Utilities.Between(1, 3), PoliceCarCoordinates.Z),
                    Utilities.Between(0, 360), PedUtilities.GetRandomCop())
                .GiveWeapon(WeaponHash.Flare)
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
                        .SetHealth(Utilities.Between(0, 200)),
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Passenger)
                        .ShouldRandomlySpawn(true)
                        .SetHealth(Utilities.Between(0, 200))
                })
                .SetEngineHealth(Utilities.Between(50, 200))
                .ShouldBeDamaged(true)
                .ShouldRandomlyBurstTires(true)
                .SetDoorsOpen(Utilities.RandomBool(), Utilities.RandomBool(), Utilities.RandomBool(),
                    Utilities.RandomBool(), Utilities.RandomBool())
                .ShouldHaveBlip(true),

            new CrashedVehicle(
                    new Vector3(Coordinates.X + Utilities.Between(3, 5), Coordinates.Y + Utilities.Between(3, 5),
                        Coordinates.Z), Utilities.Between(0, 360), VehicleUtilities.GetSafeRandomVehicle())
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
                .SetDoorsOpen(Utilities.RandomBool(), Utilities.RandomBool(), Utilities.RandomBool(),
                    Utilities.RandomBool(), Utilities.RandomBool())
                .SetRotation(new[] {0f, Utilities.Between(0, 180), 0f}),

            new CrashedVehicle(
                    new Vector3(Coordinates.X + Utilities.Between(3, 5), Coordinates.Y + Utilities.Between(3, 5),
                        Coordinates.Z), Utilities.Between(0, 360), VehicleUtilities.GetSafeRandomVehicle())
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
                .SetDoorsOpen(Utilities.RandomBool(), Utilities.RandomBool(), Utilities.RandomBool(),
                    Utilities.RandomBool(), Utilities.RandomBool())
                .SetRotation(new[] {0f, Utilities.Between(0, 180), 0f})
        };

        public CivilianPed[] CivilianPeds => new CivilianPed[]
        {
        };

        public void Accept()
        {
        }

        public void Start(Ped[] CivilianPeds = null, Vehicle[] CrashedCars = null)
        {
            ShowSubtitle(
                "[Police cop] I have no idea how to manage this traffic, I just started this job, help!",
                12500);
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