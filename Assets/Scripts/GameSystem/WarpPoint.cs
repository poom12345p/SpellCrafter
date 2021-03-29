using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WarpPoint : MonoBehaviour
{
    public string warpToScence;
    public int spawnPointNumber;
    MapManager mapManager;
    public float moveDir;
    bool isWarping;
    // Start is called before the first frame update
    void Start()
    {
        mapManager = GameObject.FindGameObjectWithTag("MapManager").GetComponent<MapManager>();
    }

    private void Update()
    {
        if(isWarping)
        {
            GameManager.instance.Player.moveHorizontal(moveDir);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //if(warpToScence != mapManager.myScence)
            //{
            //    SceneManager.LoadScene(warpToScence);
            //}
            MapManager.instance.SaveMap();
            GameManager.instance.loadMode = GameManager.LoadMode.WARP;
            GameManager.instance.Warp(warpToScence, spawnPointNumber);
            isWarping = true;
           // GameManager.instance.Player.moveHorizontal(moveDir);

        }
    }
}
