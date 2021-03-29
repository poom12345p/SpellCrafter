using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class MapData 
{
    [System.Serializable]
    public struct InteractData
    {
        public bool active;
        public ushort state;
    }

    public string ScenceName;
    public List<InteractData> InteractActives;
    public MapData()
    {
        InteractActives = new List<InteractData>();
    }
}
