using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NPC_Data : MonoBehaviour{
    
    public Vector3 startPosition;
    public Vector3 currentPosition;

    public float wanderRange;
    public float sightRange;
    public float attackRange;
    public float speed;

    public float currentStateElapsed;
    public float recoveryTime;

    private void Awake(){
        //Gets the location of the NPC on awake
        startPosition = gameObject.transform.position;  
    }
    public void Update(){
        //Updates to keep track of the current position of the gameObject
        currentPosition = gameObject.transform.position;
    }
}
