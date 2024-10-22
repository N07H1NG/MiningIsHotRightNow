using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClick
{
    public void Interact();
    public void Focus();
    public void LoseFocus();
    //bool IsActive();
}
