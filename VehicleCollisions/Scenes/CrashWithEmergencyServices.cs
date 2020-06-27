using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;

namespace VehicleCollisions.Scenes
{
    internal class CrashWithEmergencyServices : IScene
    {
        public bool HasAdditionalTasks => false;
        
        public string Title => "Crash with emergency services";
        public string Description => "We've received a call a crash has happened with emergency services.";
        public int ResponseCode => 3;
        public float StartDistance => 120f;

        public string[] Notifications()
        {
            return new string[]
            {
                "We've received a call a crash has happened with emergency services.",
                "Head over there, evaluate the situation and take care of it."
            };
        }

        public Vector3 Coordinates => new Vector3(0, 0, 0);

        public PoliceCar[] EmergencyCars => new PoliceCar[]
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
            new CrashedVehicle(new Vector3(-751.11f, -837.71f, 22.15f), 302.96f, VehicleHash.FireTruk)
                .ShouldHaveBlip(true)
                .ShouldBeDamaged(true)
                .SetSirenActive(true)
                .SetSirenSilent(true)
                .SetDoorsOpen(true, false, false, false, true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-751.11f, -837.71f, 22.15f), 302.96f, PedUtilities.GetRandomFirefighter(), VehicleSeat.Driver)
                        .SetHealth(Utilities.Between(0, 100))
                }),
            
            new CrashedVehicle(new Vector3(-749.01f, -840.64f, 22.17f), 353.48f, VehicleUtilities.GetRandomCopCar())
                .ShouldHaveBlip(true)
                .ShouldBeDamaged(true)
                .SetDoorsOpen(true, false, false, false, true)
                .SetSirenActive(true)
                .SetSirenSilent(true)
                .ShouldRandomlyBurstTires(true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(-749.01f, -840.64f, 22.17f), 353.48f, PedUtilities.GetRandomCop(), VehicleSeat.Driver)
                        .SetHealth(Utilities.Between(0, 100))
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