using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxedPC : MonoBehaviour, IClick
{
    LightUpComponent ltp;
    BoxCollider cld;
    public Camera cam;
    public int index;
    bool correct_placement = false;
    RaycastHit hit;
    Vector3 target_position;
    float angle = 0f;
    //Bounds rememberBounds;
    bool placing = false;
    // Start is called before the first frame update
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //initialRot = transform.rotation;
        ltp = GetComponent<LightUpComponent>();
        cld = GetComponent<BoxCollider>();
    }
    void IClick.Interact(){
        if (!placing){
            placing = true;
            StartCoroutine(Placement());
            ComputerCreator.crtInst.boxPickedUp = true;
        }
        else{
            if (correct_placement){
                //Quaternion rotationDelta = Quaternion.Inverse(transform.rotation)*initialRot;
                Quaternion dRot= Quaternion.Euler(0,angle,0);
                ComputerCreator.crtInst.CreateComputer(index,target_position-Vector3.up*cld.bounds.extents.y,dRot);
                StopAllCoroutines();
                cld.enabled = false;
                Destroy(gameObject,0.5f);
            }
        }
    }

    void IClick.Focus(){
        ltp.LightUp();
    }

    void IClick.LoseFocus(){
        ltp.Dim();
    }

    IEnumerator Placement(){
        Vector3 vel = Vector3.zero;
        //initialRot = Quaternion.identity;
        //rememberBounds = cld.bounds;
        cld.isTrigger = true;
        while(placing){
            
            float d = Input.GetAxis("Mouse ScrollWheel");
            transform.rotation = Quaternion.Euler(0,d*Time.deltaTime*10000f,0) * transform.rotation;
            angle += d*Time.deltaTime*10000f;
            string[] lrs = {"Wire","Box"};
            LayerMask msk = LayerMask.GetMask(lrs);
            msk = ~msk;
            Vector3 wrldScale = new Vector3(cld.size.x*transform.lossyScale.x,cld.size.y*transform.lossyScale.y,cld.size.z*transform.lossyScale.z);
            wrldScale=transform.rotation*wrldScale;
            wrldScale = math.abs(wrldScale)/2f;
            //print(wrldScale + " and bound by" + cld.bounds.extents*2f);
            if(Physics.BoxCast(cam.transform.position,wrldScale,cam.transform.forward,out hit,transform.rotation,4,msk)){
                if (Vector3.Dot(hit.normal,Vector3.up) >=0.9 && hit.collider.gameObject.CompareTag("Floor")){
                    target_position = cam.transform.position + cam.transform.forward * hit.distance;
                    //transform.position = cam.transform.position + cam.transform.forward * hit.distance;
                    correct_placement = true;
                    ltp.LightUp();
                }
                else{
                    correct_placement = false;
                    ltp.Dim();
                    target_position = cam.transform.position + cam.transform.forward*1.3f+Vector3.down*0.8f;
                }
                //transform.position = hit.point;
            }
            else{
                correct_placement = false;
                ltp.Dim();
                target_position = cam.transform.position + cam.transform.forward*2;
            }
            transform.position = Vector3.SmoothDamp(transform.position,target_position,ref vel,0.2f);
            
            yield return null;
        }
    }

    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    //void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireCube(cam.transform.position + cam.transform.forward * hit.distance, cld.bounds.extents*2);
    //}
}
