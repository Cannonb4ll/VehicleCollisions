using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class PolicePed
    {
        public string AnimationLib;
        public string AnimationName;
        public float Heading;
        public Vector3 Location;
        public PedHash Model;
        public WeaponHash Weapon;
        public string Scenario;
        
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
        
        public PolicePed SetAnimation(string animationLib = null, string animationName = null)
        {
            AnimationLib = animationLib;
            AnimationName = animationName;

            return this;
        }
        
        public PolicePed SetScenario(string scenario = null)
        {
            Scenario = scenario;

            return this;
        }
    }
}