using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public static class KillerNavigation{

    //++++ Gets a new location for the agent to wander to ++++//
    public static Vector3 GetWanderLocation(Vector3 startPosition, float range){
        //Instantiates variables to be used in the method
        float x, z;
        Vector3 lOffset, tarL;

        //Gets the new target location...
        do{
            //Gets a random x and z position within range
            x = Random.Range(-range, range);
            z = Random.Range(-range, range);

            //Gets a new offset to add to the player's current location value
            lOffset = new Vector3(x, startPosition.y, z);
            //Adds the startPosition to the lOffset...
            tarL = startPosition + lOffset;
        }
        //...and checks if the targetLocation is valid
        while(checkWanderValidation(tarL) == false);

        //Returns a new vector3 position with the new x and z positions
        return tarL;
    }

    //++++ Checks if the target location is a valid location for this gameObject ++++//
    private static bool checkWanderValidation(Vector3 tarL){
        //Creates a new hitMesh to check if an object is touching the nav mesh
        NavMeshHit hit;
        //Gets a true or false value to check if the target location is valid
        bool isLocationValid = NavMesh.SamplePosition(tarL, out hit, 1, NavMesh.AllAreas);
        //returns the bool to where the function was called
        return isLocationValid;
    }
}
