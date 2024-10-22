using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator PlayAtRandom(){
        while(true){
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(Random.Range(10f,40f));
        }
    }
}
