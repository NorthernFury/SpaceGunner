using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceGunner
{
    class Star
    {
        public Vector2 position { get; set; }
        public Vector2 velocity { get; set; }
        public Texture2D texture { get; set; }
        public bool isActive { get; set; }

        private Color color { get; set; }

        public Star (int x, float vel, Texture2D tex, int col)
        {
            position = new Vector2(x, 0);
            texture = tex;
            isActive = true;
            velocity = new Vector2(0, vel);
            SetColor(col);
        }

        public Star(int x, int y, float vel, Texture2D tex, int col)
        {
            position = new Vector2(x, y);
            texture = tex;
            isActive = true;
            velocity = new Vector2(0, vel);
            SetColor(col);

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
            spriteBatch.Draw(texture, position, color);
        }

        private void SetColor(int col)
        {
            switch (col)
            {
                case 0:
                    color = Color.OrangeRed;
                    break;
                case 1:
                    color = Color.White;
                    break;
                case 2:
                    color = Color.Yellow;
                    break;
            }

        }
    }
}
