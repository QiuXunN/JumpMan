using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MovingEnetity
{
    private Animator anime;
    public override void Start()
    {
        base.Start();
        anime = GetComponentInChildren<Animator>();
    }

    public override void Update()
    {
        base.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.parent = this.transform;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.transform.parent = null;
    }
}
