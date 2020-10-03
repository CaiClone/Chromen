using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : Singleton<GameState>
{
    public int lvlseed = 42;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
