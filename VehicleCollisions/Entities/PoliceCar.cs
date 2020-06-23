using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class PoliceCar
    {
        public Vector3 Location;
        public float Heading;
        public VehicleHash Model;
        public bool SirenActive;
        public bool SirenSilent;
        public bool TrunkOpen;

        public PoliceCar (Vector3 location, float heading, VehicleHash model)
        {
            this.Location = location;
            this.Heading = heading;
            this.Model = model;
        }

        public PoliceCar SetSirenActive(bool sirenActive = false)
        {
            this.SirenActive = sirenActive;
            
            return this;
        }
        
        public PoliceCar SetSirenSilent(bool sirenSilent = false)
        {
            this.SirenSilent = sirenSilent;
            
            return this;
        }

        public PoliceCar SetTrunkOpen(bool trunkOpen = false)
        {
            this.TrunkOpen = trunkOpen;
            
            return this;
        }
    }
}