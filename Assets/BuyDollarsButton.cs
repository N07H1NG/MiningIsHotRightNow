using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyDollarsButton : LapTopButton
{
    [SerializeField] AudioSource speaker;
    public AudioClip purchase;
    public AudioClip failpurchase;
    [SerializeField] RectTransform bound;
    [SerializeField] float amount;
    void Update(){
        //print(gameObject.GetComponent<RectTransform>().position);
        if(RectTransformUtility.RectangleContainsScreenPoint(bound,gameObject.GetComponent<RectTransform>().position)){
            GetComponent<Collider>().enabled = true;
        }
        else{
            GetComponent<Collider>().enabled = false;
        }
    }
    
    [SerializeField] int index;
    // Start is called before the first frame update
    public override void StartClick(GameObject mouse = null)
    {
        if (ProgressManager.prmInstance.BuyDollars(amount)){
            speaker.PlayOneShot(purchase);
        }
        else{
            speaker.PlayOneShot(failpurchase);
        }
    }

    public override void EndClick()
    {
        //throw new System.NotImplementedException();
    }
}
