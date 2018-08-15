using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlackRank
{
    class Reply
    {
        public string user;
        public string thread_ts;
        public Reply()
        {
            user = String.Empty;
            thread_ts = String.Empty;
        }
    }
}
