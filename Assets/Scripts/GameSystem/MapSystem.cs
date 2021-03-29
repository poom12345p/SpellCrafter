using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MapSystem : MonoBehaviour
{
    [System.Serializable]
    public struct MapArea
    {
        public RectTransform mapImage;
        public string scenceName;
        public RectTransform mapCover;
    }

    //public RectTransform mapCam;
    public RectTransform allMap;
    public RectTransform mapHilight;
    [SerializeField]
    RectTransform mapBlackArea;
    public GameObject Bg;
    public MapArea[] _mapListArray;
    Dictionary<string, MapArea> mapList;
    MapArea curScence;
    RectTransform curScenceZone;

    bool isMapOpen = false;

    private void Start()
    {
        mapList = new Dictionary<string, MapArea>();
        for (int i = 0; i < _mapListArray.Length; i++)
        {
            //Color c = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            //_mapListArray[i].mapImage.GetComponent<Image>().color = c;
            _mapListArray[i].mapImage.gameObject.SetActive(false);
        }

        foreach (var mapdata in _mapListArray)
        {
            //mapdata.mapCover = Instantiate(mapBlackArea).GetComponent<RectTransform>();
            mapList.Add(mapdata.scenceName, mapdata);           
        }
    }
    private void Update()
    {
        if (Input.GetButtonDown("B") && UIManager.instance.isInGame)
        {
            if (UIManager.instance.uiStack.Count == 0)
            {
                UIManager.instance.CallOpenUI(gameObject);
            }
            else if (UIManager.instance.uiStack.Peek().name == "MinimapUI")
            {
                UIManager.instance.CallCloseUI();
            }

            //isMapOpen = !isMapOpen;
            //GameManager.instance.SetLittleCasterControlActive(!isMapOpen);
        }

        /*if (Input.GetKey(KeyCode.Z))
        {
            ResizeUp();
        }
        else if (Input.GetKey(KeyCode.X))
        {
            ResizeDown();
        }*/
    }
    public void ChangeMap()
    {
        string curScenceName=SceneManager.GetActiveScene().name;
        if (mapList.ContainsKey(curScenceName))
        {
            curScence = mapList[curScenceName];
            curScence.mapImage.gameObject.SetActive(true);

            //Color c = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            //curScence.mapImage.GetComponent<Image>().color = c;

            curScenceZone = curScence.mapImage.parent.GetComponent<RectTransform>();
            allMap.anchoredPosition = new Vector2(-curScenceZone.anchoredPosition.x, -curScenceZone.anchoredPosition.y);
            // mapCam.localPosition = curScence.localPosition;
            //mapCam.localScale = curScence.localScale;

            mapHilight.position = curScence.mapImage.position;
            mapHilight.sizeDelta = curScence.mapImage.rect.size;
        }
    }

    public void OpenMap()
    {
        UIManager.instance.CallOpenUI(gameObject);
        //GetComponent<BaseUI>().OpenUI();
        //allMap.gameObject.SetActive(true);
        //Bg.SetActive(true);
        ChangeMap();
    }

    public void CloseMap()
    {
        UIManager.instance.CallCloseUI();
        //GetComponent<BaseUI>().CloseUI();
        //allMap.gameObject.SetActive(false);
        //Bg.SetActive(false);
        //ChangeMap();
    }

    void ResizeUp()
    {
        foreach (var mapdata in _mapListArray)
        {
            mapdata.mapImage.localScale *=1.2f;
            mapdata.mapImage.localPosition *= 1.2f;
           // mapCam.localPosition = curScence.localPosition;
            //mapCam.localScale = curScence.localScale;
        }

    }
    void ResizeDown()
    {
        foreach (var mapdata in _mapListArray)
        {
            mapdata.mapImage.localScale /= 1.2f;
            mapdata.mapImage.localPosition /= 1.2f;
            //mapCam.localPosition = curScence.localPosition;
          //  mapCam.localScale = curScence.localScale;
        }

    }

    public void SetMapSetMap(Dictionary<string, MapData> mapData)
    {
        foreach (var mapName in mapData.Keys)
        {
            //Color c = new Color(1.0f, 1.0f, 1.0f, 1.0f);

            //mapList[mapName].mapImage.GetComponent<Image>().color = c;
            try
            {
                mapList[mapName].mapImage.gameObject.SetActive(true);
            }
            catch
            {
                Debug.LogWarningFormat("map <{0}> is not found or error", mapName);
            }
        }
    }
}
