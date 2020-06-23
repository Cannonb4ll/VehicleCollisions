using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class CrashedVehicleTrailerVehicle
    {
        public Vector3 Location;
        public float Heading;
        public VehicleHash Model;
        public VehicleHash VehicleOnTrailer;

        public CrashedVehicleTrailerVehicle (
            Vector3 location, 
            float heading, 
            VehicleHash model
        )
        {
            this.Location = location;
            this.Heading = heading;
            this.Model = model;
        }
    }
}