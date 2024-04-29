using System.IO;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData: ScriptableObject{
    public int playerHealth = 100;
    public int playerMaxHealth = 100;
    public int playerAmmo = 50;
    public int playerMaxAmmo = 100;

    //public GameObject playerRef; //used for the player transform

    //Made a float to adjust player speed
    public float moveSpeed = 5f;
}

public class SaveData{
    //C:\Users\[userName]\[Location]\[projectname]\Assets
    public static readonly string dataPath = Application.dataPath + Path.AltDirectorySeparatorChar + "PlayerSaveData" + Path.AltDirectorySeparatorChar + "PlayerData.Json"; //This cannot be modified
    
    public int playerHealth;
    public int playerMaxHealth;
    public int playerAmmo;
    public int playerMaxAmmo;
    public GameObject playerRef; //Used for the player transform
    public float moveSpeed;
}
