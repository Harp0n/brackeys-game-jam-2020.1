using Assets.Logics;
using Assets.Logics.Map;
using Graphs;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapSelection : MonoBehaviour
{
    private WorldMap worldMap;
    private readonly float leftOffset = 0.1f;
    private readonly float rightOffset = 0.25f;
    private readonly float bottomOffset = 0.0f;
    private readonly float upOffset = 0.1f;

    private readonly float islandWidth = 0.2f;
    private readonly float islandHeight = 0.4f;

    [SerializeField]
    private GameObject islandPrefab;
    [SerializeField]
    private GameObject shipPrefab;
    [SerializeField]
    private GameObject linePrefab;
    private Transform mapTransform;
    private Dictionary<Tuple<int, int>, Vector2> islandsPositions;

    // Start is called before the first frame update
    void Start()
    {
        islandsPositions = new Dictionary<Tuple<int, int>, Vector2>();
        mapTransform = gameObject.transform;    
    }

    void GenerateMapGui()
    {
        islandsPositions = new Dictionary<Tuple<int, int>, Vector2>();
        foreach (Transform children in mapTransform) // remove previous islands
        {
            Destroy(children.gameObject);
        }

        List<List<Vertex>> llVertex = worldMap.llVertex;
        float width = 1.0f - (leftOffset + rightOffset);
        float height = 1.0f - (bottomOffset + upOffset);
        int layersCount = llVertex.Count;
        float layerOffset = width / (layersCount - 1);
        for (int i = 0; i < layersCount; i++)
        {
            float percentagePositionX = leftOffset + i*layerOffset;
            List<Vertex> layerIslands = llVertex[i];
            int islandsCount = layerIslands.Count;
            float islandOffset = height / (islandsCount + 1);
            for (int j = 0; j < islandsCount; j++)
            {
                float percentagePositionY = bottomOffset + (j + 1) * islandOffset;

                float positionX = percentagePositionX * Screen.width;
                float positionY = percentagePositionY * Screen.height;
                Vector2 position = new Vector2(positionX, positionY);
                islandsPositions.Add(new Tuple<int,int>(i,j),  position+ new Vector2(islandWidth, islandWidth)*100);

                Location currentLocation = layerIslands[j].Location;
                GameObject prefab;
                if (currentLocation.Equals(worldMap.CurrentLocation.Location))
                {
                    prefab = Instantiate(shipPrefab);
                }
                else
                    prefab = Instantiate(islandPrefab);
                prefab.GetComponent<RectTransform>().offsetMin = position;
                prefab.GetComponent<RectTransform>().offsetMax = position + new Vector2(Screen.width*islandWidth, Screen.height * islandHeight);
                prefab.transform.SetParent(mapTransform);
            }
        }
        foreach (Edge edge in worldMap.WorldGraph.Edges)
        {
            Tuple<int, int> indexesA = worldMap.getIndexesOfVertex(edge.Vertices.First);
            Tuple<int, int> indexesB = worldMap.getIndexesOfVertex(edge.Vertices.Last);
            CreateLine(indexesA, indexesB, edge);
        }
    }

    private void CreateLine(Tuple<int,int> indexesA, Tuple<int, int> indexesB, Edge edge)
    {
        Vector2 positionA, positionB;
        float weight = 0.8f;
        float length_scale = 0.0067f;
        if (islandsPositions.TryGetValue(indexesA,out positionA) && islandsPositions.TryGetValue(indexesB, out positionB))
        {
            Vector2 anchor = positionA * weight + positionB * (1-weight);
            float angle = Mathf.Atan2(positionB.y - positionA.y, positionB.x - positionA.x) * 180 / Mathf.PI;
            GameObject line = Instantiate(linePrefab, new Vector3(anchor.x, anchor.y, 0.0f), Quaternion.Euler(0.0f, 0.0f, angle));
            line.GetComponent<Path>().Edge = edge;
            line.transform.localScale = new Vector3(length_scale * Vector2.Distance(positionA, positionB), 1.0f, 1.0f);
            line.transform.SetParent(mapTransform);
        }
        else
        {
            Debug.Log("PROBLEM");
        }
    }

    public void Select(Edge edge)
    {
        /*
        if(worldMap.listOfPossibleEdges().Contains(edge)){ //success
            if (worldMap.CurrentLocation.Equals(edge.Vertices.First.Location))
                worldMap.MovePlayer(edge.Vertices.Last.Location);
            else
                worldMap.MovePlayer(edge.Vertices.First.Location);

            GameObject.FindObjectOfType<GameSystem>().SelectPath(edge);
        }
        */
        Location currentLocation = worldMap.CurrentLocation.Location;
        if (currentLocation.Equals(edge.Vertices.First.Location) || currentLocation.Equals(edge.Vertices.Last.Location))
        {
            Location targetLocation = currentLocation.Equals(edge.Vertices.First.Location) ? edge.Vertices.Last.Location : edge.Vertices.First.Location;
            Debug.Log(currentLocation);
            Debug.Log(targetLocation);
            if (worldMap.RouteBetween(currentLocation, targetLocation) != null)
            {
                worldMap.MovePlayer(targetLocation);
                GameObject.FindObjectOfType<GameSystem>().SelectPath(edge);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            worldMap = new WorldMap();
            worldMap.CreateMap(howBigMap: 5, minIslandsInArray: 2, maxIslandsInArray: 4, howManyConnectionsInSameArrDivider: 2);
            GenerateMapGui();
        }
    }
}
