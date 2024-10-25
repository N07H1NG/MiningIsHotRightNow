using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class lightswitch : MonoBehaviour, IClick
{
    [SerializeField] GameObject myLight;
    [SerializeField] GameObject coil;
    LightUpComponent lightUp;
    AudioSource clickSfx;
    Color eCol;
    float colorstate = 0f;
    [SerializeField]MyAudioCue cue;
    Material coilMat;
    bool state = false;
    // Start is called before the first frame update
    void Start()
    {
        clickSfx = GetComponent<AudioSource>();
        lightUp = GetComponent<LightUpComponent>(); 
        coilMat = coil.GetComponent<MeshRenderer>().material;
        coil.GetComponent<MeshRenderer>().sharedMaterial = coilMat;
        coilMat = coil.GetComponent<MeshRenderer>().sharedMaterial;
        eCol = coilMat.GetColor("_EmissionColor");
        StartCoroutine(CoilDim());
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
        StopAllCoroutines();
        //print("ended");
        StartCoroutine(CoilDim());
        
    }

    IEnumerator CoilDim(){
        //print("started");
        bool done = false;
        while(!done){
            float dir = state?1f:-0.7f;
            colorstate += dir*Time.deltaTime;
            colorstate = math.clamp(colorstate,0f,2f);
            coilMat.SetColor("_EmissionColor",Color.Lerp(Color.black,eCol,colorstate));
            done =(colorstate == 2f && state) || (colorstate == 0f && !state);
            yield return null;
            //print(colorstate);
        }
        //print("done");
    }
}
