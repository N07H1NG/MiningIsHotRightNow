using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : LapTopButton
{
    // Start is called before the first frame update
    [SerializeField] VirtualScreen myScr;
    override public void StartClick(GameObject m){
        myScr.TurnOff();
    }
    public override void EndClick()
    {
        
    }
}
