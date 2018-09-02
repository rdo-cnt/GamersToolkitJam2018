using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRestriction : MonoBehaviour {

    public CMCamManager camMain;
    public PolygonCollider2D thisCollider;

    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public void SetAsCollBound()
    {
        camMain.SetConfineTarget(thisCollider);
    }


    /*
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            Debug.Log("exiting from " + this.gameObject.name);
            if (camMain.HasConfiner())
            {
                if (camMain.cConfine.m_BoundingShape2D == thisCollider)
                {
                    camMain.cConfine.m_BoundingShape2D = null;
                }
            }
        }
    }
    */
    
}
