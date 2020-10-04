using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static UnityEngine.InputSystem.InputAction;

public class Controlmanager : MonoBehaviour
{
    WorldControls controls;
    Camera mainCamera;

    //because new input system is fucking bullshit
    bool isMouseDown;
    List<Ingredient> selectedIngredients = new List<Ingredient>();
    GameObject potentialClient;

    LineRenderer linerenderer;


    readonly List<(string, System.Action<Controlmanager, GameObject>)> hitResponses = new List<(string, System.Action<Controlmanager, GameObject>)>()
    {
        ("Ingredient",(ins,go)=>ins.selectIngredient(go)),
        ("Client",(ins,go)=> ins.pointClient(go))
    };

    void Start()
    {
        controls = new WorldControls();


        controls.Player.Enable();

        controls.Player.Select.started += clickDown;
        controls.Player.Select.canceled += clickUp;
        controls.Player.PowerReset.performed += loop;
        mainCamera = Camera.main;

        linerenderer = GetComponent<LineRenderer>();
    }

    public void Update()
    {
        checkforexpired();
        checkInput();
        updateLines();
    }
    void checkforexpired()
    {
        List<Ingredient> toremove = new List<Ingredient>();
        foreach(var ing in selectedIngredients)
        {
            if (ing == null)
            {
                toremove.Add(ing);
            }
        }
        foreach(var ing in toremove)
            selectedIngredients.Remove(ing);
        if (selectedIngredients.Count == 0)
            resetLine();
        potentialClient = null;
    }
    void resetLine()
    {
        linerenderer.positionCount = 0;
    }
    void checkInput()
    {
        if (isMouseDown)
        {
            Vector2 pos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, ContactFilter2D.NormalAngleUpperLimit);
            if (hit.collider != null)
            {
                foreach ((var tag, var func) in hitResponses)
                {
                    if (hit.transform.CompareTag(tag))
                    {
                        func(this, hit.transform.gameObject);
                    }
                }
            }
        }
    }
    void updateLines()
    {
        if (!isMouseDown || selectedIngredients.Count<=0)
            return;
        var lpos = selectedIngredients.Count + 1;
        linerenderer.positionCount = lpos;
        for(var i = 0; i < lpos-1; i++) {
            linerenderer.SetPosition(i, selectedIngredients[i].transform.position);
        }
        Vector3 mpos;
        if (potentialClient)
            mpos = potentialClient.transform.position;
        else
            mpos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        linerenderer.SetPosition(lpos-1, new Vector2(mpos.x,mpos.y));
    }

    private void clickDown(CallbackContext ctx)
    {
        isMouseDown = true;
    }
    private void clickUp(CallbackContext ctx)
    {
        if (potentialClient)
        {
            serveOrder();
        }
        unselectAll();
        isMouseDown = false;
    }

    private void loop(CallbackContext ctx)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    private void selectIngredient(GameObject go)
    {
        var ing = go.GetComponent<Ingredient>();
        if (ing == null || selectedIngredients.Contains(ing))
            return;
        ing.Select();
        selectedIngredients.Add(ing);
    }
    void pointClient(GameObject go)
    {
        potentialClient = go;
    }
    void serveOrder()
    {
        var client = potentialClient.GetComponent<Client>();
        if (client == null)
            return;
        client.Serve(selectedIngredients);
    }
    void unselectAll()
    {
        foreach (var ing in selectedIngredients)
        {
            if (ing != null)
                ing.Unselect();
        }
        selectedIngredients.Clear();
        linerenderer.positionCount = 0;
    }
}
