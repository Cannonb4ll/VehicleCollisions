using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class CrashedVehicle
    {
        public CrashedVehicleTrailer AttachedTrailer;
        public bool BeDamaged;
        public bool BlinkingLights;
        public int BlinkingLightsDirection;
        public int EngineHealth;
        public bool HasBlip;
        public float Heading;
        public bool HoodOpen;
        public bool LeftDoorOpen;
        public bool LeftRearDoorOpen;
        public Vector3 Location;
        public VehicleHash Model;
        public VehiclePed[] Peds;
        public bool RightDoorOpen;
        public bool RightRearDoorOpen;
        public float[] Rotation;
        public bool ShouldRandomBeDamaged;
        public bool ShouldRandomBurstTires;
        public bool ShouldRandomSpawn;

        public CrashedVehicle(
            Vector3 location,
            float heading,
            VehicleHash model
        )
        {
            Location = location;
            Heading = heading;
            Model = model;
        }

        public CrashedVehicle SetPedsInVehicle(VehiclePed[] peds = null)
        {
            Peds = peds;
            return this;
        }

        public CrashedVehicle AttachTrailer(CrashedVehicleTrailer trailer = null)
        {
            AttachedTrailer = trailer;

            return this;
        }

        public CrashedVehicle SetRotation(float[] rotation = null)
        {
            Rotation = rotation;

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

        public CrashedVehicle SetDoorsOpen(bool left = false, bool right = false, bool leftRear = false,
            bool rightRear = false, bool hood = false)
        {
            LeftDoorOpen = left;
            RightDoorOpen = right;
            LeftRearDoorOpen = leftRear;
            RightRearDoorOpen = rightRear;
            HoodOpen = hood;

            return this;
        }

        public CrashedVehicle ShouldRandomlySpawn(bool randomSpawn = false)
        {
            ShouldRandomSpawn = randomSpawn;

            return this;
        }

        public CrashedVehicle ShouldHaveBlip(bool hasBlip = false)
        {
            HasBlip = hasBlip;

            return this;
        }

        public CrashedVehicle ShouldRandomlyBurstTires(bool randomBurstTires = false)
        {
            ShouldRandomBurstTires = randomBurstTires;

            return this;
        }

        public CrashedVehicle ShouldRandomlyBeDamaged(bool randomDamaged = false)
        {
            ShouldRandomBeDamaged = randomDamaged;

            return this;
        }

        public CrashedVehicle ShouldBeDamaged(bool damaged = false)
        {
            BeDamaged = damaged;

            return this;
        }
    }
}