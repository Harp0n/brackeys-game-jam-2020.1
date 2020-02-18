using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Logics.Map
{
    public class WorldMap
    {
        public Graph WorldGraph { get; set; }
        public Vertex CurrentLocation { get; set; }
        public List<Location> PossibleDestinations { get; set; }
  
        public void CreateMap()
        {

        }

        public void UpdateDestroyedLocations()
        {

        }
    }
}
