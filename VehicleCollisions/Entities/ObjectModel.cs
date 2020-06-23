using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class ObjectModel
    {
        public float Heading;
        public Vector3 Location;
        public uint ModelHash;

        public ObjectModel(Vector3 location, uint modelHash, float heading = 0f)
        {
            Location = location;
            ModelHash = modelHash;
            Heading = heading;
        }
    }
}