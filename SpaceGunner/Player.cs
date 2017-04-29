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
    public class Player
    {
        public Texture2D texture { get; set; }
        public Vector2 position { get; set; }
        public float scale { get; set; }
        public bool active { get; set; }
        public int health { get; set; }
        public float width { get { return texture.Width * scale; } }
        public float height { get { return texture.Height * scale; } }
        public bool crashed { get; set; }
        public int score { get; set; }
        public float originX { get { return position.X + (width / 2); } }
        public float originY { get { return position.Y + (height /2); } }
        public PlayerWeapons equippedWeapon { get; set; }
        public int lives { get; set; }
        public int highScore { get; set; }
        public int maxLives = 3;

        private const float P_SPEED = 250f;

        public Player()
        {
            position = new Vector2((Game1.PLAYAREAX / 2) - 20, Game1.PLAYAREAY - 50);
            active = true;
            health = 100;
            scale = Game1.scale;
            equippedWeapon = new PlayerWeapons();
            equippedWeapon.changeWeapon(PlayerWeapons.WeaponType.DualLaser);
            lives = 3;
            crashed = false;
        }

        public void Update(GameTime gameTime, KeyboardState keyState)
        {
            Vector2 direction = Vector2.Zero;
            Vector2 speed = Vector2.Zero;

            if (keyState.IsKeyDown(Keys.Left))
            {
                direction.X = -1;
                speed.X = P_SPEED;
            }
            else if (keyState.IsKeyDown(Keys.Right))
            {
                direction.X = 1;
                speed.X = P_SPEED;
            }

            if (keyState.IsKeyDown(Keys.Up))
            {
                direction.Y = -1;
                speed.Y = P_SPEED;
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                direction.Y = 1;
                speed.Y = P_SPEED;
            }

            if (keyState.IsKeyDown(Keys.Z))
            {
                equippedWeapon.changeWeapon(PlayerWeapons.WeaponType.SingleLaser);
            }
            else if (keyState.IsKeyDown(Keys.X))
            {
                equippedWeapon.changeWeapon(PlayerWeapons.WeaponType.DualLaser);
            }

            position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (position.X < 0) { position = new Vector2(0, position.Y); }
            if (position.X > Game1.PLAYAREAX - width ) { position = new Vector2(Game1.PLAYAREAX - width, position.Y); }
            if (position.Y < 0) { position = new Vector2(position.X, 0); }
            if (position.Y > Game1.PLAYAREAY - height) { position = new Vector2(position.X, Game1.PLAYAREAY - height); }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
        }

        public void LoadContent(Texture2D tex)
        {
            texture = tex;
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
