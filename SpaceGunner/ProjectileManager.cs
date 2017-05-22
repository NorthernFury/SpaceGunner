using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using static SpaceGunner.Ship;

namespace SpaceGunner
{
    public class ProjectileManager
    {
        public List<Projectile> projectiles { get; set; }
        public Dictionary<string, Texture2D> textures { get; set; }

        public ProjectileManager()
        {
            projectiles = new List<Projectile>();
            textures = new Dictionary<string, Texture2D>();
        }

        public void Update(GameTime gameTime, Player player, sfxManager sfx)
        {
            foreach (Projectile p in projectiles)
            {
                if (!p.fromPlayer) // Only do a collision check on enemy projectiles
                {
                    if (player.Collision(p))
                    {
                        p.isActive = false;
                        player.BeginExplosion(sfx.Effect("explosion"));
                    }
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
