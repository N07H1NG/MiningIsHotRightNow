using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProgressManager : MonoBehaviour
{
    float timer;
    float timerlimit;
    float pricechangerate;
    public static ProgressManager prmInstance { get; private set; }
    [SerializeField] PlayerControl myPlayer;
    public float  wallet  = 0;
    public float dollars = 400;
    public float price = 1000;
    [SerializeField] AudioClip deathclip;
    AudioSource heart;
    [SerializeField] Gradient amb;
    [SerializeField] AudioSource doorbell;
    [SerializeField] AnimationCurve walkCurve;
    
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
        float plimit = (120f-price)*0.8f;
        price += math.max(pricechangerate *Random.Range(-0.2f*t,0.1f*t)*((price>2000f)?-2:1),plimit);
        foreach(MoneyHeatMaker cnt in contributors){
            cnt.PassTimeInstantly(t);
        }
    }

    public void ChangeTemperature(float delta){
        temperature = math.max(temperature+delta,0);
        ApplyTemperatureEffects();
    }

    void ApplyTemperatureEffects(){
        float p = math.clamp(temperature/35f,0f,1f);
        myPlayer.nervousness = 0.1f+temperature/6f;
        myPlayer.walkSpeed = 4f*walkCurve.Evaluate(p);
        //myPlayer.walkSpeed = math.clamp(4-temperature/10f,1f,4f);
        myPlayer.inertia = math.clamp(2+temperature/8f,2f,8f);
        myPlayer.baseExhaustion = math.clamp(temperature/20f,0.2f,0.8f);
        myPlayer.exhaustionSpeed = 0.2f+temperature/7f;
        myPlayer.restSpeed = math.clamp(0.6f - temperature/20f,0.1f,0.6f);
        myPlayer.exhaustionCap = 1+temperature/12f;
        RenderSettings.ambientLight = amb.Evaluate(temperature/30f);
        heart.volume = math.pow(math.max((-10f+temperature)/20f,0),1.8f);
        if (temperature >=35f){
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
        timer = 0;
        timerlimit = 15f;
        pricechangerate = 0.1f;
        float multip = 1f;
        while (true){
            pricechangerate = math.max(pricechangerate,(120f-price)/5f);
            pricechangerate = math.min(pricechangerate,(5000f-price)/5f);
            price += multip*pricechangerate*Time.deltaTime;
            price = math.clamp(price,0f,5000f);
            timer += Time.deltaTime;
            if (timer>=timerlimit){
                if (Random.value >= 0.95){pricechangerate = 0f;}
                float skew = (1100f-price)/100f;
                pricechangerate+= Random.Range(-15f+skew,15f+skew);
                multip = Random.Range(0.5f,2f);
                timer = 0;
                timerlimit = Random.Range(0.2f,4f);
                
            }
            yield return null;
        }
    }

    IEnumerator Dying(){
        while (myPlayer.GetComponent<PlayerControl>().characterVelocity.magnitude ==0){
            yield return null;
        }
        
        myPlayer.GetComponentInChildren<AudioSource>().PlayOneShot(deathclip);
        myPlayer.GetComponent<Animation>().Play();
        heart.Stop();
        yield return new WaitForSeconds(0.3f);
        myPlayer.GetComponent<PlayerControl>().StopAllCoroutines();
        yield return new WaitForSeconds(3.9f);
        doorbell.Play();
        
    }


    //public void ConvertBtcToDollar(float howmuchdollar){
    //    
    //}
}
