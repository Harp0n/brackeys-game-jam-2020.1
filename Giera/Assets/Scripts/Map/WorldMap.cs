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
        public List<List<Vertex>> llVertex { get; set; }
        private int DestructionOfLocationsIterator { get; set; } = 0;

        /// <summary>
        /// Creates map for current game session
        /// </summary>
        /// <param name="howBigMap"> This means, how many Arrays of next Islands there is e.g 3 means
        ///  there will be 3 next arrays of minIslandsInArray to maxIslandsInArray </param>
        /// <param name="minIslandsInArray">Have to be more then 0. Default number: 2</param>
        /// <param name="maxIslandsInArray">Have to be more then minIslandsInArray. Default number: 5</param>
        /// <param name="howManyConnectionsInSameArrDivider">Have to be more then 1. How many connections in same array takes number 
        /// of islands in array and divides by these number, for default set: 2</param>
        public void CreateMap(int howBigMap, int minIslandsInArray, int maxIslandsInArray, int howManyConnectionsInSameArrDivider)
        {
            if (howBigMap < 0 || minIslandsInArray < 0 || maxIslandsInArray < 0 || minIslandsInArray >= maxIslandsInArray || howManyConnectionsInSameArrDivider < 0) throw new ArgumentException();

            WorldGraph = new Graph();

            Random generator = new Random();

            Vertex startingVertex = new Vertex(new Location("Starting Island"));

            CurrentLocation = startingVertex;

            llVertex = new List<List<Vertex>>();
            llVertex.Add(new List<Vertex>());
            llVertex[0].Add(startingVertex);

            WorldGraph.AddNewVertex(startingVertex);

            //Creating list of lists of Islands
            for(int i=1; i < howBigMap+1; i++)
            {
                int howManyIslands = generator.Next(minIslandsInArray, maxIslandsInArray);
                llVertex.Add(new List<Vertex>());

                for (int j = 0; j < howManyIslands; j++)
                {
                    Vertex vertex = new Vertex(new Location()); 
                    
                    WorldGraph.AddNewVertex(vertex);
                    llVertex[i].Add(vertex); 
                }
            }
            
            //Connecting Islands with routes
            for(int i = 1; i < howBigMap; i++)
            {
                int firstArrLength = llVertex[i - 1].Count;
                int secondArrLength = llVertex[i].Count;

                for(int j = 0; j < (firstArrLength > secondArrLength ? firstArrLength : secondArrLength); j++)
                {
                    //Random route creator
                    Route route = new Route();

                    Pair<Location> pair = 
                        new Pair<Location>(llVertex[i - 1][generator.Next(0, firstArrLength)].Location,
                                            llVertex[i][generator.Next(0, secondArrLength)].Location);

                    Edge edge = new Edge(pair);
                    edge.Route = new Route();
                    WorldGraph.AddNewEdge(edge);
                    
                }
            }

            //Connecting Islands in the same Array
            for(int i = 0; i < howBigMap; i++)
            {
                int howManyIslandsInArray = llVertex[i].Count;

                if (howManyIslandsInArray > 1)
                {
                    for(int j=0; j < howManyIslandsInArray / howManyConnectionsInSameArrDivider; j++)
                    {
                        //Random route creator
                        Route route = new Route();

                        Pair<Location> pair = new Pair<Location>(llVertex[i][generator.Next(0, howManyIslandsInArray)].Location, llVertex[i][generator.Next(0, howManyIslandsInArray)].Location);

                        Edge edge = new Edge(pair);
                        edge.Route = new Route();
                        WorldGraph.AddNewEdge(edge);
                    }
                }
            }
        }
        /// <summary>
        /// Move player to location, and get route that leads to it.
        /// </summary>
        /// <param name="moveToLocation"></param>
        /// <returns></returns>
        public Route MovePlayer(Location moveToLocation)
        {
            foreach(Vertex vertexIter in GraphHelper.FindAdjacentVertices(WorldGraph, CurrentLocation))
            {
                if (vertexIter.Location.Equals(moveToLocation))
                {
                    CurrentLocation = vertexIter;
                    return RouteBetween(CurrentLocation.Location, vertexIter.Location);
                }
            }

            return null;
        }

        /// <summary>
        /// Returns Route between two locations, if there is none root gives back null.
        /// </summary>
        /// <param name="one"></param>
        /// <param name="two"></param>
        /// <returns></returns>
        public Route RouteBetween(Location one, Location two)
        {
            foreach(Edge edge in WorldGraph.Edges)
            {
                if (edge.Vertices.First.Equals(one) || edge.Vertices.Last.Equals(two)) return edge.Route;
                if (edge.Vertices.First.Equals(two) || edge.Vertices.Last.Equals(one)) return edge.Route;
            }
            return null;
        }
        /// <summary>
        /// Calls next array of Locations to destroy
        /// </summary>
        public void UpdateDestroyedLocations()
        {
            if (llVertex[DestructionOfLocationsIterator] == null) throw new Exception("Null llVertex array!");
            
            foreach(Vertex vertex in llVertex[0])
            {
                WorldGraph.RemoveVertex(vertex);
            }

            llVertex[DestructionOfLocationsIterator] = null;

            DestructionOfLocationsIterator++;
        }

        /// <summary>
        /// Return array of locations from current location of player. 
        /// </summary>
        /// <returns></returns>
        public Location[] listOfPossibleLocations()
        {
            List<Location> routes = new List<Location>();

            foreach (Vertex vertex in GraphHelper.FindAdjacentVertices(WorldGraph, CurrentLocation))
            {
                routes.Add(vertex.Location);
            }
            return routes.ToArray();
        }

        /// <summary>
        /// Return array of routes from current location of player. 
        /// </summary>
        /// <returns></returns>
        public Route[] listOfPossibleRoutes()
        {
            List<Route> routes = new List<Route>();

            foreach(Edge edge in GraphHelper.FindAdjacentEdges(WorldGraph, CurrentLocation))
            {
                routes.Add(edge.Route);
            }
            return routes.ToArray();
        }
    }
}
