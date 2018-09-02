using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    //Get Collision Components
    protected Rigidbody2D m_rb;
    protected BoxCollider2D m_col;

    //Variables
    public bool canBeHurt = false;
    public bool stunned = false;
    public float stunTimer;
    public Vector2 knockbackForce;

    //Hit and hurtbox components
    public EnemyHurtBox myHurtBox;
    public EnemyHitBox enemyHitBox;

    //Get Animation Manager
    protected AnimationManager m_anim;

    //stun graphic
    public ParticleSystem stunAnim;

    // Use this for initialization
    protected virtual void Start () {
        //Get Animation Manager
        m_anim = GetComponent<AnimationManager>();

        //getBoxCollider
        m_col = GetComponent<BoxCollider2D>();

        //getBoxCollider
        m_rb = GetComponent<Rigidbody2D>();
    }

    //parry behavior
    public void OnParried(Transform attackSource)
    {
        if (stunned)
            return;

        StartCoroutine(StunTimer());

        m_rb.velocity = Vector2.zero;

        //knockback based on direction the attack came from
        m_rb.AddForce(new Vector2(knockbackForce.x * Mathf.Sign(transform.position.x - attackSource.position.x), knockbackForce.y), ForceMode2D.Impulse);


    }

    IEnumerator StunTimer()
    {
        stunned = true;
        stunAnim.gameObject.SetActive(true);
        stunAnim.Play();
        float startTime = Time.time;
        while (Time.time - startTime < stunTimer)
        {
            yield return new WaitForFixedUpdate();
        }
        stunned = false;
        stunAnim.Stop();
        stunAnim.gameObject.SetActive(false);
        

    }

}
