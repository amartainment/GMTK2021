using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Rigidbody2D myRb;
    public float speed = 10;
    public float maxSpeed = 20;
    public float currentSize = 1;
    public bool clone = false;
    public bool readyForFusion = false;
    BallManager ballManager;
    public GameObject boom;
    public int fissionAllowed = 2;
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        Vector2 startDir = new Vector2(1, -1);
        if (!clone)
        {
            SetVelocity(startDir.normalized * speed);
        }
        GetComponent<LaserCreator>().ToggleLaser(false);
        ballManager = GameObject.FindGameObjectWithTag("ballManager").GetComponent<BallManager>();
    }

    // Update is called once per frame
    void Update()
    {
        BallControls();
        BallPhysics();
    }

    void BallPhysics()
    {
        //drag
        myRb.velocity *= 0.9995f;
    }


    public void BallFission()
    {
        if (fissionAllowed > 0)
        {
            fissionAllowed--;
            maxSpeed += 5;
            prepareForFusion();
            Vector2 randomizedOffset = Random.insideUnitCircle + new Vector2(transform.localScale.x, transform.localScale.y);
            transform.localScale -= 0.5f * Vector3.one;
            GameObject newBall = Instantiate(gameObject, transform.position, Quaternion.identity);
            Vector2 newBallVelocity = new Vector2(-myRb.velocity.x, myRb.velocity.y);
            BallBehavior newBallBehavior = newBall.GetComponent<BallBehavior>();
            newBallBehavior.clone = true;
            newBallBehavior.prepareForFusion();
            newBallBehavior.SetVelocity(newBallVelocity);
            newBallBehavior.fissionAllowed = fissionAllowed;
            CreateExplosion(transform.position, 2);
            
        }
    }
        
    public void CreateExplosion(Vector2 position, float scale)
    {

       GameObject explosion =  Instantiate(boom, position, Quaternion.identity);
        explosion.transform.localScale *= scale;
    }

    public void BallFusion(GameObject otherBall)
    {
        if(otherBall !=null && readyForFusion)
        {
            maxSpeed -= 5f;
            otherBall.GetComponent<BallBehavior>().readyForFusion = false;
            transform.localScale += 0.5f * Vector3.one;
            Destroy(otherBall);
            CreateExplosion(transform.position,4);
            readyForFusion = false;
            GetComponent<SpriteRenderer>().color = Color.red;
            reduceSpeed();
            fissionAllowed++;
        }
    }

    void BallControls()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            BallFission();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {
            readyForFusion = true;
            GetComponent<SpriteRenderer>().color = Color.yellow;
        }
    }
    public void SetVelocity(Vector2 velocity)
    {
        myRb.velocity = velocity;
    }

    void IncreaseSpeed()
    {
        if (speed + 5 < maxSpeed)
        {
            speed += 5;
        }
    }

    void reduceSpeed()
    {
        if (speed - 5 > 0)
        {
            speed -= 5;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("ball"))
        {
            Vector2 reflectedDiretion = Vector2.zero;
            Vector2 collisionDirection = myRb.position - collision.GetContact(0).point;
            ContactPoint2D collisionPoint = collision.GetContact(0);
            Vector2 relativeVel = myRb.velocity;
            reflectedDiretion = relativeVel - 2 * Vector2.Dot(collisionPoint.normal, relativeVel) * collisionPoint.normal;
            IncreaseSpeed();
            SetVelocity(reflectedDiretion.normalized * speed);
        }

        if(collision.collider.CompareTag("ball"))
        {
            if(readyForFusion)
            {
                if (collision.collider.gameObject.GetComponent<BallBehavior>().readyForFusion)
                {
                    BallFusion(collision.gameObject);
                }
            }
        }

    }

    IEnumerator ReadyForFusionTimer()
    {
        yield return new WaitForSeconds(3);
        readyForFusion = true;
    }

    public void prepareForFusion()
    {
        StartCoroutine("ReadyForFusionTimer");
    }

   
    

    

    
}
