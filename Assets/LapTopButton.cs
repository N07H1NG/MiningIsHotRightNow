using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class LapTopButton : MonoBehaviour
{
    // Start is called before the first frame update
    abstract public void StartClick(GameObject mouse=null);
    abstract public void EndClick();
}


