using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charlotte.Core
{
    public class Level
    {
        protected Texture2D _map;
        protected Vector2 _position;
        protected List<Texture2D> _levels = new List<Texture2D>();
        public int LevelNumber = 1;

        public Level(Texture2D map)
        {
            _map = map;
            _position = new Vector2(0, 0);
        }

        public Level(List<Texture2D> levelMaps)
        {
            if (levelMaps.Count >= 0)
            {
                _map = levelMaps[0];
                _levels = levelMaps;
            }

            _position = new Vector2(0, 0);
        }

        public virtual void SetLevel(int levelNumber)
        {
            _map = _levels[levelNumber - 1];
            LevelNumber = levelNumber;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_map, _position, null, Color.White);
        }
    }
}
