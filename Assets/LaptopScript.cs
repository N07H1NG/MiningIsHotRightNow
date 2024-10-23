using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
        open = !open;
        myAnimator.SetBool("open", open);
        if (open){
            lightUp.Dim();
            plrObj.GetComponent<PlayerControl>().enabled = false;
            rememberPosition = playerCamera.transform.position;
            rememberRotation = playerCamera.transform.rotation;
            StartCoroutine(FlyDown());
        }
        else
        {
            lightUp.LightUp();
        }
    }
    // Update is called once per frame
    IEnumerator FlyDown()
    {
        float timer =0f;
        float p = 0f;
        Vector3 vel = Vector3.zero;
        while ((playerCamera.transform.position - cameraTarget.position).magnitude >=0.1f || playerCamera.transform.rotation != cameraTarget.rotation){
            //print(vel);
            playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position,cameraTarget.position,ref vel,0.6f);
            playerCamera.transform.rotation = Quaternion.Slerp(rememberRotation,cameraTarget.rotation,p);

            //print(cameraTarget.rotation);
            //print(playerCamera.transform.rotation);
            timer += Time.deltaTime;
            p = math.pow(timer/2f, 2f);
            print(timer.ToString() + " turns into " + p.ToString());
            //print(p);
            yield return null;
        }
        print("arrived");
    }
}
