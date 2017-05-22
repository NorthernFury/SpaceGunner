using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using static SpaceGunner.Game1;
using static SpaceGunner.Weapons;

namespace SpaceGunner
{
    public class Enemy : Ship
    {
        private AnimatedSprite explosion { get; set; }
        private Texture2D explosionTexture { get; set; }

        public Enemy(Vector2 startPos, Texture2D tex, Color col, Texture2D explosionTex) : base(startPos, tex, col)
        {
            //texture = tex;
            explosionTexture = explosionTex;
            velocity = new Vector2(0, 150);
            //position = startPos;
            lastFired = TimeSpan.Zero;
            //color = col;
            equippedWeapon = new Weapons(this);
            equippedWeapon.changeWeapon(WeaponType.SingleLaser);
            explosion = new AnimatedSprite();
            state = ShipState.Active;
        }

        public override void Update(GameTime gameTime)
        {
            if (state == ShipState.Active)
            {
                if (position.Y > PLAYAREAY)
                {
                    isAlive = false;
                    state = ShipState.Dead;
                }

                base.Update(gameTime);
            }
            else if (state == ShipState.Exploding)
            {
                if (!explosion.isActive)
                {
                    state = ShipState.Dead;
                }
                explosion.Update(gameTime);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (state == ShipState.Active)
            {
                base.Draw(spriteBatch);
            }
            else if (state == ShipState.Exploding)
            {   
                if (explosion.currentFrame < 4)
                {
                    base.Draw(spriteBatch);
                }
                explosion.Draw(spriteBatch);
            }
        }

        public void BeginExplosion(sfxManager sfx)
        {
            if (state != ShipState.Exploding)
            {
                state = ShipState.Exploding;
                explosion.Initialize(explosionTexture, position, 8, 65f, 1.0f, false);
                sfx.Effect("enemyexplosion").Play();
            }
        }
    }
}
