using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace charlotte
{
    public class Game1 : Game
    {
        // https://www.youtube.com/watch?v=UeHYtpzaJAc

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D player;
        Texture2D car_up;
        Texture2D car_down;
        Texture2D car_left;
        Texture2D car_right;
        Texture2D map;
        Vector2 position;
        Vector2 mapPosition;
        float speed = 300f;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferHeight = 2000;
            _graphics.PreferredBackBufferWidth = 2000;

            Content.RootDirectory = "Content";
            
            Window.AllowUserResizing = true;
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            mapPosition = new Vector2(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            player = Content.Load<Texture2D>("car_up");
            car_up = Content.Load<Texture2D>("car_up");
            car_down = Content.Load<Texture2D>("car_down");
            car_left = Content.Load<Texture2D>("car_left");
            car_right = Content.Load<Texture2D>("car_right");
            map = Content.Load<Texture2D>("background");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.Up))
            {
                position.Y -= speed *
               (float)gameTime.ElapsedGameTime.TotalSeconds;
                player = car_up;
            }
            if (kstate.IsKeyDown(Keys.Down))
            {
                position.Y += speed *
               (float)gameTime.ElapsedGameTime.TotalSeconds;
                player = car_down;
            }
            if (kstate.IsKeyDown(Keys.Left))
            {
                position.X -= speed *
               (float)gameTime.ElapsedGameTime.TotalSeconds;
                player = car_left;
            }
            if (kstate.IsKeyDown(Keys.Right))
            {
                position.X += speed *
               (float)gameTime.ElapsedGameTime.TotalSeconds;
                player = car_right;
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
                0f, new Vector2(player.Width / 2, player.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
