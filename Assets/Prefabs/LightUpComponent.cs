using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
public class LightUpComponent : MonoBehaviour
{
    List<MeshRenderer> mrds;
    List<Material>[] mats;
    List<Material>[] matsAdjusted;
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
        mrds = new List<MeshRenderer>(gameObject.GetComponentsInChildren<MeshRenderer>());
        int l = mrds.Count;
        mats = new List<Material>[l];
        matsAdjusted = new List<Material>[l];
        

        foreach (MeshFilter flt in GetComponentsInChildren<MeshFilter>()){
            Mesh mesh = flt.mesh;
            var meshSubMeshCount = mesh.subMeshCount;
            if (meshSubMeshCount > 1)
            {
                var descArray = new SubMeshDescriptor[meshSubMeshCount + 1];
                for (int i = 0; i < meshSubMeshCount; i++)
                {
                    descArray[i] = mesh.GetSubMesh(i);
                }
                var lastMesh = descArray[meshSubMeshCount - 1];
                descArray[meshSubMeshCount] =
                    new SubMeshDescriptor(0, lastMesh.indexStart + lastMesh.indexCount);
                mesh.SetSubMeshes(descArray);
            }
        }
       
        //changedMat
        for(int i = 0;i<mrds.Count;i++)
        {
            mats[i] = new List<Material>();
            
            mrds[i].GetSharedMaterials(mats[i]);
            matsAdjusted[i] = new List<Material>(mats[i]);
            //matsAdjusted[i].Insert(0,lightUpMaterial);
            matsAdjusted[i].Add(lightUpMaterial);
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
                mrds[i].SetSharedMaterials(matsAdjusted[i]);
            }
            
        }
        if (currentlyRunning is not null){StopCoroutine(currentlyRunning);} 
        //print("stopped Coroutine fadeout");
        currentlyRunning = Fade(1f);
        StartCoroutine(currentlyRunning);
    }

    public void Dim(){
        if(isLit){
            if (currentlyRunning is not null){StopCoroutine(currentlyRunning);}
            //print("stopped Coroutine fade in");
            currentlyRunning = Fade(-1f);
            StartCoroutine(currentlyRunning);
        }
        
    }

    IEnumerator Fade(float direction){
        //print("starting coroutine");
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
        //print("successfully finished coroutine");
    }

    void FinishDim(){
        isLit = false;
        for(int i = 0;i<mrds.Count;i++){
            mrds[i].SetSharedMaterials(mats[i]);
        }
    }


}
