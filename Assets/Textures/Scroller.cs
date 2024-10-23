using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroller : LapTopButton
{
    // Start is called before the first frame update
    

    public override void StartClick(GameObject mouse = null)
    {
        GetComponent<ScrollRect>();
    }

    public override void EndClick()
    {
        //throw new System.NotImplementedException();
    }
}
