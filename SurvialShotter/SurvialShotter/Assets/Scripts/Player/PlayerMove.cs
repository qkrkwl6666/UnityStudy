using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Drawing;

public class PlayerMove : MonoBehaviour
{
    private CinemachineBrain cinemachineBrain;
    private Animator animator;
    private CharacterController characterController;
    private float speed = 5f;

    private void Awake()
    {
        cinemachineBrain = Camera.main.GetComponent<CinemachineBrain>();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        if(!cinemachineBrain) return;

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 playerPos = transform.position;
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10.0f;
        Vector3 Point = cinemachineBrain.OutputCamera.ScreenToWorldPoint(mousePos);

        Vector3 dir = new Vector3(Point.x - playerPos.x, 0f, Point.z - playerPos.z);
        dir.Normalize();
        transform.forward = dir;

        if (x != 0 || z != 0)
        {
            animator.SetBool("isMove", true);
        }
        else
        {
            animator.SetBool("isMove", false);
        }

        Vector3 moveDir = new Vector3 (x, 0, z);
        moveDir.Normalize();

        characterController.Move(moveDir * Time.deltaTime * speed);
    }
}
