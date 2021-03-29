using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text potionText;
    public ProcessBar playerHpBar;
    public ProcessBar playerMpBar;
    public Animator fadeScreen;
    public GameObject gamePlayUI, SaveTableUI;
    SaveTableUI _SaveTableUI;
    public DialogueBox dialogueBox;

    public MapSystem mapSystem;

    private static UIManager m_instance;
    public bool isFaded;

    float fadeInTime;
    float fadeOutTime;

    public ShowCaseItem showCaseItemUI, showCaseHintUI;

    public Stack<GameObject> uiStack = new Stack<GameObject>();

    [HideInInspector]
    public bool isInGame = false;

    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = (UIManager)FindObjectOfType(typeof(UIManager));
                if (m_instance == null)
                {
                    Debug.LogError(typeof(UIManager) + "not found.");
                }
            }
            return m_instance;
        }
    }
    // Start is called before the first frame update
    IEnumerator SetStartUIWaitFade()
    {
        //GameManager.instance.SetLittleCasterControlActive(false);
        yield return new WaitUntil(() => UIManager.instance.isFaded == true);

        gamePlayUI.SetActive(true);
        // GameManager.instance.SetLittleCasterControlActive(true);
    }

    IEnumerator SetFallUIWaitFade()
    {
        //GameManager.instance.SetLittleCasterControlActive(false);
        yield return new WaitUntil(() => UIManager.instance.isFaded == true);
        GameManager.instance.BackToMainMenuSetting();
        //GameManager.instance.PauseCanvas.SetActive(false);
        gamePlayUI.SetActive(false);
        // GameManager.instance.SetLittleCasterControlActive(true);
    }

    private void Awake()
    {
        if (UIManager.instance.gameObject != gameObject)
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        fadeScreen.gameObject.SetActive(true);
        AnimationClip[] clips = fadeScreen.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "FadeScreenOut":
                    fadeOutTime = clip.length;
                    break;
                case "FadeScreenIn":
                    fadeInTime = clip.length;
                    break;
            }
        }


        dialogueBox.HideDialogueBox();
        _SaveTableUI = SaveTableUI.GetComponent<SaveTableUI>();
        SaveTableUI.SetActive(false);
    }


    public void updatePotionText(int i)
    {
        potionText.text = i.ToString();
    }
    public void SetStartGame()
    {
        StartCoroutine("SetStartUIWaitFade");
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<MainUnit>().SetHpbar(playerHpBar);
        player.GetComponent<ManaSystem>().SetMpBar(playerMpBar);
        player.GetComponent<LittleCasterMove>().SetUIManagr(this);
        //gamePlayUI.SetActive(true);
    }
    public void SetStartGameTest()
    {
        //StartCoroutine("SetStartUIWaitFade");
        var player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<MainUnit>().SetHpbar(playerHpBar);
        player.GetComponent<ManaSystem>().SetMpBar(playerMpBar);
        player.GetComponent<LittleCasterMove>().SetUIManagr(this);
        //gamePlayUI.SetActive(true);
    }

    public void FadeIn()
    {
        if (!isFaded)
        {
            fadeScreen.SetBool("SHOW", true);
            Invoke("SetIsFaded", fadeInTime);
        }
    }

    public void FadeOut()
    {
        if (isFaded)
        {
            fadeScreen.SetBool("SHOW", false);
            Invoke("SetUnFaded", fadeOutTime);
            MapUISetUp();
        }
    }


    public void FadeOut(Scene scene, LoadSceneMode mode)
    {
        if (isFaded)
        {
            fadeScreen.SetBool("SHOW", false);
            Invoke("SetUnFaded", fadeOutTime);
            MapUISetUp();
        }
    }

    void MapUISetUp()
    {
        mapSystem.ChangeMap();
    }

    void SetIsFaded()
    {
        isFaded = true;
    }

    void SetUnFaded()
    {
        isFaded = false;
    }

    public void OpenSaveUITaable()
    {
        SaveTableUI.SetActive(true);
        _SaveTableUI.UpdateTable();
        GameManager.instance.SetLittleCasterControlActive(false);
    }

    public void CloseSaveUITaable()
    {
        SaveTableUI.SetActive(false);
        GameManager.instance.SetLittleCasterControlActive(true);
    }

    public void ReturnToMenu()
    {
        GameManager.instance.FadeLoadScence("MainMenu");
        GameManager.instance.UnPauseGame();
        StartCoroutine("SetFallUIWaitFade");

        foreach (GameObject g in uiStack) CallCloseUI();
    }


    public void OpenShowCaseItemUI(SItem stem)
    {
        showCaseItemUI.Open(stem);
    }

    public void OpenHintUI(SItem info)
    {
        showCaseHintUI.Open(info);
    }

    public void SetMap(Dictionary<string, MapData> mapData)
    {
        mapSystem.SetMapSetMap(mapData);
    }
    public void CallOpenUI(GameObject goj)
    {
        if (uiStack.Count == 0 || goj.name == "Pause")
        {
            uiStack.Push(goj);
            uiStack.Peek().GetComponent<BaseUI>().OpenUI();
        }
    }

    public void CallCloseUI()
    {
        if (uiStack.Count > 0) uiStack.Pop().GetComponent<BaseUI>().CloseUI();
    }

    //public void ActiveDialogue(DialogueContainer dialogueContainer)
    //{
    //    dialogueBox.StartDialogue(dialogueContainer);
    //}

    //public void ActiveDialogue(DialogueContainer dialogueContainer)
    //{
    //    dialogueBox.StartDialogue(dialogueContainer);
    //}
}
