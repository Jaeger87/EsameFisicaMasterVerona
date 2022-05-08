using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    private Rigidbody m_Rigidbody = null;
    private SphereCollider m_Collider = null;
    [SerializeField] private float throwSpeed = 0;
    private bool flying = false;
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Collider = GetComponent<SphereCollider>();
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
        m_Collider.isTrigger = false;
        m_Collider.radius = 0.5f;
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
