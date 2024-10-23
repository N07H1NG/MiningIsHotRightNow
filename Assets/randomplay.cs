using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomplay : MonoBehaviour
{
    [SerializeField] Vector2 minMax =  new Vector2(10f,40f);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PlayAtRandom());
    }

    IEnumerator PlayAtRandom(){
        while(true){
            GetComponent<AudioSource>().Play();
            yield return new WaitForSeconds(Random.Range(minMax.x,minMax.y));
        }
    }
}
