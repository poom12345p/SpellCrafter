using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField]
    AudioClip mainmenuAudio;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        gameManager.ChangSound(mainmenuAudio);
    }

    [SerializeField]
    Button[] menuBotton; 

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        gameManager.CreateNewSave();
        gameManager.StartGame();
    }

    public void StartLoadGame()
    {
        gameManager.LoadGameSave();
        gameManager.StartGame();
    }

    public void StartNewGameAllTest()
    {
        gameManager.CreateNewSave();
        gameManager.StartGame();
    }

    public void PreventClick()
    {
        foreach (var btn in menuBotton)
        {
            btn.interactable = false;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
