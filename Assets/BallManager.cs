using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FuseBalls(GameObject ball1, GameObject ball2)
    {
        if (ball1 != null && ball2 != null)
        {
            Vector2 centeredLocation = (ball1.transform.position + ball2.transform.position) / 2;
            Vector2 resultantVelocity = ball1.GetComponent<Rigidbody2D>().velocity + ball2.GetComponent<Rigidbody2D>().velocity;
            GameObject fusedBall = Instantiate(ball1, centeredLocation, Quaternion.identity);
            BallBehavior fusedBallBehavior = fusedBall.GetComponent<BallBehavior>();
            Destroy(ball1);
            Destroy(ball2);
            fusedBallBehavior.clone = true;
            fusedBallBehavior.SetVelocity(resultantVelocity.normalized*fusedBallBehavior.speed);
            
            
        }
    }
}
