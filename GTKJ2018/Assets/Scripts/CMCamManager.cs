using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CMCamManager : MonoBehaviour {

    public Cinemachine.CinemachineVirtualCameraBase cBase;
    public Cinemachine.CinemachineConfiner cConfine;

	// Use this for initialization
	void Start ()
    {
        //cBase.Follow = GameObject.FindObjectOfType<PlayerController>().transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetFollowTarget(Transform followTarget)
    {
        cBase.Follow = followTarget;
    }

    public void SetConfineTarget(PolygonCollider2D confineTarget)
    {
        //Debug.Log("setting confine target to " + confineTarget.name);
        cConfine.m_BoundingShape2D = confineTarget;
    }

    public bool HasConfiner()
    {
        if (cConfine.m_BoundingShape2D)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
