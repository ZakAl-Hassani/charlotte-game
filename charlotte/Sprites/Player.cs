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
        public Player(Texture2D texture, Texture2D crashTexture, Vector2 startPosition) : base(texture, crashTexture)
        {
            this.Speed = 200f;
            this._startPosition = startPosition;
        }

        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                Position.Y -= Speed * (float)(Math.Cos(Rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.X += Speed * (float)(Math.Sin(Rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                Position.Y += Speed * (float)(Math.Cos(Rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.X -= Speed * (float)(Math.Sin(Rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                Rotation -= RotationAngle;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                Rotation += RotationAngle;
            }

            /* Keep rotation always around the 360 mark */
            if (Rotation == -360f || Rotation == 360f)
            {
                Rotation = 0f;
            }

            /* Check for reset */
            if (kstate.IsKeyDown(Keys.Space))
            {
                if (Speed == 0f)
                {
                    Rotation = 0f;
                    RotationAngle = 0.05f;
                    Speed = 200f;
                    _texture = _nonCrashedText;
                    Position = _startPosition;
                }
            }

            base.Update(gameTime);
        }
    }
}
