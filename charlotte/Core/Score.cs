using Microsoft.Xna.Framework;
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
        protected Texture2D _score;
        protected Vector2 _position;

        public Score(Texture2D score)
        {
            _score = score;
            _position = new Vector2(900, 100);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_score, _position, null, Color.White);
        }
    }
}
