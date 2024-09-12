using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform playerTarget;
    public float moveSpeed;

    private void LateUpdate()
    {
        if(playerTarget != null)
        {
            if(playerTarget.position != transform.position)
            {
                transform.position = Vector3.Lerp(transform.position, playerTarget.position, moveSpeed * Time.deltaTime);
            }
        }
    }
}
