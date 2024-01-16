using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charlotte.Core
{
    public class Level
    {
        protected ContentManager _content;
        protected string _mapName;
        protected Texture2D _map;
        protected Vector2 _position;
        public int LevelNumber = 1;


        public Level(ContentManager content, string mapName)
        {
            _mapName = mapName;
            _position = new Vector2(0, 0);
            _content = content;
        }

        public Level(Texture2D map)
        {
            _map = map;
            _position = new Vector2(0, 0);
        }

        public virtual void LoadContent()
        {
            _map = _content.Load<Texture2D>(_mapName);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_map, _position, null, Color.White);
        }
    }
}
