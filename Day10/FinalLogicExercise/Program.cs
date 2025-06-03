using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Rules rules = new Rules();
            rules.AddRules(11, "wow");
            rules.AddRules(13, "bob");
            rules.Print(100);
        }

    }

    class Rules
    {
        public Dictionary<int, string> rules;
        public Rules()
        {
            rules = new Dictionary<int, string>
            {
                { 3, "foo"},
                { 4, "baz"},
                { 5, "bar"},
                { 9, "huzz"},
                { 7, "jazz"}
            };
        }
        public void Print(int num)
        {
            for (int i = 1; i <= num; i++)
            {
                string output = "";
                List<int> sortedKeys = new (rules.Keys);
                sortedKeys.Sort();
                foreach (int key in sortedKeys)
                {
                    if (i % key == 0)
                    {
                        output += rules[key];
                    }

                }
                Console.Write((output == "" ? i.ToString() : output) + ", ");
            }
        }

        public void AddRules(int input, string output)
        {
            rules.Add(input, output);
        }
}
}