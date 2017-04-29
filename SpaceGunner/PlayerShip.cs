using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    class PlayerShip : Ship
    {
        private int boundsX { get; set; }
        private int boundsY { get; set; }

        private const float P_SPEED = 250f;

        public PlayerShip(Vector2 pos)
        {
            position = new Vector2(pos.X, pos.Y);
            active = true;
            health = 100;
            scale = 0.6f;
        }

    }
}
