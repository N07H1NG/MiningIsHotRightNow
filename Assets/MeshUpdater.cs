using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshUpdater : MonoBehaviour
{
    // Start is called before the first frame update
    public static MeshUpdater updtref;
    public List<Mesh> updated = new List<Mesh>();
    void Awake(){
        updtref = this;
    }
}
