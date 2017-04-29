using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class PlayerWeapons
    {
        public enum WeaponType { SingleLaser, DualLaser };

        public WeaponType weapon { get; set; }
        public Vector2 velocity { get; set; }
        public float fireRate { get; set; }

        public void changeWeapon(WeaponType toWeapon)
        {
            if (toWeapon == WeaponType.SingleLaser)
            {
                weapon = toWeapon;
                velocity = new Vector2(0, -350);
                fireRate = 460f;
            }
            else if (toWeapon == WeaponType.DualLaser)
            {
                weapon = toWeapon;
                velocity = new Vector2(0, -450);
                fireRate = 350f;
            }
        }
    }
}
