﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemyController : MonoBehaviour {

    //Get Collision Components
    protected Rigidbody2D m_rb;
    protected BoxCollider2D m_col;

    public bool bFacingLeft;


    // Use this for initialization
    void Start () {

        //getBoxCollider
        m_col = GetComponent<BoxCollider2D>();

        //getBoxCollider
        m_rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Check for player position
    void checkDirection()
    {
        
    }
}