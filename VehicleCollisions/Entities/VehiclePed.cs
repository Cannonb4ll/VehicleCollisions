using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class VehiclePed
    {
        public bool HasBlip = true;
        public float Heading;
        public int Health = 200;
        public Vector3 Location;
        public PedHash Model;
        public VehicleSeat Seat;
        public bool ShouldRandomSpawn;

        public VehiclePed(Vector3 location, float heading, PedHash model, VehicleSeat seat)
        {
            Location = location;
            Heading = heading;
            Model = model;
            Seat = seat;
        }

        public VehiclePed SetHealth(int health = 200)
        {
            Health = health;

            return this;
        }

        public VehiclePed ShouldHaveBlip(bool hasBlip = false)
        {
            HasBlip = hasBlip;

            return this;
        }

        public VehiclePed ShouldRandomlySpawn(bool randomSpawn = false)
        {
            ShouldRandomSpawn = randomSpawn;

            return this;
        }
    }
}