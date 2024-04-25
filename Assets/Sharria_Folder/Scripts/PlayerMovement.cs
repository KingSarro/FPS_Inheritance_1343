using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWalk : MonoBehaviour{
    //Makes a refernce to the InputManager Class
    public static InputManager input = null;
    //Makes a reference to the current object's rigidbody
    private Rigidbody rb = null;
    //Creates a Vector3 to be used in player Walk and defaults it to zero
    private Vector3 moveValue = Vector3.zero;
    //Made a float to adjust player speed
    [SerializeField] float moveSpeed = 5f;

//Calls the Awake method to save objects/components to their refernce holder
    private void Awake(){
        //Sets a new instance of Inputmanager to input
        input = new InputManager();
        //Saves the rigidbody component's access to rb
        rb = GetComponent<Rigidbody>();
    }//Closes Awake
    
    private void OnEnable(){
        //This turns on the input manager 
        input.Enable();
        //the input is subscribing to the onWalkPerformed event.
        input.Player.Walk.performed += onWalkPerformed;
        //the input is subscribing to the onWalkCancelled event.
        input.Player.Walk.canceled += onWalkCancelled;
    }//closes onEnable

    private void OnDisable(){
        //This turns off the input manager 
        input.Disable();
        //the input is unsubscribing to the onWalkPerformed event.
        input.Player.Walk.performed -= onWalkPerformed;
        //the input is unsubscribing to the onWalkCancelled event.
        input.Player.Walk.canceled -= onWalkCancelled;
        //the input is unsubscribing to the onWalkPerformed event.
    }//Close onDisable

    //==Because we want to make sure rigid body calculations are going to be correct, were doing a fixed update
    private void FixedUpdate(){//Fixed Update already uses time.DeltaTime
        //Sets the Walk of the objects
        rb.velocity = moveValue * moveSpeed;
    }//closes fixed update

    //Checks if the assaigned input is being triggered and traking it's value
    public void onWalkPerformed(InputAction.CallbackContext value){
        //The value that was read gets saved to moveValue
        moveValue = value.ReadValue<Vector3>();
    }//Closes the onMovePerformed

    //Checks if the assaigned input has been interrupted
    public void onWalkCancelled(InputAction.CallbackContext value){
        //moveValue gets set to zero
        moveValue = Vector3.zero;
    }//closes the onMovecancled
    

}//closes the class