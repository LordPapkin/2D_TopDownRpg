using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform lookAt;
    [SerializeField] private float boundX = 0.15f;
    [SerializeField] private float boundY = 0.05f;

    private float deltaX;
    private float deltaY;
    Vector3 delta;

    private void LateUpdate()
    {
        delta = Vector3.zero;

        //This is to check if we're inside the bounds on the X axis
        deltaX = lookAt.position.x - transform.position.x;
        if(deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            }
            else
            {
                delta.x = deltaX + boundX;
            }
        }
        //This is to check if we're inside the bounds on the Y axis
        deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < - boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
