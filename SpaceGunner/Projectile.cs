using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class Projectile
    {
        public Texture2D texture { get; set; }
        public float scale { get; set; }
        public Vector2 velocity { get; set; }
        public Vector2 position { get; set; }
        public bool isActive { get; set; }
        public float width { get { return texture.Width * scale; } }
        public float height { get { return texture.Height * scale; } }
        private Vector2 origin { get { return new Vector2(width / 2, height / 2); } }

        public Projectile(Vector2 origin, Texture2D tex, Vector2 vel)
        {
            scale = Game1.scale;
            velocity = vel;
            position = origin;
            texture = tex;
            isActive = true;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.Y < -60 || position.Y > Game1.SCREENAREAY)
            {
                isActive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
        }

        public void LoadContent(Texture2D tex)
        {
            texture = tex;
        }
    }
}
