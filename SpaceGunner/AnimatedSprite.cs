﻿using Microsoft.Xna.Framework;
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

        public void Initialize(Texture2D tex, Vector2 pos, int frameCount, float frameInterval, float textureScale, bool looped)
        {
            sheet = tex;
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
                        currentFrame = frames;
                        if (!isLooping)
                        {
                            isActive = false;
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
    }
}
