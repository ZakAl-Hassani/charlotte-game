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
        Vector2 copcar_position;
        float copcar_rotation;
        Texture2D map;
        //Vector2 position;
        //float rotation;
        //float angle;
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

            //= new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);
            //rotation = 0f;
            //angle = 0.05f;


            mapPosition = new Vector2(0, 0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            playerTexture = Content.Load<Texture2D>("car");
            playerTexture_crash = Content.Load<Texture2D>("carcrash");
            copcar = Content.Load<Texture2D>("copcar");
            map = Content.Load<Texture2D>("MapTile_0_0");

            sprites = new List<Sprite>();
            sprites.Add(new Sprite(copcar));

            player = new Player(playerTexture, playerTexture_crash);
            player.Position = new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2);

            Sprite copcarSprite = sprites[0];
            copcarSprite.Rotation = 0f;
            copcarSprite.Position = new Vector2(_graphics.PreferredBackBufferWidth / 4, _graphics.PreferredBackBufferHeight / 4);
            sprites[0] = copcarSprite;

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            var kstate = Keyboard.GetState();
            foreach (var sprite in sprites)
            {
                sprite.Update(gameTime);
            }
            player.Update(gameTime, kstate, _graphics, sprites);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(map, mapPosition, null, Color.White);
            
            foreach(var sprite in sprites)
            {
                sprite.Draw(_spriteBatch);
            }
            player.Draw(_spriteBatch);

            //_spriteBatch.Draw(copcar, copcar_position, null, Color.White,
            //    copcar_rotation, new Vector2(copcar.Width / 2, copcar.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            //_spriteBatch.Draw(player, position, null, Color.White,
            //    rotation, new Vector2(player.Width / 2, player.Height / 2), Vector2.One, SpriteEffects.None, 0f);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
