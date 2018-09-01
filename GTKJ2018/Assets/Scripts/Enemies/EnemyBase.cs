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

    //Hit and hurtbox components
    public EnemyHurtBox myHurtBox;
    public EnemyHitBox enemyHitBox;

    //Get Animation Manager
    protected AnimationManager m_anim;

    // Use this for initialization
    protected virtual void Start () {
        //Get Animation Manager
        m_anim = GetComponent<AnimationManager>();

        //getBoxCollider
        m_col = GetComponent<BoxCollider2D>();

        //getBoxCollider
        m_rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
