using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCreator : MonoBehaviour
{
    // Start is called before the first frame update
    public bool laserEnabled = false;
    public bool laserAvailable = false;
    bool lineMade = false;
    public float laserRange = 5f;
    public LayerMask laserLayerMask;
    bool laserTimerRunning = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForBallsInRange();
        InputTesting();
    
    }

    public void ToggleLaser(bool val)
    {
        laserAvailable = val;
        if(val == true)
        {
            GetComponent<SpriteRenderer>().color = Color.blue;

        } else
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    

    void InputTesting()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            ToggleLaser(true);
        }
    }

    void CheckForBallsInRange()
    {
        
        if (laserAvailable)
        {
            
            GameObject closestBall = FindClosestBall();
            if (closestBall != null)
            {
                if (Vector2.Distance(closestBall.transform.position, transform.position) < laserRange)
                {
                   
                    //disable other ones laser so it doesnt overlap lasers
                    //closestBall.GetComponent<LaserCreator>().laserAvailable = false;
                    CreateLaser(closestBall);
                } else
                {
                    EndLaser();
                }
            } else
            {
                EndLaser();
            }
        } else
        {
            EndLaser();
        }
    }

    void CreateLaser(GameObject closestBall)
    {
        LineRenderer laserLine = GetComponent<LineRenderer>();
        laserLine.positionCount = 2;
        laserLine.SetPosition(0, transform.position);
        laserLine.SetPosition(1, closestBall.transform.position);
        Vector2 lineDirection = closestBall.transform.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lineDirection.normalized, lineDirection.magnitude,laserLayerMask);
        if(hit.collider!=null)
        {
            Debug.Log(hit.collider.gameObject);
            Destroy(hit.collider.gameObject);
        }
    }

    
    void EndLaser()
    {
        LineRenderer laserLine = GetComponent<LineRenderer>();
        laserLine.positionCount = 0;
    }

    GameObject FindClosestBall()
    {
        GameObject closestBall = null;
        GameObject[] balls = GameObject.FindGameObjectsWithTag("ball");
        List<GameObject> ballList = new List<GameObject>();
        for(int i=0; i<balls.Length;i++)
        {
            //add every ball except yourself and ones which have laser power up
            if(balls[i] != gameObject)
            {
                if (balls[i].GetComponent<LaserCreator>().laserAvailable)
                {
                    ballList.Add(balls[i]);
                }
            }
        }

        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach(GameObject ball in ballList)
        {
            Vector3 diff = ball.transform.position - transform.position;
            float currentDistance = diff.sqrMagnitude;
            if(currentDistance < distance)
            {
                closestBall = ball;
                distance = currentDistance;
            }
        }

        return closestBall;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
    }

    public void EnableLaser()
    {
        StopCoroutine("laserEnabledTimer");
            StartCoroutine("laserEnabledTimer");
        
    }
    IEnumerator laserEnabledTimer()
    {
        
        GetComponent<SpriteRenderer>().color = Color.blue;
        
        ToggleLaser(true);
        yield return new WaitForSeconds(5);
        ToggleLaser(false);
        
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("paddle"))
        {
            EnableLaser();
        }
    }


}
    


