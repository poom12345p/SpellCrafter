using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class MapManager : MonoBehaviour
{
    public List<CheckPoint> checkPoints;
    public List<SpawnPoint> spawnPoints;
    public List<Interactable> iteractPoint;//neeed manual assign
    [HideInInspector]
    public string myScenceName; 
    GameManager gameManager;
    public CinemachineVirtualCamera CM;
    public static MapManager instance;
    public AudioClip mapBGM;

    float camShakeTime;
    // Start is called before the first frame update
    private void Awake()
    {
        spawnPoints = new List<SpawnPoint>();
        checkPoints = new List<CheckPoint>();
        foreach (var sp in GameObject.FindObjectsOfType<SpawnPoint>())
        {
            if(!sp.GetComponent<CheckPoint>())
                spawnPoints.Add(sp.GetComponent<SpawnPoint>());
        }

        foreach (var cp in GameObject.FindObjectsOfType<CheckPoint>())
        {
            if (cp.GetComponent<CheckPoint>())
                checkPoints.Add(cp.GetComponent<CheckPoint>());
        }
    }
    void Start()
    {
        instance = this;
        myScenceName = SceneManager.GetActiveScene().name;
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        Debug.Log(gameManager);
        Debug.Log(gameManager.gameSave);
        CM.Follow = gameManager.Player.transform;
        CM.LookAt = gameManager.Player.transform;

        AudioCheck();
        //Debug.Log(GameObject.FindObjectsOfType<AudioListener>());

        if (GameManager.instance.loadMode == GameManager.LoadMode.START || GameManager.instance.loadMode == GameManager.LoadMode.REBORN)
        {
            CheckPoint c = GetCheckPoints(gameManager.gameSave.saveCheckpointNumber);
            if (c && GameManager.instance.spawnToPoint == 0)
                c.SetPlayerOnSpawnPoint(gameManager.Player.gameObject);
            GameManager.instance.loadMode = GameManager.LoadMode.WARP;
        }
        else if (GameManager.instance.loadMode == GameManager.LoadMode.WARP)
        {
            foreach(SpawnPoint sp in spawnPoints)
            {
                if(sp.number == GameManager.instance.spawnToPoint)
                {
                    GameManager.instance.spawnToPoint = 0;
                    sp.SetPlayerOnSpawnPoint(GameManager.instance.Player.gameObject);
                }
            }
        }
    
        LoadMap();
        
        //UIManager.instance.FadeOut();
        GameManager.instance.Player.moveHorizontal(0);
        StartCoroutine("UIWaitFadeOut");
    }

    IEnumerator UIWaitFadeOut()
    {
        //GameManager.instance.SetLittleCasterControlActive(false);
        yield return new WaitUntil(() => UIManager.instance.isFaded == false);
        GameManager.instance.SetLittleCasterControlActive(true);
      
        // GameManager.instance.SetLittleCasterControlActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(camShakeTime > 0)
        {
            camShakeTime -= Time.deltaTime;

            if(camShakeTime <= 0.0f)
            {
                var cinemachineBasicMultiChannelPerlin = CM.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0.0f;
            }
        }
    }

    public CheckPoint GetCheckPoints(int number)
    {
        CheckPoint c = null;
        foreach(var checkpoint in checkPoints)
        {
            if(checkpoint.number.Equals(number))
            {
                c = checkpoint;
            }
        }
        return c;
    }

    public void SaveMap()
    {
        MapData MapData = new MapData();
        foreach(var intp in iteractPoint)
        {
            MapData.InteractData intData=new MapData.InteractData();
            intData.active = intp.isActive;
            intData.state = intp.state;
            MapData.InteractActives.Add(intData);
        }
        MapData.ScenceName = myScenceName;

        GameManager.instance.SaveMap(MapData);
    }

    public void LoadMap()
    {

        MapData mapdata=GameManager.instance.GetMapData(myScenceName);
        if(mapdata != null)
        {
            for(int i=0;i< iteractPoint.Count;i++)
            {
                if (!mapdata.InteractActives[i].active)
                {
                    iteractPoint[i].DisableInteract();
                    iteractPoint[i].SetState(mapdata.InteractActives[i].state);
                }
                else
                {
                    if (!iteractPoint[i].gameObject.active) iteractPoint[i].gameObject.SetActive(true);
                   
                    iteractPoint[i].SetState(mapdata.InteractActives[i].state);
                }
            }
        }
    }

    public void AudioCheck()
    {
        var audiobgmS = GameManager.instance.bgmAudioSource;
        if (mapBGM != audiobgmS.clip)
        {
            GameManager.instance.ChangSound(mapBGM);
            //audiobgmS.Play();
        }
    }

    public void ChangeMapSound(AudioClip clip)
    {
       GameManager.instance.ChangSound(clip);
    }

    public void SilenceMapSound()
    {
        ChangeMapSound(null);
    }

    public void PlayMapSound()
    {
        ChangeMapSound(mapBGM);
    }

    public void ShakeCam(float intensity,float time)
    {
        var cinemachineBasicMultiChannelPerlin = CM.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        camShakeTime = time;
    }

    public void ShakeCam()
    {
        ShakeCam(2.0f, 0.25f);
    }

    public void ChanngeCamConfider(Collider confinder)
    {
        CinemachineConfiner cmc= CM.GetComponent<CinemachineConfiner>();
        cmc.m_BoundingVolume = confinder;
    }
}
