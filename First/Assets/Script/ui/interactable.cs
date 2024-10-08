using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class interactable : MonoBehaviour
{
    public string promptmessage;

   public void baseinteract()
   {
        interact();
   }

    protected virtual void interact()
    {
        
    }
}
