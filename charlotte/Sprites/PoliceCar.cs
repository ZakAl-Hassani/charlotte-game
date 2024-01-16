using charlotte.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charlotte.Sprites
{
    public class PoliceCar : Sprite
    {
        private float _initialSpeed = 100f;
        private float _initialRotationAngle = 0.05f;

        public PoliceCar(ContentManager content, GraphicsDeviceManager graphics) : base(content, graphics)
        {
            this.Speed = _initialSpeed;
            this.Rotation = random.Next(0, 359);
            this.RotationAngle = _initialRotationAngle;

            int randomX = random.Next(0, 1800);
            int randomY = random.Next(0, 1800);

            this._startPosition = new Vector2(randomX, randomY);
            Position = _startPosition;
        }

        public override void LoadContent()
        {
            _texture = _content.Load<Texture2D>("Cars/copcar");
            _crashTexture = _content.Load<Texture2D>("Cars/copcarcrash");
        }

        public override void Update(GameTime gameTime)
        {
            if (movesBeforeChange == 0)
            {
                movesBeforeChange = 5;

                if (turnsBeforeChange == 0)
                {
                    turnsBeforeChange = 50;

                    if (DateTime.Now.Ticks % 2 == 0)
                    {
                        RotationAngle = RotationAngle * -1;
                    }

                }
                else
                {
                    turnsBeforeChange -= 1;
                }

                Rotation += RotationAngle;
            }
            else
            {
                Position.Y -= Speed * (float)(Math.Cos(Rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.X += Speed * (float)(Math.Sin(Rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                movesBeforeChange -= 1;
            }
        }
    }
}
