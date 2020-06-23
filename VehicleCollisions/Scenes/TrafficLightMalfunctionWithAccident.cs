using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;

namespace VehicleCollisions.Scenes
{
    internal class TrafficLightMalfunctionWithAccident : IScene
    {
        private static Random rnd => new Random();
        public string Title => "Traffic light malfunction with accident";

        public string Description =>
            "We've received a call traffic lights has crashed on D. Rockford Hills crossing, a crash happened.";

        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call traffic lights has crashed on Dorset Rockford Hills crossing, a crash happened.",
                "Manage traffic, treat the victims and tow away the vehicles"
            };
        }

        // Define the coordinates of the main accident
        public Vector3 Coordinates => new Vector3(-630.16f, -342.18f, -34.41f);

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

        //Todo: tow truck nakijken als ze opgehaald moeten worden voor alle 3
        public CrashedVehicle[] CrashedCars => new[]
        {
            new CrashedVehicle(new Vector3(-634.35f, -339.18f, 34.42f), 344.37f,
                    VehicleUtilities.GetSafeRandomVehicle())
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-630.51f, -342.37f, 34.17f), 350.48f, PedUtilities.GetRandomPed(),
                        VehicleSeat.Driver)
                })
                .ShouldRandomlyBeDamaged(true)
                .SetBlinkingLights(true)
                .SetEngineHealth(rnd.Next(-50, 100))
                .ShouldHaveBlip(true),
            new CrashedVehicle(new Vector3(-637.04f, -336.79f, 34.72f), 232.38f,
                    VehicleUtilities.GetSafeRandomVehicle())
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-627.51f, -346.37f, 34.17f), 34.48f, PedUtilities.GetRandomPed(),
                        VehicleSeat.Driver)
                })
                .ShouldRandomlyBeDamaged(true)
                .SetBlinkingLights(true, 1)
                .SetEngineHealth(rnd.Next(-50, 100))
                .SetDoorsOpen(false, false, false, false, true)
                .ShouldHaveBlip(true),
            new CrashedVehicle(new Vector3(-632.7f, -343.53f, 34.48f), 26.27f, VehicleUtilities.GetSafeRandomVehicle())
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-633.51f, -339.37f, 34.17f), 238.48f, PedUtilities.GetRandomPed(),
                            VehicleSeat.Driver)
                        .SetHealth(0)
                })
                .ShouldRandomlyBeDamaged(true)
                .SetBlinkingLights(true, 2)
                .SetEngineHealth(rnd.Next(-50, 100))
                .SetDoorsOpen(false, false, false, false, true)
                .ShouldHaveBlip(true)
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