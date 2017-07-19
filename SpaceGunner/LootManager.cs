using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SpaceGunner
{
    public class LootManager
    {
        public List<PowerUp> powerUpTable { get; set; }
        public List<ActivePowerUp> activePowerUps { get; set; }

        private Random rng { get; set; }

        public LootManager()
        {
            powerUpTable = new List<PowerUp>();
            activePowerUps = new List<ActivePowerUp>();
            rng = new Random();
        }

        public void Update(GameTime gameTime, Player player)
        {
            foreach(ActivePowerUp p in activePowerUps)
            {
                p.Update(gameTime);

                // we'll probably want to check if a player has collected it here
                // then add the effect to the player?
                if (p.Collision(player))
                {
                    p.isActive = false;
                    p.powerUp.Collect(player);
                }
            }

            //remove inactive powerups
            activePowerUps.RemoveAll(p => p.isActive == false);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(ActivePowerUp p in activePowerUps)
            {
                p.sprite.Draw(spriteBatch);
            }
        }

        // Add item to the list of available powerups
        public void AddItem(PowerUp powerUp)
        {
            powerUpTable.Add(powerUp);
        }

        public void CalculateDrop(Enemy enemy)
        {
            // roll a random drop based on the enemy's loot chance
            // roll a random item based on rarity
            // if successful add item to the active list
            if (rng.NextDouble() < enemy.itemDropRate)
            {
                PowerUp powerUpRef = powerUpTable.OfType<WeaponPowerUp>().FirstOrDefault(w => w.type == Weapons.WeaponType.DualLaser);
                ActivePowerUp activePowerup = new ActivePowerUp(powerUpRef, enemy.currentOrigin, 3000f);

                activePowerup.sprite.Start(activePowerup.position);

                activePowerUps.Add(activePowerup);
            }

            // as a test, lets make every enemy drop a power up!
            // create a new instance of loot
            /*
            PowerUp powerUpRef = powerUpTable.OfType<WeaponPowerUp>().FirstOrDefault(w => w.type == Weapons.WeaponType.DualLaser);
            ActivePowerUp activePowerup = new ActivePowerUp(powerUpRef, enemy.currentOrigin, 3000f);
            
            activePowerup.sprite.Start(activePowerup.position);

            activePowerUps.Add(activePowerup);
            */
        }
    }
}
