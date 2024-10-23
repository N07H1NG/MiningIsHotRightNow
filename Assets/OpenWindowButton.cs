using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWindowButton : LapTopButton
{
    [SerializeField] GameObject Window;
    // Start is called before the first frame update
    public override void StartClick(GameObject mouse = null)
    {
        Window.SetActive(true);
        Window.transform.SetAsLastSibling();
        mouse.transform.SetAsLastSibling();
    }
    public override void EndClick()
    {
        
    }
}
