using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    Player player;

    Text text;
    GameObject jogador;
    public string pistolText;
    WeaponSwitch weapon;


    void Awake ()
    {
        jogador = GameObject.FindGameObjectWithTag("Player");
        weapon = jogador.GetComponentInChildren<WeaponSwitch>();
        text = GetComponent <Text> ();
        score = 0;
        
    }


    void Update ()
    {
        if (weapon.showAK || weapon.showRayGun)
        {
            text.text = "Ammo: " + score;
        }
        else
        {
            text.text = "Infinito";
        }
 

            
  
        

    }
}
