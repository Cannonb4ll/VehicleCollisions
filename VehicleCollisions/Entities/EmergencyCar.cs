using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class EmergencyCar
    {
        public float Heading;
        public Vector3 Location;
        public VehicleHash Model;
        public bool SirenActive;
        public bool SirenSilent;
        public bool TrunkOpen;

        public EmergencyCar(Vector3 location, float heading, VehicleHash model)
        {
            Location = location;
            Heading = heading;
            Model = model;
        }

        public EmergencyCar SetSirenActive(bool sirenActive = false)
        {
            SirenActive = sirenActive;

            return this;
        }

        public EmergencyCar SetSirenSilent(bool sirenSilent = false)
        {
            SirenSilent = sirenSilent;

            return this;
        }

        public EmergencyCar SetTrunkOpen(bool trunkOpen = false)
        {
            TrunkOpen = trunkOpen;

            return this;
        }
    }
}