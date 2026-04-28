using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float mouseSensetivity = 0.5f;
    [SerializeField] float speed = 500.0f;   
    Vector3 velocity;
    Camera playerCamera;
    Vector3 cameraRotation;
    Rigidbody rb;
    GameObject interactableGameObject = null;
    AudioSource soundEmitter;
    [SerializeField] AudioClip[] footsteps_leaves = new AudioClip[5];
    [SerializeField] AudioClip[] footsteps_wood = new AudioClip[5];
    AudioClip[] footsteps_current;

    void Start()
    { 
        playerCamera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody>();
        soundEmitter = GetComponent<AudioSource>();
        footsteps_current = footsteps_leaves;
        StartCoroutine(playFootstepsSounds(0.5f));
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

    public void OnInteract(InputValue input){
        if(interactableGameObject != null){
            var interactable = interactableGameObject.GetComponent<Interactable>();
            interactable.Interact();   
        }
    }

    public void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "Interactable") {
             interactableGameObject = other.gameObject;
        }
    }

    public void OnCollisionEnter(Collision other){
        switch(other.gameObject.tag){
            case "FloorLeaves":{
                footsteps_current = footsteps_leaves;
                break;
            }
            case "FloorWood":{
                footsteps_current = footsteps_wood;
                break;
            }
        }
    }

    private IEnumerator playFootstepsSounds(float waitTime){
        while(true){
            if(velocity != Vector3.zero){
                int r = Random.Range(0,5);
                soundEmitter.PlayOneShot(footsteps_current[r]);
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}
