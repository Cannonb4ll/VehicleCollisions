using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;

namespace VehicleCollisions.Scenes
{
    internal class SceneExampleFile : IScene
    {
        public string Title => "<Title>";
        public string Description => "<Description>";
        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new string[]
                { };
        }

        public Vector3 Coordinates => new Vector3(0, 0, 0);

        public PoliceCar[] PoliceCars => new PoliceCar[]
        {
        };

        public PolicePed[] PolicePeds => new PolicePed[]
        {
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new CrashedVehicle[]
        {
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

        public async Task RunAdditionalTasks()
        {
        }
    }
}