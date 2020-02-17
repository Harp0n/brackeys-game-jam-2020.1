using Assets.Logics;
using Assets.Logics.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    private List<Hole> HolesMast { get; set; }
    private List<ISystem> Systems { get; set; }
    private List<Hole> HolesHull { get; set; }
    private bool IsDrown { get; set; }
    private float WaterOnBoard { get; set; }
    private float Speed { get; set; }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
