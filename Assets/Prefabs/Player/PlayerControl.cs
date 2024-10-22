using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
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
    [SerializeField] Vector2 maxTurnSpeed;
    CharacterController myController;
    Transform cameraTransform;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible =false;
        Cursor.lockState = CursorLockMode.Locked;
        lookVector = transform.forward;
        myController = GetComponent<CharacterController>();
        cameraTransform = gameObject.transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0,Input.GetAxis("Vertical")).normalized;
        mouseDelta = new Vector2(Input.GetAxis("Mouse X"),-1*Input.GetAxis("Mouse Y")) ;

        mouseDelta = math.clamp(mouseDelta,-1*maxTurnSpeed,maxTurnSpeed);
        gameObject.transform.Rotate(0,mouseDelta.x,0);
        targetVelocity = transform.rotation* inputVector;
        Vector3 diff = (targetVelocity-characterVelocity);
        characterVelocity += diff.normalized*math.clamp(inertia*Time.deltaTime,0,diff.magnitude);
        myController.SimpleMove(characterVelocity*walkSpeed);
        cameraTransform.Rotate(mouseDelta.y,0,0);
    }

    
}
