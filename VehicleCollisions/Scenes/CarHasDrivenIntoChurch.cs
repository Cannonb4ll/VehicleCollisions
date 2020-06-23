using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using static CitizenFX.Core.UI.Screen;
using VehicleCollisions.Utils;
using static CitizenFX.Core.Native.API;

namespace VehicleCollisions.Scenes
{
    // TODO coordinaten auto klpppen niets
    internal class CarHasDrivenIntoChurch : IScene
    {
        public string Title => "Car has driven into church";
        public string Description => "We've received a call about a car crashing into a church, car has caught on fire.";
        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new string[]
            {
                "We've received a call about a car crashing into a church, car has caught on fire.",
                "See if there are any victims, treat them and tow away the vehicle.",
            };
        }
        
        // Define the coordinates of the main accident
        public Vector3 Coordinates => new Vector3(-792.32f, -9.57f, -40.24f);

        // Define the on-scene police cars (if any)
        public PoliceCar[] PoliceCars => new PoliceCar[] {
            
        };
        
        // Define the on-scene police officers (if any)
        public PolicePed[] PolicePeds => new PolicePed[] {
            
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new CrashedVehicle[]
        {
            new CrashedVehicle(new Vector3(-791.42f, -8.95f, 40.27f), 308.68f, VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(-25)
                .ShouldHaveBlip(true)
                .ShouldBeDamaged(true)
                .SetDoorsOpen(true, false, false, false, true),
        };
     
        public CivilianPed[] CivilianPeds => new CivilianPed[]
        {
            new CivilianPed(new Vector3(-792.43f, -7.07f, 40.27f), 342.86f, PedUtilities.GetRandomPed())
                .SetHealth(0), 
        };

        public void Accept()
        {
            
        }
        
        public void Start(Ped[] CivilianPeds = null, Vehicle[] CrashedCars = null)
        {
            // Start a fire :)
            StartScriptFire(-788.83f, -6.8f, 40f, 5, false);
        }

        public void Finish()
        {
        }
        
        public async Task RunAdditionalTasks()
        {
            
        }
    }
}