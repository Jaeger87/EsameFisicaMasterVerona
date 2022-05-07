using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PortalTeleporter : MonoBehaviour {

	public Transform player;
	public Transform reciever;
	public Transform playerParent;
	private bool playerIsOverlapping = false;
	public bool firstCamera;

	// Update is called once per frame
	void Update () {
		if (playerIsOverlapping)
		{
			Vector3 portalToPlayer = player.position - transform.position;
			float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

			// If this is true: The player has moved across the portal
			if (dotProduct < 0f)
			{
				// Teleport him!
				player.GetComponent<FirstPersonController>().enabled = false;
				float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
				rotationDiff += 180;
				//playerController.rotateByTeleporter(rotationDiff);

				rotationDiff = firstCamera ? rotationDiff : -rotationDiff;

				playerParent.Rotate(Vector3.up, rotationDiff);
				//player.RotateAround(Vector3.up, rotationDiff);
				Vector3 positionOffset = Quaternion.Euler(0f, rotationDiff, 0f) * portalToPlayer;
				player.position = reciever.position + positionOffset;

				playerIsOverlapping = false;
				
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
			other.GetComponent<FirstPersonController>().enabled = true;
		}
	}
}
