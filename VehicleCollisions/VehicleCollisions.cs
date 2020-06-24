using System;
using System.Threading.Tasks;
using CalloutAPI;
using CitizenFX.Core;
using VehicleCollisions.Scenes;
using VehicleCollisions.Utils;
using static CitizenFX.Core.Native.API;
using static CitizenFX.Core.UI.Screen;

namespace VehicleCollisions
{
    [CalloutProperties("Vehicle Collisions", "Dennis Smink", "1.0.0", Probability.Medium)]
    public class VehicleCollisions : Callout
    {
        private Ped[] civilianPeds;
        private Vehicle[] crashedCars;
        private Vehicle[] policeCars;
        private Ped[] policePeds;
        private readonly Random random = new Random();
        private readonly IScene scene;
        private int[] spawnedObjects;

        public VehicleCollisions()
        {
            scene = new SceneFactory(Utilities.Between(1, 10)).GetScene();
            //scene = new SceneFactory(9).GetScene();

            InitBase(new Vector3(scene.Coordinates.X, scene.Coordinates.Y, scene.Coordinates.Z));

            ShortName = scene.Title;
            CalloutDescription = scene.Description;
            ResponseCode = scene.ResponseCode;
            StartDistance = scene.StartDistance;
        }

        public override async Task Init()
        {
            // When user accepts continue
            OnAccept();

            // Spawn the on-scene cop car (if any)
            await SpawnOnSceneCopCars();

            // Spawn the on-scene ped cops
            await SpawnOnScenePedCops();

            // Spawn the scene objects
            await SpawnObjects();

            // Spawn crashed cars
            await SpawnCrashedCars();

            // Spawn the civilians for the cars
            await SpawnCivilians();

            // Run the notifications
            RunNotifications();

            // Run the accept method
            scene.Accept();
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);

            // Run the scene start method
            scene.Start(civilianPeds, crashedCars);

            Tick += scene.RunAdditionalTasks;
        }

        public async Task SpawnOnSceneCopCars()
        {
            policeCars = new Vehicle[scene.PoliceCars.Length];
            for (var i = 0; i < scene.PoliceCars.Length; i++)
            {
                var policeCar = scene.PoliceCars[i];

                policeCars[i] = await SpawnVehicle(policeCar.Model, policeCar.Location, policeCar.Heading);

                if (policeCar.SirenActive) policeCars[i].IsSirenActive = true;
                if (policeCar.SirenSilent) policeCars[i].IsSirenSilent = true;

                if (policeCar.TrunkOpen) SetVehicleDoorOpen(policeCars[i].Handle, 5, false, false);
            }
        }

        public async Task SpawnOnScenePedCops()
        {
            policePeds = new Ped[scene.PolicePeds.Length];
            for (var i = 0; i < scene.PolicePeds.Length; i++)
            {
                var policePed = scene.PolicePeds[i];

                policePeds[i] = await SpawnPed(policePed.Model, policePed.Location);
                policePeds[i].AlwaysKeepTask = true;
                policePeds[i].BlockPermanentEvents = true;
                policePeds[i].Task.PlayAnimation("anim@amb@waving@female", "air_wave");

                policePeds[i].Weapons.Give(policePed.Weapon, 1, true, true);

                SetEntityHeading(policePeds[i].Handle, policePed.Heading);
            }
        }

        public async Task SpawnObjects()
        {
            spawnedObjects = new int[scene.ObjectModels.Length];

            for (var i = 0; i < scene.ObjectModels.Length; i++)
            {
                var spawnedObject = scene.ObjectModels[i];

                // Request the model
                RequestModel(spawnedObject.ModelHash);

                // Wait until model has loaded
                while (!HasModelLoaded(spawnedObject.ModelHash)) await BaseScript.Delay(1000);

                // Create the object
                spawnedObjects[i] = CreateObjectNoOffset(spawnedObject.ModelHash, spawnedObject.Location.X,
                    spawnedObject.Location.Y, spawnedObject.Location.Z, true, false, true);

                SetEntityHeading(spawnedObjects[i], spawnedObject.Heading);

                // Place object to ground
                PlaceObjectOnGroundProperly(spawnedObjects[i]);

                // Freeze the object
                FreezeEntityPosition(spawnedObjects[i], true);
            }
        }

        public async Task SpawnCrashedCars()
        {
            crashedCars = new Vehicle[scene.CrashedCars.Length];
            for (var i = 0; i < scene.CrashedCars.Length; i++)
            {
                var crashedCar = scene.CrashedCars[i];

                // If the car should randomly spawn, make it a 50% chance
                // TODO: This is not possible for now because the array length bug
                /*if (crashedCar.ShouldRandomSpawn && Utilities.RandomBool())
                {
                    continue;
                }*/

                // Spawn the actual vehicle
                crashedCars[i] = await SpawnVehicle(crashedCar.Model, crashedCar.Location, crashedCar.Heading);

                var crashedCarHandle = crashedCars[i].Handle;

                SetVehicleEngineOn(crashedCarHandle, true, true, false);

                // Set the engine health
                crashedCars[i].EngineHealth = crashedCar.EngineHealth;

                // Rotate the car if we defined this
                if (crashedCar.Rotation != null)
                    SetEntityRotation(crashedCars[i].Handle, crashedCar.Rotation[0], crashedCar.Rotation[1],
                        crashedCar.Rotation[2], 1, true);

                // Attach a blip
                if (crashedCar.HasBlip)
                {
                    crashedCars[i].AttachBlip();
                    crashedCars[i].AttachedBlip.Color = BlipColor.Blue;
                }

                // Randomly burst tires
                if (crashedCar.ShouldRandomBurstTires)
                {
                    if (Utilities.RandomBool() && crashedCars[i].Wheels[0] != null) crashedCars[i].Wheels[0].Burst();
                    if (Utilities.RandomBool() && crashedCars[i].Wheels[1] != null) crashedCars[i].Wheels[1].Burst();
                    if (Utilities.RandomBool() && crashedCars[i].Wheels[2] != null) crashedCars[i].Wheels[2].Burst();
                    if (Utilities.RandomBool() && crashedCars[i].Wheels[3] != null) crashedCars[i].Wheels[3].Burst();
                }

                // Randomly be damaged
                if (crashedCar.ShouldRandomBeDamaged && Utilities.RandomBool())
                {
                    var damage = random.Next(99, 1000);

                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, -0.84f, 2.21f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, 0.84f, 2.21f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, -0.84f, -2.21f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, 0.84f, -2.21f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, -1.0f, 0.0f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, 1.0f, 0.0f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, 0.0f, 0.0f, 1.25f, damage, 50.0f, true);

                    SetVehicleDamage(crashedCarHandle, 0.0f, 4.0f, 0.0f, 800.0f, 125.0f, true);
                }

                // Be damaged
                if (crashedCar.BeDamaged)
                {
                    var damage = random.Next(99, 1000);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, -0.84f, 2.21f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, 0.84f, 2.21f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, -0.84f, -2.21f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, 0.84f, -2.21f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, -1.0f, 0.0f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, 1.0f, 0.0f, 0.22f, damage, 50.0f, true);
                    if (Utilities.RandomBool())
                        SetVehicleDamage(crashedCarHandle, 0.0f, 0.0f, 1.25f, damage, 50.0f, true);

                    SetVehicleDamage(crashedCarHandle, 0.0f, 4.0f, 0.0f, 800.0f, 125.0f, true);
                }

                // Let the lights blink if we want
                if (crashedCar.BlinkingLights)
                {
                    if (crashedCar.BlinkingLightsDirection == 0)
                    {
                        SetVehicleIndicatorLights(crashedCars[i].Handle, 0, true);
                        SetVehicleIndicatorLights(crashedCars[i].Handle, 1, true);
                    }
                    else if (crashedCar.BlinkingLightsDirection == 1)
                    {
                        SetVehicleIndicatorLights(crashedCars[i].Handle, 1, true);
                    }
                    else if (crashedCar.BlinkingLightsDirection == 2)
                    {
                        SetVehicleIndicatorLights(crashedCars[i].Handle, 0, true);
                    }
                }

                // Determine doors
                if (crashedCar.LeftDoorOpen) SetVehicleDoorOpen(crashedCarHandle, 0, false, false);
                if (crashedCar.RightDoorOpen) SetVehicleDoorOpen(crashedCarHandle, 1, false, false);
                if (crashedCar.LeftRearDoorOpen) SetVehicleDoorOpen(crashedCarHandle, 2, false, false);
                if (crashedCar.RightRearDoorOpen) SetVehicleDoorOpen(crashedCarHandle, 3, false, false);
                if (crashedCar.HoodOpen) SetVehicleDoorOpen(crashedCarHandle, 4, false, false);

                // If we have any drivers for the crashed car, spawn these
                if (crashedCar.Peds != null)
                    foreach (var vehiclePed in crashedCar.Peds)
                    {
                        if (vehiclePed.ShouldRandomSpawn && Utilities.RandomBool()) continue;

                        var driver = await SpawnPed(vehiclePed.Model, vehiclePed.Location);

                        // Put driver into vehicle
                        driver.SetIntoVehicle(crashedCars[i], vehiclePed.Seat);

                        // Set vehicle ped's health
                        SetEntityHealth(driver.Handle, vehiclePed.Health);

                        // Attach a blip if we want to
                        if (vehiclePed.HasBlip)
                        {
                            driver.AttachBlip();
                            driver.AttachedBlip.Color = BlipColor.Green;
                        }
                    }

                // Attach trailer
                if (crashedCar.AttachedTrailer != null)
                {
                    var trailer = await SpawnVehicle(crashedCar.AttachedTrailer.Model,
                        crashedCar.AttachedTrailer.Location, crashedCar.AttachedTrailer.Heading);

                    AttachVehicleToTrailer(crashedCars[i].Handle, trailer.Handle, 50f);

                    if (crashedCar.AttachedTrailer.VehicleOnTrailer != null)
                    {
                        var vehicleOnTrailer = await SpawnVehicle(crashedCar.AttachedTrailer.VehicleOnTrailer.Model,
                            crashedCar.AttachedTrailer.VehicleOnTrailer.Location,
                            crashedCar.AttachedTrailer.VehicleOnTrailer.Heading);

                        AttachVehicleOnToTrailer(vehicleOnTrailer.Handle, trailer.Handle, 0.0f, 0.65f, -1.98f,
                            0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f, 0.0f);
                    }
                }
            }
        }

        public async Task SpawnCivilians()
        {
            civilianPeds = new Ped[scene.CivilianPeds.Length];
            for (var i = 0; i < scene.CivilianPeds.Length; i++)
            {
                var civilian = scene.CivilianPeds[i];

                civilianPeds[i] = await SpawnPed(civilian.Model, civilian.Location);
                civilianPeds[i].AlwaysKeepTask = true;

                civilianPeds[i].Weapons.Give(civilian.Weapon, 1, true, true);

                if (civilian.Invisible) civilianPeds[i].IsVisible = false;

                SetEntityHealth(civilianPeds[i].Handle, civilian.Health);

                if (civilian.AnimationLib != null)
                {
                    RequestAnimDict(civilian.AnimationLib);
                    civilianPeds[i].Task.PlayAnimation(civilian.AnimationLib, civilian.AnimationName);
                }

                if (civilian.Scenario != null)
                    TaskStartScenarioInPlace(civilianPeds[i].Handle, civilian.Scenario, 0, true);

                if (civilian.HasBlip)
                {
                    civilianPeds[i].AttachBlip();
                    civilianPeds[i].AttachedBlip.Color = BlipColor.Green;
                }
            }
        }

        public void RunNotifications()
        {
            foreach (var notification in scene.Notifications()) ShowNotification($"[Dispatch] {notification}");
        }

        public override void OnCancelBefore()
        {
            //Tick -= scene.RunAdditionalTasks;
            
            try
            {
                if (crashedCars.Length > 0)
                    // Remove crashed car blip
                    foreach (var spawnedCar in crashedCars)
                    {
                        if (spawnedCar == null || spawnedCar.AttachedBlip == null) continue;

                        spawnedCar.AttachedBlip?.Delete();

                        // Remove ped blip
                        foreach (var passenger in spawnedCar.Occupants)
                        {
                            if (passenger == null) continue;

                            passenger.AttachedBlip?.Delete();
                        }
                    }
            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                if (spawnedObjects.Length > 0)
                    // Clear out spawned cones
                    foreach (var spawnedObject in spawnedObjects)
                        Entity.FromHandle(spawnedObject).Delete();
            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                if (policeCars.Length > 0)
                    // Remove the on-scene cop cars (if any)
                    foreach (var policeCar in policeCars)
                    {
                        if (policeCar == null) continue;

                        policeCar.Delete();
                    }
            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                if (policePeds.Length > 0)
                    // Remove the on-scene cop cars (if any)
                    foreach (var policePed in policePeds)
                    {
                        if (policePed == null) continue;

                        policePed.Delete();
                    }
            }
            catch (Exception e)
            {
                // ignored
            }

            try
            {
                // Run the scene finish method and pass along peds, cars, etc.
                scene.Finish();
            }
            catch (Exception e)
            {
                // ignored
            }
        }
    }
}