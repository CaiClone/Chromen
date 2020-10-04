using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public SpriteRenderer logo;
    public Sprite newlogo;
    public void Start()
    {
        if (GameState.Instance.loopcount >= 1)
        {
            logo.sprite = newlogo;
        }
    }
    public void LoadScene()
    {
        AudioController.Instance.Play("beep");
        Utils.WaitAndRun(0.5f, () => SceneManager.LoadScene("Story"));
    }
}
