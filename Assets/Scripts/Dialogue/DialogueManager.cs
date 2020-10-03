using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public DialogueUI dialogueUI;
    public void PlayClick()
    {
        AudioController.Instance.Play("click");
    }
    public void AutoComplete(DialogueUI dui)
    {
        Utils.WaitAndRun(4.5f, () => dui.MarkLineComplete());
    }
}
