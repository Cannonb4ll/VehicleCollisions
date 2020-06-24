using System;
using System.Threading.Tasks;
using CitizenFX.Core;
using VehicleCollisions.Entities;
using VehicleCollisions.Utils;
using static CitizenFX.Core.UI.Screen;
using static CitizenFX.Core.Native.API;

namespace VehicleCollisions.Scenes
{
    internal class MilitaryTransportEngineFailure : IScene
    {
        public bool HasAdditionalTasks => true;
        
        private static readonly VehicleHash[] TrailerVehicles =
        {
            VehicleHash.Hydra,
            VehicleHash.Lazer,
            VehicleHash.Cargobob,
            VehicleHash.Savage,
            VehicleHash.Valkyrie,
            VehicleHash.Valkyrie2
        };

        private static readonly VehicleHash[] BadGuyVehicles =
        {
            VehicleHash.Insurgent2,
            VehicleHash.Mesa3,
            VehicleHash.XLS2,
            VehicleHash.Contender,
            VehicleHash.Cognoscenti2,
            VehicleHash.Cog552,
            VehicleHash.Kuruma2,
            VehicleHash.Schafter6
        };

        private readonly int CarFixingTime = Utilities.Between(15, 60) * 1000;
        private readonly VehicleCollisions _vehicleCollisions;

        public float[] AccidentHeadings =
        {
            252.9f,
            68.29f,
            267.02f,
            141.23f,
            85.29f,
            302.37f,
            17.57f,
            316.11f,
            282.09f,
            27.12f
        };

        public int AccidentIndex;

        public Vector3[] AccidentLocations =
        {
            new Vector3(-134.27f, -2425.59f, 6.0f),
            new Vector3(-1556.8f, -740.74f, 11.2f),
            new Vector3(367.38f, -515.48f, 34.33f),
            new Vector3(727.63f, 76.22f, 81.87f),
            new Vector3(629.17f, -2041.29f, 28.86f),
            new Vector3(1389.26f, -1109.17f, 52.51f),
            new Vector3(-1949.83f, 273.31f, 85.79f),
            new Vector3(-458.47f, -2886.79f, 6.0f),
            new Vector3(1000.01f, -2599.96f, 42.95f),
            new Vector3(1008.78f, -843.24f, 31.76f)
        };

        private Ped BadGuyDriver;
        private Vehicle BadGuyVehicle;

        private int CalloutStarted;
        private bool CarFixed;
        public Vector3 RandomCoordinates;

        private Ped[] SpawnedCivilianPeds;
        private Vehicle[] SpawnedCrashedCars;

        public MilitaryTransportEngineFailure(VehicleCollisions vehicleCollisions)
        {
            _vehicleCollisions = vehicleCollisions;

            // Get a random accident
            AccidentIndex = Utilities.Between(0, AccidentLocations.Length);

            // Get the random accident coordinates
            RandomCoordinates = AccidentLocations[AccidentIndex];
        }

        private static Random rnd => new Random();
        public string Title => "Military transport in trouble";
        public string Description => "We've received a call from the military, a truck with load is having issues.";
        public int ResponseCode => 3;
        public float StartDistance => 25f;

        public string[] Notifications()
        {
            return new[]
            {
                "We've received a call from the military, a truck with load is having issues.",
                "Protect the military while they deal with the situation."
            };
        }

        public Vector3 Coordinates => new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z);

        public PoliceCar[] PoliceCars => new PoliceCar[]
        {
        };

        public PolicePed[] PolicePeds => new PolicePed[]
        {
        };

        public ObjectModel[] ObjectModels => new ObjectModel[]
        {
        };

        public CrashedVehicle[] CrashedCars => new[]
        {
            new CrashedVehicle(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z),
                    AccidentHeadings[AccidentIndex], VehicleHash.Barracks2)
                .SetEngineHealth(Utilities.Between(0, 200))
                .ShouldHaveBlip(true)
                .SetBlinkingLights(true)
                .SetDoorsOpen(false, false, false, false, true)
                .SetPedsInVehicle(new[]
                {
                    new VehiclePed(new Vector3(RandomCoordinates.X, RandomCoordinates.Y, RandomCoordinates.Z),
                            AccidentHeadings[AccidentIndex], PedUtilities.GetRandomMilitary(), VehicleSeat.Driver)
                        .ShouldHaveBlip()
                })
                .AttachTrailer(
                    new CrashedVehicleTrailer(
                            new Vector3(RandomCoordinates.X - 5, RandomCoordinates.Y - 5, RandomCoordinates.Z),
                            AccidentHeadings[AccidentIndex], VehicleHash.TRFlat)
                        .SetVehicleOnTrailer(new CrashedVehicleTrailerVehicle(
                            new Vector3(RandomCoordinates.X - 5, RandomCoordinates.Y - 5, RandomCoordinates.Z), 248f,
                            TrailerVehicles[Utilities.Between(0, TrailerVehicles.Length)]))
                )
        };

        public CivilianPed[] CivilianPeds => new[]
        {
            new CivilianPed(new Vector3(RandomCoordinates.X + 2f, RandomCoordinates.Y - 2f, RandomCoordinates.Z - 0.5f),
                    345.26f, PedUtilities.GetRandomMilitary())
                .SetScenario("PROP_HUMAN_BUM_BIN")
                .ShouldHaveBlip()
        };

        public void Accept()
        {
        }

        public void Start(Ped[] civilianPeds, Vehicle[] crashedCars)
        {
            SpawnedCivilianPeds = civilianPeds;
            SpawnedCrashedCars = crashedCars;

            CalloutStarted = Game.GameTime;

            ShowSubtitle(
                "[Military officer] I am trying to fix this truck, please guard us while I fix this piece of junk",
                12500);
        }

        public void Finish()
        {
            BadGuyVehicle?.AttachedBlip?.Delete();
            BadGuyDriver?.AttachedBlip?.Delete();
        }

        public async Task RunAdditionalTasks()
        {
            if (Game.GameTime - CalloutStarted < CarFixingTime) return;

            // Code below here wont be executed unless after 15 seconds
            if (!CarFixed)
            {
                CarFixed = true;

                // 15% chance the mechanic gets overwhelmed
                if (Utilities.RandomBool(15))
                {
                    MechanicGetsOverWhelmedByFumes();

                    return;
                }

                // 15% chance it catches on fire
                if (Utilities.RandomBool(15))
                {
                    TheTruckCouldNotBeFixedAndCaughtOnFire();

                    return;
                }

                // 10% chance it gets attacked
                if (Utilities.RandomBool(10))
                {
                    MilitaryGetsAttacked();

                    return;
                }

                // Default to fixed and drive away
                TheTruckIsFixedAndCanDriveAway();
            }
        }

        public async void TheTruckCouldNotBeFixedAndCaughtOnFire()
        {
            ShowSubtitle("[Military officer] SHIT! Wrong cable! Put out the fire or run!", 10000);

            // Fix engine health and shut the hood
            SetVehicleEngineHealth(SpawnedCrashedCars[0].Handle, -5);

            SpawnedCivilianPeds[0].Task.FleeFrom(SpawnedCrashedCars[0].Driver);
        }

        public async void MechanicGetsOverWhelmedByFumes()
        {
            ShowSubtitle("[Military officer] Ooof... These fumes are not good, I do not feel well..", 10000);

            await BaseScript.Delay(2000);

            SetEntityHealth(SpawnedCivilianPeds[0].Handle, 0);
        }

        public async void MilitaryGetsAttacked()
        {
            ShowNotification("[Dispatch] We've received a call a suspicious vehicle is heading towards your location.");
            ShowNotification("[Dispatch] We marked the vehicle on your map, check out the vehicle.");

            var spawnLocationBadGuys =
                World.GetNextPositionOnStreet(Game.PlayerPed.GetOffsetPosition(new Vector3(Utilities.Between(100, 300),
                    Utilities.Between(100, 300), 0)));

            BadGuyVehicle =
                await _vehicleCollisions._SpawnVehicle(BadGuyVehicles[Utilities.Between(0, BadGuyVehicles.Length)],
                    spawnLocationBadGuys, 100f);

            BadGuyVehicle.AttachBlip();
            BadGuyVehicle.AttachedBlip.Sprite = BlipSprite.GunCar;

            BadGuyDriver = await _vehicleCollisions._SpawnPed(PedUtilities.GetRandomPed(), spawnLocationBadGuys);
            BadGuyDriver.AttachBlip();

            BadGuyDriver.SetIntoVehicle(BadGuyVehicle, VehicleSeat.Driver);

            BadGuyDriver.Task.DriveTo(BadGuyVehicle,
                new Vector3(RandomCoordinates.X + Utilities.Between(0, 1),
                    RandomCoordinates.Y + Utilities.Between(0, 1), RandomCoordinates.Z), 30f, 100f, 786996);

            BadGuyDriver.Weapons.Give(WeaponHash.MicroSMG, 100, true, true);

            // Wait until car is in reach...
            while (!_vehicleCollisions._IsCarInReach(BadGuyVehicle, SpawnedCrashedCars[0], 35f))
                await BaseScript.Delay(2000);

            // Start the shooting
            BadGuyDriver.Task.VehicleShootAtPed(SpawnedCivilianPeds[0]);
            BadGuyDriver.AlwaysKeepTask = true;

            // React and run away
            SpawnedCivilianPeds[0].Task.ReactAndFlee(BadGuyDriver);
            SpawnedCivilianPeds[0].AlwaysKeepTask = true;
        }

        public async void TheTruckIsFixedAndCanDriveAway()
        {
            ShowSubtitle("[Military officer] Alright, all good! Thanks for guarding us.", 10000);

            SpawnedCivilianPeds[0].SetIntoVehicle(SpawnedCrashedCars[0], VehicleSeat.Passenger);

            // Fix engine health and shut the hood
            SetVehicleEngineHealth(SpawnedCrashedCars[0].Handle, GetEntityMaxHealth(SpawnedCrashedCars[0].Handle));
            SetVehicleDoorShut(SpawnedCrashedCars[0].Handle, 4, false);

            // Disable blinkers
            SetVehicleIndicatorLights(SpawnedCrashedCars[0].Handle, 0, false);
            SetVehicleIndicatorLights(SpawnedCrashedCars[0].Handle, 1, false);


            // Wait for realism
            await BaseScript.Delay(2500);

            // Sound the horn nicely :)
            StartVehicleHorn(SpawnedCrashedCars[0].Handle, 500, 1, false);
            await BaseScript.Delay(200);
            StartVehicleHorn(SpawnedCrashedCars[0].Handle, 500, 1, false);

            // Drive to fort zancudo
            SpawnedCrashedCars[0].Driver.Task
                .DriveTo(SpawnedCrashedCars[0], new Vector3(-2333.18f, 3406.62f, 30.21f), 25f, 100f, 786603);

            // Remove blip
            SpawnedCrashedCars[0].AttachedBlip?.Delete();
        }
    }
}