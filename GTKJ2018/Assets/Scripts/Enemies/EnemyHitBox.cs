using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBox : MonoBehaviour
{

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
        //if it hits the player
        if (other.GetComponent<PlayerController>() != null)
        {
            //if the hitbox deals damage at all
            if (damageDealt > 0)
            {
                //Put damage players function here
            }
        }
    }
}
