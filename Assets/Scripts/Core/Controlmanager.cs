using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Controlmanager : MonoBehaviour
{
    WorldControls controls;
    Camera mainCamera;

    //because new input system is fucking bullshit
    bool isMouseDown;
    List<Ingredient> selectedIngredients = new List<Ingredient>();
    readonly List<(string, System.Action<Controlmanager, GameObject>)> hitResponses = new List<(string, System.Action<Controlmanager,GameObject>)>()
    {
        ("Ingredient",(ins,go)=>ins.selectIngredient(go))
    };

    void Start()
    {
        controls = new WorldControls();


        controls.Player.Enable();

        controls.Player.Select.started += clickDown;
        controls.Player.Select.canceled += clickUp;
        mainCamera = Camera.main;
    }

    public void Update()
    {
        if (isMouseDown)
        {
            Vector2 pos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, ContactFilter2D.NormalAngleUpperLimit);
            if (hit.collider != null)
            {
                foreach ((var tag, var func) in hitResponses)
                {
                    if (hit.transform.tag == tag)
                    {
                        func(this, hit.transform.gameObject);
                    }
                }
            }
        }
    }
    public void clickDown(CallbackContext ctx)
    {
        isMouseDown = true;
        Debug.Log("DOWN");
    }
    public void clickUp(CallbackContext ctx)
    {
        foreach (var ing in selectedIngredients)
        {
            if(ing!=null)
                ing.Unselect();
        }
        isMouseDown = false;
    }

    private void selectIngredient(GameObject go)
    {
        var ing = go.GetComponent<Ingredient>();
        if (ing == null)
            return;
        ing.Select();
        selectedIngredients.Add(ing);
    }

}
