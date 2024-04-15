using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_NPC_Wander : State{
    //[Header("Jeff References")]
    //----Jeff References----//
    protected NavMeshAgent agent;
    protected NPC_Data jeffData;
    //----Location Values----//
    private protected float range;
    protected Vector3 targetLocation;
    [SerializeField] protected float targetThreshold;
    //----Cooldown Timer----//
    protected float cooldownTimer = 0;
    [SerializeField] protected float cooldownTimerDuration = 2.5f;
    protected bool isOnCooldown = false;

    //*******************************************************************************************/

    //====Startup Methods====//
    private void Awake(){
        //Gets a reference to the parent object's NPC_Data component
        jeffData = gameObject.GetComponentInParent<NPC_Data>();
        //Gets a reference to the parent object's NavMeshAgent component
        agent = gameObject.GetComponentInParent<NavMeshAgent>();
    }
    private void Start(){
        //Gets the range from the JeffData
        range = jeffData.wanderRange;
        Debug.Log("Wander State has started!!!");
    }

    public override void Enter_State(){
        Debug.Log("Wander State been ENTERED!!!");
        //Gives the agent a new location to head towards
        Debug.Log("New Destination has been SET!!!");
        targetLocation = getWanderLocation();
        agent.SetDestination(targetLocation);
    }

    //*******************************************************************************************/

    //====Update Methods====//
    public override void Update_State(){
        //Gets the distance from the target location to the current location
        float distance = Vector3.Distance(targetLocation, jeffData.currentPosition);
        //!* Not going to set this to isComplete true because we want to continue wandering until other state triggers
        //Checks if the distance is within the threshold
        if(distance <= targetThreshold && isOnCooldown == false){
            //Sets the isOnCooldown flag to true
            isOnCooldown = true;
        }
        else if(isOnCooldown == true){
            //Adds 1 second to the timer
            cooldownTimer += Time.deltaTime;

            //If the timer is greater than or equal to the cooldown duration...
            if(cooldownTimer >= cooldownTimerDuration){
                //Reset the timer and cooldown tracker
                isOnCooldown = false;
                cooldownTimer = 0;

                //Get a new target location
                targetLocation = getWanderLocation();
                //Sets the agent's destination to the target location
                agent.SetDestination(targetLocation);
            }
        }
    }

    //*********************************************************************************************/

    //====Exit Methods====//
    public override void Exit_State(){
        Debug.Log("Wander State been EXITED!!!");
        //Resets some variables from this state
        targetLocation = Vector3.zero;
        isComplete = false;
    }

    //********************************************************************************************/
    
    //====Other Methods====//
    //++++ Gets a new location for the agent to wander to ++++//
    private Vector3 getWanderLocation(){
        //Instantiates variables to be used in the method
        float x, z;
        Vector3 lOffset, tarL;

        //Gets the new target location...
        do{
            //Gets a random x and z position within range
            x = Random.Range(-range, range);
            z = Random.Range(-range, range);

            //Gets a new offset to add to the player's current location value
            lOffset = new Vector3(x, jeffData.startPosition.y, z);
            //Adds the startPosition to the lOffset...
            tarL = jeffData.startPosition + lOffset;
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


