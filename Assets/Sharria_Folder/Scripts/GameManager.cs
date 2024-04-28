using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    public static InputManager menuInputs = null;
    public static InputManager cheatInputs = null;
    public Canvas statCanvas;
    public Canvas pauseMenuCanvas;

    private bool isPaused = false;

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
        SetupEnabledInputActions();
        
    }
    private void OnDisable(){
        //This turns on the input manager 
        menuInputs.Disable();
        cheatInputs.Disable();
        SetupDisabledInputActions();
    }

    //============Save/Load Methods==============//
    private void CallPauseMenuPerformed(InputAction.CallbackContext value){
        //Switches the current setting of the menu tot he opposite
        isPaused = !isPaused;
        //Loads the pause menu over the current scene
        if(isPaused){
            SceneManager.LoadScene("Pause_Menu", LoadSceneMode.Additive);
            //Finds all instances of type Canvas
            var foundCanvasObjects = FindObjectsOfType<Canvas>();
            //Looks through all instances of Canvases in foundCanvasObjects
            foreach(var c in foundCanvasObjects){
                //If the name of the canvas is "Canvas_PauseMenu"... save it to
                if(c.name == "Canvas_PauseMenu"){
                    //...save it to pauseMenuCanvas
                    pauseMenuCanvas = c;
                }
            }//ends foreach-loop
        }
        else{
            SceneManager.UnloadSceneAsync("Pause_Menu");
            //Resets the pause menu to be null again
            pauseMenuCanvas = null;
        }
        
    }
    
    //********************************************************************    
    private void SaveGame(){
        SaveData sd = new SaveData();
        //saves all the values we want to save to the SaveData Class
        sd.playerHealth = PlayerData.playerHealth;
        sd.playerAmmo = PlayerData.playerAmmo;
        sd.playerMaxHealth = PlayerData.playerMaxHealth;
        sd.playerMaxAmmo = PlayerData.playerMaxAmmo;
        sd.playerTransform = PlayerData.playerTransform;
        sd.moveSpeed = PlayerData.moveSpeed;
        //Converts the objects of SaveData to a Json format
        string jsonText = JsonUtility.ToJson(sd);
        Debug.Log("Json file was saved");
        Debug.Log(jsonText);
        //Saves it to the Json file
        File.WriteAllText(sd.dataPath, jsonText);
    }
    private void LoadGame(){

    }



    //============Cheat Methods=============//
    /*
    There are children objects of statCanvas that need to be accessed specifically
    Child 0: HealthText
    Child 1: AmmoText
    */
    //======---------Setup Methods----------========//
    private void SetupEnabledInputActions(){
        //Menu Inputs on performed being set
        menuInputs.MenuInputs.PauseMenu.performed += CallPauseMenuPerformed;
        menuInputs.MenuInputs.Save_Cheat.performed += SaveGamePerformed;
        menuInputs.MenuInputs.Load_Cheat.performed += LoadGamePerformed;
        //Menu Inputs on cancelled being set
        menuInputs.MenuInputs.PauseMenu.canceled += EmptyCancelCalls;
        menuInputs.MenuInputs.Save_Cheat.canceled += EmptyCancelCalls;
        menuInputs.MenuInputs.Load_Cheat.canceled += EmptyCancelCalls;
        //****************************************************************
        //Cheat Inputs on performed being setup
        cheatInputs.Cheats.ReduceAmmo.performed += ReduceAmmoPerformed;
        cheatInputs.Cheats.ReduceHealth.performed += ReduceHealthPerformed;
        cheatInputs.Cheats.AddAmmo.performed += AddAmmoPerformed;
        cheatInputs.Cheats.AddHealth.performed += AddHealthPerformed;
        //Cheat Inputs on cancelled being setup
        cheatInputs.Cheats.ReduceAmmo.canceled += ReduceAmmoCancelled;
        cheatInputs.Cheats.ReduceHealth.canceled += ReduceHealthCancelled;
        cheatInputs.Cheats.AddAmmo.canceled += AddAmmoCancelled;
        cheatInputs.Cheats.AddHealth.canceled += AddHealthCancelled;
    }
    private void SetupDisabledInputActions(){
        //Menu Inputs on performed being set
        menuInputs.MenuInputs.PauseMenu.performed -= CallPauseMenuPerformed;
        menuInputs.MenuInputs.Save_Cheat.performed -= SaveGamePerformed;
        menuInputs.MenuInputs.Load_Cheat.performed -= LoadGamePerformed;
        //Cheat Inputs on cancelled being set
        menuInputs.MenuInputs.PauseMenu.canceled -= EmptyCancelCalls;
        menuInputs.MenuInputs.Save_Cheat.canceled -= EmptyCancelCalls;
        menuInputs.MenuInputs.Load_Cheat.canceled -= EmptyCancelCalls;
        //***********************************************************************
        cheatInputs.Cheats.ReduceAmmo.performed -= ReduceAmmoPerformed;
        cheatInputs.Cheats.ReduceHealth.performed -= ReduceHealthPerformed;
        cheatInputs.Cheats.AddAmmo.performed -= AddAmmoPerformed;
        cheatInputs.Cheats.AddHealth.performed -= AddHealthPerformed;
        //Cheat Inputs on cancelled being setup
        cheatInputs.Cheats.ReduceAmmo.canceled -= ReduceAmmoCancelled;
        cheatInputs.Cheats.ReduceHealth.canceled -= ReduceHealthCancelled;
        cheatInputs.Cheats.AddAmmo.canceled -= AddAmmoCancelled;
        cheatInputs.Cheats.AddHealth.canceled -= AddHealthCancelled;
    }
    //=======---------OnPerformed--------=======// (This will do calculations for certain actions [Pressing the button])
    //-----------Health cheats---------//
    private void AddHealthPerformed(InputAction.CallbackContext value){
        //changes the value of playerAmmo
        PlayerData.playerHealth += 1;
    }
    private void ReduceHealthPerformed(InputAction.CallbackContext value){
        //changes the value of playerAmmo
        PlayerData.playerHealth -= 1;
    }
    //----------Ammo cheats-----------//
    private void AddAmmoPerformed(InputAction.CallbackContext value){
        //changes the value of playerAmmo
        PlayerData.playerAmmo += 1;
    }
    private void ReduceAmmoPerformed(InputAction.CallbackContext value){
        //changes the value of playerAmmo
        PlayerData.playerAmmo -= 1;
    }
    //------------Save/Load Data-----------//
    private void SaveGamePerformed(InputAction.CallbackContext value){
        SaveGame();
    }
    private void LoadGamePerformed(InputAction.CallbackContext value){
        LoadGame();
    }

    //**************************************************************************************************
    //=========----------On Cancelled---------=========// (This will display the changes after the action has happened [When the button is released])
    //-----------Health cheats---------//
    private void AddHealthCancelled(InputAction.CallbackContext value){
        //Calls the function for update the text of the player
        var statText = statCanvas.transform.GetChild(0).GetComponent<TextChanger>(); //Accesses child 0 [Health Text]
        statText.UpdateHealthText(PlayerData.playerHealth);
    }
    private void ReduceHealthCancelled(InputAction.CallbackContext value){
        //Calls the function for update the text of the player
        var statText = statCanvas.transform.GetChild(0).GetComponent<TextChanger>(); //Accesses child 0 [Health Text]
        statText.UpdateHealthText(PlayerData.playerHealth);
    }
    //----------Ammo cheats-----------//
    private void AddAmmoCancelled(InputAction.CallbackContext value){
        //Calls the function for update the text of the player
        var statText = statCanvas.transform.GetChild(1).GetComponent<TextChanger>(); //Accesses child 1 [Ammo Text]
        statText.UpdateAmmoText(PlayerData.playerAmmo);
    }
    private void ReduceAmmoCancelled(InputAction.CallbackContext value){
        //Calls the function for update the text of the player
        var statText = statCanvas.transform.GetChild(1).GetComponent<TextChanger>(); //Accesses child 1 [Ammo Text]
        statText.UpdateAmmoText(PlayerData.playerAmmo);
    }
    //----------save/Load data-------------//



    //==========OTHER FUNCTIONS================//
    private void EmptyCancelCalls(InputAction.CallbackContext value){
        //This is for all the on Cancelled calls that don't have anything specific that needs to happen
    }
}
