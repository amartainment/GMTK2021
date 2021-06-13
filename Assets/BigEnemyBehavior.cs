using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemyBehavior : EnemyBehavior
{
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    

    // Update is called once per frame
    

    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);

        if(collision.collider.CompareTag("ball"))
        {
            SplitIntoBabies();
        }
    }

    void SplitIntoBabies()
    {
        myRb = GetComponent<Rigidbody2D>();
        Vector2 randomPosition = Random.insideUnitCircle * 2f + myRb.position;
        Vector2 randomPosition2 = Random.insideUnitCircle * 2f + myRb.position;
        Instantiate(enemyPrefab, randomPosition, Quaternion.identity);
        Instantiate(enemyPrefab, randomPosition2, Quaternion.identity);
        Destroy(gameObject);

    }
}
