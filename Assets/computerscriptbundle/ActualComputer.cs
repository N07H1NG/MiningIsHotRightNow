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

    // Start is called before the first frame update
    void Start()
    {
        ltp = gameObject.GetComponent<LightUpComponent>();
        myMhm =gameObject.GetComponent<MoneyHeatMaker>();
        myHum = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void IClick.Interact(){
        on = !on;
        myHum.PlayOneShot(clck.GetRandomClip());
        if(on){
            myMhm.TurnOn();
            myHum.PlayDelayed(0.2f);
            
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
