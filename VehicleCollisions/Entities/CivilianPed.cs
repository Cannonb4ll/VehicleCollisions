using CitizenFX.Core;

namespace VehicleCollisions.Entities
{
    public class CivilianPed
    {
        public Vector3 Location;
        public float Heading;
        public PedHash Model;
        public int Health = 200;
        public WeaponHash Weapon;
        public bool Invisible;
        public string AnimationLib = null;
        public string AnimationName = null;
        public string Scenario = null;
        public bool HasBlip;
        
        public CivilianPed (Vector3 location, float heading, PedHash model)
        {
            this.Location = location;
            this.Heading = heading;
            this.Model = model;
        }

        public CivilianPed SetHealth(int health = 200)
        {
            this.Health = health;

            return this;
        }
        
        public CivilianPed GiveWeapon(WeaponHash weapon)
        {
            this.Weapon = weapon;
            
            return this;
        }

        public CivilianPed SetInvisible(bool invisible = false)
        {
            this.Invisible = invisible;
            
            return this;
        }

        public CivilianPed SetAnimation(string animationLib = null, string animationName = null)
        {
            this.AnimationLib = animationLib;
            this.AnimationName = animationName;
            
            return this;
        }
        
        public CivilianPed SetScenario(string scenario = null)
        {
            this.Scenario = scenario;
            
            return this;
        }
        
        public CivilianPed ShouldHaveBlip(bool hasBlip = false)
        {
            this.HasBlip = hasBlip;
            
            return this;
        }
    }
}