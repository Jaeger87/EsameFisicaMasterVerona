using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereButtonManager : MonoBehaviour
{
    private const float timeEnigma = 19.0f;
    private bool enigmaStarted = false;
    private float timeRemaning;
    private bool enigmaEnded;
    
    [SerializeField] private AudioSource countDownSound;
    [SerializeField] private AudioSource openDoorSound;
    
    public List<SphereButton> buttons;
    public GameObject door;

    // Update is called once per frame
    void Update()
    {
        if (enigmaEnded)
            return;

        if(enigmaStarted)
        {
            timeRemaning -= Time.deltaTime;
            if (timeRemaning < 0)
                resetEnigma();
        }
    }
    
    public void startEnigma()
    {
        if (enigmaStarted)
        {
            bool accumulatorPressed = true;
            foreach (SphereButton b in buttons)
                accumulatorPressed &= b.isPressed();
            if(accumulatorPressed)
                endEnigma();
            
        }

        else
        {
            enigmaStarted = true;
            timeRemaning = timeEnigma;
            countDownSound.Play(0);
        }
            
        
    }

    private void resetEnigma()
    {
        enigmaStarted = false;
        countDownSound.Stop();
        foreach (SphereButton b in buttons)
            b.resetButton();

    }


    private void endEnigma()
    {
        enigmaEnded = true;
        countDownSound.Stop();
        door.SetActive(true);
        openDoorSound.Play(0);
        //apri portale


    }
}
