using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class DialogueManager : MonoBehaviour
{
    public void PlayClick()
    {
        AudioController.Instance.Play("click");
    }
    public void AutoComplete(DialogueRunner dr)
    {
        Utils.WaitAndRun(4.5f, () => nextLine(dr));
    }
    private void nextLine(DialogueRunner dr)
    {
        if(dr.IsDialogueRunning)
        {
            dr.gameObject.GetComponent<DialogueUI>().MarkLineComplete();
        }
    }
}
