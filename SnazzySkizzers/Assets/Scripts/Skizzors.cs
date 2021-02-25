using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Skizzors : MonoBehaviour
{
    PlayerControls controls;
    Animator skizzorsAnim;
    Animator pantsAnimation;
    Vector3 move;
    Vector3 rotate;
    Transform skizzorsTransform;
    GameObject[] targetCrosses;

    int cutPoints;

    void Awake()
    {
        controls = new PlayerControls();

        controls.Gameplay.Cut.performed += ctx => Cut();

        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;

        controls.Gameplay.Rotate.performed += ctx => rotate = ctx.ReadValue<Vector2>();
        controls.Gameplay.Rotate.canceled += ctx => rotate = Vector2.zero;
        
        skizzorsTransform = GameObject.FindWithTag("Player").transform;

        skizzorsAnim = GetComponent<Animator>();

        targetCrosses = GameObject.FindWithTag("Manager").GetComponent<TargetManager>().targets;
        cutPoints = targetCrosses.Length;

        pantsAnimation = GameObject.FindWithTag("Clothing").GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 m = new Vector3(move.x, move.y, 0) * Time.deltaTime;
        transform.Translate(m, Space.World);
        
        Vector3 skizzorsPosition = skizzorsTransform.position;
        if ((skizzorsPosition.x + m.x) < -1.4f)
        {
            move.x = -1.4f - skizzorsPosition.x;
        }
        else if ((skizzorsPosition.x + m.x) > 1.4f)
        {
            move.x = 1.4f - skizzorsPosition.x;
        }
        else
        {
            m = new Vector3(move.x, move.y, 0) * Time.deltaTime;
        }

        if ((skizzorsPosition.y + m.y) < -0.9f)
        {
            move.y = -0.9f - skizzorsPosition.y;
        }
        else if ((skizzorsPosition.y + m.y) > 2.1f)
        {
            move.y = 2.1f - skizzorsPosition.y;
        }
        else
        {
            m = new Vector3(move.x, move.y, 0) * Time.deltaTime;
        }

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

    public void Cut()
    {
        if (skizzorsAnim != null)
        {
            skizzorsAnim.SetTrigger("UseSkizzors");
        }

        for(int i = 0; i < targetCrosses.Length; i++)
        {
            if(targetCrosses[i].GetComponent<SkizzorsCutPoint>().CuttingTime == true && targetCrosses[i].GetComponent<SkizzorsCutPoint>().crossAnim != null)
            {
                targetCrosses[i].GetComponent<SkizzorsCutPoint>().crossAnim.SetTrigger("CutCross");
                targetCrosses[i].GetComponent<SkizzorsCutPoint>().crossCut = true;
                Debug.Log("Cross Cut is true");

                cutPoints--;
            }
            else
            {
                targetCrosses[i].GetComponent<SkizzorsCutPoint>().crossCut = false;
                Debug.Log("Cross Cut is false");
            }
        }

        if(cutPoints <= 0)
        {
            pantsAnimation.SetTrigger("AllCrossesCut");
        }
    }
}
