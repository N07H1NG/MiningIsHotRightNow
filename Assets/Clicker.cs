using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    GameObject focused;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(Screen.width/2f,Screen.height/2f, 0));
        if (Physics.Raycast(ray, out RaycastHit hit, 4f))
            {
                if (hit.collider.gameObject != focused){
                    focused?.GetComponent<IClick>().LoseFocus();
                    focused = null;
                    HoverOverObject(hit.collider.gameObject);
                }
                if(Input.GetMouseButtonDown(0)){
                    //print("mouse");
                    focused?.GetComponent<IClick>().Interact();
                }
                //print(focused);
            }
        else{
            focused?.GetComponent<IClick>().LoseFocus();
            focused = null;
        }
        
    }

    /// <summary>
    /// OnMouseDown is called when the user has pressed the mouse button while
    /// over the GUIElement or Collider.
    /// </summary>
    /// <summary>
    /// Called every frame while the mouse is over the GUIElement or Collider.
    /// </summary>
    

    void HoverOverObject(GameObject target){
        if (target.TryGetComponent(out IClick clickObj)){
            clickObj.Focus();
            focused = target;
        }
    }
}
