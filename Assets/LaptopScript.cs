using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class LaptopScript : MonoBehaviour, IClick
{
    LightUpComponent lightUp;
    Animator myAnimator;
    Transform cameraTarget;
    Vector3 rememberPosition;
    Quaternion rememberRotation;
    bool open = false;
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject plrObj;
    [SerializeField] VirtualScreen scr;

    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        lightUp = GetComponent<LightUpComponent>();
        myAnimator = GetComponent<Animator>();
        myAnimator.SetBool("open", open);
        cameraTarget = transform.GetChild(0);
    }
    // Start is called before the first frame update
    public void Focus(){
        lightUp.LightUp();
    }
    public void LoseFocus(){
        lightUp.Dim();
    }

    public void Interact(){
        StopAllCoroutines();
        open = !open;
        myAnimator.SetBool("open", open);
        if (open){
            lightUp.Dim();
            plrObj.GetComponent<PlayerControl>().enabled = false;
            rememberPosition = playerCamera.transform.position;
            rememberRotation = playerCamera.transform.rotation;
            playerCamera.GetComponentInChildren<Clicker>().enabled = false;
            StartCoroutine(FlyDown(true));
        }
    }
    // Update is called once per frame
    IEnumerator FlyDown(bool on)
    {
        float timer =0f;
        float p = 0f;
        Vector3 vel = Vector3.zero;
        float speed = on?1f:3f;
        Quaternion begin = playerCamera.transform.rotation;
        do {
            //print(vel);
            playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position,cameraTarget.position,ref vel,speed*0.6f);
            playerCamera.transform.rotation = Quaternion.Slerp(begin,cameraTarget.rotation,p);

            //print(cameraTarget.rotation);
            //print(playerCamera.transform.rotation);
            timer += Time.deltaTime*speed;
            p = math.pow(timer/2f, 2f);
            //print("WHAT " + (playerCamera.transform.position - cameraTarget.position).magnitude.ToString());
            //print(p);
            yield return null;
        }while ((playerCamera.transform.position - cameraTarget.position).magnitude >=0.4f || p<=1f);
        if(on){scr.LoadScreen(playerCamera);}
        else{StartCoroutine(FlyBackUp());}
    }

    IEnumerator FlyBackUp()
    {
        float timer =0f;
        float p = 0f;
        Vector3 vel = Vector3.zero;
        myAnimator.SetBool("open", false);
        do {
            //print(vel);
            playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position,rememberPosition,ref vel,0.6f);
            playerCamera.transform.rotation = Quaternion.Slerp(cameraTarget.rotation,rememberRotation,p);

            //print(cameraTarget.rotation);
            //print(playerCamera.transform.rotation);
            timer += Time.deltaTime;
            p = math.pow(timer/2f, 2f);
            //print("WHAT " + (playerCamera.transform.position - cameraTarget.position).magnitude.ToString());
            //print(p);
            yield return null;
        }while ((playerCamera.transform.position - rememberPosition).magnitude >=0.4f || p<=1f);
        playerCamera.transform.position = rememberPosition;
        
        open = false;
        
        plrObj.GetComponent<PlayerControl>().enabled = true;
        playerCamera.GetComponentInChildren<Clicker>().enabled = true;
        lightUp.LightUp();
    }


    public void Off(){
        StartCoroutine(FlyDown(false));
    }

    


    
}
