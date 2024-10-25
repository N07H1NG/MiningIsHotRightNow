using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour,IClick
{
    [SerializeField] AudioClip open;
    public bool delivery = false;
    LightUpComponent ltp;
    AudioSource aSrc;
    // Start is called before the first frame update
    void Start()
    {
        aSrc = GetComponent<AudioSource>();
        ltp = GetComponent<LightUpComponent>();
    }

    // Update is called once per frame
    void IClick.Focus(){
        if(delivery){
            ltp.LightUp();
        }
    }

    void IClick.LoseFocus(){
        ltp.Dim();
    }

    void IClick.Interact(){
        if (delivery){
            aSrc.Stop();
            aSrc.PlayOneShot(open);
            ComputerCreator.crtInst.doorOpened = true;
            delivery = false;
            GetComponent<Collider>().enabled = false;
        }

    }
}
