using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTarget : MonoBehaviour
{
    [SerializeField] private GameObject exit;
    [SerializeField] private AudioSource openDoorSound;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            exit.SetActive(true);
            openDoorSound.Play(0);
        }
    }
}
