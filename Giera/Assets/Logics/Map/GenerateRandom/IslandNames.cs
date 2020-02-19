using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tests.Map
{
    public static class IslandNames
    {
        private static string path = Directory.GetCurrentDirectory() + "/locationNames"; // lista imion fantasy (może nie działać trzeba sprawdzić)

        private static List<String> Names;

        public static string GetRandomName()
        {
            if(Names == null)
            {
                Names = new List<String>();
                using (StreamReader sr = File.OpenText(path))
                {
                    String s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Names.Add(s);
                    }
                }
            }

            Random r = new Random();

            return Names[r.Next(0, Names.Count)];
        }
    }
}
