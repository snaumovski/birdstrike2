using Unity.Mathematics;
using UnityEngine;
using Object = UnityEngine.Object;

public static class PathHelper {

    public enum Parts {

        Wall,
        Floor,
        Furniture,
        Poi

    }
    private static string[ ] _values = {"walls", "floors", "furniture"};

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

    public static GameObject ImportRoom( this GameObject room ) {
        GameObject output = Object.Instantiate( room );

        // search through first anchor 
        foreach ( Transform c1 in output.transform ) {
            // search through each  layer
            foreach ( Transform c2 in c1 ) {
                // search through each object in that layer 
                foreach ( Transform c3 in c2 ) {
                    foreach ( var v in _values ) {
                        // assign tag 
                        if ( !c3.name.ToLower( ).Contains( v.ToLower( ) ) ) continue;

                        c3.gameObject.tag = v;
                    }
                }
            }
        }
        return output;
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