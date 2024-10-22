using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;


public class LightUpComponent : MonoBehaviour
{
    List<MeshRenderer> mrds;
    List<Material>[] mats;
    public  Material lightUpMaterial;
    bool isLit = false;
    IEnumerator currentlyRunning;
    // Start is called before the first frame update
     /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        mrds = new List<MeshRenderer>( gameObject.GetComponentsInChildren<MeshRenderer>());
        int l = mrds.Count;
        mats = new List<Material>[l];
        //changedMat
        for(int i = 0;i<mrds.Count;i++)
        {
            mats[i] = new List<Material>();
            mrds[i].GetSharedMaterials(mats[i]);
        }
    }

    /// <summary>
    /// Called when the mouse enters the GUIElement or Collider.
    /// </summary>
    public void LightUp(){
        if (!isLit){
            isLit = true;
            for(int i = 0;i<mrds.Count;i++)
            {
                List<Material> tmp = new List<Material>(mats[i]);
                tmp.Add(lightUpMaterial);
                mrds[i].SetSharedMaterials(tmp);
            }
            
        }
        if (currentlyRunning is not null){StopCoroutine(currentlyRunning);} 
        print("stopped Coroutine fadeout");
        currentlyRunning = Fade(1f);
        StartCoroutine(currentlyRunning);
    }

    public void Dim(){
        if(isLit){
            if (currentlyRunning is not null){StopCoroutine(currentlyRunning);}
            print("stopped Coroutine fade in");
            currentlyRunning = Fade(-1f);
            StartCoroutine(currentlyRunning);
        }
        
    }

    IEnumerator Fade(float direction){
        print("starting coroutine");
        float v = lightUpMaterial.GetFloat("_Strength");
        float p = direction*(v*2f-1f);
        do{
            v =(1 +p*direction)/2f;
            p+= 4f*Time.deltaTime;
            lightUpMaterial.SetFloat("_Strength",v);
            yield return null;
        }while (p<=1 && p>=-1);
        if (direction == -1){
            FinishDim();
        }
        print("successfully finished coroutine");
    }

    void FinishDim(){
        isLit = false;
        for(int i = 0;i<mrds.Count;i++){
            mrds[i].SetSharedMaterials(mats[i]);
        }
    }


}
