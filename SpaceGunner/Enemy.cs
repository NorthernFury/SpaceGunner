using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class Enemy
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public float scale { get; set; }
        public bool isActive { get; set; }
        public int health { get; set; }
        public Vector2 velocity { get; set; }
        public TimeSpan nextShot { get; set; }
        public float width { get { return texture.Width * scale; } }
        public float height { get { return texture.Height * scale; } }
        public Vector2 origin { get { return new Vector2(position.X + (width / 2), position.Y + (height / 2)); } }

        public Enemy(Vector2 starPos, Texture2D tex)
        {
            scale = Game1.scale;
            health = 100;
            isActive = true;
            texture = tex;
            velocity = new Vector2(0, 150);
            position = starPos;
            nextShot = TimeSpan.Zero;
        }

        public void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (position.Y > Game1.PLAYAREAY)
            {
                isActive = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public bool Collision (Projectile projectile)
        {
            if ((position.Y + height > projectile.position.Y) &&
                (position.Y < projectile.position.Y) &&
                (position.X < projectile.position.X + projectile.width) &&
                (position.X + width > projectile.position.X))
            {
                return true;
            }

            return false;
        }
    }
}
