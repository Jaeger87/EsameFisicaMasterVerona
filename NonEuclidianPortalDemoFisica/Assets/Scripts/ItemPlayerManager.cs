using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlayerManager : MonoBehaviour
{
    private MagicBall itemGrabbed = null;

    // Update is called once per frame
    void Update()
    {
        if(itemGrabbed != null)
            if (Input.GetKeyDown(KeyCode.E))
            {
                itemGrabbed.throwAway(transform.forward);
            }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            MagicBall mb = other.gameObject.GetComponent<MagicBall>();
            if (!mb.isFlying())
            {
                itemGrabbed = other.gameObject.GetComponent<MagicBall>();
                itemGrabbed.transform.parent = transform;
                itemGrabbed.transform.localPosition = new Vector3(0f, 0.3f, 1f);
            }   

        }
    }
    
    
}
