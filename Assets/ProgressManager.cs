using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProgressManager : MonoBehaviour
{
    public static ProgressManager prmInstance { get; private set; }
    [SerializeField] PlayerControl myPlayer;
    public float  wallet  = 0;
    public float dollars = 0;
    public float price = 1000;
    
    public float temperature =0;

    public HashSet<MoneyHeatMaker> contributors;
    
    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        prmInstance = this;
    }
    void Start(){
        StartCoroutine(PriceChange());
    }

    public void PassTimeInstantly(float t){
        foreach(MoneyHeatMaker cnt in contributors){
            cnt.PassTimeInstantly(t);
        }
    }

    public void ChangeTemperature(float delta){
        temperature = math.max(temperature+delta,0);
        ApplyTemperatureEffects();
    }

    void ApplyTemperatureEffects(){

    }

    public bool BuyDollars(float amount){
        if(wallet>=amount/price){
            wallet-=amount/price;
            dollars += amount;
            return true;
        }
        else{
            return false;
        }
    }

    IEnumerator PriceChange(){
        float timer = 0;
        float timerlimit = 15f;
        float pricechangerate = 0;
        while (true){
            price += pricechangerate*Time.deltaTime;
            price = math.clamp(price,120f,5000f);
            timer += Time.deltaTime;
            if (timer>=timerlimit){
                pricechangerate+= Random.Range(-15f,15f);
                timer = 0;
                timerlimit = Random.Range(20f,80f);
            }
        }
    }


    //public void ConvertBtcToDollar(float howmuchdollar){
    //    
    //}
}
