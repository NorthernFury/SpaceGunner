using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class ActivePowerUp
    {
        public Vector2 position { get; set; }
        public float lifetime { get; set; }
        public bool isActive { get; set; }
        public bool isCollected { get; set; }
        public PowerUp powerUp { get; set; }
        public AnimatedSprite sprite { get; set; }

        public ActivePowerUp(PowerUp powerUpRef, Vector2 pos, float timeAlive)
        {
            position = pos;
            lifetime = timeAlive;
            isActive = true;
            isCollected = false;
            powerUp = powerUpRef;

            sprite = powerUpRef.sprite.Clone();
        }

        public void Update(GameTime gameTime)
        {
            sprite.Update(gameTime);
        }

        public bool Collision(Player player)
        {
            if ((position.Y + sprite.frameHeight > player.position.Y) &&
                (position.Y < player.position.Y + player.height) &&
                (position.X < player.position.X + player.width) &&
                (position.X + sprite.frameWidth > player.position.X) &&
                isActive == true)
            {
                return true;
            }

            return false;
        }
    }
}