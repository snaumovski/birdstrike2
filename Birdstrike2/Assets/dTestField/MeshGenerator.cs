using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

    public GameObject start;
    public GameObject end;
//grab two points
    private float3 _start;
    private float3 _end;

    void Start( ) {
        
        
        
    }

    // Update is called once per frame
    void Update( ) {


        _start = start.transform.position;
        _end = end.transform.position;
        
        
        
    }

}