﻿using Microsoft.Xna.Framework;
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

        private const float P_SPEED = 250f;

        public Player()
        {
            position = new Vector2((Game1.PLAYAREAX / 2) - 20, Game1.PLAYAREAY - 50);
            equippedWeapon = new Weapons();
            equippedWeapon.changeWeapon(Weapons.WeaponType.SingleLaser, this);
            lives = 3; // The default at game start is set in NewGame()
            crashed = false;
            lastFired = TimeSpan.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            if (position.X < 0) { position = new Vector2(0, position.Y); }
            if (position.X > Game1.PLAYAREAX - width ) { position = new Vector2(Game1.PLAYAREAX - width, position.Y); }
            if (position.Y < 0) { position = new Vector2(position.X, 0); }
            if (position.Y > Game1.PLAYAREAY - height) { position = new Vector2(position.X, Game1.PLAYAREAY - height); }

            base.Update(gameTime);
        }

        public void ChangeDirection(Vector2 newDir)
        {
            velocity = new Vector2(newDir.X * P_SPEED, newDir.Y * P_SPEED);
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
