using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject playerObject;
    public Transform spawnLocation;
    public CMCamManager camManager;
    public UIController uiControl;

    public bool setCamOffset = false;
    public Vector2 offset;

    public float playerRespawnTime = 3.0f;
    float playerRespawnStart;
    bool isRespawning = false;

	// Use this for initialization
	void Awake ()
    {
		if (!playerObject)
        {
            SpawnPlayerAtStart();
        }
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isRespawning)
        {
            
            if (playerRespawnStart + playerRespawnTime < Time.time)
            {
                Destroy(playerObject);
                playerObject = null;
                SpawnPlayerAtStart();
                isRespawning = false;
            }
        }
		
	}

    public void SpawnPlayerAtStart()
    {
        if (!playerObject && playerPrefab)
        {
            playerObject = GameObject.Instantiate(playerPrefab);
            GameObject camOffsetObject = new GameObject();

           

            playerObject.transform.position = spawnLocation.position;
            camManager.SetFollowTarget(playerObject.transform);
            uiControl.SetPlayerHearts(7);

            if (setCamOffset)
            {
                camOffsetObject.transform.parent = playerObject.transform;
                camOffsetObject.transform.position = playerObject.transform.position + (Vector3)offset;
                camManager.SetFollowTarget(camOffsetObject.transform);

            }
            else
                Destroy(camOffsetObject);
        }

        
        
    }

    public void KillPlayer() //plays sound, sets player character animation to die, respawn after set time.
    {
        //play sound
        //set player animation
        //set wait time.
        isRespawning = true;
        playerRespawnStart = Time.time;
    }



}
