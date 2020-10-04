using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class StoryManager : MonoBehaviour
{
    public bool MusicOn = true;
    public TextMeshProUGUI renderText;
    public GameObject image;
    private void Start()
    {
        if (MusicOn)
        {
            Utils.WaitAndRun(1, () => AudioController.Instance.Play("Song1", true));
        }
        Utils.WaitAndRun(1, () => loadStory(GameState.Instance.lvl));
    }
    private void loadStory(string name)
    {
        FindObjectOfType<DialogueRunner>().StartDialogue(GameState.Instance.lvl);
    }
    public void PlayClick()
    {
        AudioController.Instance.Play("click");
    }

    [YarnCommand("setcolorDad")]
    public void setcolorDad()
    {
        renderText.color = Color.white;
    }

    [YarnCommand("setcolorMe")]
    public void setcolorMe()
    {
        renderText.color = new Color(1f, 0.84f, 0.66f);
    }

    [YarnCommand("ShowImage")]
    public void showImage()
    {
        image.SetActive(true);
        StartCoroutine(fadeInImage());
    }

    [YarnCommand("LevelLoad")]
    public void LevelLoad(string[] parameters)
    {
        StartCoroutine(fadeOutImage(0.3f));
        if (parameters[0] == "Menu")
        {
            GameState.Instance.lvl = "story1";
            Utils.Instance._WaitAndRun(1f, () => SceneManager.LoadScene("Menu"));
        }
        else
        {
            GameState.Instance.lvl = parameters[0];
            Utils.Instance._WaitAndRun(3f, () => SceneManager.LoadScene("Gameplay"));
        }
    }

    IEnumerator fadeInImage(float speed = 0.3f)
    {
        var img = image.GetComponent<SpriteRenderer>();
        for (float i = 0; i <= 1; i += Time.deltaTime * speed)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
    IEnumerator fadeOutImage(float speed = 0.3f)
    {
        var img = image.GetComponent<SpriteRenderer>();
        for (float i = 1; i > 0; i -= Time.deltaTime * speed)
        {
            // set color with i as alpha
            img.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}
