using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemInfo", menuName = "Item")]
public class SItem : ScriptableObject
{
    public ItemName itemName;
    public string description;
    public string hashcode;
    public Sprite image;
}
