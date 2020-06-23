using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class CrashedVehicleTrailerVehicle
    {
        public float Heading;
        public Vector3 Location;
        public VehicleHash Model;
        public VehicleHash VehicleOnTrailer;

        public CrashedVehicleTrailerVehicle(
            Vector3 location,
            float heading,
            VehicleHash model
        )
        {
            Location = location;
            Heading = heading;
            Model = model;
        }
    }
}