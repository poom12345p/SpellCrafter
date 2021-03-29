using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable,DialogueObserve
{
    public DialogueContainer dialogue;

    public void Speak()
    {
        DialogueBox.instance.StartDialogue(dialogue);
        DialogueBox.instance.AddObserver(this);
        isActive = false;
        GameManager.instance.Player.moveHorizontal(0);
     
    }

    public void EndSpeak()
    {
        isActive = true;
        DialogueBox.instance.RemoveObserver(this);
    }
    public virtual void PrefromActionBeforeDialogue(int textIndex)
    {
    }

    public virtual void PrefromActionChoice(int textIndex, string choice)
    {

    }

    public virtual void PrefromActionEnd()
    {
        EndSpeak();
    }
}
