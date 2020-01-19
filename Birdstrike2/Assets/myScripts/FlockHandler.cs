using System.Collections.Generic;
using dTestField;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;

public class FlockHandler : MonoBehaviour {

    public static FlockHandler Instance { get; set; }
    public bool createScene = false;
    public bool runSim = false;
    public int agentCount;
    public VuforiaMonoBehaviour tempTracker;
    public GameObject spawnScene;
    public GameObject spawnLocation;
    public Vector2 ratio = new Vector2( 1, 2 );
    public GameObject blueObj;
    public GameObject redObj;
    public GameObject blueStart;
    public GameObject redStart;
    public GameObject blueEnd;
    public GameObject redEnd;
    private List<GameObject> _redAgents;
    private List<GameObject> _blueAgents;
    private NavMeshAgent _redAgent;
    private NavMeshAgent _blueAgent;
    private GameObject _blueContainer;
    private GameObject _redContainer;
    private GameObject _tempRoom;
    private Vector3 _startPos;

    private void Awake( ) {
        Instance = this;
    }

    private void Update( ) {
        if ( createScene ) {
            _tempRoom = SetUpScene( spawnLocation, spawnScene );
            createScene = false;
        }
        if ( _tempRoom == null ) return;
        if ( !runSim ) return;

        if ( NavMeshWatcher.Instance.NavMeshReady ) {
            if ( _blueAgents != null && _redAgents.Count != null ) {
                DestroyAgents( );
            }
            _redAgent = redObj.GetComponent<NavMeshAgent>( );
            _blueAgent = blueObj.GetComponent<NavMeshAgent>( );
            SetUpAgents( );
            runSim = false;
        }
    }

    private GameObject SetUpScene( GameObject loc, GameObject room ) {
        var scene = Instantiate( room );
        scene.transform.position = loc.transform.position;
        scene.name = "Scene Room";
        return scene;
    }

    private void SetUpAgents( ) {
        // create containers 
        _blueContainer = new GameObject( "blue container" );
        _redContainer = new GameObject( "red container" );

        // get count 
        int blueAmount = (int) math.floor( agentCount / ratio.x );
        int redAmount = (int) math.floor( agentCount / ratio.y );

        // build list of agents 
        _blueAgents = BuildAgents( blueObj, _blueContainer, blueStart, blueAmount, _blueAgent );
        _redAgents = BuildAgents( redObj, _redContainer, redStart, redAmount, _redAgent );

        // update agent list 
        SetAgentList( _blueAgents, blueEnd );
        SetAgentList( _redAgents, redEnd );
    }

    private static void SetAgentList( List<GameObject> agents, GameObject point ) {
        foreach ( var agent in agents ) {
            var data = agent.GetComponent<NavMeshAgent>( );

            if ( data != null ) {
                data.SetDestination( point.transform.position );
            }
        }
    }

    private static List<GameObject> BuildAgents( GameObject obj, GameObject container, GameObject start, int amount, NavMeshAgent agentData ) {
        List<GameObject> temp = new List<GameObject>( );

        for ( int i = 0; i < amount; i++ ) {
            var instance = Instantiate( obj );
            instance.name = obj.name + " " + i;
            instance.transform.position = start.transform.position;

            if ( instance.GetComponent<NavMeshAgent>( ) == null ) {
                instance.AddComponent<NavMeshAgent>( );
            }
            temp.Add( instance );
        }
        return temp;
    }

    public void DestroyAgents( ) {
        _redAgents?.Clear( );
        _blueAgents?.Clear( );

        if ( _redContainer != null ) {
            foreach ( Transform agent in _redContainer.transform ) {
                Destroy( agent.gameObject );
            }
            Destroy( _redContainer );
        }

        if ( _blueContainer != null ) {
            foreach ( Transform agent in _blueContainer.transform ) {
                Destroy( agent.gameObject );
            }
            Destroy( _blueContainer );
        }
        Debug.Log( "agents cleared " );
    }

}