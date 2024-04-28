using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData{
    public static int playerHealth = 100;
    public static int playerMaxHealth = 100;
    public static int playerAmmo = 50;
    public static int playerMaxAmmo = 100;

    public static Transform playerTransform;
    //Made a float to adjust player speed
    public static float moveSpeed = 5f;


}

public class SaveData{
    public int playerHealth;
    public int playerMaxHealth;
    public int playerAmmo;
    public int playerMaxAmmo;
    public Transform playerTransform;
    public float moveSpeed;
}
