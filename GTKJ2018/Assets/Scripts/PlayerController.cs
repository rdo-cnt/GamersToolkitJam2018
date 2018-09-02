/*
 * GMTK Jam 2018
 * MintFox
 * Sigrath
 * Basinga
 * Cuurian
 * Ikkir
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool controllable = true;
    public bool forceWalk = false; //used by level endings
    public int currentHealth = 7;
    public int maxHealth = 7;
    bool isDead = false;
    public AudioSource damageSound;
    

    public float walkSpeed_max = 5; // max walking speed
    public float walkSpeed_acceleration = 15; // how fast you move towards max walking speed

    public float jump_forceInit; // initial kick (
    public float jump_forceHold; // force to add while holding the button
    public bool jump_airControl; // wether or not you can control momentum midair

    public LayerMask groundLayers; // layers that can be stood on and jumped off
    private bool isGrounded = true;

    private bool jumpInput; // getting the input in the update, to use it in fixed update
    private bool parryInput;

    private bool isParrying;
    private bool parry_blocked = false; //when enabled, you can't parry

    public float parryRadius; //radius of where your parry effects
    public float parryTime; //how long does the parry last?
    public float parryBounceForce; //how high can you bounce off enemies?
    public LayerMask parryLayers; // what can you parry?
    //public Vector2 parryOffset; // how far forward is the hitbox
    //public Vector2 parryHitboxSize; // how big is the hitbox
    public AudioSource parrySoundEffect; //sound to play when parrying
    public float parry_missCooldown = 0.5f; //punishement for missing parry
    private bool parry_inAir; // check if the parry was done in the air or not

    private Rigidbody2D rb; //components we'll be messing with
    private Collider2D colli;
    private SpriteRenderer sprite;

    public PhysicsMaterial2D physMat_ground;
    public PhysicsMaterial2D physMat_air; // having no friction in the air keeps you from sliding on walls

    float invincibilityTimer = .5f;
    float invincibilityStart;
    bool isInvincible;


    // Use this for initialization
    void Awake()
    {
        GameController.setPlayerReference(this); // set self as player reference to the game controller
        rb = GetComponent<Rigidbody2D>(); //grabbing your components
        colli = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump")) // getting jump input
        {
            jumpInput = true;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            parryInput = true;
        }

        //if (isInvincible)
        //{
        //    if (invincibilityStart + invincibilityTimer < Time.time)
        //    {
        //        SetInvincible(false);
        //    }
        //}

    }

    private void FixedUpdate()
    {
        GroundCheck(); // check for ground before doing anything

        Walk(); // moves the player left/right

        if (jumpInput && isGrounded) // check for ground before jumping
            StartCoroutine(Jump());

        if (parryInput && !parry_blocked && !isParrying)
            StartCoroutine(Parry());


        jumpInput = false; //clearing the input since it wasn't used
        parryInput = false;
    }

    void OnDrawGizmos()
    {

        //Gizmos.color = Color.blue;
        //if (isParrying)
        //{
        //    if (sprite.flipX == false)
        //        Gizmos.DrawCube(transform.position + (Vector3)parryOffset, parryHitboxSize);
        //    else
        //        Gizmos.DrawCube(transform.position - (Vector3)parryOffset, parryHitboxSize);

        //}

    }


    IEnumerator Parry()
    {
        isParrying = true; //make sure you can't parry while you parry
        float startTime = Time.time; //grabbing the time to compare to later
        parrySoundEffect.Play();

        if (!isGrounded)
            parry_inAir = true;
        else
            parry_inAir = false;

        bool hitSomething = false;

        while (Time.time - startTime < parryTime) //loop this for <parryTime> seconds
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, parryRadius, parryLayers); //grabbing everything hit

            //Collider2D[] hits = new Collider2D[0];

            //if (sprite.flipX == true) // parry either left or right, depending on the sprite's direction
            //    hits = Physics2D.OverlapBoxAll(rb.position - parryOffset, parryHitboxSize, 0, parryLayers);
            //else
            //    hits = Physics2D.OverlapBoxAll(rb.position + parryOffset, parryHitboxSize, 0, parryLayers);

            //Collider2D[] underHits = new Collider2D[0];

            //if (!isGrounded)
            //    underHits = Physics2D.OverlapBoxAll(rb.position + Vector2.down * 0.5f, new Vector2(1, 0.25f), 0, parryLayers); // small hitbox under the player just for bouncing

            //if (underHits.Length > 0)
            //{
            //    ParryBounce();
            //}


            if (hits.Length > 0) // if you parry anything
            {
                hitSomething = true;

                foreach (Collider2D col in hits)
                {
                    Debug.Log("hits at " + col.gameObject.name);
                    EnemyBase enemy = col.GetComponent<EnemyBase>();
                    if (enemy != null)
                    {
                        Debug.Log(col.name);
                        enemy.OnParried(transform);
                    }
                    Bullet bullet = col.GetComponent<Bullet>();
                    if (bullet != null)
                    {
                        Debug.Log("reflecting bullet");
                        bullet.ReflectBullet();
                    }
                }

                if (!isGrounded) // and you're not on the ground
                {

                    ParryBounce(); //bounce off of what you parried
                }
                isParrying = false;
                yield break;
            }
            

            yield return new WaitForFixedUpdate(); // looping on fixed update for more consistency

        }
        isParrying = false;

        if (!parry_inAir && !hitSomething)
        {
            parry_blocked = true;
            yield return new WaitForSeconds(parry_missCooldown);
        }
        parry_blocked = false;
    }

    void ParryBounce()
    {
        Vector2 newVel = rb.velocity;
        newVel.y = 0;
        rb.velocity = newVel;


        rb.AddForce(Vector2.up * parryBounceForce, ForceMode2D.Impulse);

    }

    void GroundCheck()
    {
        //isGrounded = true;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down, 0.5f, groundLayers); // send a circle down to check for ground

        if (hit && colli.IsTouchingLayers(groundLayers))
        {
            isGrounded = true;
            colli.sharedMaterial = physMat_ground; // keeps you from sliding on the floor
        }
        else
        {
            isGrounded = false;
            colli.sharedMaterial = physMat_air; // keeps you from sticking to walls
        }
    }


    void Walk()
    {

        if (!isGrounded && !jump_airControl) // if we have no air control, do not move in the air
            return;

        if (isGrounded && isParrying && !parry_inAir)
            return;

        float moveInput = Input.GetAxisRaw("Horizontal"); //grabing input
        if (isDead || !controllable) //cant move while dead or has no control.
        {
            moveInput = 0;
        }
        if (forceWalk)
        {
            moveInput = 1.0f;
        }
        if (rb.velocity.x < walkSpeed_max && moveInput > 0) //checks if you can move right
        {
            rb.AddForce(Vector2.right * walkSpeed_acceleration * moveInput);
            sprite.flipX = false; //don't flip around
        }
        if (rb.velocity.x > -walkSpeed_max && moveInput < 0) //checks if you can move left
        {
            rb.AddForce(Vector2.right * walkSpeed_acceleration * moveInput);
            sprite.flipX = true; //flip around
        }



    }

    IEnumerator Jump()
    {

        Vector2 newVel = rb.velocity;
        if (rb.velocity.y < 0)
            newVel.y = 0;

        rb.velocity = newVel;

        rb.AddForce(Vector2.up * jump_forceInit, ForceMode2D.Impulse); //add the initial jump kick


        while (Input.GetButton("Jump") && rb.velocity.y > 0) // while you hold jump, keeps adding the force upwards. Gives more height and a bit more air time.
        {
            rb.AddForce(Vector2.up * jump_forceHold);
            yield return new WaitForFixedUpdate();
        }

    }

    void SetInvincible(bool status)
    {
        isInvincible = status;
        
        this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        if (isInvincible)
        {
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            StartCoroutine(StartInvincibility());
        }
        
    }

    public IEnumerator StartInvincibility()
    {
        yield return new WaitForSeconds(invincibilityTimer);
        SetInvincible(false);
    }

    public void Die()
    {
        Debug.Log("got player die");
        GameController.instance.PlaySoundClip(0);
        GameController.instance.levelController.KillPlayer();
        isDead = true;
        
    }

    public void TakeDamage(int damage)
    {
        if (!isDead && !isInvincible)
        {
            SetInvincible(true);
            currentHealth -= damage;

            damageSound.Play();

            if (currentHealth < 0 || currentHealth >= maxHealth)
            {
                currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
                
            }
            GameController.instance.levelController.uiControl.SetPlayerHearts(currentHealth);
            if (currentHealth <= 0)
            {
                Die();
            }
        }
        
    }

    

}
