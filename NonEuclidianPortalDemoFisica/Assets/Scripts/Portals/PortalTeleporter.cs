using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PortalTeleporter : MonoBehaviour {

	public Transform player;
	public Transform reciever;
	public Transform playerParent;
	private bool playerIsOverlapping = false;
	private bool itemIsOverlapping = false;
	private Transform ItemTransform = null;
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

		if (itemIsOverlapping)
		{
			MagicBall mb = ItemTransform.GetComponent<MagicBall>();
			if(mb.isFlying())
			{
				Vector3 portalToItem = mb.transform.position - transform.position;
				float dotProduct = Vector3.Dot(transform.up, portalToItem);

				// If this is true: The Item has moved across the portal
				if (dotProduct < 0f)
				{
					// Teleport him!
					float rotationDiff = -Quaternion.Angle(transform.rotation, reciever.rotation);
					rotationDiff += 180;

					rotationDiff = firstCamera ? rotationDiff : -rotationDiff;
					
					Rigidbody mbRigidBody = mb.GetComponent<Rigidbody>();

					Quaternion EulerRotation = Quaternion.Euler(0f, rotationDiff, 0f);
					
					Vector3 mbvelocity = mb.GetComponent<Rigidbody>().velocity;
					
					Vector3 newVelocity = EulerRotation * mbvelocity;

					mbRigidBody.velocity = newVelocity;
					
					//player.RotateAround(Vector3.up, rotationDiff);
					Vector3 positionOffset = EulerRotation * portalToItem;
					mb.transform.position = reciever.position + positionOffset;
					itemIsOverlapping = false;
				}
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = true;
		}

		if (other.tag == "Item")
		{
			itemIsOverlapping = true;
			ItemTransform = other.transform;
		}
	}

	void OnTriggerExit (Collider other)
	{
		if (other.tag == "Player")
		{
			playerIsOverlapping = false;
			other.GetComponent<FirstPersonController>().enabled = true;
		}
		
		if (other.tag == "Item")
		{
			itemIsOverlapping = false;
			ItemTransform = null;
		}
	}
}
