using charlotte.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charlotte.Sprites
{
    public class Player : Sprite
    {
        public Player(Texture2D texture, Texture2D crashTexture) : base(texture, crashTexture)
        {
            this.Speed = 200f;
        }

        public override void Update(GameTime gameTime, KeyboardState kstate, GraphicsDeviceManager _graphics, List<Sprite> sprites)
        {
            base.Update(gameTime, kstate, _graphics, sprites);
        }
    }
}
