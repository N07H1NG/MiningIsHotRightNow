using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class FakeMouse : MonoBehaviour
{
    Vector2 mouseDelta;
    LapTopButton focused;
    LapTopButton clicked;
    RectTransform rct;
    public AudioSource speaker;
    public AudioClip[] click;

    void Start(){
        rct = gameObject.GetComponent<RectTransform>();
    }
    // Start is called before the first frame updat

    // Update is called once per frame
    void Update()
    {
        //print("Mouse updates");
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
        rct.anchoredPosition += mouseDelta*4f;
        rct.anchoredPosition = math.clamp(rct.anchoredPosition, new Vector2(-960,-540), new Vector2(960,540));

        if(Input.GetMouseButtonDown(0)){
            
            speaker.PlayOneShot(click[0]);
            focused?.StartClick(gameObject);
            clicked = focused;
        }
        if(Input.GetMouseButtonUp(0)){
            speaker.PlayOneShot(click[1]);
            clicked?.EndClick();
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out LapTopButton btn)){
            clicked?.EndClick();
            focused = btn;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.TryGetComponent(out LapTopButton btn)){
            if (focused == btn){
                focused = null;
            }
            if(clicked ==btn){
                clicked = null;
            }
        }
    }

    
}
