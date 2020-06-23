using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class CrashedVehicleTrailer
    {
        public float Heading;
        public Vector3 Location;
        public VehicleHash Model;
        public CrashedVehicleTrailerVehicle VehicleOnTrailer;

        public CrashedVehicleTrailer(
            Vector3 location,
            float heading,
            VehicleHash model
        )
        {
            Location = location;
            Heading = heading;
            Model = model;
        }

        public CrashedVehicleTrailer SetVehicleOnTrailer(CrashedVehicleTrailerVehicle vehicle)
        {
            VehicleOnTrailer = vehicle;

            return this;
        }
    }
}