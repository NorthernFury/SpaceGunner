using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class SpeedPowerUp : PowerUp
    {
        public int speedBonus { get; set; }

        public SpeedPowerUp(Texture2D spriteSheet, int frameCount, double itemRarity) : base(spriteSheet, frameCount, itemRarity)
        {
            speedBonus = 10;
        }
    }
}
