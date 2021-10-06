
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform lookAt;
    public float boundXWalk = 5f;
    public float boundXSprint = 3f;
    public bool isSprinting;
    public float boundY = 0.05f;

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float boundX = isSprinting ? boundXSprint : boundXWalk;
        float deltaX = lookAt.position.x - transform.position.x;
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

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
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
