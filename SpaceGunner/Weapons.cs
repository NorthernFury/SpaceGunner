using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceGunner
{
    public class Weapons
    {
        public enum WeaponType { SingleLaser, DualLaser, SpreadShot };
        public WeaponType weapon { get; set; }
        public float fireRate { get; set; }
        public Texture2D texture { get; set; }
        public string name { get; private set; }

        private struct WeaponStats
        {
            public Vector2 velocity;
            public float rotation;
        }
        private int projectileCount { get; set; }
        private WeaponStats[] stats { get; set; }
        private Ship parent { get; set; }

        public Weapons(Ship parent)
        {
            stats = new WeaponStats[10];
            this.parent = parent;
        }

        public void changeWeapon(WeaponType toWeapon)
        {
            if (toWeapon == WeaponType.SingleLaser)
            {
                projectileCount = 1;
                weapon = toWeapon;
                name = "Single Laser";

                for (int i=0; i < projectileCount; i++)
                {
                    stats[i].velocity = new Vector2(0, -350);
                    stats[i].rotation = 0f;
                }
                fireRate = 800f;
            }
            else if (toWeapon == WeaponType.DualLaser)
            {
                projectileCount = 2;
                weapon = toWeapon;
                name = "Dual Laser";
                for (int i = 0; i < projectileCount; i++)
                {
                    stats[i].velocity = new Vector2(0, -350);
                    stats[i].rotation = 0f;
                }
                fireRate = 800f;
            }
            else if (toWeapon == WeaponType.SpreadShot)
            {
                projectileCount = 3;
                weapon = toWeapon;
                name = "Spread Laser";

                for (int i = 0; i < projectileCount; i++)
                {
                    stats[i].velocity = new Vector2((100 * i) - 100, -350);
                    stats[i].rotation = (0.5f * i) - 0.5f;
                }
                fireRate = 800f;
            }

            if (parent is Enemy)
            {
                for (int i = 0; i < projectileCount; i++)
                {
                    stats[i].velocity *= -1;

                }
            }
        }

        public void Fire(GameTime gameTime, ProjectileManager pm, SoundEffect sfx, Ship victim)
        {
            bool fromPlayer = true;

            if (gameTime.TotalGameTime.Subtract(parent.lastFired) > TimeSpan.FromMilliseconds(fireRate))
            {
                parent.lastFired = gameTime.TotalGameTime;

                if (parent is Enemy)
                {
                    texture = pm.textures["red"];
                    // below will fire bullet toward player's current position when entering firing "cone"
                    //stats[0].velocity = new Vector2(victim.currentOrigin.X - attacker.currentOrigin.X, stats[0].velocity.Y);
                    stats[0].velocity = new Vector2(0, stats[0].velocity.Y);
                    fromPlayer = false;
                }
                else
                {
                    texture = pm.textures["blue"];
                }

                switch (weapon)
                {
                    case WeaponType.SingleLaser:
                        pm.projectiles.Add(new Projectile(parent.currentOrigin, texture, stats[0].velocity, fromPlayer));
                        break;
                    case WeaponType.DualLaser:
                        for (int i = 0; i < projectileCount; i++)
                        {
                            pm.projectiles.Add(new Projectile(new Vector2(parent.position.X + (parent.width * i), parent.position.Y), texture, stats[i].velocity, fromPlayer));
                        }
                        break;
                    case WeaponType.SpreadShot:
                        for (int i = 0; i < projectileCount; i++)
                        {
                            pm.projectiles.Add(new Projectile(parent.currentOrigin, texture, stats[i].velocity, stats[i].rotation, fromPlayer));
                        }
                        break;
                    default:
                        break;
                }

                if (sfx != null)
                {
                    sfx.Play();
                }
            }
        }
    }
}
