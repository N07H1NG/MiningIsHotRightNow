using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWindowButton : LapTopButton
{
    [SerializeField] GameObject Window;
    // Start is called before the first frame update
    public override void StartClick(GameObject mouse = null)
    {
        Window.SetActive(false);
    }
    public override void EndClick()
    {
        
    }
}
