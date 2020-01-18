﻿using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public static class PathHelper {

    public static Vector3 Quantize( Vector3 v, Vector3 q ) {
        float x = q.x * Mathf.Floor( v.x / q.x );
        float y = q.x * Mathf.Floor( v.y / q.y );
        float z = q.x * Mathf.Floor( v.z / q.z );
        return new Vector3( x, y, z );
    }

    public static float3 Quantize( float3 v, float3 q ) {
        float x = q.x * Mathf.Floor( v.x / q.x );
        float y = q.x * Mathf.Floor( v.y / q.y );
        float z = q.x * Mathf.Floor( v.z / q.z );
        return new float3( x, y, z );
    }

    public static Bounds QauntizeBounds( Vector3 center, Vector3 size, float factor ) {
        return new Bounds( Quantize( center, factor * size ), size );
    }
    
    
    

}
public class MeshGenerator : MonoBehaviour {

    public int xSize;
    public int ySize;
    public GameObject start;
    public GameObject end;
    public Material meshMaterial;

    //grab two points
    private float3 _start;
    private float3 _end;
    private GameObject _mesh;
    private float3[ ] _vertices;
    private NavMeshData _navMeshData;
    private AsyncOperation _operation;
    private NavMeshDataInstance _navInstance;
    private List<NavMeshBuildSource> _navSources = new List<NavMeshBuildSource>( );

    void Start( ) {
        // build mesh object 
        _mesh = new GameObject( "mesh" );
        _mesh.AddComponent<MeshRenderer>( );
        _mesh.AddComponent<MeshFilter>( );
    }

    private void GenerateMesh( ) {
        _vertices = new float3[ ( xSize + 1 ) * ( ySize + 1 ) ];
        int i = 0;

        for ( int y = 0; y < ySize; y++ ) {
            for ( int x = 0; x < xSize; x++ ) {
                _vertices[ i ] = new float3( x, y, 0 );
                i++;
            }
        }
    }

    // Update is called once per frame
    void Update( ) {
        _start = start.transform.position;
        _end = end.transform.position;
    }

}