using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGunner
{
    public class Projectile
    {
        public bool isActive { get; set; }
        public bool fromPlayer { get; set; }
        public float width { get { return texture.Width * scale; } }
        public float height { get { return texture.Height * scale; } }
        public Vector2 position { get; private set; }

        private Texture2D texture { get; set; }
        private float scale { get; set; }
        private Vector2 velocity { get; set; }
        private float rotation { get; set; }
        private Vector2 origin { get { return new Vector2(width / 2, height / 2); } }

        public Projectile(Vector2 origin, Texture2D tex, Vector2 vel, bool isFromPlayer)
        {
            scale = Game1.scale;
            velocity = vel;
            position = origin;
            texture = tex;
            isActive = true;
            fromPlayer = isFromPlayer;
            rotation = 0f;
        }

        public Projectile(Vector2 origin, Texture2D tex, Vector2 vel, float rot, bool isFromPlayer)
        {
            scale = Game1.scale;
            velocity = vel;
            position = origin;
            texture = tex;
            isActive = true;
            fromPlayer = isFromPlayer;
            rotation = rot;
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
            spriteBatch.Draw(texture, position, null, Color.White, rotation, origin, scale, SpriteEffects.None, 0f);
        }

        public void LoadContent(Texture2D tex)
        {
            texture = tex;
        }
    }
}
