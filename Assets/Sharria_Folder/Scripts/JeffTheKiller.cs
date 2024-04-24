using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JeffTheKiller : MonoBehaviour{
    //jjj
    [Header("Other References")]
    [SerializeField] private Transform playerLocation;
    [SerializeField] private static NavMeshAgent agent;

    [Header("Jeff References")]
    private Vector3 startPosition;
    [SerializeField] private float wanderRange;
    [SerializeField] private float pursuedRange;
    [SerializeField] private float attackRange;
    [SerializeField] private float rangeThreshold;
    [SerializeField] private JeffState jState;
    bool isOnCooldown = false;
    float counter;
    private Vector3 targetLocation;

    private void Start(){
        agent = FindAnyObjectByType<NavMeshAgent>();
        startPosition = transform.position;
        jState = JeffState.wander;
        SetWanderState();
    }
    private void Update(){
        if(isOnCooldown == false){
            //If the pursue state is active
            if(jState == JeffState.pursue){
                //If the player is within the pursued range
                if(Vector3.Distance(transform.position, playerLocation.position) <= pursuedRange){
                    //If the player is within the attack range... change the state
                    if(Vector3.Distance(transform.position, playerLocation.position) <= attackRange){
                        //Set the JeffState to Recovery
                        jState = JeffState.attack;
                        OnAttackState();
                    }
                    //The just carry out with the pursue state
                    else{
                        OnPursueState();
                    }
                }
            }
            //If the Attack State is Active
            if(jState == JeffState.attack){
                //If the player is within the attack range
                if(Vector3.Distance(transform.position, playerLocation.position) <= attackRange){
                    if(isOnCooldown == false){
                        //Set the JeffState to Recovery
                        jState = JeffState.recovery;
                        SetRecoveryState();
                    }
                    else{
                        //do nothing
                    }
                    
                }
            }
            //If the Wander State is Active
            else if (jState == JeffState.wander){
                //If the target location is reached and not in range of the player, reset the target location
                if (Vector3.Distance(transform.position, targetLocation) < rangeThreshold && Vector3.Distance(transform.position, playerLocation.position) > pursuedRange){
                    //Sets recovery
                    jState = JeffState.recovery;
                    SetRecoveryState();
                }
                //Else, do nothing
            }
        }
        else{
            if (jState == JeffState.recovery){
                counter += Time.deltaTime;
                //Reset the new state
                if(counter >= 3){
                    counter = 0;
                    isOnCooldown = false;

                    if(Vector3.Distance(transform.position, playerLocation.position) <= pursuedRange){
                        jState = JeffState.pursue;
                        OnPursueState();
                    }
                    else{
                        jState = JeffState.wander;
                        SetWanderState();
                    }
                }
            }
        }
        
    }

    //===============Wander State=================//
    private void SetWanderState(){
        //Gets new target location
        targetLocation = KillerNavigation.GetWanderLocation(startPosition, wanderRange);
        //Sets the new navTarget
        agent.SetDestination(targetLocation);
    }
    //===============Pursue State=================//
    private void OnPursueState(){
        //Sets the new navTarget
        agent.SetDestination(playerLocation.position);
    }
    //===============Attack State=================//
    private void OnAttackState(){
        Debug.Log("Player Was Attacked");
        //Recover from checking
        jState = JeffState.recovery;
        SetRecoveryState();
    }
    //===============Recovery State=================//
    private void SetRecoveryState(){
        isOnCooldown = true;
    }
}

public enum JeffState{
    wander, 
    pursue, 
    attack, 
    recovery
}