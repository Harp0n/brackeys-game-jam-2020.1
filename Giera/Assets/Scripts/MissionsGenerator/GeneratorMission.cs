using Assets.Logics.Map;
using Graphs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.MissionsGenerator
{
    class GeneratorMission
    {
        private readonly float PERCENTAGE_OF_PRICE_PER_UNIT = 0.1f;
        /// <summary>
        /// Returns mission randomly generated on current worldMap, from current location. Sets mission destination
        /// and price per unit depending of routes hardness. Quantity is not set cuz its should be available on 
        /// graphic menu to get as much quantity as player wants.
        /// </summary>
        /// <param name="worldMap"></param>
        /// <param name="currentLocation"></param>
        /// <returns></returns>
        public Mission GenerateMission(WorldMap worldMap, Vertex currentLocation)
        {
            Mission mission = new Mission();

            Vertex destination = worldMap.WorldGraph.Vertices[UnityEngine.Random.Range(0, worldMap.WorldGraph.Vertices.Length)];

            mission.Cargo.Destination = destination.Location;

            Vertex[] locations = DijkstraSearch.Search(worldMap.WorldGraph, currentLocation, destination);

            List<Route> routes = new List<Route>();

            for(int i=0; i < locations.Length-1; i++)
            {
                routes.Add(worldMap.RouteBetween(locations[i].Location, locations[i + 1].Location));
            }

            int hardnessModifier = 0;

            foreach(Route route in routes)
            {
                hardnessModifier += route.HowHard + route.HowLong;
            }

            mission.Cargo.PricePerUnit = hardnessModifier * PERCENTAGE_OF_PRICE_PER_UNIT;

            return mission;
        }
    }
}
