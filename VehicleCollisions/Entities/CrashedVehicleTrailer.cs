using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class CrashedVehicleTrailer
    {
        public Vector3 Location;
        public float Heading;
        public VehicleHash Model;
        public CrashedVehicleTrailerVehicle VehicleOnTrailer = null;

        public CrashedVehicleTrailer (
            Vector3 location, 
            float heading, 
            VehicleHash model
        )
        {
            this.Location = location;
            this.Heading = heading;
            this.Model = model;
        }

        public CrashedVehicleTrailer SetVehicleOnTrailer(CrashedVehicleTrailerVehicle vehicle)
        {
            this.VehicleOnTrailer = vehicle;
            
            return this;
        }
    }
}