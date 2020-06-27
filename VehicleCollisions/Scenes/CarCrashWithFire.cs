using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;
using static CitizenFX.Core.Native.API;
using static CitizenFX.Core.UI.Screen;

namespace VehicleCollisions.Scenes
{
    internal class CarCrashWithFire : IScene
    {
        public float[] AccidentCarHeadings =
        {
            282.36f,
            248.36f,
            318.99f,
            316.81f
        };

        public Vector3[] AccidentCarLocations =
        {
            new Vector3(-1493.08f, -384.96f, 39.57f),
            new Vector3(-1228.46f, -901.29f, 11.84f),
            new Vector3(-712.2f, -918.54f, 18.71f),
            new Vector3(28.81f, -1351.37f, 28.94f)
        };

        public int AccidentIndex;

        public Vector3[] AccidentLocations =
        {
            new Vector3(-1488.34f, -380.85f, 40.16f),
            new Vector3(-1224.07f, -905.78f, 12.33f),
            new Vector3(-711.27f, -913.37f, 19.22f),
            new Vector3(29.43f, -1347.31f, 29.5f)
        };

        public int FireBlip;
        public bool FirePutOut;

        public Vector3 RandomCoordinates;
        public Vector3 RandomCrashedCarCoordinates;
        public float RandomCrashedCarHeading;

        public CarCrashWithFire()
        {
            // Get a random accident
            AccidentIndex = Utilities.Between(0, AccidentLocations.Length);

            // Get the random accident coordinates
            RandomCoordinates = AccidentLocations[AccidentIndex];
            RandomCrashedCarCoordinates = AccidentCarLocations[AccidentIndex];
            RandomCrashedCarHeading = AccidentCarHeadings[AccidentIndex];
        }

        public bool HasAdditionalTasks => true;

        public string Title => "Car crash with fire";
        public string Description => "We've received a call a car has crashed and a fire has broken out.";
        public int ResponseCode => 3;
        public float StartDistance => 25f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call a car has crashed and a fire has broken out.",
                "Head over there, put out the fire and treat the victims."
            };
        }

        public Vector3 Coordinates => RandomCoordinates;

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
            new CrashedVehicle(RandomCrashedCarCoordinates, RandomCrashedCarHeading,
                    VehicleUtilities.GetSafeRandomVehicle())
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver)
                        .SetHealth(Utilities.Between(0, 200)),
                    new VehiclePed(Coordinates, 0f, PedUtilities.GetRandomPed(), VehicleSeat.Passenger)
                        .SetHealth(Utilities.Between(0, 200))
                })
                .SetEngineHealth(Utilities.Between(-50, 50))
                .ShouldBeDamaged(true)
                .ShouldRandomlyBurstTires(true)
                .SetDoorsOpen(Utilities.RandomBool(), false, false, false, true)
                .ShouldHaveBlip(true)
        };

        public CivilianPed[] CivilianPeds => new[]
        {
            new CivilianPed(
                    new Vector3(RandomCoordinates.X + Utilities.Between(1, 2),
                        RandomCoordinates.Y + Utilities.Between(1, 2), RandomCoordinates.Z),
                    +Utilities.Between(0, 360), PedUtilities.GetRandomPed())
                .ShouldHaveBlip()
        };

        public void Accept()
        {
        }

        public void Start(Ped[] CivilianPeds = null, Vehicle[] CrashedCars = null)
        {
            ShowNotification(
                "[Information] It seems a fire has broken out in the shop, put the fires out as soon as possible!");

            // Start a fire :)
            StartScriptFire(Coordinates.X, Coordinates.Y, Coordinates.Z - 0.65f, 3, false);

            // Add the blip
            FireBlip = AddBlipForCoord(Coordinates.X, Coordinates.Y, Coordinates.Z);
            SetBlipSprite(FireBlip, 648);
            SetBlipFlashes(FireBlip, true);
        }

        public void Finish()
        {
            RemoveBlip(ref FireBlip);
        }

        public async Task RunAdditionalTasks()
        {
            var fires = GetNumberOfFiresInRange(Coordinates.X, Coordinates.Y, Coordinates.Z, 25f);

            if (!FirePutOut)
                // TODO: Can't use this yet as the notification is behind the 911 dispatch window from FivePD
                // TODO: If anyone knows how to solve, give it an offset or something let me know (I tried SetFloatingHelpTextScreenPosition)
                //SetTextComponentFormat("STRING");
                //SetFloatingHelpTextScreenPosition(5, 50f, 30f);
                //AddTextComponentString($"Fires left: {fires}");
                //DisplayHelpTextFromStringLabel(0, false, false, -1);

                if (fires < 1)
                {
                    FirePutOut = true;

                    // Remove the blip from radar
                    RemoveBlip(ref FireBlip);

                    // Show the user it has been put out, all OK.
                    ShowNotification(
                        "[Information] You've put out all the fires.");
                }
        }
    }
}