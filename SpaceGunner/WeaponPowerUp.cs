using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SpaceGunner.Weapons;

namespace SpaceGunner
{
    public class WeaponPowerUp : PowerUp
    {
        public WeaponType type { get; set; }

        public WeaponPowerUp(Texture2D spriteSheet, int frames, double itemRarity, WeaponType weaponType) : base(spriteSheet, frames, itemRarity)
        {
            type = weaponType;
        }

        override public void Collect(Player player)
        {
            player.equippedWeapon.changeWeapon(type);
        }
    }
}
