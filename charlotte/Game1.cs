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

        Player player;
        List<Sprite> policeCars;
        List<Sprite> coins;
        

        GameEngine engine;
        List<Level> levels;
        //Level level;
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
            engine = new GameEngine();

            score = new Score(Content, _graphics);

            /* Levels */
            levels = new List<Level>();
            levels.Add(new Level(Content, "Maps/MapTile_0_0"));
            levels.Add(new Level(Content, "Maps/Level2"));
            engine.AddLevels(levels);

            /* Player */
            player = new Player(Content, _graphics);
            player.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            /* Set up police cars */
            policeCars = new List<Sprite>();
            coins = new List<Sprite>();
            
            for (int i = 0; i < 30; i++)
            {
                policeCars.Add(new PoliceCar(Content, _graphics));
                coins.Add(new Coin(Content, _graphics));
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            /* Load levels */
            foreach (var level in levels)
            {
                level.LoadContent();
            }

            /* Initialise score */
            score.LoadContent();
            player.LoadContent();
            foreach(var policeCar in policeCars)
            {
                policeCar.LoadContent();
            }
            foreach(var coin in coins)
            {
                coin.LoadContent();
            }

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();
            if (kstate.IsKeyDown(Keys.L))
            {
                if (engine.LevelNumber == 1) {
                    engine.SetLevel(2);
                } else
                {
                    engine.SetLevel(1);
                }   
            }

            foreach (var policeCar in policeCars)
            {
                policeCar.Update(gameTime);
                policeCar.StayWithinScreen(_graphics);
            }

            player.Update(gameTime);
            player.DetectCollision(policeCars);
            player.StayWithinScreen(_graphics);

            List<Sprite> playerList = new List<Sprite>();
            playerList.Add(player);
            foreach (var coin in coins)
            {
                
                var coinHit =  coin.DetectCollision(playerList);
                if (coinHit && !coin.Collected)
                {
                    player.Score = player.Score + 1;
                    coin.Collected = true;
                }
                coin.Update(gameTime);
            }

            score.SetScore(player.Score);
            score.SetLives(player.Lives);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            engine.Level.Draw(_spriteBatch);
            
            
            foreach(var policeCar in policeCars)
            {
                policeCar.Draw(_spriteBatch);
            }

            foreach(var coin in coins)
            {
                coin.Draw(_spriteBatch);
            }

            player.Draw(_spriteBatch);
            score.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
