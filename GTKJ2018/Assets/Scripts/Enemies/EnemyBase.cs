using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    //Variables
    public bool canBeHurt = false;
    public int health = 1;

    //Get Animation Manager
    protected AnimationManager m_anim;

    // Use this for initialization
    void Start () {
        //Get Animation Manager
        m_anim = GetComponent<AnimationManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
