using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlackRank
{
    class Reaction
    {
        public string name;
        public List<string> users;
        public Reaction()
        {
            name = String.Empty;
            users = new List<string>();
        }
    }
}
