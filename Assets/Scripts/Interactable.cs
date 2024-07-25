using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Interactable : MonoBehaviour
{
    //hàm tương tácda
    public string promptMessenge;
    

    //gọi vào player
    public void BaseInteract()
    {
        Interact();
    }
    public virtual void Interact()
    {

    }
}
