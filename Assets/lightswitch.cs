using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightswitch : MonoBehaviour, IClick
{
    [SerializeField] GameObject myLight;
    LightUpComponent lightUp;
    AudioSource clickSfx;
    [SerializeField]MyAudioCue cue;
    bool state = true;
    // Start is called before the first frame update
    void Start()
    {
        clickSfx = GetComponent<AudioSource>();
        lightUp = GetComponent<LightUpComponent>(); 
    }

    // Update is called once per frame
    public void Focus(){
        lightUp.LightUp();
    }
    public void LoseFocus(){
        lightUp.Dim();
    }

    public void Interact(){
        clickSfx.PlayOneShot(cue.GetRandomClip());
        state = !state;
        myLight.GetComponent<Light>().enabled = state;
    }
}
