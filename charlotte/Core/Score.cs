using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charlotte.Core
{
    public class Score
    {
        protected int _score;
        protected int _lives;
        protected Vector2 _position;
        protected SpriteFont _font;
        protected ContentManager _content;
        protected GraphicsDeviceManager _graphics;
        private bool _gameOver = false;

        public Score(ContentManager content, GraphicsDeviceManager graphics)
        {
            _position = new Vector2(900, 100);
            _content = content;
            _graphics = graphics;
        }

        public virtual void SetScore(int score)
        {
            _score = score;
        }

        public virtual void SetLives(int lives)
        {
            _lives = lives;
            if (_lives == 0)
            {
                _gameOver = true;
            }
        }

        public virtual void LoadContent()
        {
            if (_content != null)
            {
                _font = _content.Load<SpriteFont>("Fonts/Font");
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (_gameOver)
            {
                spriteBatch.DrawString(_font, $"Score : {_score} / GAME OVER...", new Vector2(10, 10), Color.White);
            } else
            {
                spriteBatch.DrawString(_font, $"Score : {_score} / lives : {_lives}", new Vector2(10, 10), Color.White);
            }
            
        }
    }
}
