using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    class Enemy
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public float scale { get; set; }
        public bool isActive { get; set; }
        public int health { get; set; }
        public Vector2 velocity { get; set; }
        public float width { get { return texture.Width * scale; } }
        public float height { get { return texture.Height * scale; } }
        public Vector2 originX { get { return new Vector2(position.X + (width / 2), position.Y); } }
        public Vector2 originY { get { return new Vector2(position.X, position.Y + (height / 2)); } }

        public Enemy(Vector2 starPos, Texture2D tex)
        {
            scale = Game1.scale;
            health = 100;
            isActive = true;
            texture = tex;
            velocity = new Vector2(0, 150);
            position = starPos;
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
    }
}
