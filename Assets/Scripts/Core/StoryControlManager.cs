using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using static UnityEngine.InputSystem.InputAction;

public class StoryControlManager : MonoBehaviour
{
    WorldControls controls;
    void Start()
    {
        controls = new WorldControls();


        controls.Story.Enable();

        controls.Story.Next.performed += nextDialog;

        Utils.WaitAndRun(0.5f, setLoops);
        
    }
    private void setLoops()
    {
        var memory = FindObjectOfType<InMemoryVariableStorage>();
        if (memory != null)
        {
            memory.SetValue("$looped", GameState.Instance.loopcount);
            memory.SetValue("looped", GameState.Instance.loopcount);
        }
    }
    private void nextDialog(CallbackContext ctx)
    {
        var dui = FindObjectOfType<DialogueUI>();
        if (dui)
        {
            dui.MarkLineComplete();
        }
    }
}
