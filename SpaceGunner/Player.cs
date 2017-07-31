using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static SpaceGunner.Weapons;

namespace SpaceGunner
{
    public class Player : Ship
    {
        public bool crashed { get; set; }
        public int lives { get; set; }
        public int score { get; set; }
        public int highScore { get; set; }

        private const float P_SPEED = 250f;
        private float invincibleTime = 2500f;
        private float elapsed { get; set; }

        public Player(TextureManager tm)
        {
            position = new Vector2((Game1.PLAYAREAX / 2) - 20, Game1.PLAYAREAY - 50);
            equippedWeapon = new Weapons(this, tm);
            equippedWeapon.changeWeapon(WeaponType.SingleLaser);
            crashed = false;
            lastFired = TimeSpan.Zero;
            explosion = tm.animated["ShipExplosion"];
            elapsed = 0;
            SetTexture(tm.texture["PlayerShip"]);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (position.X < 0) { position = new Vector2(0, position.Y); }
            if (position.X > Game1.PLAYAREAX - width ) { position = new Vector2(Game1.PLAYAREAX - width, position.Y); }
            if (position.Y < 0) { position = new Vector2(position.X, 0); }
            if (position.Y > Game1.PLAYAREAY - height) { position = new Vector2(position.X, Game1.PLAYAREAY - height); }

            // maintain invinsibility after respawn, then set to active
            if (state == ShipState.Invinsible)
            {
                elapsed += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                if (elapsed > invincibleTime)
                {
                    state = ShipState.Active;
                    elapsed = 0;
                }

            }
        }

        public void ChangeDirection(Vector2 newDir)
        {
            velocity = new Vector2(newDir.X * P_SPEED, newDir.Y * P_SPEED);
        }

        public void ResetPlayer()
        {
            position = new Vector2((Game1.PLAYAREAX / 2) - 20, Game1.PLAYAREAY - 50);
            if (state == ShipState.Dead)
            {
                lives--;
                elapsed = 0;
                state = ShipState.Invinsible;
            }
        }
    }
}
