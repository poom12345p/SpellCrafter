using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameSave 
{
    public Inventory inventory;
    public string saveScence;
   
    public int saveCheckpointNumber;

    public List<MapData> mapData;
    public GameSave(){

        inventory = new Inventory();
        saveScence = "tutorial01";
        saveCheckpointNumber = 0;
        mapData = new List<MapData>();
        
    }

    // Start is called before the first frame update
    
}
