using charlotte.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
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
        private float _initialSpeed = 200f;
        private float _initialRotationAngle = 0.05f;
        private int _score = 0;
        private int _level = 1;
        private int _lives = 3;


        public Player(ContentManager content, GraphicsDeviceManager graphics) : base(content, graphics) {
            this.Speed = _initialSpeed;
            this.RotationAngle = _initialRotationAngle;
            this._startPosition = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            _score = 0;
        }

        public int Score
        {
            get
            {
                return _score;
            }
            set {
                _score = value;
            }
        }

        public int Level
        {
            get
            {
                return _level;
            }
        }

        public int Lives
        {
            get
            {
                return _lives;
            }
        }



        public override void LoadContent()
        {
            _texture = _content.Load<Texture2D>("Cars/car");
            _nonCrashedTexture = _texture;
            _crashTexture = _content.Load<Texture2D>("Cars/carcrash");

            base.LoadContent();
        }

        public override bool DetectCollision(List<Sprite> sprites)
        {
            var collisionDetected = base.DetectCollision(sprites);

            return collisionDetected;
        }

        public override void Update(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();

            if (!_crashed)
            {
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
            } else
            {
                /* Check for reset */
                if (kstate.IsKeyDown(Keys.Space))
                {
                    if (Speed == 0f && _lives > 1)
                    {
                        Rotation = 0f;
                        RotationAngle = _initialRotationAngle;
                        Speed = _initialSpeed;
                        _texture = _nonCrashedTexture;
                        Position = _startPosition;
                        _crashed = false;
                        _lives = _lives - 1;
                    }
                    if (_lives == 1)
                    {
                        _lives = 0;
                    }
                }
            }


            /* Keep rotation always around the 360 mark */
            if (Rotation == -360f || Rotation == 360f)
            {
                Rotation = 0f;
            }

            

            //_score = (int)gameTime.TotalGameTime.TotalSeconds;

            base.Update(gameTime);
        }
    }
}
