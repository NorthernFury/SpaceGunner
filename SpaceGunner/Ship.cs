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
        public bool active { get; set; }
        public int health { get; set; }
        public float width { get { return texture.Width / 1 * scale; } }
        public float height { get { return texture.Height / 1 * scale; } }
        public Vector2 originX { get { return new Vector2(position.X + (width / 2), position.Y); } }
        public Vector2 originY { get { return new Vector2(position.X, position.Y + (height / 2)); } }
        public Texture2D texture { get; set; }
        public float scale { get; set; }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
