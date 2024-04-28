using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOItem : ScriptableObject
{
    public string itemName;
    public int level;

    [System.Serializable]
    public struct STAT
    {
        public string name;
        public int value;
    }

    public List<STAT> stat = new List<STAT>();

    public int maxStack;
    public int price;

    public Sprite icon;
    public Transform prefab;

    [Multiline]
    public string description;
}
