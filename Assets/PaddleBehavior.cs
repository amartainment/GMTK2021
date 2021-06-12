using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PaddleBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myRb;
    public bool bottomPaddle = false;
    public float speed = 10;
    float dstTravelled = 0;
    public PathCreator pathCreator;
    public EndOfPathInstruction end;
    
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        MoveAlongPath();
    //    MovePaddles();
    }

    void MoveAlongPath()
    {
        if (Input.GetKey(KeyCode.D))
        {
            dstTravelled += speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            dstTravelled -= speed * Time.deltaTime;
        }
        else
        {
            
        }

        
        transform.position = pathCreator.path.GetPointAtDistance(dstTravelled, end);
        transform.up = pathCreator.path.GetNormalAtDistance(dstTravelled, end);
        //transform.rotation = pathCreator.path.GetRotationAtDistance(dstTravelled, end);

    }
    void MovePaddles()
    {
        if(bottomPaddle)
        {
            if(Input.GetKey(KeyCode.D))
            {
                myRb.velocity = Vector2.right * speed;
            } else if(Input.GetKey(KeyCode.A))
            {
                myRb.velocity = Vector2.left * speed;
            }
            else
            {
                myRb.velocity = Vector2.zero;
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                myRb.velocity = Vector2.right * speed;
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                myRb.velocity = Vector2.left * speed;
            }
            else
            {
                myRb.velocity = Vector2.zero;
            }
        }
    }
        
    
}
