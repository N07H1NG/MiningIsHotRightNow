using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class ComputerGeneric : MonoBehaviour, IClick
{
    bool on = false;
    
    MoneyHeatMaker myMhm;
    LightUpComponent ltp;
    public MyAudioCue clck;

    AudioSource myHum;
    AudioSource myClick;

    // Start is called before the first frame update
    void Start()
    {
        ltp = gameObject.GetComponent<LightUpComponent>();
        myMhm =gameObject.GetComponent<MoneyHeatMaker>();
        myHum = GetComponent<AudioSource>();
        myClick = gameObject.AddComponent<AudioSource>();
        myClick.volume = 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IClick.Interact(){
        on = !on;
        myClick.PlayOneShot(clck.GetRandomClip());
        if(on){
            myMhm.TurnOn();
            myHum.Play();
            
        }
        else{
            myMhm.TurnOff();
            myHum.Stop();
        }
    }

    void IClick.Focus(){
        ltp.LightUp();

    }


    void IClick.LoseFocus(){
        ltp.Dim();
    }




}
