using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class lightswitch : MonoBehaviour, IClick
{
    [SerializeField] GameObject myLight;
    [SerializeField] GameObject coil;
    LightUpComponent lightUp;
    AudioSource clickSfx;
    Color eCol;
    float colorstate = 1f;
    [SerializeField]MyAudioCue cue;
    Material coilMat;
    bool state = true;
    // Start is called before the first frame update
    void Start()
    {
        clickSfx = GetComponent<AudioSource>();
        lightUp = GetComponent<LightUpComponent>(); 
        coilMat = coil.GetComponent<MeshRenderer>().sharedMaterial;
        eCol = coilMat.GetColor("_EmissionColor");
        StartCoroutine(coilDim());
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

    IEnumerator coilDim(){
        while(true){
            float dir = state?1:-1;
            colorstate += dir*Time.deltaTime;
            colorstate = math.clamp(colorstate,0f,3f);
            coilMat.SetColor("_EmissionColor",Color.Lerp(Color.black,eCol,colorstate));
        }
    }
}
