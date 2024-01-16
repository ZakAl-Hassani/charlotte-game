using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace charlotte.Core
{
    public class GameEngine
    {
        protected List<Level> _levels;
        protected int _level;

        public GameEngine() {
            _levels = new List<Level>();
        }

        public virtual void AddLevels(List<Level> levels)
        {
            _levels = levels;
            _level = 1;
        }

        public virtual void SetLevel(int level)
        {
            _level = level;
        }

        public Level Level 
        { 
            get 
            {
                return _levels[_level - 1];
            } 
        }

        public int LevelNumber
        {
            get
            {
                return _level;
            }
        }
    }
}
