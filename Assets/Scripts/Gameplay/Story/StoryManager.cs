using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public void PlayClick()
    {
        AudioController.Instance.Play("click");
    }
}
