using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : LapTopButton
{
    [SerializeField] AudioSource speaker;
    public AudioClip purchase;
    public AudioClip failpurchase;
    [SerializeField] RectTransform bound;
    [SerializeField] float price;
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
        if (ProgressManager.prmInstance.dollars >= price){
            ProgressManager.prmInstance.dollars -= price;
            speaker.PlayOneShot(purchase);
            ComputerCreator.crtInst.OrderBox(index);
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
