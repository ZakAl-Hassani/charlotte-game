using charlotte.Core;
using Microsoft.Xna.Framework;
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
        public PoliceCar(Texture2D texture, Texture2D crashTexture, Vector2 startPosition) : base(texture, crashTexture, startPosition)
        {
            
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
