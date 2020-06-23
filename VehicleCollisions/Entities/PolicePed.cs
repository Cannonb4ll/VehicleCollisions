using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class PolicePed
    {
        public Vector3 Location;
        public float Heading;
        public PedHash Model;
        public WeaponHash Weapon;

        public PolicePed (Vector3 location, float heading, PedHash model)
        {
            this.Location = location;
            this.Heading = heading;
            this.Model = model;
        }

        public PolicePed GiveWeapon(WeaponHash weapon)
        {
            this.Weapon = weapon;
            
            return this;
        }
    }
}