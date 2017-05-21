using Microsoft.Xna.Framework;
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

        public Weapons()
        {
            stats = new WeaponStats[10];
        }

        public void changeWeapon(WeaponType toWeapon, Ship caller)
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

                // I really want to do this mathematically
                // CREDIT to Kris S. for making me feel like an idiot and solving this!
                for (int i = 0; i < projectileCount; i++)
                {
                    stats[i].velocity = new Vector2((100 * i) - 100, -350);
                    stats[i].rotation = (0.5f * i) - 0.5f;
                }
                fireRate = 800f;
            }

            if (caller is Enemy)
            {
                for (int i = 0; i < projectileCount; i++)
                {
                    stats[i].velocity *= -1;

                }
            }
        }

        public void Fire(GameTime gameTime, ProjectileManager pm, sfxManager sfx, Ship attacker, Ship victim)
        {
            bool fromPlayer = true;

            if (gameTime.TotalGameTime.Subtract(attacker.lastFired) > TimeSpan.FromMilliseconds(fireRate))
            {
                attacker.lastFired = gameTime.TotalGameTime;

                if (attacker is Enemy)
                {
                    texture = pm.textures[(int)Game1.Colors.Red];
                    //stats[0].velocity = new Vector2(victim.currentOrigin.X - attacker.currentOrigin.X, stats[0].velocity.Y);
                    stats[0].velocity = new Vector2(0, stats[0].velocity.Y);
                    fromPlayer = false;
                }
                else
                {
                    texture = pm.textures[(int)Game1.Colors.Blue];
                    sfx.Effect("laser1").Play();
                }

                switch (attacker.equippedWeapon.weapon)
                {
                    case WeaponType.SingleLaser:
                        pm.projectiles.Add(new Projectile(attacker.currentOrigin, texture, stats[0].velocity, fromPlayer));
                        break;
                    case WeaponType.DualLaser:
                        for (int i = 0; i < projectileCount; i++)
                        {
                            pm.projectiles.Add(new Projectile(new Vector2(attacker.position.X + (attacker.width * i), attacker.position.Y), texture, stats[i].velocity, fromPlayer));
                        }
                        break;
                    case WeaponType.SpreadShot:
                        for (int i = 0; i < projectileCount; i++)
                        {
                            pm.projectiles.Add(new Projectile(attacker.currentOrigin, texture, stats[i].velocity, stats[i].rotation, fromPlayer));
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
