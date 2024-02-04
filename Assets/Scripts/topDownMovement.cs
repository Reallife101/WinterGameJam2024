using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topDownMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Animator ani;
    private Vector2 movement;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();
        rb.velocity = movement*moveSpeed;

        ani.SetFloat("Horizontal", movement.x);
        ani.SetFloat("Vertical", movement.y);
    }
}
