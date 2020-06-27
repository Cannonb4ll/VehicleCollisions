using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class EmergencyPed
    {
        public string AnimationLib;
        public string AnimationName;
        public float Heading;
        public Vector3 Location;
        public PedHash Model;
        public string Scenario;
        public WeaponHash Weapon;

        public EmergencyPed(Vector3 location, float heading, PedHash model)
        {
            Location = location;
            Heading = heading;
            Model = model;
        }

        public EmergencyPed GiveWeapon(WeaponHash weapon)
        {
            Weapon = weapon;

            return this;
        }

        public EmergencyPed SetAnimation(string animationLib = null, string animationName = null)
        {
            AnimationLib = animationLib;
            AnimationName = animationName;

            return this;
        }

        public EmergencyPed SetScenario(string scenario = null)
        {
            Scenario = scenario;

            return this;
        }
    }
}