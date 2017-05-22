using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceGunner
{
    public class Ship
    {
        public enum ShipState { Active, Exploding, Dead };

        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public bool isAlive { get; set; }
        public float width { get { return texture.Width * scale; } }
        public float height { get { return texture.Height * scale; } }
        public Vector2 origin { get { return new Vector2(width + (width / 2), height + (height / 2)); } }
        public Vector2 currentOrigin { get { return new Vector2(position.X + (width / 2), position.Y + (height / 2)); } }
        public Weapons equippedWeapon { get; set; }
        public TimeSpan lastFired { get; set; }
        public ShipState state { get; set; }

        private Color color { get; set; }
        private int health { get; set; }
        private Texture2D texture { get; set; }
        private float scale { get; set; }
        private bool isFiring { get; set; }

        public Ship()
        {
            position = new Vector2(0, 0);
            velocity = new Vector2(0, 0);
            color = Color.White;
            isAlive = true;
            health = 100;
            scale = Game1.scale;
            isFiring = false;
        }

        public Ship(Vector2 startPos, Texture2D tex, Color col)
        {
            position = startPos;
            velocity = new Vector2(0, 0);
            color = col;
            isAlive = true;
            health = 100;
            scale = Game1.scale;
            texture = tex;
            isFiring = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
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

        public bool Collision(Ship ship)
        {
            if ((position.Y + height > ship.position.Y) && 
                (position.Y < ship.position.Y + ship.height) &&
                (position.X < ship.position.X + ship.width) &&
                (position.X + width > ship.position.X))
            {
                return true;
            }

            return false;

        }
        public void SetTexture(Texture2D tex)
        {
            texture = tex;
        }
    }
}
