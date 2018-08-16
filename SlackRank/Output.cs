using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SlackRank
{
    class Output
    {
        public static void WriteTupleListToFile(List<Tuple<double,string>> tupleList, string path)
        {
            string allFileText = ComposeFileText(tupleList);
            File.WriteAllText(path, allFileText);
        }

        private static string ComposeFileText(List<Tuple<double, string>> tupleList)
        {
            int numUsers = tupleList.Count;
            tupleList.Sort((x, y) => y.Item1.CompareTo(x.Item1));
            string allFileText = String.Empty;
            for (int i = 0; i < numUsers; i++)
            {
                string writeName = tupleList[i].Item2 + ",";
                string writePercentage = tupleList[i].Item1.ToString();
                allFileText += (writeName + writePercentage + Environment.NewLine);
            }
            return allFileText;
        }
    }
}
