using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class VehiclePed
    {
        public Vector3 Location;
        public float Heading;
        public PedHash Model;
        public VehicleSeat Seat;
        public int Health = 200;
        public bool HasBlip = true;
        public bool ShouldRandomSpawn;
        
        public VehiclePed (Vector3 location, float heading, PedHash model, VehicleSeat seat)
        {
            this.Location = location;
            this.Heading = heading;
            this.Model = model;
            this.Seat = seat;
        }
        
        public VehiclePed SetHealth(int health = 200)
        {
            this.Health = health;

            return this;
        }
        
        public VehiclePed ShouldHaveBlip(bool hasBlip = false)
        {
            this.HasBlip = hasBlip;
            
            return this;
        }
        
        public VehiclePed ShouldRandomlySpawn(bool randomSpawn = false)
        {
            this.ShouldRandomSpawn = randomSpawn;
            
            return this;
        }
    }
}