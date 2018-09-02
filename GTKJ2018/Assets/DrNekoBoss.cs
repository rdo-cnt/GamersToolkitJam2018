using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrNekoBoss : MonoBehaviour {

    public BossRoom bossRoom;
    public Transform[] movePositions;
    public GameObject laserPrefab;
    public Transform laserGunHole1;
    public Transform laserGunHole2;

    public ParticleSystem stunAnim;
    int currentHealth = 7;

    int targetMovePos;
    int moveState = 0;
    float moveSpeed = 1f;
    float moveLerp = 0.0f;
    Vector3 lastPosition;
    int moveCounter = 0;
    int moveNextShot = 3;

    float delayBeforeFire = 1.0f;
    float delayAfterFire = 1.0f;
    float delayWhenStun = 4.0f;

    float delayBeforeChainMoving = 1.5f;
    float delayBeforeStartMoving = 2.5f;

    public SpriteRenderer thisSpriteRenderer;
    float fadeInStepTimer = 1.0f;
    float fadeInStep = .2f;

    float currentLastTime;


    public AudioSource laserSound;
    public AudioSource damagedSound;
    public AudioSource damagedSound2;

	// Use this for initialization
	void Start ()
    {
        lastPosition = this.transform.position;
        thisSpriteRenderer.color = new Color(thisSpriteRenderer.color.r, thisSpriteRenderer.color.g, thisSpriteRenderer.color.b, 0.0f);
        currentLastTime = Time.time;
        moveLerp = 0.0f;
        stunAnim.Stop();
        stunAnim.gameObject.SetActive(false);
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (moveState == 0) //just starting out
        {
            if (currentLastTime + fadeInStepTimer < Time.time)
            {
                if (thisSpriteRenderer.color.a >= .99f)
                {
                    moveState = 1;
                    targetMovePos = Random.Range(0, movePositions.Length);
                    GameController.instance.levelController.uiControl.SetEnemyHearts(currentHealth);
                }
                else
                {
                    thisSpriteRenderer.color = new Color(thisSpriteRenderer.color.r, thisSpriteRenderer.color.g, thisSpriteRenderer.color.b, thisSpriteRenderer.color.a + fadeInStep);
                }
                currentLastTime = Time.time;

                
            }
        }
        if (moveState == 1)
        {
            moveLerp += moveSpeed * Time.deltaTime;
            
            this.transform.position = Vector3.Lerp(lastPosition, movePositions[targetMovePos].transform.position, moveLerp);
            if (lastPosition.x < movePositions[targetMovePos].transform.position.x)
            {
                this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            else
            {
                this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            if (moveLerp >= 1.0f)
            {
                
                moveLerp = 0.0f;
                moveCounter++;
                if (moveCounter >= moveNextShot)
                {
                    moveState = 3;
                }
                else
                {
                    lastPosition = this.transform.position;
                    List<int> moveOptions = new List<int>();
                    for (int i = 0; i < movePositions.Length; i++)
                    {
                        if (i != targetMovePos)
                        {
                            moveOptions.Add(i);
                        }
                    }
                    int newMovePos = moveOptions[Random.Range(0, moveOptions.Count)];
                    targetMovePos = newMovePos;
                    moveState = 2;
                    currentLastTime = Time.time;
                }
            }

        }
        else if (moveState == 2) //delayAfterMove
        {
            if (currentLastTime + delayBeforeChainMoving < Time.time)
            {
                lastPosition = this.transform.position;
                moveState = 1;
            }
        }
        else if (moveState == 3) //firing 1
        {
            if (currentLastTime + delayBeforeFire < Time.time)
            {
                moveState = 4;
                currentLastTime = Time.time;
            }
        }
        else if (moveState == 4) //firing 2
        {
            if (GameController.instance.levelController.playerObject.transform.position.x < this.transform.position.x)
            {
                this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            }
            else
            {
                this.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
            }
            FireLaser();
            moveState = 5;
            currentLastTime = Time.time;
        }
        else if (moveState == 5) //firing after
        {
            
            if (currentLastTime + delayAfterFire < Time.time)
            {
                moveState = 1;
                ResetMoves();
                
            }
        }
        else if (moveState == 6)// took damage
        {
            if (currentLastTime + delayWhenStun < Time.time)
            {
                ResetMoves();
                moveState = 1;
                stunAnim.Stop();
                stunAnim.gameObject.SetActive(false);
                TriggerNextEnemy();
            }
            
        }



		
	}

    void ResetMoves()
    {
        lastPosition = this.transform.position;
        moveLerp = 0.0f;
        moveCounter = 0;
        moveNextShot = Random.Range(2, 4);
    }

    void FireLaser()
    {
        laserSound.Play();
        GameObject newLaserShot = Instantiate(laserPrefab, laserGunHole1);
        newLaserShot.transform.localPosition = new Vector3(0, 0, 0);
        newLaserShot.transform.parent = null;
        if (this.transform.localScale.x <= 0.0f)
        {
            newLaserShot.GetComponent<Bullet>().FlipDirection();
        }
        if (currentHealth < 4)
        {
            GameObject newLaserShot2 = Instantiate(laserPrefab, laserGunHole2);
            newLaserShot2.transform.localPosition = new Vector3(0, 0, 0);
            newLaserShot2.transform.parent = null;
            if (this.transform.localScale.x <= 0.0f)
            {
                newLaserShot2.GetComponent<Bullet>().FlipDirection();
            }
        }
    }

    public void TakeDamage() //called when hit from laser reflection
    {
        damagedSound.Play();
        damagedSound2.Play();

        stunAnim.gameObject.SetActive(true);
        stunAnim.Play();
        currentLastTime = Time.time;
        moveState = 6;
        currentHealth--;
        GameController.instance.levelController.uiControl.AddScoreNumber(50);
        if (currentHealth == 0)
        {
            GameController.instance.levelController.uiControl.AddScoreNumber(5000);
        }
        GameController.instance.levelController.uiControl.SetEnemyHearts(currentHealth);
        

    }

    void TriggerNextEnemy()
    {
        bossRoom.TriggerNext(currentHealth);
    }




}
