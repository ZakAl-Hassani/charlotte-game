using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing.Drawing2D;

namespace charlotte
{
    public class Game1 : Game
    {
        // https://www.youtube.com/watch?v=UeHYtpzaJAc

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D player;
        Texture2D map;
        Vector2 position;
        float rotation;
        float angle;
        Vector2 mapPosition;
        
        float speed = 200f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 1800;
            _graphics.PreferredBackBufferWidth = 1800;

            Content.RootDirectory = "Content";
            
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            rotation = 0f;
            angle = 0.05f;
            mapPosition = new Vector2(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player = Content.Load<Texture2D>("car");
            map = Content.Load<Texture2D>("MapTile_0_0");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
            {
                position.Y -= speed * (float)(Math.Cos(rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.X += speed * (float)(Math.Sin(rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                position.Y += speed * (float)(Math.Cos(rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                position.X -= speed * (float)(Math.Sin(rotation)) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                rotation -= angle;
                if (rotation == -360f || rotation == 360f)
                {
                    rotation = 0f;
                }
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                rotation += angle;
                if (rotation == -360f || rotation == 360f)
                {
                    rotation = 0f;
                }
            }
            if (position.X > _graphics.PreferredBackBufferWidth -
           player.Width / 2)
            {
                position.X = _graphics.PreferredBackBufferWidth -
               player.Width / 2;
            }
            else if (position.X < player.Width / 2)
            {
                position.X = player.Width / 2;
            }
            if (position.Y > _graphics.PreferredBackBufferHeight -
           player.Height / 2)
            {
                position.Y = _graphics.PreferredBackBufferHeight -
               player.Height / 2;
            }
            else if (position.Y < player.Height / 2)
            {
                position.Y = player.Height / 2;
            }


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(map, mapPosition, null, Color.White);
            _spriteBatch.Draw(player, position, null, Color.White,
                rotation, new Vector2(player.Width / 2, player.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
