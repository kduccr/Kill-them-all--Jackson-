using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    
	[SerializeField]float velocidade = 6f;
    float currentStamina;
    [SerializeField] float maxStamina = 100;
    [SerializeField] float timeToFullStamina;
    float velocidadeOriginal;
    [SerializeField] float velocidadeDash;
    float tempoDash = 0.2f;
    bool estaDandoDash;
	float moveX;
	float moveY;
	Rigidbody2D rigid;
    Animator anim;
    AudioSource playerAudio;
    public AudioClip dashClip;
    public GameObject poisonEffect;
    public Slider staminaSlider;

    
    
    PlayerHealth playerHealth;
    
   
    
    public bool isSlow;
    bool canDash = true;




    void Start () {

        currentStamina = maxStamina;
        velocidadeOriginal = velocidade;
		rigid = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        
        playerHealth = GetComponent<PlayerHealth>();
        
        


    }
    void StopDash()
    {
        estaDandoDash = false;
        velocidade = velocidadeOriginal;
        //colli.enabled = true;
        playerHealth.damageable = true;
        Invoke("FillStamina", timeToFullStamina);

    }
    void FillStamina()
    {
        currentStamina = 100;
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && !estaDandoDash && canDash && currentStamina == 100)
        {
            playerHealth.damageable = false;
            //colli.enabled = false;
            anim.SetTrigger("EstaDandoDash");
            estaDandoDash = true;
            velocidade = velocidadeDash;
            Invoke("StopDash", tempoDash);
            playerAudio.clip = dashClip;
            playerAudio.Play();
            currentStamina = 0;
        }
    }
	void Update () {

        staminaSlider.value = Mathf.Lerp(staminaSlider.value, currentStamina, Time.deltaTime * 3);
        Dash();
        
        moveX = Input.GetAxisRaw ("Horizontal");

		moveY = Input.GetAxisRaw ("Vertical");
        rigid.velocity = new Vector2(moveX * velocidade, moveY * velocidade) ;
        rigid.velocity.Normalize();

        /*
        if (moveX != 0)
        {
            anim.SetFloat(("Horizontal"), Mathf.Abs(moveX));
        }
        else if (moveX == 0)
        {
            anim.SetFloat(("Horizontal"),0);
        }
        if (moveY != 0)
        {
            anim.SetFloat(("Vertical"), Mathf.Abs(moveY));
        }
        else if (moveY == 0)
        {
            anim.SetFloat(("Vertical"),0);
        }
        */
	}
    public void SlowEffect(float poisonSlow)
    {
        isSlow = true;
        canDash = false;
        //Instantiate(poisonEffect, posionPos.transform.position, transform.rotation);
        velocidade = poisonSlow;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        Invoke("ReturnToNormal", 3f);


    }
    void ReturnToNormal()
    {
        canDash = true;
        isSlow = false;
        velocidade = velocidadeOriginal;
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

}
