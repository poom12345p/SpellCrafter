using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    
    [Header("SaveGamefile")]
    public bool isTesting = true;
    [SerializeField]
    public string savefileName;
    string path; 
    //[Space(20)]

    //
    [Header("Important Stuff")]
    [Space(20)]
    public GameObject PlayerPrefabs;
    public GameObject MainCanvas;
    public GameObject UICanvas;
    public GameObject SettingCanvas;
    public GameObject PauseCanvas;
    public UIManager gamePlayUI;
    [HideInInspector]
    public GameSave gameSave=null;
    [HideInInspector]
    public LittleCasterMove Player;
    [HideInInspector]
    public SettingUI settingUI;

    GameObject character;
    EventSystem es;

    public Button pauseSelected;

    public Dictionary<string, MapData> mapData;

    public StaticArea staticArea;
    private static GameManager m_instance;
    public static GameManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = (GameManager)FindObjectOfType(typeof(GameManager));
                if (m_instance == null)
                {
                    Debug.LogError(typeof(GameManager) + "not found.");
                }
            }
          
            return m_instance;
        }
    }

    bool isLoadSave=false;
    bool isPauseGame = false;
    public int spawnToPoint=0;

    [Header("Audio")]
    [Space(20)]
    public AudioSource bgmAudioSource;
    public float bgmMaxVolume;
    bool isChangingSound=false;

    public enum LoadMode
    {
        START, REBORN, LOADGAME,WARP
    }
    public LoadMode loadMode = LoadMode.START;
    public bool isStartSnece = true;
    [Space(20)]
    [Header("Game Item Parameter")]
    public static int manaShardUpgradeAmount=4, lifeShardUpgradeAmount=3, potionShardUpgradeAmount=1, magicBranchUpgradeAmount = 1;

    public string[] scencenameList;
    int curScenceindex=0;
    


    public MainCharControl2 GetCharControl
    {
        get
        {
            return Player.gameObject.GetComponent<MainCharControl2>();
        }
    }

   // public DialogueBox dialogueBox;
    
    private void Awake()
    {
        if (GameManager.instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(MainCanvas);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/" + savefileName + ".fun";
        mapData = new Dictionary<string, MapData>();
        if(isTesting)
        {
            CreateNewSave();
            StartGameTest();
            UIManager.instance.gamePlayUI.SetActive(true);
        }
        SceneManager.sceneLoaded += UIManager.instance.FadeOut;
        es = GameObject.Find("EventSystem").GetComponent<EventSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Escape") && loadMode != LoadMode.START)
        {
            CheckPauseGame();
        }

        if (loadMode != LoadMode.START)
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                MapManager.instance.SaveMap();
                curScenceindex =(scencenameList.Length + curScenceindex - 1)%scencenameList.Length;
                Warp(scencenameList[curScenceindex], 1);
            }
            else if (Input.GetKeyDown(KeyCode.P))
            {
                MapManager.instance.SaveMap();
                curScenceindex = (curScenceindex + 1) % scencenameList.Length;
                Warp(scencenameList[curScenceindex], 1);
            }
        }
    }

    public void CheckPauseGame()
    {
        if (Time.timeScale != 0f)
        {
            if (UIManager.instance.uiStack.Count != 0)
            {
                UIManager.instance.CallCloseUI();
                SetLittleCasterControlActive(true);
            }
            else PauseGame();
        }
        else
        {
            UnPauseGame();
        }

        //isPauseGame = !isPauseGame;
    }

    public void PauseGame()
    {
        UIManager.instance.CallOpenUI(PauseCanvas);
        Time.timeScale = 0;
        es.SetSelectedGameObject(pauseSelected.gameObject);
        SetLittleCasterControlActive(false);
    }

    public void UnPauseGame()
    {
        UIManager.instance.CallCloseUI();
        Time.timeScale = 1;
        es.SetSelectedGameObject(null);
        SetLittleCasterControlActive(true);
    }


    IEnumerator LoadSceneWaitForFade(string sceneName)
    {
        yield return new WaitUntil(() => UIManager.instance.isFaded == true);
        staticArea.InActiveAll();
      
        SceneManager.LoadScene(sceneName);
       // yield return new WaitUntil(() => UIManager.instance.isFaded == false);
       // UIManager.instance.FadeOut();

    }

    IEnumerator FadeAndChangeSound(AudioClip audioclip, float FadeTime)
    {
        while (bgmAudioSource.volume > 0)
        {
            bgmAudioSource.volume -= bgmMaxVolume * Time.fixedDeltaTime / FadeTime;

            yield return new WaitForFixedUpdate();
        }

        bgmAudioSource.Stop();
        if (audioclip != null)
        {
            bgmAudioSource.clip = audioclip;
            bgmAudioSource.Play();
            while (bgmAudioSource.volume < bgmMaxVolume)
            {
                bgmAudioSource.volume += bgmMaxVolume * Time.fixedDeltaTime / FadeTime;

                yield return new WaitForFixedUpdate();
            }
            bgmAudioSource.volume = bgmMaxVolume;
        }
        isChangingSound = false;
       
    }

    public void CreateNewSave()
    {
        gameSave = new GameSave();
        //SaveGame(gameSave);
        isLoadSave = true;

        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/save.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, gameSave);
        stream.Close();


        mapData = new Dictionary<string, MapData>();

        UIManager.instance.SetMap(mapData);

        Debug.Log("Save Game Success at scene : " + gameSave.saveScence + " , number :" + gameSave.saveCheckpointNumber + "\n save at: " + path);

        Debug.LogFormat("create new save check point at {0} / number : {1}", gameSave.saveScence, gameSave.saveCheckpointNumber);
    }

    public void LoadGameSave()
    {
        //string path = Application.persistentDataPath + "/"+ savefileName+".fun";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            gameSave = formatter.Deserialize(stream) as GameSave;
            stream.Close();
            Debug.Log("load at "+gameSave.saveCheckpointNumber);
            isLoadSave = true;

            mapData = new Dictionary<string, MapData>();
            foreach(MapData md in gameSave.mapData)
            {
                mapData.Add(md.ScenceName, md);
            }

            UIManager.instance.SetMap(mapData);

        }
        else
        {
            Debug.LogError("Save file not found "+path);
        }
    }
    public void StartGame()
    {
        if (isLoadSave)
        {
            isPauseGame = false;
            UIManager.instance.isInGame = true;

            LoadPlayer();
            gamePlayUI.SetStartGame(); 
            SetUpPlayer();
            LoadSetting();
            //SceneManager.LoadScene(gameSave.saveScence);
            FadeLoadScence(gameSave.saveScence);
        }
        SaveGame();
        //var mapMng = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        //mapMng.GetCheckPoints(gameSave.saveCheckPoint.number).SetPlayerOnCheckPoint(Player);

    }


    public void StartGameTestAllMap()
    {
        if (isLoadSave)
        {
            LoadPlayer();
            gamePlayUI.SetStartGame();
            SetUpPlayer();
            LoadSetting();
            //SceneManager.LoadScene(gameSave.saveScence);
            FadeLoadScence("AllScence");
        }
        //SaveGame();
        //var mapMng = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        //mapMng.GetCheckPoints(gameSave.saveCheckPoint.number).SetPlayerOnCheckPoint(Player);

    }

    public void StartGameTest()
    {
        if (isLoadSave)
        {
            LoadPlayer();
            gamePlayUI.SetStartGameTest();
            SetUpPlayer();
            //SceneManager.LoadScene(gameSave.saveScence);
            //FadeLoadScence(SceneManager.GetActiveScene().name);
        }
        //var mapMng = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
        //mapMng.GetCheckPoints(gameSave.saveCheckPoint.number).SetPlayerOnCheckPoint(Player);

    }

    public void RebornPlayer()
    {
        LoadGameSave();
        Debug.Log("reborn load scence :" + gameSave.saveScence +" /at check point :"+gameSave.saveCheckpointNumber);
        SetLittleCasterControlActive(true);
        GameManager.instance.loadMode = GameManager.LoadMode.REBORN;
        FadeLoadScence(gameSave.saveScence);
        Player.Reborn();
    
    }

     void SaveGame(GameSave save)
    {
        SaveMapdata();

        BinaryFormatter formatter = new BinaryFormatter();
       // string path = Application.persistentDataPath + "/" + savefileName + ".fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, save);
        stream.Close();
    }

    public void SaveGame(CheckPoint cp)
    {
        gameSave.inventory.SetInventory(Player.GetInventory());
        gameSave.saveScence = cp.sceneName;
        gameSave.saveCheckpointNumber = cp.number;
        MapManager.instance.SaveMap();
        SaveMapdata();

        BinaryFormatter formatter = new BinaryFormatter();
        //string path = Application.persistentDataPath + "/save.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, gameSave);
        stream.Close();
        Debug.Log("Save Game Success at scene : " + gameSave.saveScence + " , number :" + gameSave.saveCheckpointNumber+"\n save at: "+path);

    }
    public void SaveGame()
    {
        gameSave.inventory.SetInventory(Player.GetInventory());
        //gameSave.saveScence = cp.sceneName;
        //gameSave.saveCheckpointNumber = cp.number;
        if(MapManager.instance) MapManager.instance.SaveMap();
        SaveMapdata();

        BinaryFormatter formatter = new BinaryFormatter();
       // string path = Application.persistentDataPath + "/save.fun";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, gameSave);
        stream.Close();
        Debug.Log(gameSave.inventory.manaShard);
        Debug.Log("Save Game Success at scene : " + gameSave.saveScence + " , number :" + gameSave.saveCheckpointNumber + "\n save at: " + path);
    }

    void SaveMapdata()
    {

        gameSave.mapData.Clear();
        foreach (var data in mapData.Values)
        {
            gameSave.mapData.Add(data);

        }
    }

    void LoadPlayer()
    {
        character = Instantiate(PlayerPrefabs);
        Player = character.GetComponent<LittleCasterMove>();
        DontDestroyOnLoad(Player.gameObject);
        bgmAudioSource.transform.SetParent(character.transform);
        bgmAudioSource.transform.localPosition = Vector3.zero;
    }

    void LoadSetting()
    {
        if (settingUI == null)
        {
            settingUI = Instantiate(SettingCanvas).GetComponent<SettingUI>();
            DontDestroyOnLoad(settingUI.gameObject);
        }

        UICanvas.GetComponent<SpellUI>().character = character;
        UICanvas.GetComponent<SpellUI>().skm = UICanvas.GetComponent<SpellUI>().character.GetComponent<Spellcraft_2>(); 
    }

    void SetUpPlayer()
    {

        Player.SetUp();
        Player.SetInventory(gameSave.inventory);
        Player.ResetLittleCaster();
        //Debug.Log(Player.GetInventory());

    }

    public void Warp(string scenceName,int number)
    {
        loadMode = GameManager.LoadMode.WARP;
        spawnToPoint = number;
        // SceneManager.LoadScene(scenceName);
        FadeLoadScence(scenceName);
        SetLittleCasterControlActive(false);

    }

    public void FadeLoadScence(string sneceName)
    {
        UIManager.instance.FadeIn();
        StartCoroutine("LoadSceneWaitForFade", sneceName);
        Debug.Log(gameSave.saveScence);
    }

    public void SaveMap(MapData data)
    {
        if (mapData.ContainsKey(data.ScenceName))
        {
            mapData[data.ScenceName] = data;
        }
        else
        {
            mapData.Add(data.ScenceName, data);
        }
    }
    public MapData GetMapData(string sceneName)
    {
        if (mapData.ContainsKey(sceneName))
        {
            return mapData[sceneName];
        }
        else
        {
            return null;
        }
    }

    public void SetLittleCasterControlActive(bool b)
    {
        GetCharControl.SetActiveControl(b);
        if (!b)
        {
            Player.PrefromStay();
            
        }
    }

    public void ChangSound(AudioClip clip)
    {
        if(!isChangingSound)
        {
            isChangingSound = true;
            StartCoroutine(FadeAndChangeSound(clip, 1.0f));
       
        }
    }
    public void BackToMainMenuSetting()
    {
       bgmAudioSource.transform.SetParent(this.transform);
       Destroy(Player.gameObject);
       Destroy(settingUI.gameObject);
       loadMode = LoadMode.START;
       Player = null;
       settingUI = null;
    }


}
