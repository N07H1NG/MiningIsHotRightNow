using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour
{
    int cnt;
    void Start(){
        cnt = GetComponent<MeshFilter>().mesh.subMeshCount;
        print(cnt);
    }
    // Start is called before the first frame update
    void Update()
    {
        int t = GetComponent<MeshFilter>().mesh.subMeshCount;
        if (t!=cnt){
            //print(t);
            cnt = t;
        }
    }

    // Update is called once per fra
}
