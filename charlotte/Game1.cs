using charlotte.Core;
using charlotte.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace charlotte
{
    public class Game1 : Game
    {
        // https://www.youtube.com/watch?v=UeHYtpzaJAc
        // https://www.youtube.com/watch?v=MMWbDFzst58

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Texture2D playerTexture;
        Texture2D playerTexture_crash;

        Player player;
        List<Sprite> sprites;

        Texture2D copcar;
        Level level;
        Score score;


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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("car");
            playerTexture_crash = Content.Load<Texture2D>("carcrash");
            copcar = Content.Load<Texture2D>("copcar");


            /* Load levels */
            List<Texture2D> levels = new List<Texture2D>();
            levels.Add(Content.Load<Texture2D>("MapTile_0_0"));
            levels.Add(Content.Load<Texture2D>("Level2"));
            level = new Level(levels);

            /* Initialise score */
            score = new Score(Content.Load<Texture2D>("ScoreList"));

            player = new Player(playerTexture, playerTexture_crash, new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2));
            player.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            /* Set up police cars */
            sprites = new List<Sprite>();

            Random random = new Random();
            
            for (int i = 0; i < 30; i++)
            {
                int randomX = random.Next(0, 1800);
                int randomY = random.Next(0, 1800);

                var sprite = new PoliceCar(copcar, copcar, new Vector2(randomX, randomY));
                sprite.Rotation = 0f;
                sprite.Speed = 100f;
                sprite.Position = new Vector2(randomX, randomY);
                sprites.Add(sprite);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.L))
            {
                if (level.LevelNumber == 1) {
                    level.SetLevel(2);
                } else
                {
                    level.SetLevel(1);
                }
                    
            }

            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
                sprite.StayWithinScreen(_graphics);
            }
            player.Update(gameTime);
            player.DetectCollision(sprites);
            player.StayWithinScreen(_graphics);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            level.Draw(_spriteBatch);
            
            
            foreach(var sprite in sprites)
            {
                sprite.Draw(_spriteBatch);
            }
            player.Draw(_spriteBatch);
            score.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
