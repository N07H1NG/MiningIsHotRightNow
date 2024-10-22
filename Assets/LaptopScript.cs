using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopScript : MonoBehaviour, IClick
{
    LightUpComponent lightUp;
    Animator myAnimator;
    Transform cameraTarget;
    bool open = false;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        lightUp = GetComponent<LightUpComponent>();
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("open", open);
        cameraTarget = transform.GetChild(0);
    }
    // Start is called before the first frame update
    public void Focus(){
        lightUp.LightUp();
    }
    public void LoseFocus(){
        lightUp.Dim();
    }

    public void Interact(){
        open = !open;
        myAnimator.SetBool("open", open);
        if (open){
            lightUp.Dim();
            
        }
        else
        {
            lightUp.LightUp();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
