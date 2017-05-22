using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using static SpaceGunner.Game1;
using static SpaceGunner.Ship;

namespace SpaceGunner
{
    public class EnemyManager
    {
        public List<Enemy> enemies { get; set; }
        public Dictionary<string, Texture2D> textures { get; set; }
        public Texture2D explosionTexture { get; set; }

        private float frequency = 3000f;
        private TimeSpan lastSpawn = TimeSpan.Zero;
        private Random rnd { get; set; }

        public EnemyManager()
        {
            enemies = new List<Enemy>();
            rnd = new Random();
            textures = new Dictionary<string, Texture2D>();
        }

        public void Update(GameTime gameTime, Player player, ProjectileManager pm, sfxManager sfx)
        {
            enemies.RemoveAll(e => e.state == ShipState.Dead);

            if (gameTime.TotalGameTime.Subtract(lastSpawn) > TimeSpan.FromMilliseconds(rnd.Next((int)frequency / 3, (int)frequency)))
            {
                enemies.Add(new Enemy(new Vector2(rnd.Next(0, 550),-50), textures["red"], Color.Crimson, textures["explosion"]));
                lastSpawn = gameTime.TotalGameTime;
            }

            foreach (Enemy en in enemies)
            {
                if (en.state == ShipState.Active)
                {
                    if (player.position.X < en.currentOrigin.X && player.position.X + player.width > en.currentOrigin.X)
                    {
                        en.equippedWeapon.Fire(gameTime, pm, null, player);
                    }

                    // check for collision with player ship
                    if (en.Collision(player))
                    {
                        en.BeginExplosion(sfx);
                        player.crashed = true;
                    }

                    // check for collision with a projectile
                    foreach (Projectile p in pm.projectiles)
                    {
                        if (en.Collision(p) && p.fromPlayer)
                        {
                            en.BeginExplosion(sfx);
                            p.isActive = false;
                            player.score++;
                            if (player.score > player.highScore) { player.highScore = player.score; }
                        }
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

        public void LoadContent(string name, Texture2D texture)
        {
            textures.Add(name, texture);
        }
    }
}
