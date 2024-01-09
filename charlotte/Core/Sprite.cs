using charlotte.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
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
        protected Vector2 _startPosition;
        public Vector2 Position;
        public float Speed;
        public Vector2 Velocity;
        public float Rotation;
        public float RotationAngle;
        public Input Input;

        protected int movesBeforeChange = 0;
        protected int turnsBeforeChange = 10;

        public Sprite(Texture2D texture)
        {
            _texture = texture;
            _nonCrashedText = texture;
            Rotation = 0f;
            RotationAngle = 0.05f;
            Velocity = new Vector2(0, 0);
        }

        public Sprite(Texture2D texture, Texture2D crashTexture)
        {
            _texture = texture;
            _nonCrashedText = texture;
            Rotation = 0f;
            RotationAngle = 0.05f;
            Velocity = new Vector2(0, 0);
            _crashTexture = crashTexture;
        }

        public Sprite(Texture2D texture, Texture2D crashTexture, Vector2 startPosition)
        {
            _texture = texture;
            _nonCrashedText = texture;
            Rotation = 0f;
            RotationAngle = 0.05f;
            Velocity = new Vector2(0, 0);
            _crashTexture = crashTexture;

        }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);
            }
        }



        public virtual void Update(GameTime gameTime) { }

        public virtual void Draw(SpriteBatch spriteBatch) {
            if (_texture != null)
            {
                spriteBatch.Draw(_texture, Position, null, Color.White,
                    Rotation, new Vector2(_texture.Width / 2, _texture.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            }
        }

        public virtual void DetectCollision(List<Sprite> sprites)
        {
            if (_texture != null )
            {
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

        public virtual void StayWithinScreen(GraphicsDeviceManager _graphics)
        {
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
            }
        }
    }

    
}
