using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class Enemy : Ship
    {
        public TimeSpan nextShot { get; set; }

        public Enemy(Vector2 starPos, Texture2D tex)
        {
            scale = Game1.scale;
            health = 100;
            isAlive = true;
            texture = tex;
            velocity = new Vector2(0, 150);
            position = starPos;
            nextShot = TimeSpan.FromMilliseconds(1500f);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
                //position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.Y > Game1.PLAYAREAY)
            {
                isAlive = false;
            }
        }

    }
}
