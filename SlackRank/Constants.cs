using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SlackRank
{
    class Constants
    {
        public static string PATH = "C:\\users\\crech\\Desktop\\SlackData";
        public static string USERS_PATH = "C:\\users\\crech\\Desktop\\SlackData\\users.json";
        public static string USERS_BY_MESSAGE_PERCENTAGE_PATH = "../../../UsersByMessagePercentage.csv";
        public static string RAW_ACTION_RANK_PATH = "../../../RawActionRank.csv";
        public static string UNWEIGHTED_PAGE_RANK_PATH = "../../../UnweightedPageRank.csv";
        public static string WEIGHTED_PAGE_RANK_PATH = "../../../WeightedPageRank.csv";

        public static int REPLY_WEIGHT_FACTOR = 0;
        public static double DAMPING_FACTOR = 0.85;
        public static double MESSAGE_BASELINE = 10;
    }
}
