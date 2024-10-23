using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Video;

public class VirtualScreen : MonoBehaviour
{
    [SerializeField] LaptopScript lpt;
    [SerializeField]  RawImage cursor;
    [SerializeField] GameObject content;
    [SerializeField] RawImage bg;
    VideoPlayer vp;
    [SerializeField]Texture bootVidTexture;
    [SerializeField]Texture bgTexture;
    [SerializeField] Texture offTexture;
    GameObject plrCamera;
    Vector3 lookTarget;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        vp = GetComponent<VideoPlayer>();
    }
    // Start is called before the first frame update
    public void LoadScreen(GameObject cam){
        
        //cursor.gameObject.SetActive(true);
        cursor.rectTransform.anchoredPosition = Vector2.zero;
        plrCamera = cam;
        StartCoroutine(LookAtMouse());
        StartCoroutine(Boot());
    }

    IEnumerator LookAtMouse(){
        lookTarget = cursor.transform.position;
        Vector3 vel = Vector3.zero;
        while (true){
            lookTarget = Vector3.SmoothDamp(lookTarget, cursor.transform.position, ref vel,1f);
            plrCamera.transform.LookAt(lookTarget);
        yield return null;
        }
    }


    public void TurnOff(){
        StopAllCoroutines();
        content.SetActive(false);
        //cursor.gameObject.SetActive(false);
        //cursor.gameObject.SetActive(false);
        bg.texture = offTexture;
        lpt.Off();
    }

    IEnumerator Boot(){
        bg.texture = bootVidTexture;
        vp.Play();
        yield return new WaitForSeconds(1.5f);
        content.SetActive(true);
        //cursor.gameObject.SetActive(true);
        bg.texture = bgTexture;
    }


}
