using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceGunner
{
    public class TextureManager
    {
        public Dictionary<string, AnimatedSprite> animated { get; private set; }
        public Dictionary<string, Texture2D> texture { get; private set; }

        public TextureManager()
        {
            animated = new Dictionary<string, AnimatedSprite>();
            texture = new Dictionary<string, Texture2D>();
        }

        public void AddSprite(string name, AnimatedSprite sprite)
        {
            animated.Add(name, sprite);
        }

        public void AddTexture(string name, Texture2D tex)
        {
            texture.Add(name, tex);
        } 
    }
}
