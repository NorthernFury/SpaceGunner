using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static SpaceGunner.Game1;

namespace SpaceGunner
{
    public class EnemyManager
    {
        public List<Enemy> enemies { get; set; }
        public Texture2D[] textures { get; set; }
        public Texture2D explosionTexture { get; set; }

        private float frequency = 3000f;
        private TimeSpan lastSpawn = TimeSpan.Zero;
        private Random rnd { get; set; }

        public EnemyManager()
        {
            enemies = new List<Enemy>();
            rnd = new Random();
            textures = new Texture2D[sizeof(Colors)];
        }

        public void Update(GameTime gameTime, Player player, ProjectileManager projectiles, sfxManager sfx)
        {
            enemies.RemoveAll(e => e.state == ShipState.Dead);

            if (gameTime.TotalGameTime.Subtract(lastSpawn) > TimeSpan.FromMilliseconds(rnd.Next((int)frequency / 3, (int)frequency)))
            {
                enemies.Add(new Enemy(new Vector2(rnd.Next(0, 550),-50), textures[(int)Colors.Red], Color.Crimson, explosionTexture));
                lastSpawn = gameTime.TotalGameTime;
            }

            foreach (Enemy en in enemies)
            {
                if (en.state == ShipState.Active)
                {
                    if (player.position.X < en.currentOrigin.X && player.position.X + player.width > en.currentOrigin.X)
                    {
                        en.equippedWeapon.Fire(gameTime, projectiles, sfx, en, player);
                    }

                    // check for collision with player ship
                    if ((en.position.Y + en.height > player.position.Y) &&
                        (en.position.Y < player.position.Y + player.height) &&
                        (en.position.X < player.position.X + player.width) &&
                        (en.position.X + en.width > player.position.X))
                    {
                        en.BeginExplosion(sfx);
                        player.crashed = true;
                    }
                }

                en.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Enemy en in enemies)
            {
                en.Draw(spriteBatch);
            }
        }

        public void ResetEnemies()
        {
            enemies.Clear();
        }
    }
}
