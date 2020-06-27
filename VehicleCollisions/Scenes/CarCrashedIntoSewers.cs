using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;

namespace VehicleCollisions.Scenes
{
    internal class CarCrashedIntoSewers : IScene
    {
        public bool HasAdditionalTasks => false;
        public string Title => "Car crashed into sewers";
        public string Description => "We've received a call that car has driven down and crashed into the sewers.";
        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call that car has driven down and crashed into the sewers.",
                "Treat the victim and tow away the vehicle"
            };
        }

        // Define the coordinates of the main accident
        public Vector3 Coordinates => new Vector3(642.83f, -1482.24f, 9.65f);

        // Define the on-scene police cars (if any)
        public EmergencyCar[] EmergencyCars => new EmergencyCar[]
        {
        };

        // Define the on-scene police officers (if any)
        public EmergencyPed[] EmergencyPeds => new EmergencyPed[]
        {
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new[]
        {
            new CrashedVehicle(new Vector3(642.83f, -1482.24f, 9.65f), 323f, VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(50)
                .ShouldBeDamaged(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-638.51f, -1479.37f, 10.17f), 250f, PedUtilities.GetRandomPed(),
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

        public void Start(Ped[] civilianPeds = null, Vehicle[] crashedCars = null)
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