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
