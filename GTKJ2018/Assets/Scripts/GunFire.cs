using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : EnemyBase {


    public EnemySpawner mySpawner;
    public ParticleSystem main;
    public ParticleSystem secondary;
    public GameObject bulletPrefab;

    public GameObject explosionPrefab;
    public GameObject runnerPrefab;

    Transform target;

    public float lookDistance; // distance between the player and him before he starts shooting
    bool inRange;
    public float restingTime;
    bool isResting;

    public bool stopIt;
    public bool startIt;

    float firingStart;
    public float firingDelay = .50f;
    public float firingSpeed = .2f;
    bool isFireStarted;
    bool isFiringReal;
    int bulletCounter = 0;
    public int bulletMax = 6;
    bool hitByBullet;

    public AudioSource shootingSound;

	// Use this for initialization
	protected override void Start ()
    {
        //main.gameObject.SetActive(false);
        main.Stop();
        target = GameController.player.transform;

        secondary.Stop();
        m_rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        target = GameController.player.transform;
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

        MonitorShooting(); //ik's shooting timer script stuff

        if ( Mathf.Abs(target.position.x - transform.position.x) < lookDistance) // if player is closer than the lookDistance
            inRange = true;

        if (inRange && !isResting && !isFiringReal && !isFireStarted)
            startIt = true;

        if (stunned) //after hit, remember it
            hitByBullet = true;

        if (hitByBullet && !stunned) //after recovering from the stun, explode
            Explode();

	}

    void MonitorShooting()
    {
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


    public void Init()
    {
        stunned = false;
        hitByBullet = false;
        StopFiring();
        
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        Instantiate(runnerPrefab, transform.position, transform.rotation);
        //Destroy(gameObject);
        this.gameObject.SetActive(false);
        mySpawner.enemyDeployed = false;

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
        StartCoroutine(RestTimer());
        Debug.Log("stopping it");
        main.Stop();
        secondary.Stop();
        isFireStarted = false;
        isFiringReal = false;
        bulletCounter = 0;

    }

    IEnumerator RestTimer()
    {
        isResting = true;
        yield return new WaitForSeconds(restingTime);
        isResting = false;

    }

    void FireBullet()
    {
        shootingSound.Play();
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = this.transform.position; //position of gun here
    }
}
