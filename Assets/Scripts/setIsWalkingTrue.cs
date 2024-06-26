using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setIsWalkingTrue : MonoBehaviour
{
    private bool isWalking = false;
    [SerializeField] private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }
        animator.SetBool("isWalking", isWalking);
    }
}
