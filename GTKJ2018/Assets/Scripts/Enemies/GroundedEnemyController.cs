using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedEnemyController : EnemyBase
{


    //Variable speed
    public float speed = 4f;

    //Directional Variables
    public bool bFacingLeft;
    protected float fRaycastDistance = 0.18f;
    protected float fRaycastOffset = 0.80f;
    public LayerMask whatIsGround;
    protected bool bRightSideTouch;
    protected bool bLeftSideTouch;



    // Use this for initialization
    protected override void Start () {
        base.Start();
        
        m_rb.velocity = new Vector2(-speed, 0);
        changeDirectionLeft(true);

    }
	
	// Update is called once per frame
	void Update () {

        //Collisions
        bRightSideTouch = isSided(1.3f);
        bLeftSideTouch = isSided(-1.1f);

        if(bLeftSideTouch)
        {
            changeDirectionLeft(false);
        }
        if (bRightSideTouch)
        {
            changeDirectionLeft(true);
        }

        Movement();

    }

    void Movement()
    {
        if (bFacingLeft)
        {
            m_rb.velocity = new Vector2(-speed, m_rb.velocity.y);
        }
        else
        {
            m_rb.velocity = new Vector2(speed, m_rb.velocity.y);
        }
    }

    //Check for player position
    void checkDirection()
    {
        
    }

    public bool isSided(float offsetX)
    {
        //get ends of collision box
        Vector2 min = m_col.bounds.min;
        Vector2 max = m_col.bounds.max;

        //set offset for better precision on raycast positioning
        min.y += 0.1f;
        max.y -= 0.1f;

        //determines on which side to put the raycast
        if (offsetX > 0)
            min.x = m_col.bounds.max.x;
        else
            max.x = m_col.bounds.min.x;

        //determines the width and starting position of the raycast
        Vector2 originLine = max - min;
        Vector2 center = min + originLine * 0.5f;

        Vector2 centerTemp = center;
        centerTemp.y += (max.y - min.y) / 3;

        //center.y += originLine.y * raycastOffsetY;
        //Create line that shoots downwards from the feet of our unit
        RaycastHit2D hitInfo = Physics2D.Raycast(centerTemp, new Vector2(offsetX, 0), fRaycastDistance, whatIsGround);

        Vector2 centerTemp2 = center;
        centerTemp2.y -= (max.y - min.y) / 4;

        RaycastHit2D hitInfo2 = Physics2D.Raycast(centerTemp2, new Vector2(offsetX, 0), fRaycastDistance, whatIsGround);

        //if there was no collider
        if (hitInfo.collider == null || hitInfo2.collider == null)
        {
            //this will draw a line in our screen, similar to the raycast
            Debug.DrawRay(centerTemp, new Vector2(fRaycastDistance * (Mathf.Abs(offsetX) / offsetX), 0), Color.green);
            Debug.DrawRay(centerTemp2, new Vector2(fRaycastDistance * (Mathf.Abs(offsetX) / offsetX), 0), Color.green);
            //transform.parent = null;
            return false;
        }

        Debug.DrawRay(centerTemp, new Vector2(fRaycastDistance * (Mathf.Abs(offsetX) / offsetX), 0), Color.red);
        Debug.DrawRay(centerTemp2, new Vector2(fRaycastDistance * (Mathf.Abs(offsetX) / offsetX), 0), Color.red);


        return true;
    }

    //Function moves facing direction to left if the bool is true. otherwise it turns to the right
    void changeDirectionLeft(bool isLeft)
    {
        
        bFacingLeft = isLeft;
        if (isLeft)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.z); //Make it look to the left
        }
        else
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.lossyScale.x), transform.lossyScale.y, transform.lossyScale.z); //Make it look to the right
        }
    }


}
