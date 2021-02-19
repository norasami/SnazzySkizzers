using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skizzors : MonoBehaviour
{
    PlayerControls controls;
    Animator anim;
    Vector3 move;
    Vector3 rotate;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Cut.performed += ctx => Cut();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;

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
        Vector3 m = new Vector3(move.x, move.y, 0) * Time.deltaTime;
        transform.Translate(m, Space.World);

        Vector3 r = new Vector3(0, 0, -rotate.x) * 100f * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(r, m);
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
