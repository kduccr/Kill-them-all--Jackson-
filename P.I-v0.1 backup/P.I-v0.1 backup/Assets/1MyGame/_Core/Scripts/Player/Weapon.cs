using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    //Saber quem é o invetário
    public Inventory inv;

    // DAMAGE REGION
    public float damage;   // controle


    // FIRE RATE REGION

    public float fireRate;


    // START POSITION REGION

    public Transform[] startPos;

    //VELOCIDADE
    public Vector3 bulletSpeed;

    // AUDIO REGION
    #region 


    public AudioClip rayGunClip;
    public AudioClip gunClip;
    public AudioClip pistolClip;
#endregion

    public void Shoot()
    {
        foreach (Transform pos in startPos)
        {
            GameObject bala = (GameObject)Instantiate(inv.balaAtual.gameObject);
            bala.transform.position = transform.position;
            // bala.GetComponent<Rigidbody2D>().AddForce(bulletSpeed);
        }
    }
}
