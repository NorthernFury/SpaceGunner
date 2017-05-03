using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class Ship
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public bool isAlive { get; set; }
        public int health { get; set; }
        public float width { get { return texture.Width * scale; } }
        public float height { get { return texture.Height * scale; } }
        public Vector2 origin { get { return new Vector2(width + (width / 2), height + (height / 2)); } }
        public Vector2 currentOrigin { get { return new Vector2(position.X + (width / 2), position.Y + (height / 2)); } }
        public Texture2D texture { get; set; }
        public float scale { get; set; }

        public Ship()
        {
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);

        }
        public virtual void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public bool Collision(Projectile projectile)
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
