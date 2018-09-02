using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{
   
    public bool destroyOnHit = false;
    //damage variable
    public int damageDealt = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool playerTest = true;
        bool enemyTest = false;
        if (this.transform.parent.GetComponent<Bullet>()) //gonna be ugly here but make it so
        {
            Bullet bullet = this.transform.parent.GetComponent<Bullet>();
            if (bullet.isReflected)
            {
                //test for enemy hits
                enemyTest = true;
                playerTest = false;

            }
            else
            {
                enemyTest = false;
                playerTest = true;
                //test for player hits
            }
        }
        //otherwise
        if (playerTest)
        {
            if (other.GetComponent<PlayerController>() != null)
            {
                Debug.Log("trigger enter on player" + gameObject.name);
                //if the hitbox deals damage at all
                if (damageDealt > 0)
                {
                    //Put damage players function here
                    
                    other.GetComponent<PlayerController>().TakeDamage(damageDealt);
                }
                if (destroyOnHit)
                {
                    Destroy(this.transform.parent.gameObject); //hitbox always child direct to main object root
                }
            }
        }
        if (enemyTest)
        {
            if (other.GetComponent<EnemyBase>() != null)
            {
                Debug.Log("trigger enter on enemy" + gameObject.name);
                //if the hitbox deals damage at all
                if (damageDealt > 0)
                {
                    //Put damage players function here

                    other.GetComponent<EnemyBase>().OnParried(this.transform);
                }
                if (destroyOnHit)
                {
                    Destroy(this.transform.parent.gameObject); //hitbox always child direct to main object root
                }
            }
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision entering");
        if (collision.gameObject.name == "ForeGround")
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
