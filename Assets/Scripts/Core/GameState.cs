using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class GameState :MonoBehaviour
{
    public int lvlseed = 42;
    public string lvl = "Level1";
    public int loopcount = 0;

    public static GameState Instance;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    public void loopOnce()
    {
        loopcount++;
        var memory = FindObjectOfType<InMemoryVariableStorage>();
        if (memory != null)
        {
            memory.SetValue("$looped", loopcount);
        }
    }
}
