using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    private Rigidbody m_Rigidbody = null;
    [SerializeField] private float throwSpeed = 0;
    private bool flying = false;
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0.8f,0);
    }

    public void throwAway(Vector3 forwardDirection)
    {
        transform.parent = null;
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        flying = true;
        m_Rigidbody.AddForce((forwardDirection * throwSpeed) + Vector3.up * 5, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collisione");
    }

    public bool isFlying()
    {
        return flying;
    }
}
