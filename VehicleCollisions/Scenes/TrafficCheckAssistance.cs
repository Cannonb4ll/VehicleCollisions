using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;
using static CitizenFX.Core.Native.API;

namespace VehicleCollisions.Scenes
{
    internal class TrafficCheckAssistance : IScene
    {
        public int SpeedZone;
        public string Title => "Assistance with traffic control required";
        public string Description => "We need assistance with our traffic control.";
        public int ResponseCode => 1;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new[]
            {
                "We need assistance with our traffic control.",
                "Head over here and check users licenses on insurance and suspicious activities."
            };
        }

        public Vector3 Coordinates => new Vector3(1163.2f, 403.07f, 91.1f);

        public PoliceCar[] PoliceCars => new[]
        {
            new PoliceCar(new Vector3(1150.52f, 390.2f, 91.06f), 3.98f, VehicleHash.Police)
                .SetSirenActive(true)
                .SetSirenSilent(true),
            new PoliceCar(new Vector3(1172.3f, 405.77f, 90.82f), 333.36f, VehicleHash.Police2)
        };

        public PolicePed[] PolicePeds => new[]
        {
            new PolicePed(new Vector3(1149.05f, 386.33f, 91.39f), 139.1f, PedUtilities.GetRandomCop())
                .GiveWeapon(WeaponHash.Flare)
        };

        public ObjectModel[] ObjectModels => new[]
        {
            new ObjectModel(new Vector3(1154.31f, 391.1f, 91.31f), 939377219),
            new ObjectModel(new Vector3(1154.16f, 394.02f, 91.38f), 939377219),
            new ObjectModel(new Vector3(1154.36f, 397.93f, 91.4f), 939377219),
            new ObjectModel(new Vector3(1158.93f, 402.97f, 91.26f), 939377219),
            new ObjectModel(new Vector3(1164.12f, 408.57f, 91.1f), 939377219),
            new ObjectModel(new Vector3(1149.65f, 385.87f, 91.35f), 3320760085, 319.61f),
            new ObjectModel(new Vector3(1149.04f, 387.5f, 91.41f), 3320760085, 319.61f)
        };

        public CrashedVehicle[] CrashedCars => new CrashedVehicle[]
        {
        };

        public CivilianPed[] CivilianPeds => new[]
        {
            new CivilianPed(new Vector3(-1149.18f, -391.75f, 91.52f), 3.88f, PedUtilities.GetRandomPed())
                .SetInvisible(true),
            new CivilianPed(new Vector3(-1150.95f, -394.03f, 91.49f), 3.88f, PedUtilities.GetRandomPed())
                .SetInvisible(true),
            new CivilianPed(new Vector3(-1148.91f, -384.68f, 91.34f), 3.88f, PedUtilities.GetRandomPed())
                .SetInvisible(true)
        };

        public void Accept()
        {
            SpeedZone = AddSpeedZoneForCoord(1163.2f, 403.07f, 91.1f, 15f, 5, false);
        }

        public void Start(Ped[] CivilianPeds = null, Vehicle[] CrashedCars = null)
        {
        }

        public void Finish()
        {
            RemoveSpeedZone(SpeedZone);
        }

        public Task RunAdditionalTasks()
        {
            return null;
        }
    }
}