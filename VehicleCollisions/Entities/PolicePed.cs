using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class PolicePed
    {
        public float Heading;
        public Vector3 Location;
        public PedHash Model;
        public WeaponHash Weapon;

        public PolicePed(Vector3 location, float heading, PedHash model)
        {
            Location = location;
            Heading = heading;
            Model = model;
        }

        public PolicePed GiveWeapon(WeaponHash weapon)
        {
            Weapon = weapon;

            return this;
        }
    }
}