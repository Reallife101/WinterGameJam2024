using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topDownMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator ani;
    [SerializeField] FMODUnity.EventReference FootstepSFX;
    [SerializeField] float footstepSFXSpeed;
    private float footstepSpeed = 0.5f;
    float timer = 0.0f;
    private Vector2 movement;


    // Update is called once per framessssssss
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        rb.velocity = movement*moveSpeed;

        ani.SetFloat("Horizontal", movement.x);
        ani.SetFloat("Vertical", movement.y);

        if ((timer > footstepSpeed) && (movement.x != 0 | movement.y != 0))
        {
            FMODUnity.RuntimeManager.PlayOneShot(FootstepSFX);
            timer = 0.0f;
        } 
        timer += footstepSFXSpeed;
    }
}
