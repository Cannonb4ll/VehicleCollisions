using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;
using static CitizenFX.Core.Native.API;
using static CitizenFX.Core.UI.Screen;

namespace VehicleCollisions.Scenes
{
    internal class CarPetrolLeak : IScene
    {
        private readonly float _accidentHeading;

        private readonly Dictionary<float, Vector3> _accidentLocations = new Dictionary<float, Vector3>()
        {
            { 47.96f, new Vector3(-1524.05f, -421.89f, 35.05f) },
            { 50.78f, new Vector3(-1545.86f, -399.08f, 41.59f) },
            { 207.52f, new Vector3(-1576.22f, -1018.98f, 12.63f) },
            { 254.38f, new Vector3(-1533.24f, -996.0f, 12.62f) },
            { 318.08f, new Vector3(-1656.0f, -875.04f, 8.54f) },
            { 140.02f, new Vector3(-1597.96f, -922.82f, 8.57f) },
            { 212.08f, new Vector3(-1158.08f, -1746.83f, 3.64f) },
            { 123.17f, new Vector3(-981.02f, -1477.39f, 4.62f) },
            { 349.89f, new Vector3(-814.2f, -1311.96f, 4.61f) },
            { 32.56f, new Vector3(-696.85f, -1104.73f, 14.13f) },
            { 210.25f, new Vector3(-918.37f, -167.66f, 41.48f) },
            { 268.46f, new Vector3(-314.82f, -697.72f, 32.64f) },
            { 140.95f, new Vector3(104.91f, -1400.37f, 28.87f) },
            { 291.28f, new Vector3(438.72f, -1479.69f, 28.91f) },
            { 46.0f, new Vector3(712.04f, -980.38f, 23.73f) },
        };

        public Vector3 RandomCoordinates;
        
        public CarPetrolLeak()
        {
            var randomized = _accidentLocations.ElementAt(Utilities.Between(0, _accidentLocations.Count));

            _accidentHeading = randomized.Key;
            RandomCoordinates = randomized.Value;
        }
        
        public int FireBlip;
        
        public bool HasAdditionalTasks => false;

        public string Title => "Car petrol tank leak";
        public string Description => "We've received a call a cars petrol tank is leaking.";
        public int ResponseCode => 3;
        public float StartDistance => 25f;

        public string[] Notifications()
        {
            return new string[]
            {
                "We've received a call a cars petrol tank is leaking.",
                "Deal with the leaking tank, tow away the car and cleanup the scene.s"
            };
        }

        public Vector3 Coordinates => new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z);

        public EmergencyCar[] EmergencyCars => new EmergencyCar[]
        {
        };

        public EmergencyPed[] EmergencyPeds => new EmergencyPed[]
        {
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new CrashedVehicle[]
        {
            new CrashedVehicle(Coordinates, _accidentHeading, VehicleUtilities.GetSafeRandomVehicle())
                .SetDoorsOpen(true, false, false, false, false)
                .SetPetrolTankHealth(Utilities.Between(300, 700))
                .SetEngineRunning(false)
                .ShouldHaveBlip(true),
        };

        public CivilianPed[] CivilianPeds => new CivilianPed[]
        {
            new CivilianPed(
                    new Vector3(RandomCoordinates.X + Utilities.Between(2, 3),
                        RandomCoordinates.Y + Utilities.Between(2, 3), RandomCoordinates.Z),
                    +Utilities.Between(0, 360), PedUtilities.GetRandomPed())
                //.SetAnimation("misstrevor1ig_7", "ortega_05_panic_idle")
        };

        public void Accept()
        {
        }

        public void Start(Ped[] CivilianPeds = null, Vehicle[] CrashedCars = null)
        {
            ShowSubtitle("[Car owner] I don't know what happened, all of the sudden the petrol tank started leaking!", 10000);
            
            if (Utilities.RandomBool())
            {
                ShowNotification(
                    "[Information] It seems a fire has broken out under the car, put the fires out as soon as possible!");

                // Start a fire :)
                StartScriptFire(Coordinates.X, Coordinates.Y, Coordinates.Z - 0.65f, 1, true);

                // Add the blip
                FireBlip = AddBlipForCoord(Coordinates.X, Coordinates.Y, Coordinates.Z);
                SetBlipSprite(FireBlip, 648);
                SetBlipFlashes(FireBlip, true);
            }
        }

        public void Finish()
        {
            RemoveBlip(ref FireBlip);
        }

        public Task RunAdditionalTasks()
        {
            return null;
        }
    }
}