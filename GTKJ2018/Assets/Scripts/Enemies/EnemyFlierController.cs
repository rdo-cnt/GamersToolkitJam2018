using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlierController : EnemyBase {

    //Might have parent spawner
    public EnemySpawner mySpawner;
    public float speed = 2f;
    public int direction = -1;
    public float distanceThreshold = 6f;
    public bool sinWaveMotion = true;
    float sinWaveMagnitude = .9f;
    float sinWaveSpeed = 1.52f;
    float sinLerp = 0.0f;
    int sinMovement = 0;
    Vector3 startPos;

    bool wasRegionDeployed = false;

    // Use this for initialization
    protected override void Start() {
        base.Start();

        //getBoxCollider
        m_col = GetComponent<BoxCollider2D>();

        //getBoxCollider
        m_rb = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        //set velocity
        m_rb.velocity = new Vector2(speed * direction, 0);

        //if I have a parent
        /* //old stuff
        if (mySpawner != null)
        {
            float n = Mathf.Abs(transform.position.x - mySpawner.transform.position.x);
            if(n > distanceThreshold)
                m_rb.velocity = new Vector2(-speed, speed);
            if(n>(distanceThreshold*2))
            {
                mySpawner.enemyDeployed = false;
                this.gameObject.SetActive(false);
            }
        }
        */
        //new stuff
        if (GameController.instance.levelController)
        {
            if (Vector3.Magnitude(this.transform.position - GameController.instance.levelController.playerObject.transform.position) > 11f) //simple as possible set spawn
            {
                if (mySpawner)
                {

                    this.gameObject.SetActive(false);
                    if (!wasRegionDeployed)
                    {
                        mySpawner.enemyDeployed = false;
                    }
                    
                }

            }
        }


    }

    private void FixedUpdate()
    {
        if (sinWaveMotion)
        {
            
            
            
            if (!stunned)
            {
                //whatIsGround = LayerMask.NameToLayer("Nothing");
                m_rb.gravityScale = 0.0f;
                sinLerp += sinMovement * sinWaveSpeed * Time.deltaTime;
                float sinLerpBase = Mathf.Sin(sinLerp);
                if (sinLerpBase >= 0)
                {
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(startPos.y, startPos.y + sinWaveMagnitude, sinLerpBase), this.transform.position.z);
                }
                else
                {
                    this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(startPos.y, startPos.y - sinWaveMagnitude, -sinLerpBase), this.transform.position.z);
                }
            }
            else
            {
                m_rb.gravityScale = 10.0f;
                startPos = this.transform.position;
                //whatIsGround = LayerMask.NameToLayer("Ground");
            }
 
        }
        else
        {
            float n = Mathf.Abs(transform.position.x - mySpawner.transform.position.x);
            if (n > distanceThreshold)
                m_rb.velocity = new Vector2(-speed, speed);
            if (n > (distanceThreshold * 2))
            {
                mySpawner.enemyDeployed = false;
                this.gameObject.SetActive(false);
            }
        }
    }

    public void InitFromRegion()
    {
        wasRegionDeployed = true;
        if (GameController.instance.levelController)
        {
            if (GameController.instance.levelController.playerObject.GetComponent<SpriteRenderer>().flipX)
            {
                this.transform.position = GameController.instance.levelController.playerObject.transform.position + new Vector3(-10f, 0, 0);
            }
            else
            {
                this.transform.position = GameController.instance.levelController.playerObject.transform.position + new Vector3(+10f, 0, 0);
            }
            if (GameController.instance.levelController.playerObject.transform.position.x < this.transform.position.x)
            {
                changeDirectionLeft(true);
            }
            else
            {
                changeDirectionLeft(false);
            }
        }
        startPos = this.transform.position;
        stunned = false;
        stunAnim.Stop();
        stunAnim.gameObject.SetActive(false);
        if (sinWaveMotion)
        {
            sinMovement = 1;
            sinLerp = Random.Range(0.0f, 2.0f);
        }
    }

    public void InitMovement()
    {
        if (GameController.instance.levelController)
        {
            if (GameController.instance.levelController.playerObject.transform.position.x < this.transform.position.x)
            {
                changeDirectionLeft(true);
            }
            else
            {
                changeDirectionLeft(false);
            }
        }
        startPos = this.transform.position;
        stunned = false;
        stunAnim.Stop();
        stunAnim.gameObject.SetActive(false);
        if (sinWaveMotion)
        {
            sinMovement = 1;
            sinLerp = Random.Range(0.0f, 2.0f);
        }
    }

    public void changeDirectionLeft(bool faceLeft)
    {
        if (faceLeft == false)
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        }
    }
}
