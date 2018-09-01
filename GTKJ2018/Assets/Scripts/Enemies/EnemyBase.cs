using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    //Variables
    public bool canBeHurt = false;
    public int health = 1;

    //Animation Components
    protected SpriteRenderer m_sr;
    protected Animator m_anim;
    AnimatorStateInfo currentState;
    protected float playbackTime;

    // Use this for initialization
    void Start () {
        m_anim = GetComponent<Animator>();
        m_sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
