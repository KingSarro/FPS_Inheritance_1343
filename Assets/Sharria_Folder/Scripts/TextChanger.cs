using UnityEngine;
using TMPro;
using System;

public class TextChanger : MonoBehaviour{
    private TMP_Text text;
    private void Awake(){
        text = gameObject.GetComponent<TMP_Text>();
    }


    public void UpdateHealthText(int t){
        text.text = "Health: " + t.ToString() + "/" + PlayerData.playerMaxHealth.ToString();
    }

    public void UpdateAmmoText(int t){
        text.text = "Ammo: " + t.ToString() + "/" + PlayerData.playerMaxAmmo.ToString();
    }
}
