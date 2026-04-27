using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float mouseSensetivity = 0.5f;
    [SerializeField] float speed = 500.0f;   
    Vector3 velocity;
    Camera playerCamera;
    Vector3 cameraRotation;
    Rigidbody rb;

    void Start()
    { 
        playerCamera = GetComponentInChildren<Camera>();
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        rb.linearVelocity = Quaternion.AngleAxis(cameraRotation.x, Vector3.up) * velocity * speed * Time.deltaTime;
    }
    public void OnJump()
    {
        Debug.Log("Jump!");
    }

    public void OnMove(InputValue input)
    {
        var v = input.Get<Vector2>();
        velocity = new Vector3(v.x,0,v.y);
    }

    public void OnLook(InputValue input){
        var v = input.Get<Vector2>() * mouseSensetivity;
        cameraRotation.x += v.x;
        cameraRotation.y += v.y;
        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -90f,90f);

        transform.localRotation = Quaternion.AngleAxis(cameraRotation.x, Vector3.up);
        playerCamera.transform.localRotation = Quaternion.AngleAxis(cameraRotation.y, Vector3.left);
    }
}
