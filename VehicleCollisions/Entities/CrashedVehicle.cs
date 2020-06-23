using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class CrashedVehicle
    {
        public Vector3 Location;
        public float Heading;
        public int EngineHealth;
        public VehicleHash Model;
        public VehiclePed[] Peds;
        public float[] Rotation;
        public bool BlinkingLights;
        public int BlinkingLightsDirection;
        public bool LeftDoorOpen;
        public bool RightDoorOpen;
        public bool LeftRearDoorOpen;
        public bool RightRearDoorOpen;
        public bool HoodOpen;
        public bool ShouldRandomSpawn;
        public bool HasBlip;
        public bool ShouldRandomBurstTires;
        public bool ShouldRandomBeDamaged;
        public bool BeDamaged;
        public CrashedVehicleTrailer AttachedTrailer;
        
        public CrashedVehicle (
            Vector3 location, 
            float heading, 
            VehicleHash model
            )
        {
            this.Location = location;
            this.Heading = heading;
            this.Model = model;
        }

        public CrashedVehicle SetPedsInVehicle(VehiclePed[] peds = null)
        {
            this.Peds = peds;
            return this;
        }

        public CrashedVehicle AttachTrailer(CrashedVehicleTrailer trailer = null)
        {
            this.AttachedTrailer = trailer;
            
            return this;
        }
        
        public CrashedVehicle SetRotation(float[] rotation = null)
        {
            this.Rotation = rotation;
            
            return this;
        }

        // Direction 0 = both
        // Direction 1 = left
        // Direction 2 = right
        public CrashedVehicle SetBlinkingLights(bool blinking = false, int direction = 0)
        {
            BlinkingLights = blinking;
            BlinkingLightsDirection = direction;

            return this;
        }
        
        public CrashedVehicle SetEngineHealth(int health = 1000)
        {
            EngineHealth = health;

            return this;
        }

        public CrashedVehicle SetDoorsOpen(bool left = false, bool right = false, bool leftRear = false, bool rightRear = false, bool hood = false)
        {
            this.LeftDoorOpen = left;
            this.RightDoorOpen = right;
            this.LeftRearDoorOpen = leftRear;
            this.RightRearDoorOpen = rightRear;
            this.HoodOpen = hood;
            
            return this;
        }

        public CrashedVehicle ShouldRandomlySpawn(bool randomSpawn = false)
        {
            this.ShouldRandomSpawn = randomSpawn;
            
            return this;
        }
        
        public CrashedVehicle ShouldHaveBlip(bool hasBlip = false)
        {
            this.HasBlip = hasBlip;
            
            return this;
        }

        public CrashedVehicle ShouldRandomlyBurstTires(bool randomBurstTires = false)
        {
            this.ShouldRandomBurstTires = randomBurstTires;
            
            return this;
        }
        
        public CrashedVehicle ShouldRandomlyBeDamaged(bool randomDamaged = false)
        {
            this.ShouldRandomBeDamaged = randomDamaged;
            
            return this;
        }
        
        public CrashedVehicle ShouldBeDamaged(bool damaged = false)
        {
            this.BeDamaged = damaged;
            
            return this;
        }
    }
}