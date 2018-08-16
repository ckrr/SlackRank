using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlackRank
{
    class User
    {
        public string id;
        public string name;
        public Profile profile;
        public User()
        {
            id = String.Empty;
            name = String.Empty;
            profile = new Profile();
        }
    }
}
