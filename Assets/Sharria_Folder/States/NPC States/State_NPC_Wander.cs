using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class State_NPC_Wander : State{
    //----Location Values----//
    public Vector3 startPosition;
    public Vector3 currentPosition;

    [SerializeField] private float wanderRange;

    //----Cooldown Timer----//
    protected float cooldownTimer = 0;
    [SerializeField] protected float cooldownTimerDuration = 2.5f;
    protected bool isOnCooldown = false;

    //!*====Startup Methods====//
    public override void Enter_State(){
        //Gets the location of the NPC on awake
        startPosition = transform.parent.transform.position; 
        Debug.Log("Wander State been ENTERED!!!");
        //Sets the object's speed
        sCore.agent.speed = 2.0f;
        //Sets the target location to the new location
        targetLocation = getWanderLocation();
        //Sets the objects destination to the target location
        sCore.agent.SetDestination(targetLocation);
    }

    //!*====Update Methods====//
    public override void Update_State(){
        //Updates what the current location of the gameObject
        currentPosition = transform.parent.transform.position;

        //Gets the distance from the target location to the current location
        distanceFromTarget = Vector3.Distance(targetLocation, currentPosition);
        //!* Not going to set this to isComplete true because we want to continue wandering until other state triggers
        //Checks if the distance is within the threshold
        if(distanceFromTarget <= targetDistanceThreshold && isOnCooldown == false){
            //Sets the isOnCooldown flag to true
            isOnCooldown = true;
        }
        //If the gameobject is on a cooldown
        else if(isOnCooldown == true){
            //Adds 1 second to the timer
            cooldownTimer += Time.deltaTime;

            //If the timer is greater than or equal to the cooldown duration...
            if(cooldownTimer >= cooldownTimerDuration){
                //Reset the timer and cooldown tracker
                isOnCooldown = false;
                cooldownTimer = 0;

                //Gives the agent a new location to head towards
                Debug.Log("New Destination has been SET!!!");
                //Get a new target location
                targetLocation = getWanderLocation();
                //Sets the agent's destination to the target location
                sCore.agent.SetDestination(targetLocation);
            }
        }
    }

    //!*====Exit Methods====//
    public override void Exit_State(){
        Debug.Log("Wander State been EXITED!!!");
        //Resets some variables from this state
        targetLocation = Vector3.zero;
        sCore.agent.speed = 0.0f;
        isComplete = false;
    }

    //!*====Other Methods====//
    //++++ Gets a new location for the agent to wander to ++++//
    private Vector3 getWanderLocation(){
        //Instantiates variables to be used in the method
        float x, z;
        Vector3 tarOffset, tarL;

        //Gets the new target location...
        do{
            //Gets a random x and z position within range
            x = Random.Range(-wanderRange, wanderRange);
            z = Random.Range(-wanderRange, wanderRange);

            //Gets a new offset to add to the player's current location value
            tarOffset = new Vector3(x, startPosition.y, z);
            //Adds the startPosition to the tarOffset...
            tarL = startPosition + tarOffset;
        }
        //...and checks if the targetLocation is valid
        while(checkWanderValidation(tarL) == false);

        //Returns a new vector3 position with the new x and z positions
        return tarL;
    }
    //++++ Checks if the target location is a valid location for this gameObject ++++//
    private bool checkWanderValidation(Vector3 tarL){
        //Creates a new hitMesh to check if an object is touching the nav mesh
        NavMeshHit hit;
        //Gets a true or false value to check if the target location is valid
        bool isLocationValid = NavMesh.SamplePosition(targetLocation, out hit, 1, NavMesh.AllAreas);
        //returns the bool to where the function was called
        return isLocationValid;
    }
}


