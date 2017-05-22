using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace SpaceGunner
{
    public class ProjectileManager
    {
        public List<Projectile> playerProjectiles { get; set; }
        public List<Projectile> enemyProjectiles { get; set; }
        public List<Projectile> projectiles { get; set; }
        public Dictionary<string, Texture2D> textures { get; set; }

        public ProjectileManager()
        {
            playerProjectiles = new List<Projectile>();
            enemyProjectiles = new List<Projectile>();
            projectiles = new List<Projectile>();
            textures = new Dictionary<string, Texture2D>();
        }

        public void Update(GameTime gameTime, Player player)
        {
            foreach (Projectile p in projectiles)
            {
                // check for collision
                if (player.Collision(p) && !p.fromPlayer)
                {
                    p.isActive = false;
                    player.crashed = true;
                }
               
                p.Update(gameTime);
            }

            RemoveProjectiles(projectiles);
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

        public void LoadContent(string name, Texture2D texture)
        {
            textures.Add(name, texture);
        }
    }
}
