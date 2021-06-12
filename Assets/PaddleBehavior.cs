using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D myRb;
    public bool bottomPaddle = false;
    public float speed = 10;
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();  
    }

    // Update is called once per frame
    void Update()
    {
        MovePaddles();
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
