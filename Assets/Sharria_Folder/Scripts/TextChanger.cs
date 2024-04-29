using UnityEngine;
using TMPro;
using System;

public class TextChanger : MonoBehaviour{
    [SerializeField] private PlayerData playerData;
    private TMP_Text text;
    private void Awake(){
        text = GetComponent<TMP_Text>();
    }


    public void UpdateHealthText(int newHealth){
        var pData = 
        text.text = "Health: " + newHealth.ToString() + "/" + playerData.playerMaxHealth.ToString();
    }

    public void UpdateAmmoText(int newAmmo){
        text.text = "Ammo: " + newAmmo.ToString() + "/" + playerData.playerMaxAmmo.ToString();
    }
}
