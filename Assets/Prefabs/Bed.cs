using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;


public class Bed : MonoBehaviour, IClick
{
    LightUpComponent lightUp;
    Transform cameraTarget;
    Vector3 rememberPosition;
    Quaternion rememberRotation;
    [SerializeField] GameObject playerCamera;
    [SerializeField] GameObject plrObj;

    
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        lightUp = GetComponent<LightUpComponent>();
        cameraTarget = transform.GetChild(0);
    }
    // Start is called before the first frame update
    public void Focus(){
        //print("bedwasfocused");
        lightUp.LightUp();
    }
    public void LoseFocus(){
        lightUp.Dim();
    }

    public void Interact(){
        StopAllCoroutines();

        lightUp.Dim();
        plrObj.GetComponent<PlayerControl>().enabled = false;
        rememberPosition = playerCamera.transform.position;
        rememberRotation = playerCamera.transform.rotation;
        playerCamera.GetComponentInChildren<Clicker>().enabled = false;
        StartCoroutine(FlyDown());
    }
    // Update is called once per frame
    IEnumerator FlyDown()
    {
        float timer =0f;
        float p = 0f;
        Vector3 vel = Vector3.zero;
        float speed = 1f;
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
        yield return new WaitForSeconds(2f);
        ProgressManager.prmInstance.PassTimeInstantly(1000f);
        StartCoroutine(FlyBackUp());
    }

    IEnumerator FlyBackUp()
    {
        float timer =0f;
        float p = 0f;
        Vector3 vel = Vector3.zero;
        do {
            //print(vel);
            playerCamera.transform.position = Vector3.SmoothDamp(playerCamera.transform.position,rememberPosition,ref vel,0.6f);
            playerCamera.transform.rotation = Quaternion.Slerp(cameraTarget.rotation,rememberRotation,p);

            //print(cameraTarget.rotation);
            //print(playerCamera.transform.rotation);
            timer += Time.deltaTime;
            p = math.pow(timer/1.2f, 0.8f);
            //print("WHAT " + (playerCamera.transform.position - cameraTarget.position).magnitude.ToString());
            //print(p);
            yield return null;
        }while ((playerCamera.transform.position - rememberPosition).magnitude >=0.4f || p<=1f);
        playerCamera.transform.position = rememberPosition;  
        plrObj.GetComponent<PlayerControl>().enabled = true;
        playerCamera.GetComponentInChildren<Clicker>().enabled = true;
        lightUp.LightUp();
    }


    


    
}
