using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using static CitizenFX.Core.UI.Screen;
using VehicleCollisions.Utils;
using static CitizenFX.Core.Native.API;

namespace VehicleCollisions.Scenes
{
internal class BigAccidentOnBridge : IScene
    {
        public string Title => "Big accident on bridge";
        public string Description => "We've received a call about several cars crashing into each other on the bridge.";
        public int ResponseCode => 3;
        public float StartDistance => 120f;

        
        private static Random rnd => new Random();

        public Vector3 RandomCoordinates;

        public BigAccidentOnBridge()
        {
            RandomCoordinates = new Vector3(rnd.Next(510, 807), rnd.Next(-861, -841), rnd.Next(40, 42));
        }
        
        public string[] Notifications()
        {
            return new string[]
            {
                "We've received a call about several cars crashing into each other on the bridge.",
                "Manage the traffic, treat the victims and tow away the vehicles.",
            };
        }
        
        // Define the coordinates of the main accident
        public Vector3 Coordinates => new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z);

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
            new CrashedVehicle(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), rnd.Next(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(25)
                .ShouldHaveBlip(true)
                .ShouldRandomlyBeDamaged(true)
                .SetPedsInVehicle(new VehiclePed[]
                {
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver )
                        .SetHealth(rnd.Next(0, 200)),
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f, PedUtilities.GetRandomPed(), VehicleSeat.Passenger )
                        .SetHealth(rnd.Next(0, 200))
                        .ShouldRandomlySpawn(true)
                }),
            new CrashedVehicle(new Vector3(RandomCoordinates.X + rnd.Next(3,6), RandomCoordinates.Y + rnd.Next(3,6), RandomCoordinates.Z), rnd.Next(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(0)
                .ShouldHaveBlip(true)
                .ShouldRandomlyBeDamaged(true)
                .SetPedsInVehicle(new VehiclePed[]
                {
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver )
                        .SetHealth(0), 
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f, PedUtilities.GetRandomPed(), VehicleSeat.Passenger )
                        .SetHealth(rnd.Next(0, 200))
                        .ShouldRandomlySpawn(true)
                }),
            new CrashedVehicle(new Vector3(RandomCoordinates.X + rnd.Next(3,6), RandomCoordinates.Y + rnd.Next(3,6), RandomCoordinates.Z), rnd.Next(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(0)
                .ShouldRandomlySpawn(true)
                .ShouldHaveBlip(true)
                .ShouldRandomlyBeDamaged(true)
                .SetPedsInVehicle(new VehiclePed[]
                {
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver )
                        .SetHealth(0), 
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f, PedUtilities.GetRandomPed(), VehicleSeat.Passenger )
                        .SetHealth(rnd.Next(0, 200))
                        .ShouldRandomlySpawn(true)
                }),
            new CrashedVehicle(new Vector3(RandomCoordinates.X + rnd.Next(3,6), RandomCoordinates.Y + rnd.Next(3,6), RandomCoordinates.Z), rnd.Next(0, 360), VehicleUtilities.GetSafeRandomVehicle())
                .SetEngineHealth(0)
                .ShouldRandomlySpawn(true)
                .ShouldHaveBlip(true)
                .ShouldRandomlyBeDamaged(true)
                .SetPedsInVehicle(new VehiclePed[]
                {
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f, PedUtilities.GetRandomPed(), VehicleSeat.Driver )
                        .SetHealth(0), 
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z), 0f, PedUtilities.GetRandomPed(), VehicleSeat.Passenger )
                                            .SetHealth(rnd.Next(0, 200))
                                            .ShouldRandomlySpawn(true)
                }),
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