using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour
{
    public int currentEnergy = 0;
    
    
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Slider energySlider;
    public Image damageImage;
    
    public AudioClip energyClip;
    public AudioClip[] hurtClip;
    public AudioClip deathClip;


    public AudioClip ammoClip;
    public AudioClip healthClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);

    Tiro tiro;
    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;

    Collider2D playerCollider;
    SlimeExplode slime;
    GameObject slimeEnemy;
    WeaponSwitch weapon;
    GameObject player;
    
    
    
    
    //PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    

    public bool damageable;
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weapon = player.GetComponentInChildren<WeaponSwitch>();
        tiro = GetComponent<Tiro>();
        playerCollider = this.gameObject.GetComponent<BoxCollider2D>();
        
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
        damageable = true;
        Energy();



    }

    void Energy()
    {
        energySlider.value = currentEnergy;
    }
    void Update ()
    {
        MaxEnergy();
        MaxHealth();
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, Time.deltaTime *5);
        
        
   
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            
            
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        if (damageable == true) {
            damaged = true;

            currentHealth -= amount;

           // healthSlider.value = currentHealth;
            
            int spawnPointIndex = Random.Range(0, hurtClip.Length);

            playerAudio.clip = hurtClip[spawnPointIndex];
            playerAudio.Play();
        }
        if (currentHealth <= 0 && !isDead)
        {
            Death ();
        }
        if (currentHealth > 0)
        {
            playerCollider.enabled = true;
            anim.SetBool("IsDead", false);
        }
        
    }


    void Death ()
    {
        playerCollider.enabled = false;
        playerAudio.Stop();
        playerAudio.clip = deathClip;
        playerAudio.Play();
        isDead = true;

        //playerShooting.DisableEffects ();
        
        anim.SetBool("IsDead", true);




        playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "HealBox")
        {

            playerAudio.clip = healthClip;
            playerAudio.Play();
            currentHealth += 25;
            healthSlider.value = currentHealth;

        }
        if (other.gameObject.tag == "BulletEnemy")        
        {
            damaged = true;
            int spawnPointIndex = Random.Range(0, hurtClip.Length);
            playerAudio.clip = hurtClip[spawnPointIndex];
            playerAudio.Play();
            currentHealth -= 10;
            healthSlider.value = currentHealth;
        }

        if (other.gameObject.tag == "AmmoBox")
        {
            playerAudio.clip = ammoClip;
            playerAudio.Play();
            if (weapon.showAK == true)
            {
                tiro.ammoAK += 100;
                ScoreManager.score = tiro.ammoAK;
            }
            if (weapon.showRayGun == true)
            {
                tiro.ammoRaygun += 100;
                ScoreManager.score = tiro.ammoRaygun;
            }


            //TODO update Canvas de ammo de acordo com a arma
            
        }

        if (other.gameObject.tag == "EnergyBox")
        {

            playerAudio.clip = energyClip;
            playerAudio.Play();
            currentEnergy += 25;
            energySlider.value = currentEnergy;

        }

       

    }

    void DamagePlayer()
    {
        slimeEnemy = GameObject.FindGameObjectWithTag("Slime");
        slime = slimeEnemy.GetComponent<SlimeExplode>();
        TakeDamage(slime.attackDamage);
        healthSlider.value = currentHealth;
    }



    void MaxEnergy()
    {
        if (currentEnergy > 100) { currentEnergy = 100; }
    }
    void MaxHealth()
    {
        if (currentHealth > 100) { currentHealth = 100; }
    }

    
    
    }
    