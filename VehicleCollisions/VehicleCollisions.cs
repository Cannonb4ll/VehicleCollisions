﻿using System;
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
        private readonly IScene _scene;
        private Ped[] _civilianPeds;
        private Vehicle[] _crashedCars;
        private Vehicle[] _policeCars;
        private Ped[] _policePeds;
        private int[] _spawnedObjects;
        
        public VehicleCollisions()
        {
            _scene = new SceneFactory(Utilities.Between(1, 10)).GetScene(this);
            //scene = new SceneFactory(2).GetScene(this);

            InitBase(new Vector3(_scene.Coordinates.X, _scene.Coordinates.Y, _scene.Coordinates.Z));

            ShortName = _scene.Title;
            CalloutDescription = _scene.Description;
            ResponseCode = _scene.ResponseCode;
            StartDistance = _scene.StartDistance;
        }

        public override async Task Init()
        {
            // When user accepts, continue
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
            _scene.Accept();
        }

        public override void OnStart(Ped player)
        {
            base.OnStart(player);

            // Run the scene start method
            _scene.Start(_civilianPeds, _crashedCars);

            if (_scene.HasAdditionalTasks)
            {
                Tick += _scene.RunAdditionalTasks;
            }
        }

        public async Task SpawnOnSceneCopCars()
        {
            _policeCars = new Vehicle[_scene.PoliceCars.Length];
            for (var i = 0; i < _scene.PoliceCars.Length; i++)
            {
                var policeCar = _scene.PoliceCars[i];

                _policeCars[i] = await SpawnVehicle(policeCar.Model, policeCar.Location, policeCar.Heading);

                if (policeCar.SirenActive) _policeCars[i].IsSirenActive = true;
                if (policeCar.SirenSilent) _policeCars[i].IsSirenSilent = true;

                if (policeCar.TrunkOpen) SetVehicleDoorOpen(_policeCars[i].Handle, 5, false, false);
            }
        }

        public async Task SpawnOnScenePedCops()
        {
            _policePeds = new Ped[_scene.PolicePeds.Length];
            for (var i = 0; i < _scene.PolicePeds.Length; i++)
            {
                var policePed = _scene.PolicePeds[i];

                _policePeds[i] = await SpawnPed(policePed.Model, policePed.Location);
                _policePeds[i].AlwaysKeepTask = true;
                _policePeds[i].BlockPermanentEvents = true;

                _policePeds[i].Weapons.Give(policePed.Weapon, 1, true, true);

                SetEntityHeading(_policePeds[i].Handle, policePed.Heading);

                if (policePed.AnimationLib != null)
                {
                    RequestAnimDict(policePed.AnimationLib);
                    _policePeds[i].Task.PlayAnimation(policePed.AnimationLib, policePed.AnimationName);
                }

                if (policePed.Scenario != null)
                    TaskStartScenarioInPlace(_policePeds[i].Handle, policePed.Scenario, 0, true);
            }
        }

        public async Task SpawnObjects()
        {
            _spawnedObjects = new int[_scene.ObjectModels.Length];

            for (var i = 0; i < _scene.ObjectModels.Length; i++)
            {
                var spawnedObject = _scene.ObjectModels[i];

                // Request the model
                RequestModel(spawnedObject.ModelHash);

                // Wait until model has loaded
                while (!HasModelLoaded(spawnedObject.ModelHash)) await BaseScript.Delay(1000);

                // Create the object
                _spawnedObjects[i] = CreateObjectNoOffset(spawnedObject.ModelHash, spawnedObject.Location.X,
                    spawnedObject.Location.Y, spawnedObject.Location.Z, true, false, true);

                SetEntityHeading(_spawnedObjects[i], spawnedObject.Heading);

                // Place object to ground
                PlaceObjectOnGroundProperly(_spawnedObjects[i]);

                // Freeze the object
                FreezeEntityPosition(_spawnedObjects[i], true);
            }
        }

        public async Task SpawnCrashedCars()
        {
            _crashedCars = new Vehicle[_scene.CrashedCars.Length];
            for (var i = 0; i < _scene.CrashedCars.Length; i++)
            {
                var crashedCar = _scene.CrashedCars[i];

                // If the car should randomly spawn, make it a 50% chance
                // TODO: This is not possible for now because the array length bug
                /*if (crashedCar.ShouldRandomSpawn && Utilities.RandomBool())
                {
                    continue;
                }*/

                // Spawn the actual vehicle
                _crashedCars[i] = await SpawnVehicle(crashedCar.Model, crashedCar.Location, crashedCar.Heading);

                var crashedCarHandle = _crashedCars[i].Handle;

                SetVehicleEngineOn(crashedCarHandle, true, true, false);

                // Set the engine health
                _crashedCars[i].EngineHealth = crashedCar.EngineHealth;

                // Rotate the car if we defined this
                if (crashedCar.Rotation != null)
                    SetEntityRotation(_crashedCars[i].Handle, crashedCar.Rotation[0], crashedCar.Rotation[1],
                        crashedCar.Rotation[2], 1, true);

                // Attach a blip
                if (crashedCar.HasBlip)
                {
                    _crashedCars[i].AttachBlip();
                    _crashedCars[i].AttachedBlip.Color = BlipColor.Blue;
                }

                // Randomly burst tires
                if (crashedCar.ShouldRandomBurstTires)
                {
                    if (Utilities.RandomBool() && _crashedCars[i].Wheels[0] != null) _crashedCars[i].Wheels[0].Burst();
                    if (Utilities.RandomBool() && _crashedCars[i].Wheels[1] != null) _crashedCars[i].Wheels[1].Burst();
                    if (Utilities.RandomBool() && _crashedCars[i].Wheels[2] != null) _crashedCars[i].Wheels[2].Burst();
                    if (Utilities.RandomBool() && _crashedCars[i].Wheels[3] != null) _crashedCars[i].Wheels[3].Burst();
                }

                // Randomly be damaged
                if (crashedCar.ShouldRandomBeDamaged && Utilities.RandomBool())
                {
                    var damage = Utilities.Between(99, 1000);

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
                    var damage = Utilities.Between(99, 1000);
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
                        SetVehicleIndicatorLights(_crashedCars[i].Handle, 0, true);
                        SetVehicleIndicatorLights(_crashedCars[i].Handle, 1, true);
                    }
                    else if (crashedCar.BlinkingLightsDirection == 1)
                    {
                        SetVehicleIndicatorLights(_crashedCars[i].Handle, 1, true);
                    }
                    else if (crashedCar.BlinkingLightsDirection == 2)
                    {
                        SetVehicleIndicatorLights(_crashedCars[i].Handle, 0, true);
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
                        driver.SetIntoVehicle(_crashedCars[i], vehiclePed.Seat);

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

                    AttachVehicleToTrailer(_crashedCars[i].Handle, trailer.Handle, 50f);

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
            _civilianPeds = new Ped[_scene.CivilianPeds.Length];
            for (var i = 0; i < _scene.CivilianPeds.Length; i++)
            {
                var civilian = _scene.CivilianPeds[i];

                _civilianPeds[i] = await SpawnPed(civilian.Model, civilian.Location);
                _civilianPeds[i].AlwaysKeepTask = true;

                _civilianPeds[i].Weapons.Give(civilian.Weapon, 1, true, true);

                if (civilian.Invisible)
                {
                    _civilianPeds[i].IsVisible = false;
                    FreezeEntityPosition(_civilianPeds[i].Handle, true);
                }

                SetEntityHealth(_civilianPeds[i].Handle, civilian.Health);

                if (civilian.AnimationLib != null)
                {
                    RequestAnimDict(civilian.AnimationLib);
                    _civilianPeds[i].Task.PlayAnimation(civilian.AnimationLib, civilian.AnimationName);
                }

                if (civilian.Scenario != null)
                    TaskStartScenarioInPlace(_civilianPeds[i].Handle, civilian.Scenario, 0, true);

                if (civilian.HasBlip)
                {
                    _civilianPeds[i].AttachBlip();
                    _civilianPeds[i].AttachedBlip.Color = BlipColor.Green;
                }
            }
        }

        public void RunNotifications()
        {
            foreach (var notification in _scene.Notifications()) ShowNotification($"[Dispatch] {notification}");
        }

        public override void OnCancelBefore()
        {
            //Tick -= scene.RunAdditionalTasks;

            try
            {
                if (_crashedCars.Length > 0)
                    // Remove crashed car blip
                    foreach (var spawnedCar in _crashedCars)
                    {
                        if (spawnedCar != null && spawnedCar.AttachedBlip != null)
                        {
                            spawnedCar.AttachedBlip?.Delete(); 
                        }

                        // Remove ped blip
                        foreach (var passenger in spawnedCar.Occupants)
                        {
                            if (passenger == null) continue;

                            passenger.AttachedBlip?.Delete();
                        }
                    }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                if (_civilianPeds.Length > 0)
                    // Remove crashed car blip
                    foreach (var spawnedCivilian in _civilianPeds)
                    {
                        if (spawnedCivilian != null && spawnedCivilian.AttachedBlip != null)
                        {
                            spawnedCivilian.AttachedBlip?.Delete();;
                        }

                        if (!spawnedCivilian.IsVisible) spawnedCivilian.Delete();
                    }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                if (_spawnedObjects.Length > 0)
                    // Clear out spawned cones
                    foreach (var spawnedObject in _spawnedObjects)
                        Entity.FromHandle(spawnedObject).Delete();
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                if (_policeCars.Length > 0)
                    // Remove the on-scene cop cars (if any)
                    foreach (var policeCar in _policeCars)
                    {
                        if (policeCar == null) continue;

                        policeCar.Delete();
                    }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                if (_policePeds.Length > 0)
                    // Remove the on-scene cop cars (if any)
                    foreach (var policePed in _policePeds)
                    {
                        if (policePed == null) continue;

                        policePed.Delete();
                    }
            }
            catch (Exception)
            {
                // ignored
            }

            try
            {
                // Run the scene finish method and pass along peds, cars, etc.
                _scene.Finish();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public async Task<Vehicle> _SpawnVehicle(VehicleHash vehicleHash,
            Vector3 location,
            float heading = 0.0f)
        {
            var spawned = await SpawnVehicle(vehicleHash, location, heading);

            return spawned;
        }

        public async Task<Ped> _SpawnPed(PedHash pedHash, Vector3 location, float heading = 0.0f)
        {
            var spawned = await SpawnPed(pedHash, location, heading);

            return spawned;
        }

        public bool _IsCarInReach(Vehicle origin, Vehicle destination, float reach = 25f)
        {
            var total = World.GetDistance(origin.Position, destination.Position);

            if (total < reach) return true;

            return false;
        }
    }
}