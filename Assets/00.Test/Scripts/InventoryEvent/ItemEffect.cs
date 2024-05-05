using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemEffect : ScriptableObject
{
    // Abstract method to be implemented by subclasses. It defines the role of the item effect.
    public abstract bool ExecuteRole();
}
