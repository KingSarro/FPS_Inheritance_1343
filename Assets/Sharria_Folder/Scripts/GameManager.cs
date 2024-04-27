using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{
    Canvas statCanvas;
    Canvas pauseMenuCanvas;

    // Start is called before the first frame update
    void Awake(){
        //Finds a list of all canvas objects
        var foundCanvasObjects = FindObjectsOfType<Canvas>();
        //Runs through the list of canvas objects and compares their name to find which canvas is which
        foreach(var canvas in foundCanvasObjects){
            //If its the stat canvas
            if(canvas.name == "StatCanvas"){
                statCanvas = canvas;
            }
            //if its the pause canvas
            else if(canvas.name == "PauseMenuCanvas"){
                pauseMenuCanvas = canvas;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
