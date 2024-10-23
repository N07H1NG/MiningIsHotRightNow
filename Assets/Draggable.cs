using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapTopDraggable: LapTopButton
{
    Vector3 offset;
    bool draggin = false;
    Transform m;
    Transform wind;
    void Start(){
        wind = transform.parent;
    }
    override public void StartClick(GameObject mouse=null){
        offset = wind.position - mouse.transform.position;
        m = mouse.transform;
        draggin = true;
        m.GetComponent<Collider>().enabled = false;
        wind.SetAsLastSibling();
        m.SetAsLastSibling();
    }
    override public void EndClick(){
        draggin = false;
        m.GetComponent<Collider>().enabled = true;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(draggin){
            wind.position = m.position + offset;
        }
        
    }
}