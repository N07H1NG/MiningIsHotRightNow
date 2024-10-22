using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using Random = UnityEngine.Random;

public class movecar : MonoBehaviour
{
    Vector3 start;
    Vector3 finish;
    [SerializeField] float range;
    [SerializeField] float speed = 20;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        finish = start + transform.right*range;
        print("Theyseemerolin");
        StartCoroutine(RideAround());
    }

    // Update is called once per frame
    IEnumerator RideAround()
    {   
        
        while (true){
            print("patrolin they trina catch me ridin dirty");
            float p = Random.Range(0,2);
            float d = (p-0.5f)*-2f*speed*Random.Range(0.5f,1.5f);
            print(p);
            print(d);
            print("they hatin");
            transform.position = Vector3.Lerp(start,finish,p);
            Vector3 trg = Vector3.Lerp(start,finish,1-p);
            while (transform.position != trg){
                
                transform.position = Vector3.Lerp(start,finish,p);
                p+= Time.deltaTime*d;
                p = math.clamp(p,0,1);
                yield return null;
            }
            
            yield return new WaitForSeconds(Random.Range(2f,16f));
        }       
    }
}
