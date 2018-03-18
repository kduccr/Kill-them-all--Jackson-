using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tiro : MonoBehaviour {

    public int ammoRaygun = 100;
    public int ammoAK;

    float timer;
    public float spellSpeed;
    public GameObject rayShoot;
    
    public GameObject bulletPistol;
    public GameObject bulletAK;
    public GameObject bulletGL;
    public GameObject shotgunBulletPrefab;
    public GameObject ultimateBomb;
    public GameObject slowSpell;
    public GameObject startPos;
    public Transform[] shotgunStartPos;
    public Transform[] vectorStartPos;
    public GameObject ultPos;
    [SerializeField] float timeBetweenAttacks;
    float timeBetweenAttacksUlt = 3.4f;
    public Transform[] spawnPoints;
    Transform trans;
    public AudioClip rayGunClip;
    public AudioClip gunClip;
    public AudioClip pistolClip;
    public AudioClip explosionClip;
    public AudioClip[] noManaClip;
    AudioSource gunAudio;
    public bool isParticle;
    public float tempoUlt = 2.5f;

    public bool playKame;
    public bool playAk;
    public bool playIce;
    bool isUlt;
    bool isUlting;

    public float pistolDamage = 1;
    public float akDamage = .85f;
    public float rayGunDamage = 2;


    

    ParticleSystem gunParticles;

    Animator anim;


    public float flashSpeed = 5f;
    public Color ultimateFlashColor;
    public Color iceSlowFlashColor;
    public Image damageImage;

    
    PlayerHealth playerHealth;
    Player player;
    WeaponSwitch weapon;

    GameObject jogador;
    
    

    public GameObject audioSource;
    GameObject playerVida;

    void UltimateFlash()
    {
        damageImage.color = ultimateFlashColor;
    }

    void IceSlowFlash()
    {
        damageImage.color = iceSlowFlashColor;
        //damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
    }

    void IceSlow()
    {
        Invoke("IceSlowFlash", 2.4f);
        timer = 0f;
        GameObject slow = (GameObject)Instantiate(slowSpell, ultPos.transform.position, trans.rotation);
        playerHealth.currentEnergy -= 15;
        playerHealth.energySlider.value = playerHealth.currentEnergy;
        //Invoke("StopIceSlow", 0f);

    }



    void Ultimate()
    {
        InvokeRepeating("UltimateFlash", 0f, 0.1f);
        damageImage.color = ultimateFlashColor;
        isUlt = true;
        playerHealth.currentEnergy = 0;
        playerHealth.energySlider.value = playerHealth.currentEnergy;
        GameObject ultimate = (GameObject)Instantiate(ultimateBomb, ultPos.transform.position, trans.rotation);
        Invoke("StopUltimate", tempoUlt);

        Destroy(ultimate, 6);
    }

    void StopUltimate()
    {
        isUlting = false;
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        gunParticles.Stop();

        isUlt = false;
        CancelInvoke("UltimateFlash");

    }

    void Start () {

        jogador = GameObject.FindGameObjectWithTag("Player");
        weapon = jogador.GetComponentInChildren<WeaponSwitch>();
		//spawnPoints = GetComponent<Transform> ();
        player = GetComponent<Player>();
        trans = GetComponent<Transform>();
        gunAudio = GetComponent<AudioSource>();
        
        
        
        isUlt = false;
        anim = GetComponent<Animator>();
        gunParticles = GetComponent<ParticleSystem>();
        


    }

 
	
	// Update is called once per frame
	void Update () {

        UpdateAmmoCanvas();
        playerVida = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerVida.GetComponent<PlayerHealth>();
       

        if (playerHealth.currentHealth <= 0)
        {
            gunAudio.mute = true;
        }
        else
        {
            gunAudio.mute = false;
        }

        timer += Time.deltaTime;

        if (timer >= 0.8f && Input.GetMouseButtonDown(0) && ammoRaygun >=1 && !isUlting && weapon.showRayGun == true )
        {
            
            TiroRayGun();
        }

        if (timer >= 0.8f && Input.GetMouseButtonDown(0) && ammoRaygun >= 1 && !isUlting && weapon.showShotGun == true)
        {

            TiroShotGun();
        }
        else if (timer >= 0.05 && Input.GetMouseButton(0) && weapon.showVector == true && ammoAK >= 1 && !isUlting)
        {

            TiroVector();
        }

        if (timer >= timeBetweenAttacks && Input.GetKeyDown(KeyCode.E) && playerHealth.currentEnergy >=15)
        {
            playIce = true;
            IceSlow();
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            playIce = false;
        }

        if (timer >= 0.5f && Input.GetMouseButton(0) && weapon.showPistol == true)
        {
            
            TiroPistol();
        }

        if (timer >= 0.7f && Input.GetMouseButton(0) && weapon.showGL == true)
        {

            TiroGl();
        }

        else if (timer >= timeBetweenAttacks && Input.GetMouseButton(0) && weapon.showAK == true && ammoAK >= 1 && !isUlting)
        {
            
            TiroAK();
            
        }

       if (timer >= timeBetweenAttacks && Input.GetKeyDown(KeyCode.R) && playerHealth.currentEnergy >= 100 && !isUlt)
        {
            isUlting = true;
            playKame = true;
            anim.SetTrigger("Ultimate");

            Invoke("Ultimate", 2.5f);
            //Ultimate();
            
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            playKame = false;
        }
       
        if (playerHealth.currentEnergy < 90 && Input.GetKeyDown(KeyCode.R) )
        {
            int spawnPointIndex = Random.Range(0, noManaClip.Length);

            gunAudio.clip = noManaClip[spawnPointIndex];
            
            gunAudio.Play();
        }
        
    }
    public void TiroPistol()
    {
        timer = 0f;
        UpdateAmmoCanvas();
        gunAudio.PlayOneShot(pistolClip, 0.25f);
        int spawnPointIndex = Random.Range(0, spawnPoints.Length);
        GameObject tiroPistol = (GameObject)Instantiate(bulletPistol, startPos.transform.position, startPos.transform.rotation);
        //GameObject tiroPistol = (GameObject)Instantiate(bulletPistol, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
        Destroy(tiroPistol, 3);
    }

    public void TiroShotGun()
    {
        for (int y = 0; y < shotgunStartPos.Length; y++)
        {

            GameObject tiroPistol = (GameObject)Instantiate(shotgunBulletPrefab, shotgunStartPos[y].position, shotgunStartPos[y].rotation);
            Destroy(tiroPistol.gameObject, 8f);


        }
    }
    public void TiroVector()
    {
        timer = 0f;
        int spawnPointIndex = Random.Range(0, vectorStartPos.Length);
        GameObject tiroPistol = (GameObject)Instantiate(bulletPistol, vectorStartPos[spawnPointIndex].position, vectorStartPos[spawnPointIndex].rotation);
        Destroy(tiroPistol, 3);
    }

    public void TiroGl()
    {
        timer = 0f;
       
        GameObject novoSpell = (GameObject)Instantiate(bulletGL, startPos.transform.position, startPos.transform.rotation);

        Destroy(novoSpell, 3);

        
    }

    public void TiroAK()
    {

        
        
        timer = 0f;
        ammoAK--;

        gunAudio.PlayOneShot(gunClip, 0.65f);
        GameObject tiroAK = (GameObject)Instantiate(bulletAK, startPos.transform.position, startPos.transform.rotation);



        Destroy(tiroAK, 3);
    }

    void TiroRayGun()
    {
        ammoRaygun--;
        timer = 0;
        gunAudio.clip = rayGunClip;
        gunAudio.Play();
        GameObject novoSpell = (GameObject)Instantiate(rayShoot, startPos.transform.position, playerVida.transform.rotation);
        //novoSpell.GetComponent<Rigidbody2D>().AddForce(playerVida.transform.up *spellSpeed);
        novoSpell.GetComponent<Rigidbody2D>().velocity = playerVida.transform.up * spellSpeed;
        Destroy(novoSpell, 2f);
    }

    void UpdateAmmoCanvas()
    {
        if (weapon.showAK == true)
        {
            ScoreManager.score = ammoAK;
        }
        if (weapon.showRayGun == true)
        {
            ScoreManager.score = ammoRaygun;
        }
        
    }

    

   



}

