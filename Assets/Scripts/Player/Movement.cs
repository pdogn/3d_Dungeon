using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum state
{
    idle,
    run,
    attack
}

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float moveSpeed = 5f;

    [SerializeField] Transform cam;

    Animator anim;
    float horInput, verInput;

    state crrState;
    bool stateComplete;

    // Start is called before the firs frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Ani();
    }

    void Move()
    {
        horInput = Input.GetAxisRaw("Horizontal") * moveSpeed;
        verInput = Input.GetAxisRaw("Vertical") * moveSpeed;

        if(horInput != 0 || verInput != 0)
        {
            Vector3 camForward = cam.forward;
            Vector3 camRight = cam.right;

            camForward.y = 0;
            camRight.y = 0;

            Vector3 forwardRelative = verInput * camForward;

            Vector3 rightRelative = horInput * camRight;

            Vector3 moveDir = (forwardRelative + rightRelative).normalized;

            Quaternion tg = Quaternion.LookRotation(moveDir);
            this.transform.rotation = Quaternion.Lerp(transform.rotation, tg, .3f);

            rb.velocity = new Vector3(moveDir.x * moveSpeed, 0, moveDir.z * moveSpeed);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
    
    void Ani()
    {
        if(horInput != 0 || verInput != 0)
        {
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogError("attack");
        }
    }

    void UpdateState()
    {
        switch (crrState)
        {
            case state.idle:
                //do idle
                startIdle();
                break;
            case state.run:
                //do run
                startRun();
                break;
            case state.attack:
                //do attack
                startAttack();
                break;
        }
    }

    void selectState()
    {
        if (stateComplete == false) return;

        if (horInput != 0 || verInput != 0)
        {
            crrState = state.run;
        }
        else
        {
            crrState = state.idle;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.LogError("attack");
            crrState = state.attack;
        }
    }

    void startIdle()
    {
        anim.Play("idle");
    }
    void startRun()
    {
        anim.Play("run");
    }
    void startAttack()
    {
        anim.Play("attack");
    }
}
