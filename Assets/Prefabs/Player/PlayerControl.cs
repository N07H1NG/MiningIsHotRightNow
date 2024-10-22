using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;
public class PlayerControl : MonoBehaviour
{
    [SerializeField] float walkSpeed;
    [SerializeField] float inertia;
    Vector3 characterVelocity;
    Vector3 targetVelocity;
    Vector3 inputVector;
    Vector2 mouseDelta;
    Vector3 lookVector;
    Vector3 targetVector;
    Vector3 dampVelocity = Vector3.zero;
    [SerializeField] Vector2 maxTurnSpeed;
    [SerializeField] float nervousness=0.1f;
    CharacterController myController;
    Transform cameraTransform;
    Transform actualCamera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible =false;
        Cursor.lockState = CursorLockMode.Locked;
        
        myController = GetComponent<CharacterController>();
        cameraTransform = gameObject.transform.GetChild(0);
        actualCamera = cameraTransform.GetChild(0);
        lookVector = cameraTransform.forward;
        targetVector = cameraTransform.forward;
        StartCoroutine(HeadBob());
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical")).normalized;
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"),-1*Input.GetAxis("Mouse Y"));
        //mouseDelta*= Time.deltaTime*100;
        mouseDelta = math.clamp(mouseDelta,-1*maxTurnSpeed,maxTurnSpeed);
        float d = Vector3.Dot(targetVector,Vector3.up);
        if(math.abs(d) > 0.9f){
            print("Close");
            mouseDelta.y *= (mouseDelta.y*d>=0)?1:0;
            print(mouseDelta.y);
        }
        targetVector = Quaternion.Euler(0,mouseDelta.x,0f) * targetVector;
        targetVector = Quaternion.AngleAxis(mouseDelta.y,Vector3.Cross(Vector3.up,targetVector) )*targetVector;
        lookVector = Vector3.SmoothDamp(lookVector,targetVector,ref dampVelocity,(5+Vector3.Dot(lookVector,targetVector)*nervousness)/20f);
        transform.forward = new Vector3(lookVector.x,0,lookVector.z).normalized;
        cameraTransform.forward = lookVector;
        //transform.Rotate(axis: Vector3.up, angle: mouseDelta.x);
        //print(mouseDelta.x);
        //cameraTransform.transform.Rotate(axis: Vector3.right, angle: mouseDelta.y);
        //cameraTransform.Rotate(cameraTransform.right,mouseDelta.y);
        //cameraTransform.
        //cameraTransform.forward = lookTransform.forward;
        //gameObject.transform.forward =new Vector3(cameraTransform.forward.x,0,cameraTransform.forward.z).normalized;
        targetVelocity = transform.rotation* inputVector;
        Vector3 diff = targetVelocity-characterVelocity;
        characterVelocity += diff.normalized*math.clamp(inertia*Time.deltaTime,0,diff.magnitude);
        myController.SimpleMove(characterVelocity*walkSpeed);
        
    }

    IEnumerator HeadBob()
    {
        float timer=0;
        Vector3 camDamp = Vector3.zero;
        Vector3 targetPos;
        while(true){
            timer += 3f*walkSpeed*characterVelocity.magnitude*Time.deltaTime;
            targetPos= cameraTransform.position + new Vector3(0,math.sin(timer)*0.8f,0)*characterVelocity.magnitude;
            timer = timer%(2*math.PI);
            actualCamera.position = Vector3.SmoothDamp(actualCamera.position,targetPos,ref camDamp,0.5f);
            actualCamera.LookAt(cameraTransform.position + cameraTransform.forward*8f);
            yield return null;
        }
    }

    
}
