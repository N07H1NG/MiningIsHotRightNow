using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stupid : MonoBehaviour
{
    Vector3 wind = Vector3.zero;
    Vector3 targetWind;
    Vector3 vel = Vector3.zero;
    [SerializeField]float pow;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeWind());        
    }

    // Update is called once per frame
    void Update()
    {
        
        gameObject.GetComponent<Rigidbody>().AddForce(wind*pow);
        wind = Vector3.SmoothDamp(wind,targetWind,ref vel, 1f);
    }

    IEnumerator ChangeWind(){
        while(true){
            targetWind = Random.insideUnitSphere;
            yield return new WaitForSeconds(1f);
            targetWind = Vector3.zero;
            yield return new WaitForSeconds(15f);
        }
        
    }
}
