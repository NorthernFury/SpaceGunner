using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace SpaceGunner
{
    class Starfield
    {
        public List<Star> field { get; set; }
        public Texture2D texture { get; set; }
        private int maxStars = 150;
        private Random rnd { get; set; }

        public Starfield()
        {
            field = new List<Star>();
            rnd = new Random();
        }

        public void LoadContent(GraphicsDevice graphicsDevice)
        {
            // create the texture our stars will use
            // define screen bounds for the field
            texture = new Texture2D(graphicsDevice, 1, 1);
            texture.SetData(new Color[] { Color.White });

            // fill screen with initial starfield
            InitializeField();
        }

        public void Update(GameTime gameTime)
        {
            // clean up any dead stars (don't run this on our universe tho!!!!)
            field.RemoveAll(star => star.isActive == false);

            // Check if there are 100 stars in the field
            // Generate a new star at the top of the screen at a random point and random depth
            // iterate through the star list, moving down based on velocity

            if (field.Count < maxStars)
            {
                field.Add(new Star(rnd.Next(0, Game1.PLAYAREAX), (float)rnd.Next(200, 400), texture, rnd.Next(3)));
            }

            foreach (Star star in field)
            {
                star.Update(gameTime);
            }            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // iterate through the field and call the draw for each star
            foreach (Star star in field)
            {
                star.Draw(spriteBatch);
            }
        }

        private void InitializeField()
        {
            for (int i = 0; i < maxStars; i++)
            {
                field.Add(new Star(rnd.Next(0, Game1.PLAYAREAX), rnd.Next(0, Game1.PLAYAREAY), (float)rnd.Next(150, 250), texture, rnd.Next(3)));
            }
        }

        public void ResetField()
        {
            field.Clear();
            InitializeField();
        }
    }
}
