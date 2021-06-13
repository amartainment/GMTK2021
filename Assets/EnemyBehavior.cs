using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    DamagePointManager pointManager;
    protected Rigidbody2D myRb;
    Transform target;
    public float speed = 10;
    bool hoverTimerRunning = false;
    Vector3 startingPosition;
    Vector2 targetVelocity;
    public float smoothness = 5;
    public float health = 5;
    public GameObject explosion;
    public AudioClip boomClip;
    public AudioSource mySource;
    public enum State
    {
        hover,fly,retreat
    }

    State state;

    virtual public void Start()
    {
        startingPosition = transform.position;
        pointManager = GameObject.Find("DamagePoints").GetComponent<DamagePointManager>();
        myRb = GetComponent<Rigidbody2D>();
        state = State.hover;
    }

    // Update is called once per frame
     public void Update()
    {
        LerpVelocity();
        Death();

        switch (state)
        {
            case State.hover:
                
                Hover();

                break;
            case State.fly:
                FlyToTarget();
                break;
            case State.retreat:
                Retreat();
                break;

        }
    }


     public void Hover()
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
        targetVelocity = direction.normalized * speed;

    }

     public void DamageEnemy(float d)
    {
        health -= d;
    }

    virtual public void Death()
    {
        if(health <=0)
        {
            
            MyEventSystem.enemyDead(1);
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void LerpVelocity()
    {
       myRb.velocity =  Vector2.Lerp(myRb.velocity, targetVelocity, Time.deltaTime * smoothness);
    }
    
    void Retreat()
    {
        Vector3 direction = startingPosition - transform.position;
        transform.up = direction.normalized;
        targetVelocity = direction.normalized * speed;
        StartCoroutine("StartFlyTimer");
    }
    IEnumerator StartFlyTimer()
    {
        hoverTimerRunning = true;
        yield return new WaitForSeconds(2);
        target = pointManager.ReturnATransform();
        state = State.fly;
        hoverTimerRunning = false;
    }


    virtual public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.CompareTag("wall"))
        {
            Camera.main.GetComponent<CameraShake>().shakeDuration = 0.05f;
            MyEventSystem.damagedWall(1);
            state = State.retreat;
        }
    }

    private void OnDestroy()
    {
        
    }



}
