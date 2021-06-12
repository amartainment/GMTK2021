using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePointManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Transform> points;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform ReturnATransform()
    {
        Transform targetTransform;
        int randomIndex = Random.Range(0, points.Count);
        targetTransform = points[randomIndex];
        return targetTransform;
    }
}
