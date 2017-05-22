using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace SpaceGunner
{
    public class sfxManager
    {
        private Dictionary<string, SoundEffect> bank { get; set; }

        public sfxManager()
        {
            bank = new Dictionary<string, SoundEffect>();
        }

        public SoundEffect Effect(string name)
        {
            return bank[name];
        }

        public void LoadContent(string name, SoundEffect effect)
        {
            bank.Add(name, effect);
        }
    }
}
