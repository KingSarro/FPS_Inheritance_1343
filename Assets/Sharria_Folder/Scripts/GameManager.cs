using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour{
    public static InputManager menuInputs = null;
    public static InputManager cheatInputs = null;
    public Canvas statCanvas;
    public Canvas pauseMenuCanvas;

    // Start is called before the first frame update
    void Awake(){
        statCanvas = FindObjectOfType<Canvas>();
        //Sets a new instance of Inputmanager to input
        menuInputs = new InputManager();
        cheatInputs = new InputManager();
    }

    private void Start(){
        //Get Access to the textChanger on the canvas components
        var statText = statCanvas.transform.GetChild(0).GetComponent<TextChanger>(); //Accesses child 0 [Health Text]
        statText.UpdateHealthText(PlayerData.playerHealth);
        statText = statCanvas.transform.GetChild(1).GetComponent<TextChanger>(); //Accesses child 1 [Ammo Text]
        statText.UpdateAmmoText(PlayerData.playerAmmo);
        
    }

    private void OnEnable(){
        //This turns on the input manager 
        menuInputs.Enable();
        cheatInputs.Enable();

        cheatInputs.Cheats.ReduceAmmo.performed += reduceAmmo;
        cheatInputs.Cheats.ReduceHealth.performed += reduceHealth;
        cheatInputs.Cheats.AddAmmo.performed += addAmmo;
        cheatInputs.Cheats.AddHealth.performed += addHealth;
    }
    private void OnDisable(){
        //This turns on the input manager 
        menuInputs.Disable();
        cheatInputs.Disable();

        cheatInputs.Cheats.ReduceAmmo.performed -= reduceAmmo;
        cheatInputs.Cheats.ReduceHealth.performed -= reduceHealth;
        cheatInputs.Cheats.AddAmmo.performed -= addAmmo;
        cheatInputs.Cheats.AddHealth.performed -= addHealth;
    }

    //============Cheat Methods=============//
    /*
    There are children objects of statCanvas that need to be accessed specifically
    Child 0: HealthText
    Child 1: AmmoText
    */

    //-----------Health cheats---------//
    private void addHealth(InputAction.CallbackContext value){
        //changes the value of playerAmmo
        PlayerData.playerHealth += 1;
        //Calls the function fo update the text of the player
        var statText = statCanvas.transform.GetChild(0).GetComponent<TextChanger>(); //Accesses child 1 [Ammo Text]
        statText.UpdateAmmoText(PlayerData.playerHealth);
    }
    private void reduceHealth(InputAction.CallbackContext value){
        //changes the value of playerAmmo
        PlayerData.playerHealth -= 1;
        //Calls the function fo update the text of the player
        var statText = statCanvas.transform.GetChild(0).GetComponent<TextChanger>(); //Accesses child 1 [Ammo Text]
        statText.UpdateAmmoText(PlayerData.playerHealth);
    }
    //----------Ammo cheats-----------//
    private void addAmmo(InputAction.CallbackContext value){
        //changes the value of playerAmmo
        PlayerData.playerAmmo += 1;
        //Calls the function fo update the text of the player
        var statText = statCanvas.transform.GetChild(1).GetComponent<TextChanger>(); //Accesses child 1 [Ammo Text]
        statText.UpdateAmmoText(PlayerData.playerAmmo);
    }
    private void reduceAmmo(InputAction.CallbackContext value){
        //changes the value of playerAmmo
        PlayerData.playerAmmo -= 1;
        //Calls the function fo update the text of the player
        var statText = statCanvas.transform.GetChild(1).GetComponent<TextChanger>(); //Accesses child 1 [Ammo Text]
        statText.UpdateAmmoText(PlayerData.playerAmmo);
    }
    
}
