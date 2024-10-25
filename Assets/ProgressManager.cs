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
    public float dollars = 400;
    public float price = 1000;
    [SerializeField] AudioClip deathclip;
    AudioSource heart;
    [SerializeField] Gradient amb;
    [SerializeField] AudioSource doorbell;
    
    public float temperature =0;

    public HashSet<MoneyHeatMaker> contributors = new HashSet<MoneyHeatMaker>();
    
    // Start is called before the first frame update
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        prmInstance = this;
    }
    void Start(){
        heart = GetComponent<AudioSource>();
        StartCoroutine(PriceChange());
        ApplyTemperatureEffects();
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
        myPlayer.nervousness = 0.1f+temperature/6f;
        myPlayer.walkSpeed = math.clamp(4-temperature/16f,1,4f);
        myPlayer.inertia = math.clamp(2+temperature/3f,2f,8f);
        myPlayer.baseExhaustion = math.clamp(temperature/5f,0.2f,0.6f);
        myPlayer.exhaustionSpeed = 0.2f+temperature/3f;
        myPlayer.restSpeed = math.clamp(0.6f - temperature/10f,0.1f,0.6f);
        myPlayer.exhaustionCap = 1+temperature/8f;
        RenderSettings.ambientLight = amb.Evaluate(temperature/20f);
        heart.volume = math.pow(math.max((-10f+temperature)/10,0),1.6f);
        if (temperature >=25f){
            StartCoroutine(Dying());
        }


    }

    public bool BuyDollars(float amount){
        if(wallet>=amount/price && dollars + amount >= 0){
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
        float pricechangerate = 0.1f;
        while (true){
            price += pricechangerate*Time.deltaTime;
            price = math.clamp(price,120f,5000f);
            timer += Time.deltaTime;
            if (timer>=timerlimit){
                float skew = (1000f-price)/100f;
                pricechangerate+= Random.Range(-15f+skew,15f+skew);
                timer = 0;
                timerlimit = Random.Range(1f,8f);
                
            }
            yield return null;
        }
    }

    IEnumerator Dying(){
        while (myPlayer.GetComponent<PlayerControl>().characterVelocity.magnitude ==0){
            yield return null;
        }
        myPlayer.GetComponent<PlayerControl>().StopAllCoroutines();
        myPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(deathclip);
        myPlayer.GetComponent<Animation>().Play();
        heart.Stop();
        yield return new WaitForSeconds(0.9f);
        doorbell.Play();
        
    }


    //public void ConvertBtcToDollar(float howmuchdollar){
    //    
    //}
}
