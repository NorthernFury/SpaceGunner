using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class EnemyManager
    {
        public List<Enemy> enemies { get; set; }
        public Texture2D texture { get; set; }
        public float frequency = 3000f;
        public TimeSpan lastSpawn = TimeSpan.Zero;
        public Random rnd { get; set; }

        public EnemyManager()
        {
            enemies = new List<Enemy>();
            rnd = new Random();
        }

        public void Update(GameTime gameTime, Player player, ProjectileManager projectiles)
        {
            enemies.RemoveAll(e => e.isActive == false);

            if (gameTime.TotalGameTime.Subtract(lastSpawn) > TimeSpan.FromMilliseconds(rnd.Next((int)frequency / 3, (int)frequency)))
            {
                enemies.Add(new Enemy(new Vector2(rnd.Next(0, 550),-50), texture));
                lastSpawn = gameTime.TotalGameTime;
            }

            foreach (Enemy en in enemies)
            {
                if (gameTime.TotalGameTime > en.nextShot)
                {
                    projectiles.FireProjectile(gameTime, en);
                    en.nextShot = gameTime.TotalGameTime + TimeSpan.FromMilliseconds(1300f);
                }

                if ((en.position.Y + en.height > player.position.Y) &&
                    (en.position.Y < player.position.Y + player.height) &&
                    (en.position.X < player.position.X + player.width) &&
                    (en.position.X + en.width > player.position.X))
                {
                    //player.lives--;
                    en.isActive = false;
                    player.crashed = true;
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

        public void LoadContent(Texture2D tex)
        {
            texture = tex;
        }

        public void ResetEnemies()
        {
            enemies.Clear();
        }
    }
}
