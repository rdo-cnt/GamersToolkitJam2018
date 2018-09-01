using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMCamManager : MonoBehaviour {

    public Cinemachine.CinemachineVirtualCameraBase cBase;

	// Use this for initialization
	void Start ()
    {
        cBase.Follow = GameObject.FindObjectOfType<PlayerController>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetFollowTarget(Transform followTarget)
    {
        cBase.Follow = followTarget;
    }
}
