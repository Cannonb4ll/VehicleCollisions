using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class ObjectModel
    {
        public Vector3 Location;
        public uint ModelHash;
        public float Heading;

        public ObjectModel(Vector3 location, uint modelHash, float heading = 0f)
        {
            this.Location = location;
            this.ModelHash = modelHash;
            this.Heading = heading;
        }
    }
}