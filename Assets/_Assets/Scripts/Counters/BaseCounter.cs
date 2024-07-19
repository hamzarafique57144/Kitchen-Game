using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour
{
  public virtual void Interact(PlayerController player)
    {
        Debug.LogError("BaseCounter.Interact");
    }
}
