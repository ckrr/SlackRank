﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;

namespace SlackRank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<User> allUsers = UserReader.ReadUsers();
        }
    }
}
