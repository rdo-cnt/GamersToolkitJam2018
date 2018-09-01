using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float bulletSpeed = 6f;
    public Vector3 moveDirection = new Vector3(-1, 0, 0);
    public bool isReflected = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void FixedUpdate()
    {
        this.transform.position += moveDirection * Time.deltaTime * bulletSpeed;
    }

    public void ReflectBullet()
    {
        if (!isReflected)
        {
            isReflected = true;
            moveDirection *= -1;
        }
    }

    

}
