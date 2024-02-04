using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barrelmoveStill : barrelMove
{
    [SerializeField] Animator ani;
    [SerializeField] FMODUnity.EventReference explodeSFX1;

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasBeenParryed || collision.gameObject.tag == "hurt")
        {
            FMODUnity.RuntimeManager.PlayOneShot(explodeSFX1);
            explode();
        }

    }

    protected override void Update()
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

    protected override void onWallHit()
    {
        FMODUnity.RuntimeManager.PlayOneShot(explodeSFX1);
        explode();
    }
}
