using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class ProjectileManager
    {
        public enum Colors { Red, Blue, Green }
        public List<Projectile> playerProjectiles { get; set; }
        public List<Projectile> enemyProjectiles { get; set; }
        public Texture2D texture { get; set; }
        public Texture2D[] textures { get; set; }

        public ProjectileManager()
        {
            playerProjectiles = new List<Projectile>();
            enemyProjectiles = new List<Projectile>();
            textures = new Texture2D[sizeof(Colors)];
        }

        public void Update(GameTime gameTime, Player player, List<Enemy> enemies)
        {
            RemoveProjectiles(playerProjectiles);
            RemoveProjectiles(enemyProjectiles);

            foreach (Projectile proj in playerProjectiles)
            {
                //check for collision
                foreach (Enemy en in enemies)
                {
                    if (en.Collision(proj))
                    {
                        en.isAlive = false;
                        proj.isActive = false;
                        player.score++;
                        if (player.score > player.highScore) { player.highScore = player.score; }
                    }
                }

                proj.Update(gameTime);
            }

            foreach (Projectile proj in enemyProjectiles)
            {
                if (player.Collision(proj))
                {
                    proj.isActive = false;
                    player.crashed = true;
                }
                proj.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile proj in playerProjectiles) {
                proj.Draw(spriteBatch);
            }

            foreach (Projectile proj in enemyProjectiles)
            {
                proj.Draw(spriteBatch);
            }
        }

        public void LoadContent(Texture2D tex)
        {
            // just loading a basic texture for now
            // will likely want to have a list of textures loaded, for various projectile types
            texture = tex;
        }

        public void FireProjectile(GameTime gameTime, Enemy enemy)
        {
            enemyProjectiles.Add(new Projectile(new Vector2(enemy.position.X + (enemy.width /2 ), enemy.position.Y + enemy.height), textures[(int)Colors.Red], new Vector2(0, 250f)));
        }

        public void FireProjectile(GameTime gameTime, Player player)
        {
            if (player.equippedWeapon.weapon == PlayerWeapons.WeaponType.SingleLaser)
            {
                fireSingleLaser(gameTime, player);
            }
            else if (player.equippedWeapon.weapon == PlayerWeapons.WeaponType.DualLaser)
            {
                fireDualLaser(gameTime, player);
            }
        }

        public void RemoveProjectiles(List<Projectile> projectiles)
        {
            projectiles.RemoveAll(p => p.isActive == false);
        }

        private void fireSingleLaser(GameTime gameTime, Player player)
        {
            if (gameTime.TotalGameTime.Subtract(player.lastFired) > TimeSpan.FromMilliseconds(player.equippedWeapon.fireRate))
            {
                //laser.Play();
                playerProjectiles.Add(new Projectile(new Vector2(player.position.X + (player.width / 2), player.position.Y), textures[(int)Colors.Blue], player.equippedWeapon.velocity));
                //projectiles.Add(new Projectile(new Vector2(player.position.X + player.width - 10, player.position.Y), texture, velocity));
                player.lastFired = gameTime.TotalGameTime;
            }
        }

        private void fireDualLaser(GameTime gameTime, Player player)
        {
            if (gameTime.TotalGameTime.Subtract(player.lastFired) > TimeSpan.FromMilliseconds(player.equippedWeapon.fireRate))
            {
                //laser.Play();
                playerProjectiles.Add(new Projectile(new Vector2(player.position.X + 5, player.position.Y), textures[(int)Colors.Blue], player.equippedWeapon.velocity));
                playerProjectiles.Add(new Projectile(new Vector2(player.position.X + player.width - 5, player.position.Y), textures[(int)Colors.Blue], player.equippedWeapon.velocity));
                player.lastFired = gameTime.TotalGameTime;
            }
        }
    }
}
