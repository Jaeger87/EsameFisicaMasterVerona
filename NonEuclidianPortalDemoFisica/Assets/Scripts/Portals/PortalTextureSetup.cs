using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour {


	public List<Camera> cameras;
	public List<Material> cameraMaterials;

	// Use this for initialization
	void Start () {

		for(int i = 0; i < cameras.Count; i++)
        {
			Camera currentCamera = cameras[i];
			Material currentMaterial = cameraMaterials[i];
			if (currentCamera.targetTexture != null)
			{
				currentCamera.targetTexture.Release();
			}
			currentCamera.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
			currentMaterial.mainTexture = currentCamera.targetTexture;
		}

	}
	
}
