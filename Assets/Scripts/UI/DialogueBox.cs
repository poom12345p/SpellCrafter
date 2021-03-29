using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueBox : MonoBehaviour
{
    public GameObject dialogueBoxUI;
    public Text dialogueText;
    public Text nameText;
    public float typingSpeed;
    [HideInInspector]
    public DialogueContainer container;
    DialogueContainer.Dialogue curentDialogue;

    bool isTyping;
    WaitForSeconds waitTyping;
    List<DialogueObserve> dialogueObserves;

    public static DialogueBox instance;
   

    bool isHoldInteract;
    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        waitTyping = new WaitForSeconds(typingSpeed);
        dialogueObserves = new List<DialogueObserve>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!dialogueBoxUI.active) return;
        if (Input.GetAxisRaw("Interaction") == 1 && !isHoldInteract && !isTyping)
        {
            Next();
            isHoldInteract = true;
        }

        else if (Input.GetAxisRaw("Interaction") == 0 && isHoldInteract)
        {

            isHoldInteract = false;
        }
    }
    IEnumerator Type()
    {
        dialogueText.text = "";
        isTyping = true;
        foreach(char letter in curentDialogue.text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return waitTyping;
        }
        isTyping = false;
          
    }
    void SetDialogueContainer(DialogueContainer dialogueContainer)
    {
        container = dialogueContainer;
    }

    public void ShowDialogueBox()
    {
        dialogueBoxUI.SetActive(true);
    }


    public void HideDialogueBox()
    {
        dialogueBoxUI.SetActive(false);
    }

    public void StartDialogue(DialogueContainer dialogueContainer)
    {
        GameManager.instance.SetLittleCasterControlActive(false);
        SetDialogueContainer(dialogueContainer);
        ShowDialogueBox();
        curentDialogue = container.Dialogues[container.startDialogue];
        //nameText.text = container.name;
        ShowText();
        //dialogueText.text = curentDialogue.text;
    }

    public void Next()
    {

        if (curentDialogue.linkTo != -1)
        {
            foreach (var obs in dialogueObserves)
            {
                obs.PrefromActionBeforeDialogue(curentDialogue.linkTo);
            }

            curentDialogue = container.Dialogues[curentDialogue.linkTo];
            //dialogueText.text = "";
            ShowText();

            
        }
        else
        {
            DialogueObserve checkDgo=null;
            //foreach (var obs in dialogueObserves)
            //{
            //    obs.PrefromActionEnd();
            //}
            for(int i=0;i<dialogueObserves.Count;i++)
            {
                if(i>0)
                {
                    if(dialogueObserves[i-1]!=checkDgo)
                    {
                        i--;
                    }
                }
                checkDgo = dialogueObserves[i];
                dialogueObserves[i].PrefromActionEnd();
            }
            EndDialogue();
        }
    }

    public void ShowText()
    {
        StartCoroutine("Type");
    }

    public void ShowAllText()
    {
      
    }


    public void EndDialogue()
    {
        HideDialogueBox();
        GameManager.instance.SetLittleCasterControlActive(true);
    }

    public void AddObserver(DialogueObserve dgo)
    {
        if(!dialogueObserves.Contains(dgo))
            dialogueObserves.Add(dgo);
    }

    public void RemoveObserver(DialogueObserve dgo)
    {
        if (dialogueObserves.Contains(dgo))
            dialogueObserves.Remove(dgo);
    }

}
