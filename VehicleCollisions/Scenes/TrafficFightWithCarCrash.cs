using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;

namespace VehicleCollisions.Scenes
{
    internal class TrafficFightWithCarCrash : IScene
    {
        public bool HasAdditionalTasks => false;
        
        public string Title => "Traffic fight with car crash";

        public string Description =>
            "We've received a call that a fight has occured and a car has crashed down a bridge.";

        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call that a fight has occured and a car has crashed down a bridge.",
                "Arrest the driver, treat the victim and tow away the vehicles"
            };
        }

        // Define the coordinates of the main accident
        public Vector3 Coordinates => new Vector3(50.12f, -778.55f, 44.17f);

        // Define the on-scene police cars (if any)
        public PoliceCar[] PoliceCars => new PoliceCar[]
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
            new CrashedVehicle(new Vector3(50.63f, -781.75f, 43.44f), 251.22f, VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(500)
                .ShouldHaveBlip(true)
                .ShouldBeDamaged(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-50.51f, -781.37f, 44.17f), 224.48f, PedHash.Marine02SMY,
                        VehicleSeat.Driver)
                }),

            new CrashedVehicle(new Vector3(78.99f, -792.29f, 31.24f), 74.6f, VehicleUtilities.GetSafeRandomVehicle())
                .SetRotation(new[] {0f, 90f, 0f})
                .SetEngineHealth(25)
                .ShouldHaveBlip(true)
                .ShouldBeDamaged(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-78.51f, -792.37f, 31.17f), 74.48f, PedHash.Marine02SMY,
                            VehicleSeat.Driver)
                        .SetHealth(0)
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