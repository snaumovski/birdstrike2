using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace dTestField {
    public class NavMeshManager : MonoBehaviour {

        public float factor = .01f;
        public Vector3 size = Vector3.one;
        
        
        
        private Vector3 _center;
        private NavMeshData _navMesh;
        private AsyncOperation _operation;
        private NavMeshDataInstance _navInstance;
        private List<NavMeshBuildSource> _navSources = new List<NavMeshBuildSource>( );

        private void Update( ) {
            _center = transform.position;
        }

        IEnumerator Start( ) {
            while ( true ) {
                UpdateNavMesh( true );
                yield return _operation;
            }
        }

        private void OnDrawGizmosSelected( ) {
            if ( _navMesh ) {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube( _navMesh.sourceBounds.center, _navMesh.sourceBounds.size );
            }
            Gizmos.color = Color.yellow;
            var bounds = GetBounds( );
            Gizmos.DrawWireCube( bounds.center, bounds.size );
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube( _center, size );
        }

        private Bounds GetBounds( ) {
            return PathHelper.QauntizeBounds( _center, size, factor );
        }

        private void UpdateNavMesh( bool asyncUpdate ) {
            Room.Collect( ref _navSources );
            var defaultBuildSettings = NavMesh.GetSettingsByID( 0 );
            var bounds = GetBounds( );

            if ( asyncUpdate )
                _operation = NavMeshBuilder.UpdateNavMeshDataAsync( _navMesh, defaultBuildSettings, _navSources, bounds );
            else
                NavMeshBuilder.UpdateNavMeshData( _navMesh, defaultBuildSettings, _navSources, bounds );
        }

    }
}