using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed=2f;
    [SerializeField] private float runSpeed=4f;
    [SerializeField] private float rotationSpeed = 2f;
    private Vector2 moveDir = Vector2.zero;

    private bool isWalking = false;
    private bool isRunning= false;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update

    PlayerInput playerInput;
    private bool isIdle;

    void Start()
    {
        playerInput = gameObject.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        handleActions();
        
    }


    private void handleActions()
    {
        int actionNumber = 0;
        if (playerInput.actions["action1"].IsPressed()) actionNumber = 1;
        //else if (playerInput.actions["action2"].IsPressed()) actionNumber = 2;
        //else if (playerInput.actions["action3"].IsPressed()) actionNumber = 3;
        //else if (playerInput.actions["action4"].IsPressed()) actionNumber = 4;
        if (isIdle) animator.SetInteger("action",actionNumber);
        else animator.SetInteger("action", 0);
    }

    private void handleMovement()
    {
        Vector2 moveDirection = playerInput.actions["move"].ReadValue<Vector2>() * moveSpeed / 10f * Time.deltaTime;
        bool run = playerInput.actions["speed"].IsPressed();
        if(moveDirection.x == 0 && moveDirection.y == 0)
        {
            isIdle = true;
        }
        else
        {
            isIdle = false;
        }
        float speed = moveSpeed;
        
        moveDir = moveDirection;
        
        moveDir = moveDir.normalized;
        isWalking = (moveDir != Vector2.zero);
        
        if (isWalking && run)
        {
            isRunning = true;
            isWalking = false;
            speed += runSpeed;
        }
        else
        {
            isRunning = false;
        }
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        Debug.Log(speed+" "+isRunning+" "+isWalking);
        transform.position = transform.position + (new Vector3(moveDir.x,0f,moveDir.y) * speed * Time.deltaTime);
        transform.forward = Vector3.Slerp(transform.forward, new Vector3(moveDir.x, 0f, moveDir.y), rotationSpeed*Time.deltaTime);
    }
}
