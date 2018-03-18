﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSpell : MonoBehaviour
{

    [SerializeField] float speed = 10f;
    [SerializeField] float radius;

    public float delay = 4f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    
    GameObject enemy;
    float speedSlow = 1f;
    

    public GameObject[] enemiesInRange;

    EnemyMovement move;
    ChasePlayer chase;
    void Start()
    {

        countdown = delay;
        //  enemy = FindObjectOfType<Enemy>();



    }
    void Update()
    {
        
        Vector3 pos = transform.position;
        Vector3 velocity = new Vector3(0, speed * Time.deltaTime, 0);
        pos += transform.rotation * velocity;
        transform.position = pos;
        countdown -= Time.deltaTime;


        if (countdown <= 0 && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }

    }

    void Explode()
    {

        GameObject novoexplosionEffect =(GameObject)Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, radius, 9 << LayerMask.NameToLayer("Enemy"));

        foreach (Collider2D nearbyObject in col)
        {
            
            

            if (nearbyObject.transform.tag == "Enemy")
            {
                move = nearbyObject.GetComponent<EnemyMovement>();

                print("ColorChangeToBlue");
                move.ColorChangeToBlue();
                //move.ColorChangeToBlue();
                move.SlowEffect(speedSlow);
                //move.SlowEffect(speedSlow);
            }

            if (nearbyObject.transform.tag == "EnemyChase")
            {
                chase = nearbyObject.GetComponent<ChasePlayer>();
                print("ColorChangeToBlue");
                chase.ColorChangeToBlue();
                //move.ColorChangeToBlue();
                chase.SlowEffect(speedSlow);
                //move.SlowEffect(speedSlow);
            }





        }
        hasExploded = false;
        Destroy(gameObject);
        Destroy(novoexplosionEffect, 3f);
    }




    void OnDrawGizmos()
    {
        // Draw attack sphere 
        Gizmos.color = new Color(255f, 0, 0, .5f);
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
