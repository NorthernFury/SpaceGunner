using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static SpaceGunner.Game1;

namespace SpaceGunner
{
    public class ProjectileManager
    {
        public List<Projectile> playerProjectiles { get; set; }
        public List<Projectile> enemyProjectiles { get; set; }
        public List<Projectile> projectiles { get; set; }
        public Texture2D[] textures { get; set; }

        public ProjectileManager()
        {
            playerProjectiles = new List<Projectile>();
            enemyProjectiles = new List<Projectile>();
            projectiles = new List<Projectile>();
            textures = new Texture2D[sizeof(Colors)];
        }

        public void Update(GameTime gameTime, Player player, EnemyManager enemies, sfxManager sfx)
        {
            RemoveProjectiles(projectiles);

            foreach (Projectile p in projectiles)
            {
                // check for collision
                if (player.Collision(p) && !p.fromPlayer)
                {
                    p.isActive = false;
                    player.crashed = true;
                }
               
                foreach (Enemy en in enemies.enemies)
                {
                    if (en.state == ShipState.Active)
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

                p.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile p in projectiles)
            {
                p.Draw(spriteBatch);
            }
        }

        public void RemoveProjectiles(List<Projectile> projectiles)
        {
            projectiles.RemoveAll(p => p.isActive == false);
        }

        public void ResetProjectiles()
        {
            projectiles.Clear();
        }
    }
}
