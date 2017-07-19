using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGunner
{
    public class AnimatedSprite
    {
        public int currentFrame { get; private set; }
        public int frameHeight { get { return sheet.Height; } }
        public int frameWidth { get { return sheet.Width / frames; } }
        public bool isLooping { get; private set; }
        public bool isActive { get; private set; }

        private Texture2D sheet { get; set; }
        private float scale { get; set; }
        private Vector2 position { get; set; }
        private int frames { get; set; }
        private float interval { get; set; }
        private float elapsed { get; set; }
        private Rectangle sourceRect { get; set; }

        public AnimatedSprite(Texture2D textureSheet)
        {
            sheet = textureSheet;
        }

        public AnimatedSprite(Texture2D textureSheet, int frameCount, float frameInterval, float textureScale, bool looped)
        {
            sheet = textureSheet;
            frames = frameCount;
            interval = frameInterval;
            scale = textureScale;
            isLooping = looped;
        }

        public void Start(Vector2 pos)
        {
            position = pos;

            currentFrame = 0;
            elapsed = 0;
            isActive = true;
        }

        public void Start(Vector2 pos, int frameCount, float frameInterval, float textureScale, bool looped)
        {
            position = pos;
            frames = frameCount;
            interval = frameInterval;
            scale = textureScale;
            isLooping = looped;

            currentFrame = 0;
            elapsed = 0;
            isActive = true;
        }

        public void Update(GameTime gameTime)
        {
            if (isActive)
            {
                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (elapsed > interval)
                {
                    currentFrame++;

                    if (currentFrame == frames)
                    {
                        if (!isLooping)
                        {
                            isActive = false;
                        }
                        else
                        {
                            currentFrame = 0;
                        }
                    }

                    sourceRect = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);
                    elapsed = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (isActive)
            {
                spriteBatch.Draw(sheet, position, sourceRect, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            }
        }

        public AnimatedSprite Clone()
        {
            return (AnimatedSprite)MemberwiseClone();
        }
    }
}
