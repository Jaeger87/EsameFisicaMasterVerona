using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlayerManager : MonoBehaviour
{
    private GameObject itemGrabbed = null;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            Debug.Log("tri");
            itemGrabbed = other.gameObject;
            itemGrabbed.transform.parent = transform;
            itemGrabbed.transform.localPosition = new Vector3(0f, 0.3f, 1f);
        }
    }
}
