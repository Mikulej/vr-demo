using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float mouseSensetivity = 0.5f;
    [SerializeField] float speed = 300.0f;   
    Vector3 velocity;
    Camera playerCamera;
    Vector3 cameraRotation;
    Rigidbody rb;
    GameObject interactableGameObject = null;
    AudioSource soundEmitter;
    [SerializeField] AudioClip[] footsteps_leaves = new AudioClip[5];
    [SerializeField] AudioClip[] footsteps_wood = new AudioClip[5];
    AudioClip[] footsteps_current;
    GameObject potentialCarryItem;
    GameObject carryItem = null;

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

    }

    private void FixedUpdate(){
        rb.linearVelocity = Quaternion.AngleAxis(cameraRotation.x, Vector3.up) * velocity * speed * Time.deltaTime;
        if(carryItem != null){
            carryItem.transform.position = transform.position + (Quaternion.AngleAxis(cameraRotation.x, Vector3.up) * new Vector3(0.29f,0.4f,0.8f));
            carryItem.transform.rotation = transform.rotation;
        }
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

    public void OnInteract(InputValue input){// E pressed
        if(interactableGameObject != null){
            var interactable = interactableGameObject.GetComponent<Interactable>();
            interactable.Interact();   
        }
    }

    public void OnTriggerEnter(Collider other){
        var pickable = other.gameObject.GetComponent<Pickable>();
        if (other.gameObject.tag == "Interactable") {
             interactableGameObject = other.gameObject;
        }
        if(pickable != null){
            potentialCarryItem = pickable.gameObject;
        }
    }
   
    public void OnAttack(){ //Left Click pressed
        Debug.Log("Pickup!");
        if(potentialCarryItem != null){
            carryItem = potentialCarryItem;
            var carryItemRigidBody = carryItem.GetComponent<Rigidbody>();
            carryItemRigidBody.isKinematic = true;
        }
    }

    public void OnThrow(){ //Right click pressed
        Debug.Log("Throw!");
        if(carryItem != null){
            carryItem.transform.position = transform.position + (transform.rotation*Vector3.forward);
            var carryItemRigidBody = carryItem.GetComponent<Rigidbody>();
            carryItemRigidBody.isKinematic = false;
            carryItemRigidBody.linearVelocity = Quaternion.AngleAxis(cameraRotation.x, Vector3.up)*  Quaternion.AngleAxis(cameraRotation.y, Vector3.left) * Vector3.forward * 2000 * Time.deltaTime;
            carryItem = null;
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
