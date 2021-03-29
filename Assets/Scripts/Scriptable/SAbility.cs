using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "AbilityInfo", menuName = "Ability")]
public class SAbility : ScriptableObject
{
    public string elementName;
    public string elementDescription;
    public Sprite elementIcon;
    public Sprite elementClip;

    public string attackName;
    public string attackDescription;
    public Sprite attackIcon;
    public Sprite attackClip;

    public string spellName;
    public string spellDescription;
    public Sprite spellIcon;
    public Sprite spellClip;

    public string midairName;
    public string midairDescription;
    public Sprite midairIcon;
    public Sprite midairClip;

    public string passiveName;
    public string passiveDescription;
    public Sprite passiveIcon;
    public Sprite passiveClip;
}
