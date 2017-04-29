using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    class ProjectileManager
    {
        public List<Projectile> projectiles { get; set; }
        public TimeSpan lastFired { get; set; }
        public Texture2D texture { get; set; }
        public float fireRate { get; set; }

        public ProjectileManager()
        {
            projectiles = new List<Projectile>();
            lastFired = TimeSpan.Zero;
            fireRate = 400f;
        }

        public void Update(GameTime gameTime, KeyboardState keyState, Player player, List<Enemy> enemies, SoundEffect laser)
        {
            if (keyState.IsKeyDown(Keys.Space))
            {
                if (player.equippedWeapon.weapon == PlayerWeapons.WeaponType.SingleLaser)
                {
                    fireSingleLaser(gameTime, player, laser);
                }
                else if (player.equippedWeapon.weapon == PlayerWeapons.WeaponType.DualLaser)
                {
                    fireDualLaser(gameTime, player, laser);
                }
            }

            projectiles.RemoveAll(p => p.isActive == false);

            foreach (Projectile proj in projectiles) {
                proj.Update(gameTime);

                //check for collision
                foreach (Enemy en in enemies)
                {
                    if ((en.position.Y + en.height > proj.position.Y) &&
                        (en.position.Y < proj.position.Y) &&
                        (en.position.X < proj.position.X + proj.width) &&
                        (en.position.X + en.width > proj.position.X))
                    {
                        en.isActive = false;
                        proj.isActive = false;
                        player.score++;
                        if (player.score > player.highScore) { player.highScore = player.score; }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Projectile proj in projectiles) {
                proj.Draw(spriteBatch);
            }
        }

        public void LoadContent(Texture2D tex)
        {
            // just loading a basic texture for now
            // will likely want to have a list of textures loaded, for various projectile types
            texture = tex;
        }

        private void fireSingleLaser(GameTime gameTime, Player player, SoundEffect laser)
        {
            if (gameTime.TotalGameTime.Subtract(lastFired) > TimeSpan.FromMilliseconds(player.equippedWeapon.fireRate))
            {
                laser.Play();
                projectiles.Add(new Projectile(new Vector2(player.position.X + (player.width / 2), player.position.Y), texture, player.equippedWeapon.velocity));
                //projectiles.Add(new Projectile(new Vector2(player.position.X + player.width - 10, player.position.Y), texture, velocity));
                lastFired = gameTime.TotalGameTime;
            }
        }

        private void fireDualLaser(GameTime gameTime, Player player, SoundEffect laser)
        {
            if (gameTime.TotalGameTime.Subtract(lastFired) > TimeSpan.FromMilliseconds(player.equippedWeapon.fireRate))
            {
                laser.Play();
                projectiles.Add(new Projectile(new Vector2(player.position.X + 5, player.position.Y), texture, player.equippedWeapon.velocity));
                projectiles.Add(new Projectile(new Vector2(player.position.X + player.width - 5, player.position.Y), texture, player.equippedWeapon.velocity));
                lastFired = gameTime.TotalGameTime;
            }
        }
    }
}
