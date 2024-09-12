using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingEnetity : MonoBehaviour
{
    [SerializeField] public Transform PosA, PosB;
    public Transform movePos;
    [SerializeField] public float moveSpeed;

    public virtual void Start()
    {
        movePos = PosA;
    }

    public virtual void Update()
    {
        if(Vector2.Distance(transform.position, PosA.position) < 0.1f)
        {
            movePos = PosB;
        }
        if(Vector2.Distance(transform.position, PosB.position) < 0.1f)
        {
            movePos = PosA;
        }
        transform.position = Vector2.MoveTowards(transform.position, movePos.position, moveSpeed * Time.deltaTime);
    }
}
