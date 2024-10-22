using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class flicker : MonoBehaviour
{
    bool isFlickering = true;
    float timer = 0.0f;
    float targetTime;
    float targetBrightness;
    float oldBrightness;
    float brightnessScale;
    Light myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light>();
        brightnessScale = myLight.intensity;
        StartCoroutine(Flicker());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        myLight.intensity = math.lerp(oldBrightness,targetBrightness,timer/targetTime);
    }

    IEnumerator Flicker()
    {
        while (isFlickering){
            timer = 0f;
            oldBrightness = myLight.intensity;
            targetTime = Random.Range(0.1f,1f);
            targetBrightness = brightnessScale*Random.Range(4f,(5.1f-targetTime))/4.5f;
            targetTime/=2;
            yield return new WaitForSeconds(targetTime);
        }
    }
}
