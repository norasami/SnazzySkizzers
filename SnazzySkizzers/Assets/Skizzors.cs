using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skizzors : MonoBehaviour
{
    PlayerControls controls;
    Animator anim;
    Vector2 move;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Cut.performed += ctx => Cut();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        anim = GetComponent<Animator>();
    }

    void Cut()
    {
        if (anim != null)
        {
            anim.SetTrigger("UseSkizzors");
        }
    }

    void Update()
    {
        Vector2 m = new Vector2(move.x, move.y) * Time.deltaTime;
        transform.Translate(m, Space.World);
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
