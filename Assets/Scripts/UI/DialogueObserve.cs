using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DialogueObserve
{
    void PrefromActionBeforeDialogue(int textIndex);
    void PrefromActionChoice(int textIndex,string choice);
    void PrefromActionEnd();
}
