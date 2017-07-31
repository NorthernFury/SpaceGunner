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

        public Enemy(Vector2 startPos, string name, TextureManager tm, float dropRate)
        {
            velocity = new Vector2(0, 150);
            position = startPos;
            lastFired = TimeSpan.Zero;
            equippedWeapon = new Weapons(this, tm);
            equippedWeapon.changeWeapon(WeaponType.SingleLaser);
            explosion = tm.animated["ShipExplosion"];
            itemDropRate = dropRate;
            if (name == "EnemyRed")
            {
                SetTexture(tm.texture["EnemyRed"]);
            }
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
