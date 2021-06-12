using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Rigidbody2D myRb;
    public float speed = 10;
    public float currentSize = 1;
    public bool clone = false;
    public bool readyForFusion = false;
    BallManager ballManager;
    public GameObject boom;
    // Start is called before the first frame update
    void Start()
    {
        myRb = GetComponent<Rigidbody2D>();
        Vector2 startDir = new Vector2(1, -1);
        if (!clone)
        {
            SetVelocity(startDir.normalized * speed);
        }

        ballManager = GameObject.FindGameObjectWithTag("ballManager").GetComponent<BallManager>();
    }

    // Update is called once per frame
    void Update()
    {
        BallControls();
    }


    public void BallFission()
    {
        Vector2 randomizedOffset = Random.insideUnitCircle + new Vector2(transform.localScale.x,transform.localScale.y);
        transform.localScale -= 0.5f * Vector3.one;
        GameObject newBall = Instantiate(gameObject, transform.position, Quaternion.identity);
        Vector2 newBallVelocity = new Vector2(-myRb.velocity.x, myRb.velocity.y);
        BallBehavior newBallBehavior = newBall.GetComponent<BallBehavior>();
        newBallBehavior.clone = true;
        newBallBehavior.SetVelocity(newBallVelocity);
        CreateExplosion(transform.position,1) ;
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
            otherBall.GetComponent<BallBehavior>().readyForFusion = false;
            transform.localScale += 0.5f * Vector3.one;
            Destroy(otherBall);
            CreateExplosion(transform.position,2);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("ball"))
        {
            Vector2 reflectedDiretion = Vector2.zero;
            Vector2 collisionDirection = myRb.position - collision.GetContact(0).point;
            ContactPoint2D collisionPoint = collision.GetContact(0);
            Vector2 relativeVel = myRb.velocity;

            reflectedDiretion = relativeVel - 2 * Vector2.Dot(collisionPoint.normal, relativeVel) * collisionPoint.normal;
            SetVelocity(reflectedDiretion.normalized * speed);
        }

        if(collision.collider.CompareTag("ball"))
        {
            if(readyForFusion)
            {
                /*
                if(collision.collider.gameObject.GetComponent<BallBehavior>().readyForFusion)
                {
                    ballManager.FuseBalls(gameObject, collision.collider.gameObject);
                } 
                */
                BallFusion(collision.gameObject);
            }
        }

    }
}
