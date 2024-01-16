using charlotte.Core;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace charlotte.Sprites
{
    public class Coin : Sprite
    {
        protected bool _collected = false;
        public Coin(ContentManager content, GraphicsDeviceManager graphics) : base(content, graphics)
        {
            this.Speed = 0f;
            this.Rotation = 0f;
            this.RotationAngle = 0f;

            int randomX = random.Next(0, 1800);
            int randomY = random.Next(0, 1800);

            this._startPosition = new Vector2(randomX, randomY);
            Position = _startPosition;
        }

        public override bool Collected {
            get
            {
                return _collected;
            }
            set
            {
                if (value == true)
                {
                    _texture = _crashTexture;
                } else
                {
                    _texture = _nonCrashedTexture;
                }
                _collected = value;
            }
        }

        public override void LoadContent()
        {
            _texture = _content.Load<Texture2D>("Other/coin");
            _crashTexture = _content.Load<Texture2D>("Other/coincrash");
        }

    }
}
