using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetArea : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("ball"))
        {
            collision.gameObject.transform.position = Vector3.zero;
        }
    }
}
