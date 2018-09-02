using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransition : MonoBehaviour {

    bool movingPlayerToStartWalk;
    float moveDelayTime = 2.0f;
    float moveFinishTime = .5f;
    float levelLoadDelay = 5.0f;
    float moveStart;
    int moveStatus = 0;

    public PlayerController moveTarget;

    public Transform startMove;
    public Transform endMove;
    Vector3 PositionStart;
    


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (moveStatus == 1)
        {
            Debug.Log("status is 1 " + moveTarget.transform.position.x + " vs " + startMove.position.x);
            
            if (moveTarget.transform.position.x >= startMove.position.x)
            {
                Debug.Log("stopping force walk");
                moveStatus = 2;
                moveTarget.forceWalk = false;
                moveStart = Time.time;
                //play the level end tune if there is one
            }
        }
        else if (moveStatus == 2)
        {
            if (moveStart + moveDelayTime < Time.time)
            {
                moveStatus = 3;
                moveStart = Time.time;
                moveTarget.forceWalk = true;
            }
        }
        else if (moveStatus == 3)
        {
            if (moveStart + moveFinishTime < Time.time)
            {
                GameController.instance.PlaySoundClip(2);
                moveStatus = 4;
                moveStart = Time.time;
            }
        }
        else if (moveStatus == 4)
        {
            if (moveStart + levelLoadDelay < Time.time)
            {
                LoadNextLevel();
                moveStatus = 5;
            }
        }
		
	}

    public void LoadNextLevel()
    {
        GameController.nextScene();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("level end triggered by " + collision.gameObject.name);
        if (collision.GetComponent<PlayerController>())
        {
            Debug.Log("end scene tag player");
            PlayerController pControl = collision.GetComponent<PlayerController>();
            pControl.controllable = false;
            moveTarget = pControl;
            
            StartWalkToBegin();

        }
    }

    void StartWalkToBegin()
    {
        moveStart = Time.time;
        PositionStart = moveTarget.transform.position; //holdover in case we end up to the right of the point?
        moveTarget.forceWalk = true;
        moveStatus = 1;
    }

    

}
