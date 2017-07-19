using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceGunner
{
    public class PowerUp
    {
        public double rarity { get; set; }
        public AnimatedSprite sprite { get; set; }

        public PowerUp(Texture2D spriteSheet, int frameCount, double itemRarity)
        {
            rarity = itemRarity;

            sprite = new AnimatedSprite(spriteSheet, frameCount, 50f, Game1.scale, true);
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        virtual public void Collect(Player player)
        {
            // Every power up will need a Collect action, that will apply the effect to the player
        }
    }
}
