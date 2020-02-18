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

            Vertex startingVertex = new Vertex(new Location("Start"));

            CurrentLocation = startingVertex;

            llVertex = new List<List<Vertex>>();
            llVertex.Add(new List<Vertex>());
            llVertex[0].Add(startingVertex);

            WorldGraph.AddNewVertex(startingVertex);

            //Creating list of lists of Islands
            for(int i=0; i < howBigMap; i++)
            {
                int howManyIslands = generator.Next(minIslandsInArray, maxIslandsInArray);
                llVertex.Add(new List<Vertex>());

                for (int j = 0; j < howManyIslands; j++)
                {
                    Vertex vertex = new Vertex(new Location("Tests"));// Change location to random location generator
                    
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

                    WorldGraph.AddNewEdge(pair);
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

                        WorldGraph.AddNewEdge(pair);
                    }
                }
            }
        }

        public void UpdateDestroyedLocations()
        {
                
        }
    }
}
