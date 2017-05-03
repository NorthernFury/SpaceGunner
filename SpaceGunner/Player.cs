using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class Player : Ship
    {
        public bool crashed { get; set; }
        public int lives { get; set; }
        public int score { get; set; }
        public int highScore { get; set; }
        public PlayerWeapons equippedWeapon { get; set; }
        public TimeSpan lastFired { get; set; }

        private const float P_SPEED = 250f;

        public Player()
        {
            position = new Vector2((Game1.PLAYAREAX / 2) - 20, Game1.PLAYAREAY - 50);
            isAlive = true;
            health = 100;
            scale = Game1.scale;
            equippedWeapon = new PlayerWeapons();
            equippedWeapon.changeWeapon(PlayerWeapons.WeaponType.DualLaser);
            lives = 3; // The default at game start is set in NewGame()
            crashed = false;
            lastFired = TimeSpan.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (position.X < 0) { position = new Vector2(0, position.Y); }
            if (position.X > Game1.PLAYAREAX - width ) { position = new Vector2(Game1.PLAYAREAX - width, position.Y); }
            if (position.Y < 0) { position = new Vector2(position.X, 0); }
            if (position.Y > Game1.PLAYAREAY - height) { position = new Vector2(position.X, Game1.PLAYAREAY - height); }
        }

        public void ProcessInput(KeyboardState keyState)
        {
            Vector2 direction = new Vector2(0, 0);

            if (keyState.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                direction.X = 1;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                direction.Y = 1;
            }

            velocity = new Vector2(direction.X * P_SPEED, direction.Y * P_SPEED);

            if (keyState.IsKeyDown(Keys.Z))
            {
                equippedWeapon.changeWeapon(PlayerWeapons.WeaponType.SingleLaser);
            }
            else if (keyState.IsKeyDown(Keys.X))
            {
                equippedWeapon.changeWeapon(PlayerWeapons.WeaponType.DualLaser);
            }
        }

        public void ResetPlayer()
        {
            position = new Vector2((Game1.PLAYAREAX / 2) - 20, Game1.PLAYAREAY - 50);
            if (crashed)
            {
                lives--;
                crashed = false;
            }
        }
    }
}
