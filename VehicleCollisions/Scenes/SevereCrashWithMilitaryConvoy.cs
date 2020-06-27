using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;
using static CitizenFX.Core.UI.Screen;

namespace VehicleCollisions.Scenes
{
    internal class SevereCrashWithMilitaryConvoy : IScene
    {
        private Ped[] SpawnedCivilianPeds;
        private Vehicle[] SpawnedCrashedCars;
        public bool HasAdditionalTasks => false;
        public string Title => "Severe crash with military convoy";

        public string Description =>
            "We've received a call of a severe crash with a military convoy, 2 vehicles are involved.";

        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call of a severe crash with a military convoy, 2 vehicles are involved.",
                "One of your colleagues is already on scene, your assistance is required."
            };
        }

        // Define the coordinates of the main accident
        public Vector3 Coordinates => new Vector3(-1777, -677, -10);

        // Define the on-scene police cars (if any)
        public EmergencyCar[] EmergencyCars => new[]
        {
            new EmergencyCar(new Vector3(-1777f, -655f, 10f), 290f, VehicleHash.Police)
                .SetSirenActive(true)
                .SetSirenSilent(true)
                .SetTrunkOpen(true)
        };

        // Define the on-scene police officers (if any)
        public EmergencyPed[] EmergencyPeds => new[]
        {
            new EmergencyPed(new Vector3(-1780f, -658, 10), 10f, PedUtilities.GetRandomCop())
                .GiveWeapon(WeaponHash.Flare)
        };

        public ObjectModel[] ObjectModels => new[]
        {
            new ObjectModel(new Vector3(-1778.23f, -659.09f, 10f), 939377219),
            new ObjectModel(new Vector3(-1775.35f, -658.70f, 10f), 939377219),
            new ObjectModel(new Vector3(-1771.72f, -658.48f, 10f), 939377219),
            new ObjectModel(new Vector3(-1768.72f, -657.68f, 10f), 939377219),
            new ObjectModel(new Vector3(-1764.59f, -657.38f, 10f), 939377219),
            new ObjectModel(new Vector3(-1760.78f, -661.2f, 10f), 939377219),
            new ObjectModel(new Vector3(-1755.07f, -667.29f, 10f), 939377219),
            new ObjectModel(new Vector3(-1750.42f, -672.12f, 10f), 939377219),
            new ObjectModel(new Vector3(-1743.08f, -678.99f, 10f), 939377219),
            new ObjectModel(new Vector3(-1735.04f, -686.05f, 10f), 939377219)
        };

        public CrashedVehicle[] CrashedCars => new[]
        {
            new CrashedVehicle(new Vector3(-1759.75f, -674.46f, 10f), 355f, VehicleHash.Barracks3)
                .SetEngineHealth(100)
                .ShouldHaveBlip(true)
                .ShouldBeDamaged(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-1766.51f, -677.37f, 10.17f), 250f, PedUtilities.GetRandomMilitary(),
                            VehicleSeat.Driver)
                        .SetHealth(0)
                }),
            new CrashedVehicle(new Vector3(-1762.76f, -673.35f, 10f), 257f, VehicleHash.Crusader)
                .SetRotation(new[] {0f, 90f, 0f})
                .ShouldHaveBlip(true)
                .SetEngineHealth(200)
                .ShouldBeDamaged(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-1762.51f, -673.37f, 10.17f), 250f, PedUtilities.GetRandomMilitary(),
                            VehicleSeat.Driver)
                        .SetHealth(0)
                }),
            new CrashedVehicle(new Vector3(-1744.59f, -688.35f, 10f), 225f, VehicleHash.Crusader),
            new CrashedVehicle(new Vector3(-1735.32f, -696.71f, 10f), 225f, VehicleHash.Crusader)
        };

        public CivilianPed[] CivilianPeds => new[]
        {
            new CivilianPed(new Vector3(-1742, -687, 10), 39, PedUtilities.GetRandomMilitary()),
            new CivilianPed(new Vector3(-1734, -695, 10), 39, PedUtilities.GetRandomMilitary())
        };

        public void Accept()
        {
        }

        public void Start(Ped[] CivilianPeds = null, Vehicle[] CrashedCars = null)
        {
            SpawnedCivilianPeds = CivilianPeds;
            SpawnedCrashedCars = CrashedCars;

            ShowSubtitle("[Officer] I am trying to block the road here for you, I have not evaluated the situation yet",
                10000);
        }

        public void Finish()
        {
            if (SpawnedCivilianPeds != null && SpawnedCivilianPeds != null)
            {
                SpawnedCivilianPeds[0].Task.EnterVehicle(SpawnedCrashedCars[2]);
                SpawnedCivilianPeds[1].Task.EnterVehicle(SpawnedCrashedCars[3]);
            }
        }

        public Task RunAdditionalTasks()
        {
            return null;
        }
    }
}