using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryRadar : MonoBehaviour {

    public CamRestriction camRestricter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            camRestricter.SetAsCollBound();
        }

    }
}
