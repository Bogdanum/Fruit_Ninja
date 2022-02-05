using UnityEngine;

public class LobuleMovement : MonoBehaviour
{
    private float movementSpeed;

    public void SetSpeed(float speed)
    {
        if (speed != 0)
        {
            movementSpeed = speed;
        }
    } 
    
    private void Update()
    {
        transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;
    }
}
