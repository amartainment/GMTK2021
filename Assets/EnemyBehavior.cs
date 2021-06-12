using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    DamagePointManager pointManager;
    Rigidbody2D myRb;
    Transform target;
    public float speed = 10;
    bool hoverTimerRunning = false;
    public enum State
    {
        hover,fly
    }

    State state;

    void Start()
    {
        pointManager = GameObject.Find("DamagePoints").GetComponent<DamagePointManager>();
        myRb = GetComponent<Rigidbody2D>();
        state = State.hover;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.hover:
                
                Hover();
                break;
            case State.fly:
                FlyToTarget();
                break;

        }
    }


    void Hover()
    {
        if(!hoverTimerRunning)
        {
            StartCoroutine("StartFlyTimer");
        }
    }

    void FlyToTarget()
    {
        
        Vector3 direction = target.position - transform.position;
        transform.up = direction.normalized;
        myRb.velocity = direction.normalized * speed;

    }
    
    IEnumerator StartFlyTimer()
    {
        hoverTimerRunning = true;
        yield return new WaitForSeconds(5);
        target = pointManager.ReturnATransform();
        state = State.fly;
        hoverTimerRunning = false;
    }


}
