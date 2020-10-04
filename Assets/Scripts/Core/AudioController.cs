using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
    List<AudioClip> clipLibrary = new List<AudioClip>();
    List<AudioSource> active = new List<AudioSource>();

    private void Awake()
    {
        foreach (object o in Resources.LoadAll("Audio"))
        {
            clipLibrary.Add((AudioClip)o);
        }
    }
    private AudioSource InstantiateSource(AudioClip clip, bool loop)
    {
        if (active.Count > 1000)
        {
            refreshActive();
            if (active.Count > 1000)
            {
                Debug.Log("Too many sounds detected");
                return null;
            }
        }
        AudioSource source = gameObject.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
        if (loop)
        {
            source.loop = true;
            source.volume = 0.15f;
            foreach(var a in active)
            {
                Destroy(a);
            }
            active.Clear();
        }
        else
        {
            Destroy(source, clip.length);
        }
        active.Add(source);
        return source;
    }
    public void Play(string clip,bool loop=false) {
        AudioClip sound = clipLibrary.Find(x => x.name.ToLower() == clip.ToLower());
        if (sound)
        {
            InstantiateSource(sound, loop);
        }
        else
        {
            Debug.Log("Sound not found: " + clip);
        }
        
    }

    private void refreshActive()
    {
        active.RemoveAll((x) => x == null);
    }
}
