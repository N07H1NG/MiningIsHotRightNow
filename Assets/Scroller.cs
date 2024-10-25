using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : LapTopButton
{
    // Start is called before the first frame update
    ScrollRect scrollRectRef;
    bool myActive = false;
    float scrollvel = 0f;
    float dampvel;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        scrollRectRef = GetComponent<ScrollRect>();
    }

    public override void StartClick(GameObject mouse = null)
    {
        myActive = true;
    }

    public override void EndClick()
    {
        //throw new System.NotImplementedException();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (transform.parent.GetSiblingIndex() != transform.parent.parent.childCount-2){
            //print("bad");
            //print(transform.parent.parent.childCount);
            myActive = false;
        }
        else{
            
            myActive = true;
        }

        scrollvel = Mathf.SmoothDamp(scrollvel,0,ref dampvel,0.4f);
        float d = Input.GetAxis("Mouse ScrollWheel");
        //print(d);
        if(d !=0f && myActive){
            //print("ACTIVE");
            scrollvel = d*20f;
        }

        scrollRectRef.verticalNormalizedPosition = math.clamp(scrollRectRef.verticalNormalizedPosition+(scrollvel*Time.deltaTime),0,1f);
    }


     /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    //void OnGUI()
    //{
    //    
    //}
}//
