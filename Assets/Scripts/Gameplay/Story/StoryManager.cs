using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public bool MusicOn = true;
    private void Start()
    {
        if (MusicOn)
        {
            Utils.WaitAndRun(1, () => AudioController.Instance.Play("Song1", true));
        }
    }
    public void PlayClick()
    {
        AudioController.Instance.Play("click");
    }
}
