using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Map;

namespace Assets.Logics.Map
{
    public class Location
    {

        public string Name { get; set; }
        public List<Place> Places { get; set; }
        public Boolean Destroyed { get; set; }

        public Location()
        {
            Name = IslandNames.GetRandomName();
        }
        public Location(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name.ToString();
        }
    }
}
