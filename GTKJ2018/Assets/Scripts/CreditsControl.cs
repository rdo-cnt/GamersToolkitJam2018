using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsControl : MonoBehaviour {

    public AudioSource creditsPlayer;
    public AudioClip creditsClip;

    public float scrollSpeed = 10.0f;
    public Transform theEnd;


    bool isScrolling = true;

    float timeDelayReset = 10.0f;
    float timeDelayStart;


	// Use this for initialization
	void Start ()
    {
		if (creditsPlayer && creditsClip)
        {
            creditsPlayer.PlayOneShot(creditsClip);
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (theEnd.transform.position.y > Screen.height / 2.0f && isScrolling)
        {
            isScrolling = false;
            timeDelayStart = Time.time;
        }

        if (isScrolling)
        {
            this.transform.position += new Vector3(0, scrollSpeed * Time.deltaTime, 0);
        }
        else
        {
            
            if (timeDelayStart + timeDelayReset < Time.time)
            {
                
                GameController.levelIndex = -1;
                GameController.nextScene();
            }
        }
		
	}
}
