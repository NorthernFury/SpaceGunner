using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class DamagePowerUp : PowerUp
    {
        public int damageBonus { get; set; }

        public DamagePowerUp(Texture2D spriteSheet, int frameCount, double itemRarity) : base(spriteSheet, frameCount, itemRarity)
        {
            damageBonus = 10;
        }
    }
}
