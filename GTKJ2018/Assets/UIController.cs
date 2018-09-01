using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {

    public GameObject fullHeart;
    public GameObject brokenHeart;

    public Transform[] playerHeartContainers;
    public Transform[] enemyHeartContainers;

    public TMPro.TextMeshProUGUI scoreNumber;
    long scoreCopy; //main score in game controller: keep thru each level

    public int setVal = 0;
    public bool doSetVal = false;
    public bool doSetValEnemy = false;
    public bool doAddScore = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (doSetVal)
        {
            doSetVal = false;
            SetPlayerHearts(setVal);
        }
        if (doSetValEnemy)
        {
            doSetValEnemy = false;
            SetEnemyHearts(setVal);
        }
        if (doAddScore)
        {
            doAddScore = false;
            AddScoreNumber(setVal);
        }
    }

    public void SetScoreNumber(int setVal)
    {
        scoreCopy = setVal;
        SetScoreText();
    }

    public void AddScoreNumber(int addVal)
    {
        scoreCopy += addVal;
        SetScoreText();
    }

    void SetScoreText()
    {
        if (scoreCopy > 9999999999)
        {
            scoreCopy = 9999999999;
        }
        scoreNumber.text = scoreCopy.ToString().PadLeft(10, '0');
    }

    public void SetEnemyHearts(int setVal)
    {
        for (int i = 0; i < enemyHeartContainers.Length; i++)
        {
            List<Transform> children = new List<Transform>(); //clear sub hearts
            foreach (Transform t in enemyHeartContainers[i])
            {
                children.Add(t);
            }
            foreach (Transform t in children)
            {
                t.parent = null;
                Destroy(t.gameObject);
            }
            if (i < setVal)
            {
                GameObject newHeart = Instantiate(fullHeart, enemyHeartContainers[i].transform);
                newHeart.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                GameObject newHeart = Instantiate(brokenHeart, enemyHeartContainers[i].transform);
                newHeart.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }

    public void SetPlayerHearts(int setVal)
    {
        for (int i = 0; i < playerHeartContainers.Length; i++)
        {
            List<Transform> children = new List<Transform>(); //clear sub hearts
            foreach (Transform t in playerHeartContainers[i])
            {
                children.Add(t);
            }
            foreach(Transform t in children)
            {
                t.parent = null;
                Destroy(t.gameObject);
            }
            if (i < setVal)
            {
                GameObject newHeart = Instantiate(fullHeart, playerHeartContainers[i].transform);
                newHeart.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                GameObject newHeart = Instantiate(brokenHeart, playerHeartContainers[i].transform);
                newHeart.transform.localPosition = new Vector3(0, 0, 0);
            }
        }
    }
}
