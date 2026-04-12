using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private float speed = 10.0f;   
    Vector3 velocity;

    void Start()
    {
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position += velocity * step;
    }
    public void OnJump()
    {
        Debug.Log("Jump!");
    }

    public void OnMove(InputValue value)
    {
        var v = value.Get<Vector2>();

        velocity = new Vector3(v.x,0,v.y);

    }
}
