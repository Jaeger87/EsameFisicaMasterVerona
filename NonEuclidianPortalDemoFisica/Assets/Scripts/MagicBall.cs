using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    private Rigidbody m_Rigidbody = null;
    private SphereCollider m_Collider = null;
    private Vector3 m_throwDirection;
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

    public void throwAway(Vector3 throwDirection)
    {
        m_throwDirection = throwDirection;
        transform.parent = null;
        m_Rigidbody.isKinematic = false;
        m_Rigidbody.useGravity = true;
        m_Collider.isTrigger = false;
        m_Collider.radius = 0.5f;
        flying = true;
        m_Rigidbody.AddForce((m_throwDirection * throwSpeed) + Vector3.up * 5, ForceMode.Impulse);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            m_Rigidbody.isKinematic = true;
            m_Rigidbody.useGravity = false;
            m_Collider.isTrigger = true;
            m_Collider.radius = 2f;
            flying = false;
            Vector3 landingPosition = transform.position;
            Vector3 newPosition = new Vector3(landingPosition.x, 2, landingPosition.z);
            transform.position = newPosition;

        }
    }

    public bool isFlying()
    {
        return flying;
    }

    public Vector3 GetThrowDirection()
    {
        return m_throwDirection;
    }
}
