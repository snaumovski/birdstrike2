using System.Collections.Generic;
using UnityEngine;

namespace dTestField {
    public enum Parts {

        Wall,
        Floor,
        Furniture,
        Poi

    }
    public static class RoomHelper {

        private static string[ ] _values = {"walls", "floors", "furniture"};

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
    public class RoomManager : MonoBehaviour {

        private List<GameObject> _storedRooms = new List<GameObject>( );
        private void Start( ) { }

    }
}