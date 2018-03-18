using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
     int startingHealth = 100;
      
    public float healthPoints;

    public int scoreValue = 10;
    public AudioClip slimeDeathClip;
   
    public AudioClip slimeHit;
    Collider2D col;
    SpriteRenderer sprite;
    SlimeExplode slimeExplode;






    int spawnHealBox;
    int generateAllNumber;
    int spawnAmmoBox;
    int spawnEnergyBox;

    [SerializeField] GameObject healBox;
    [SerializeField] GameObject ammoBox;
    [SerializeField] GameObject energyBox;
    Vector3 deathPos;
    Animator anim;
    AudioSource enemyAudio;
    //ParticleSystem hitParticles;
    //CapsuleCollider capsuleCollider;
    public bool isDead;
    bool isSinking;

    PlayerHealth playerHealth;
    EnemyMovement move;
    GameObject player;
    Tiro tiro;

    public GameObject slimeFloorPrefab;
    public GameObject endPos;


    void Awake ()
    {
        slimeExplode = GetComponent<SlimeExplode>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        //hitParticles = GetComponentInChildren <ParticleSystem> ();
        //capsuleCollider = GetComponent <CapsuleCollider> ();
        //bullet = GetComponent<BulletMoveFoward>();
        
        //col.GetComponent<Collider2D>();
        playerHealth = player.GetComponent<PlayerHealth>();
        sprite = GetComponent<SpriteRenderer>();
        move = GetComponent<EnemyMovement>();
        tiro = player.GetComponent<Tiro>();
       
        


    }
    void GenerateAllNumber()
    {
        generateAllNumber = Random.Range(0, 3);
    }
    void GenerateHealBoxNumber()
    {
        spawnHealBox = Random.Range(0, 6);
    }
    void GenerateAmmoBoxNumber()
    {
        spawnAmmoBox = Random.Range(0, 8);
    }
    void GenerateEnergyBoxNumber()
    {
        spawnEnergyBox = Random.Range(0, 7);
    }


    void Update ()
    {


       


            if (healthPoints <= 0 && !isDead )
        {
           //playerHealth.currentEnergy += 5;
            Death();
            
            Destroy(this.gameObject, 1.25f);
            deathPos = transform.position;
            GenerateAllNumber();
            
            

            if (generateAllNumber == 0)
            {
                
                GenerateHealBoxNumber();
                if (spawnHealBox == 2)
                {

                    Instantiate(healBox, deathPos, Quaternion.identity);
                }
            }
            if (generateAllNumber == 1)
            {
                GenerateAmmoBoxNumber();
                
                if (spawnAmmoBox == 2)
                {

                    Instantiate(ammoBox, deathPos, Quaternion.identity);
                }
            }
            if (generateAllNumber == 2)
            {
                
                GenerateEnergyBoxNumber();
                if (spawnEnergyBox == 3)
                {

 
                    Instantiate(energyBox, deathPos, Quaternion.identity);
                }
                
            }
            //TODO Fix DeathSound Click Bug
        }
    }

    /*
    public void TakeDamage (int amount, Vector3 hitPoint)
    {
        //if(isDead)
          //  return;

       // enemyAudio.Play ();

        currentHealth -= amount;
            
        //hitParticles.transform.position = hitPoint;
       // hitParticles.Play();

        
    }
    */

    void Death ()
    {
        
        isDead = true;
        this.gameObject.GetComponent<Collider2D>().enabled = false;
        //col.enabled = false;
        //capsuleCollider.isTrigger = true;

        //anim.SetBool ("IsDead",true);

        //enemyAudio.clip = deathClip;
        //enemyAudio.Play ();
        
        
    }

    void FlashDamage()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
        Invoke("StopFlashDamage", 0.05f);
    }
    void StopFlashDamage()
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        

        

        if (other.gameObject.tag == "BulletAK" )
        {


            FlashDamage();

            healthPoints -= tiro.akDamage;
            anim.SetTrigger("Hit");

        }

        if (other.gameObject.tag == "RayGun")
        {

            enemyAudio.Play();
            FlashDamage();

            healthPoints -= tiro.rayGunDamage;
            anim.SetTrigger("Hit");

        }

        if (other.gameObject.tag == "Bullet")
        {

            enemyAudio.Play();
            FlashDamage();

            healthPoints -= tiro.pistolDamage;
            anim.SetTrigger("Hit");

        }

        if (other.gameObject.tag == "Ultimate")
        {
            enemyAudio.Play();

            healthPoints -= 50f;

        }

        

    }

     void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ultimate")
        {
            print("colidiu com enemy");
            enemyAudio.Play();

            healthPoints -= 50f;

        }

        if (collision.gameObject.tag == "Slow")
        {
            sprite.color = Color.blue;
            move.SlowEffect(0.5f);
            
            print("ficou Slow");
            

        }




    }

   
}
