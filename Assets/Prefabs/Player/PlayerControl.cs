using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
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
    [SerializeField] GameObject foot;
    AudioSource stepSource;
    [SerializeField] MyAudioCue stepCue;
    CharacterController myController;
    Transform cameraTransform;
    Transform actualCamera;
    [SerializeField] float exhaustion;
    // Start is called before the first frame update
    void Start()
    {
        stepSource = foot.GetComponent<AudioSource>();
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
        targetVelocity = transform.rotation* inputVector;
        Vector3 diff = targetVelocity-characterVelocity;
        characterVelocity += diff.normalized*math.clamp(inertia*Time.deltaTime,0,diff.magnitude);
        myController.SimpleMove(characterVelocity*walkSpeed);
        
    }

    IEnumerator HeadBob()
    {
        yield return null;
        float timer= 0f;
        float timer2 = 0f;
        Vector3 camDamp = Vector3.zero;
        Vector3 targetPos;
        bool walking = false;
        while(true){
            if (characterVelocity.magnitude >0){
                walking = true;
            }
            timer += 3f*walkSpeed*characterVelocity.magnitude*Time.deltaTime;
            timer2 += 2f*(1+exhaustion)*Time.deltaTime;
            targetPos= cameraTransform.position + new Vector3(0,math.sin(timer)*0.8f,0)*characterVelocity.magnitude + new Vector3(0,math.sin(timer2)*0.1f,0)*math.pow(exhaustion,0.7f);

            if (walking && timer>=2f*math.PI){
                stepSource.PlayOneShot(stepCue.GetRandomClip());
                timer-=2f*math.PI;
            }
            else if(walking &&characterVelocity.magnitude == 0){
                walking = false;
                timer = 0f;
                stepSource.PlayOneShot(stepCue.GetRandomClip());
            }
            timer2 = timer2%(2*math.PI);
            //print(math.pow(exhaustion,0.6f));
            actualCamera.position = Vector3.SmoothDamp(actualCamera.position,targetPos,ref camDamp,0.5f);
            actualCamera.LookAt(cameraTransform.position + cameraTransform.forward*8f);
            yield return null;
        }
    }

    /// <summary>
    /// OnControllerColliderHit is called when the controller hits a
    /// collider while performing a Move.
    /// </summary>
    /// <param name="hit">The ControllerColliderHit data associated with this collision.</param>
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.TryGetComponent(out Rigidbody rgbd)){
            rgbd.AddForceAtPosition(characterVelocity,hit.point);
        }
    }


    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        //exhaustion = 0.2f;
        //StopAllCoroutines();
    }
    
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        //exhaustion = 1f;
        //StartCoroutine(HeadBob());
    }



    
}
