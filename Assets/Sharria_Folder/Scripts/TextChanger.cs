using UnityEngine;
using TMPro;
using System;

public class TextChanger : MonoBehaviour{
    private TMP_Text text;
    private void Awake(){
        text = GetComponent<TMP_Text>();
    }


    public void UpdateHealthText(int newHealth){
        text.text = "Health: " + newHealth.ToString() + "/" + PlayerData.playerMaxHealth.ToString();
    }

    public void UpdateAmmoText(int newAmmo){
        text.text = "Ammo: " + newAmmo.ToString() + "/" + PlayerData.playerMaxAmmo.ToString();
    }
}
