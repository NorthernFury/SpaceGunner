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
        public float itemDropRate { get; set; }

        public Enemy(Vector2 startPos, Texture2D tex, Color col, AnimatedSprite explosionTex, float dropRate) : base(startPos, tex, col)
        {
            //texture = tex;
            velocity = new Vector2(0, 150);
            //position = startPos;
            lastFired = TimeSpan.Zero;
            //color = col;
            equippedWeapon = new Weapons(this);
            equippedWeapon.changeWeapon(WeaponType.SingleLaser);
            explosion = explosionTex;
            itemDropRate = dropRate;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (state == ShipState.Active)
            {
                if (position.Y > PLAYAREAY)
                {
                    state = ShipState.Dead;
                }

            }
        }
    }
}
