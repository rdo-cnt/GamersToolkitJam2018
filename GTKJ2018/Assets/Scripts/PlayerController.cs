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

public class PlayerController : MonoBehaviour {

    public float walkSpeed_max = 5; // max walking speed
    public float walkSpeed_acceleration = 15; // how fast you move towards max walking speed

    public float jump_forceInit; // initial kick (
    public float jump_forceHold; // force to add while holding the button
    public bool jump_airControl; // wether or not you can control momentum midair

    public LayerMask groundLayers; // layers that can be stood on and jumped off
    private bool isGrounded = true;

    private bool jumpInput; // getting the input in the update, to use it in fixed update

    private Rigidbody2D rb;
    private Collider2D colli;

    public PhysicsMaterial2D physMat_ground;
    public PhysicsMaterial2D physMat_air; // having no friction in the air keeps you from sliding on walls


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        colli = GetComponent<Collider2D>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButtonDown("Jump")) // getting jump input
        {
            jumpInput = true;
        }

	}

    private void FixedUpdate()
    {
        GroundCheck(); // check for ground before doing anything

        Walk(); // moves the player left/right

        if (jumpInput && isGrounded) // check for ground before jumping
            StartCoroutine(Jump());

        jumpInput = false; //clearing the input since it wasn't used
    }


    void GroundCheck()
    {
		//isGrounded = true;

        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 0.1f, Vector2.down, 0.5f, groundLayers); // send a circle down to check for ground

        if (hit)
        {
            isGrounded = true;
            colli.sharedMaterial = physMat_ground;
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

        float moveInput = Input.GetAxisRaw("Horizontal"); //grabing input

        if (rb.velocity.x < walkSpeed_max && moveInput > 0) //checks if you can move right
        {
            rb.AddForce(Vector2.right * walkSpeed_acceleration * Input.GetAxisRaw("Horizontal"));
        }
        if (rb.velocity.x > -walkSpeed_max && moveInput < 0) //checks if you can move left
        {
            rb.AddForce(Vector2.right * walkSpeed_acceleration * Input.GetAxisRaw("Horizontal"));
        }



    }

    IEnumerator Jump()
    {
        rb.AddForce(Vector2.up * jump_forceInit, ForceMode2D.Impulse); //add the initial jump kick


        while (Input.GetButton("Jump") && rb.velocity.y > 0) // while you hold jump, keeps adding the force upwards. Gives more height and a bit more air time.
        {
            rb.AddForce(Vector2.up * jump_forceHold);
            yield return new WaitForFixedUpdate();
        }

    }

}
