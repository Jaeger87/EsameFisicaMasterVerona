using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereButton : MonoBehaviour
{
    
    private bool pressed = false;
    private bool inArea = false;
    
    public SphereButtonManager buttonManager;
    
    private AudioSource clickButton;
    
    [SerializeField] private Material NOPressedButtonMaterial;
    [SerializeField] private Material pressedButtonMaterial;
    // Start is called before the first frame update
    void Start()
    {
        clickButton = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pressed && inArea && Input.GetKeyDown(KeyCode.E))
        {
            pressButton();
            clickButton.Play(0);
        }
    }
    
    public bool isPressed()
    {
        return pressed;
    }
    
    public void resetButton()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = NOPressedButtonMaterial;
        pressed = false;
    }
    
    public void pressButton()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = pressedButtonMaterial;
        pressed = true;
        buttonManager.startEnigma();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inArea = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inArea = false;
        }
    }
}
