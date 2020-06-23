using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class CivilianPed
    {
        public string AnimationLib;
        public string AnimationName;
        public bool HasBlip;
        public float Heading;
        public int Health = 200;
        public bool Invisible;
        public Vector3 Location;
        public PedHash Model;
        public string Scenario;
        public WeaponHash Weapon;

        public CivilianPed(Vector3 location, float heading, PedHash model)
        {
            Location = location;
            Heading = heading;
            Model = model;
        }

        public CivilianPed SetHealth(int health = 200)
        {
            Health = health;

            return this;
        }

        public CivilianPed GiveWeapon(WeaponHash weapon)
        {
            Weapon = weapon;

            return this;
        }

        public CivilianPed SetInvisible(bool invisible = false)
        {
            Invisible = invisible;

            return this;
        }

        public CivilianPed SetAnimation(string animationLib = null, string animationName = null)
        {
            AnimationLib = animationLib;
            AnimationName = animationName;

            return this;
        }

        public CivilianPed SetScenario(string scenario = null)
        {
            Scenario = scenario;

            return this;
        }

        public CivilianPed ShouldHaveBlip(bool hasBlip = false)
        {
            HasBlip = hasBlip;

            return this;
        }
    }
}