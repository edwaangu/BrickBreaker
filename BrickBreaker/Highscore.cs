using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickBreaker
{
    public class Highscore
    {
        public int score;
        public string name;

        public Highscore(int _score, string _name)
        {
            score = _score;
            name = _name;
        }
    }
}
