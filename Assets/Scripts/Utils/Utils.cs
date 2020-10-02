 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : Singleton<Utils>
{
    public static void WaitAndRun(float seconds, System.Action func)
    {
        Instance._WaitAndRun(seconds, func);
    }
    public void _WaitAndRun(float seconds, System.Action func)
    {
        StartCoroutine(_co_WaitAndRun(seconds, func));
    }
    IEnumerator _co_WaitAndRun(float seconds, System.Action func)
    {
        yield return new WaitForSeconds(seconds);
        func();
    }
}