using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
    }
    public void PlayClick()
    {
        AudioController.Instance.Play("click");
    }

    [YarnCommand("setcolorDad")]
    public void setcolorDad()
    {
        renderText.color = Color.red;
    }

    [YarnCommand("setcolorMe")]
    public void setcolorMe()
    {
        renderText.color = Color.white;
    }

    [YarnCommand("ShowImage")]
    public void showImage()
    {
        image.SetActive(true);
        StartCoroutine(fadeInImage());
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
}
