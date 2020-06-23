using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class PoliceCar
    {
        public float Heading;
        public Vector3 Location;
        public VehicleHash Model;
        public bool SirenActive;
        public bool SirenSilent;
        public bool TrunkOpen;

        public PoliceCar(Vector3 location, float heading, VehicleHash model)
        {
            Location = location;
            Heading = heading;
            Model = model;
        }

        public PoliceCar SetSirenActive(bool sirenActive = false)
        {
            SirenActive = sirenActive;

            return this;
        }

        public PoliceCar SetSirenSilent(bool sirenSilent = false)
        {
            SirenSilent = sirenSilent;

            return this;
        }

        public PoliceCar SetTrunkOpen(bool trunkOpen = false)
        {
            TrunkOpen = trunkOpen;

            return this;
        }
    }
}