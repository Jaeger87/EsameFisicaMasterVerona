using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour {

	public Transform playerCamera;
	public Transform portal;
	public Transform otherPortal;

	public bool firstCamera;
	public bool rotate180;

    private void Update()
    {
		GetComponent<Camera>().nearClipPlane = Mathf.Abs((playerCamera.position - otherPortal.position).z);


	}

    private void LateUpdate () {
		Vector3 playerOffsetFromPortal = playerCamera.position - otherPortal.position;
		transform.position = portal.position + playerOffsetFromPortal;


		float angularDifferenceBetweenPortalRotations = firstCamera ? -Quaternion.Angle(otherPortal.rotation, portal.rotation) : Quaternion.Angle(otherPortal.rotation, portal.rotation);

        if (rotate180)
        {
			angularDifferenceBetweenPortalRotations += 180f;
		}

		transform.RotateAround(portal.position, Vector3.up, angularDifferenceBetweenPortalRotations);

		Quaternion portalRotationalDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
		Vector3 newCameraDirection = portalRotationalDifference * playerCamera.forward;
		transform.rotation = Quaternion.LookRotation(newCameraDirection, Vector3.up);
	}
}
