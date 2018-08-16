using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlackRank
{
    class Message
    {
        public string type;
        public string subtype;
        public string user;
        public string text;
        public string ts;
        public List<Reply> replies;
        public List<Reaction> reactions;
        public Message()
        {
            type = String.Empty;
            subtype = String.Empty;
            user = String.Empty;
            text = String.Empty;
            ts = String.Empty;
            replies = new List<Reply>();
            reactions = new List<Reaction>();
        }
    }
}
