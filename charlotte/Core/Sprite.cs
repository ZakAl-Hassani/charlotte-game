using charlotte.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace charlotte.Core
{
    public class Sprite
    {
        protected Texture2D _texture;
        protected Texture2D _nonCrashedText;
        protected Texture2D _crashTexture;
        public Vector2 Position;
        public float Speed;
        public float Rotation;
        public float RotationAngle;
        public Input Input;

        protected int movesBeforeChange = 3;
        protected int turnsBeforeChange = 50;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            Rotation = 0f;
            RotationAngle = 0.05f;
        }

        public Sprite(Texture2D texture, Texture2D crashTexture)
        {
            _texture = texture;
            _nonCrashedText = texture;
            Rotation = 0f;
            RotationAngle = 0.05f;
            _crashTexture = crashTexture;
        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }


        public virtual void Update(GameTime gameTime, GraphicsDeviceManager _graphics)
        {
            if (movesBeforeChange == 0)
            {
                movesBeforeChange = 3;

                if (turnsBeforeChange == 0)
                {
                    turnsBeforeChange = 50;
                   
                    if (DateTime.Now.Ticks % 2 == 0)
                    {
                        RotationAngle = RotationAngle * -1;
                    }

                } else
                {
                    turnsBeforeChange -= 1;
                }

                Rotation += RotationAngle;
            } else
            {
                Position.Y -= Speed * (float)(Math.Cos(Rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                Position.X += Speed * (float)(Math.Sin(Rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                movesBeforeChange -= 1;
            }
        }

        public virtual void Update(GameTime gameTime, KeyboardState kstate, GraphicsDeviceManager _graphics, List<Sprite> sprites) {

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

            /* This is where I reset the car once it has crashed and the space bar is then pressed */
            if (kstate.IsKeyDown(Keys.Space))
            {
                if (Speed == 0f)
                {
                    Rotation = 0f;
                    RotationAngle = 0.05f;
                    Speed = 200f;
                    _texture = _nonCrashedText;
                    Position.X = _graphics.PreferredBackBufferWidth / 2;
                    Position.Y = _graphics.PreferredBackBufferHeight / 2;
                }
            }

            if (_texture != null)
            {
                if (Position.X > _graphics.PreferredBackBufferWidth - _texture.Width / 2)
                {
                    Position.X = _graphics.PreferredBackBufferWidth - _texture.Width / 2;
                }
                else if (Position.X < _texture.Width / 2)
                {
                    Position.X = _texture.Width / 2;
                }
                if (Position.Y > _graphics.PreferredBackBufferHeight - _texture.Height / 2)
                {
                    Position.Y = _graphics.PreferredBackBufferHeight - _texture.Height / 2;
                }
                else if (Position.Y < _texture.Height / 2)
                {
                    Position.Y = _texture.Height / 2;
                }


                foreach (var sprite in sprites)
                {
                    if ((Position.Y - _texture.Height / 2f < sprite.Position.Y + sprite.Rectangle.Height / 2f)
                        && (Position.Y + _texture.Height / 2f > sprite.Position.Y - sprite.Rectangle.Height / 2f)
                        && (Position.X - _texture.Width / 2f < sprite.Position.X + sprite.Rectangle.Width / 2f)
                        && (Position.X + _texture.Width / 2f > sprite.Position.X - sprite.Rectangle.Width / 2f)
                        )
                    {
                        _texture = _crashTexture;
                        Speed = 0;
                        RotationAngle = 0;
                    }
                }
            }
            


        }

        public virtual void Draw(SpriteBatch spriteBatch) {
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, Position, null, Color.White,
                    Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            }
        }
    }

    
}
