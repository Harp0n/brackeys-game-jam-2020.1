using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Map
{
    public class Location
    {
        public string Name { get; set; }
        public List<Place> MyProperty { get; set; }
        public Boolean Destroyed { get; set; }
    }
}
