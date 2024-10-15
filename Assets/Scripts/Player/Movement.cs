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

    [SerializeField] state crrState;
    public bool stateComplete;

    public bool attacking;
    float timeToAttack = .5f;
    float t = 0f;
    // Start is called before the firs frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        crrState = state.idle;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        //Ani();
        //checkAttack();
        if (stateComplete)
        {
            selectState();
        }
        UpdateState();
        checkAttack();
    }

    void Move()
    {
        horInput = Input.GetAxisRaw("Horizontal") * moveSpeed;
        verInput = Input.GetAxisRaw("Vertical") * moveSpeed;

        if((horInput != 0 || verInput != 0) && attacking == false)
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
                updateIdle();
                break;
            case state.run:
                //do run
                updateRun();
                break;
            case state.attack:
                //do attack
                updateAttack();
                break;
        }
    }

    void selectState()
    {
        stateComplete = false;
        if (attacking == false)
        {
            if ((horInput != 0 || verInput != 0))
            {
                crrState = state.run;
                startRun();
            }
            else
            {
                crrState = state.idle;
                startIdle();
            }
        }

        if (attacking)
        {
            crrState = state.attack;
            startAttack();
        }
    }

    void startIdle()
    {
        anim.Play("Idle01");
    }
    void updateIdle()
    {
        if ((horInput != 0 || verInput != 0))
        {
            stateComplete = true;
        }
    }
    void startRun()
    {
        anim.Play("BattleRunForward");
    }
    void updateRun()
    {
        if ((horInput == 0 && verInput == 0))
        {
            stateComplete = true;
        }
    }
    
    void startAttack()
    {
        Debug.LogError("attack");
        anim.Play("Attack01");
    }
    void updateAttack()
    {
        Debug.LogError("attacking");
        t += Time.deltaTime;
        if(t>= timeToAttack)
        {
            attacking = false;
            t = 0f;
            stateComplete = true;
        }
    }

    void checkAttack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attacking = true;
            stateComplete = true;
        }
    }
}
