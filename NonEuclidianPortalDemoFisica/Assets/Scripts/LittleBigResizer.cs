using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LittleBigResizer : MonoBehaviour
{
    private Transform entered;
    private Vector3 enterPoint = Vector3.zero;
    private float corridorLength = 0f;
    private float enterValue = 1f;
    private float exitValue = 1f;
    private float dotProductInEntrance = 0f;

    private void Start()
    {
        corridorLength = GetComponent<BoxCollider>().size.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (entered != null)
        {
            float distanceTraveled = Vector3.Distance(enterPoint, entered.position);
            float newScale = map(distanceTraveled, 0, corridorLength, enterValue, exitValue);
            entered.localScale = new Vector3(newScale, newScale, newScale);
        }
    }
    
    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            entered = other.transform;
            enterPoint = other.transform.position;
            Vector3 colliderToEntered = entered.position - transform.position;
            dotProductInEntrance = Vector3.Dot(transform.forward, colliderToEntered);

            if (dotProductInEntrance < 0)
            {
                enterValue = entered.localScale.x;
                exitValue = enterValue * 0.8f;
            }
            else
            {
                enterValue = entered.localScale.x;
                exitValue = enterValue * 1.2f;
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 colliderToEntered = entered.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, colliderToEntered);

            if (dotProduct * dotProductInEntrance < 0f)
            {
                entered.localScale = new Vector3(exitValue, exitValue, exitValue);
            }
            else
            {
                entered.localScale = new Vector3(enterValue, enterValue, enterValue);
            }
            entered = null;
        }
    }
    
    private float map(float value, 
        float istart, 
        float istop, 
        float ostart, 
        float ostop) {
        return ostart + (ostop - ostart) * ((value - istart) / (istop - istart));
    }
}
