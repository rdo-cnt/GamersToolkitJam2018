using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour {

    public ParticleSystem main;
    public ParticleSystem secondary;
    public GameObject bulletPrefab;

    public bool stopIt;
    public bool startIt;

    float firingStart;
    float firingDelay = .50f;
    float firingSpeed = .2f;
    bool isFireStarted;
    bool isFiringReal;
    int bulletCounter = 0;
    int bulletMax = 6;

	// Use this for initialization
	void Start ()
    {
        //main.gameObject.SetActive(false);
        main.Stop();
        secondary.Stop();
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (stopIt)
        {
            stopIt = false;
            StopFiring();
        }
        if (startIt)
        {
            startIt = false;
            StartFiring();
        }
        
        if (isFireStarted)
        {
            if (firingStart + firingDelay < Time.time && !isFiringReal)
            {
                Debug.Log("setting firing real");
                isFiringReal = true;
                firingStart = Time.time;
            }
            else if (isFiringReal)
            {
                if (firingStart + firingSpeed < Time.time)
                {
                    firingStart = Time.time;
                    if (bulletCounter < bulletMax)
                    {
                        FireBullet();
                        bulletCounter++;
                    }
                    else
                    {
                        StopFiring();
                    }
                    
                }
            }
        }
        

	}

    void StartFiring()
    {
        main.Play();
        secondary.Play();
        isFireStarted = true;
        isFiringReal = false;
        firingStart = Time.time;
    }

    void StopFiring()
    {
        Debug.Log("stopping it");
        main.Stop();
        secondary.Stop();
        isFireStarted = false;
        isFiringReal = false;
        bulletCounter = 0;
    }

    void FireBullet()
    {
        GameObject newBullet = Instantiate(bulletPrefab);
        bulletPrefab.transform.position = this.transform.position; //position of gun here
    }
}
