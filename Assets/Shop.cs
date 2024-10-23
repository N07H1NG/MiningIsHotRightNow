using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Shop : LapTopButton
{
    bool shopOpen = false;
    // Start is called before the first frame update
    override public void StartClick(GameObject m){
        shopOpen = !shopOpen;
        print("SHOP!");
    }

    public override void EndClick()
    {
        throw new System.NotImplementedException();
    }
}
