using Graphs;
using System;
using System.Collections.Generic;
using Unity;
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


            Vertex startingVertex = new Vertex(new Location("Starting Island"));

            CurrentLocation = startingVertex;

            llVertex = new List<List<Vertex>>();
            llVertex.Add(new List<Vertex>());
            llVertex[0].Add(startingVertex);

            WorldGraph.AddNewVertex(startingVertex);

            //Creating list of lists of Islands
            for(int i=1; i < howBigMap+1; i++)
            {
                int howManyIslands = UnityEngine.Random.Range(minIslandsInArray, maxIslandsInArray);
                llVertex.Add(new List<Vertex>());

                for (int j = 0; j < howManyIslands; j++)
                {
                    Vertex vertex = new Vertex(new Location()); 
                    
                    WorldGraph.AddNewVertex(vertex);
                    llVertex[i].Add(vertex); 
                }
            }
            
            //Connecting Islands with routes
            for(int i = 1; i <= howBigMap; i++)
            {
                int firstArrLength = llVertex[i - 1].Count;
                int secondArrLength = llVertex[i].Count;

                for(int j = 0; j < (firstArrLength > secondArrLength ? firstArrLength : secondArrLength); j++)
                {

                    Pair<Location> pair = 
                        new Pair<Location>(llVertex[i - 1][UnityEngine.Random.Range(0, firstArrLength)].Location,
                                            llVertex[i][UnityEngine.Random.Range(0, secondArrLength)].Location);

                    Edge edge = new Edge(pair);
                    edge.Route = new Route();
                    WorldGraph.AddNewEdge(edge);
                    
                }
            }

            //Connecting Islands in the same Array
            for(int i = 0; i <= howBigMap; i++)
            {
                int howManyIslandsInArray = llVertex[i].Count;

                if (howManyIslandsInArray > 1)
                {
                    for(int j=0; j < howManyIslandsInArray / howManyConnectionsInSameArrDivider; j++)
                    {

                        int randomIndex = UnityEngine.Random.Range(0, howManyIslandsInArray - 1);
                        Pair<Location> pair = new Pair<Location>(llVertex[i][randomIndex].Location, llVertex[i][randomIndex+1].Location);

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
                if (edge.Vertices.First.Location.Equals(one) && edge.Vertices.Last.Location.Equals(two)) return edge.Route;
                if (edge.Vertices.First.Location.Equals(two) && edge.Vertices.Last.Location.Equals(one)) return edge.Route;
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

        /// <summary>
        /// Return array of edges from current location of player. 
        /// </summary>
        /// <returns></returns>
        public List<Edge> listOfPossibleEdges()
        {
            List<Edge> edgeList = new List<Edge>(GraphHelper.FindAdjacentEdges(WorldGraph, CurrentLocation));
            return edgeList;
        }

        public Tuple<int, int> getIndexesOfVertex(Vertex vertex)
        {
            Tuple<int, int> result;
            for (int i = 0; i < llVertex.Count; i++)
            {
                List<Vertex> lVertex = llVertex[i];
                for (int j = 0; j < lVertex.Count; j++)
                {
                    if (lVertex[j].Location.Equals(vertex.Location))
                    {
                        result = new Tuple<int, int>(i,j);
                        return result;
                    }
                }
            }
            return new Tuple<int, int>(-1,-1);
        }

        public Vertex GetRandomVertex(int forwardRange)
        {
            int layerIndex = getIndexesOfVertex(CurrentLocation).Item1 + forwardRange;
            Vertex destination = llVertex[layerIndex][UnityEngine.Random.Range(0, llVertex[layerIndex].Count)];
            return destination;
        }
    }
}
