using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[System.Serializable]
public class CheckPoint : SpawnPoint
{
    public string sceneName;
    GameManager gameManager;

    GameObject settingUI;
    SettingUI setUI;
    // Start is called before the first frame update
    public CheckPoint(string _sceneName,int _number)
    {
        sceneName = _sceneName;
        number = _number;
    }
    void Start()
    {
        sceneName = SceneManager.GetActiveScene().name;
        settingUI = GameObject.FindGameObjectWithTag("SettingUI");
        setUI = settingUI.GetComponent<SettingUI>();
    }

  
    public  void SaveGame()
    {
        //MapManager.instance.SaveMap();
        GameManager.instance.SaveGame(this);
    }

    public void CheckpointInteracted()
    {
        //MapManager.instance.SaveMap();
        setUI.OpenSetting();
        GameManager.instance.Player.RefilPotion();
        GameManager.instance.Player.Reborn();
       SaveGame();
        //UIManager.instance.OpenSaveUITaable();
    }

    public override void SetPlayerOnSpawnPoint(GameObject mainCharMove)
    {
        base.SetPlayerOnSpawnPoint(mainCharMove);
        mainCharMove.GetComponent<MainUnit>().FullRestore();
    }

}
