using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelmoveStill : barrelMove
{
    [SerializeField] Animator ani;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasBeenParryed || collision.gameObject.tag == "hurt")
        {
            explode();
        }

    }

    private void Update()
    {
        ani.SetFloat("speed", rb.velocity.magnitude);
        base.Update();
    }

    protected override void Start()
    {
        deactivateParry();
        hasBeenParryed = false;
        travelDistance = Mathf.Infinity;
    }
}
