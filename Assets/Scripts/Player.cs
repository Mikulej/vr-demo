using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float mouseSensetivity = 0.5f;
    [SerializeField] float speed = 300.0f;   
    [SerializeField] float raycastRange = 10.0f;  
    Vector3 velocity;
    Camera playerCamera;
    Vector3 cameraRotation;
    Rigidbody rb;
    AudioSource soundEmitter;
    [SerializeField] AudioClip[] footsteps_leaves = new AudioClip[5];
    [SerializeField] AudioClip[] footsteps_wood = new AudioClip[5];
    AudioClip[] footsteps_current;
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
        RaycastHit interactRay;
        LayerMask mask = LayerMask.GetMask("Raycastable");
        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out interactRay,raycastRange,mask)){
            GameObject other = interactRay.transform.gameObject;

            var interactable = other.gameObject.GetComponentInParent<Interactable>();
            if (interactable != null) {
                interactable.Interact();
            }
        }
    }

    public void OnTriggerEnter(Collider other){

    }

    public void OnTriggerExit(Collider other){

    }
   
    public void OnAttack(){ //Left Click pressed
        RaycastHit interactRay;
        LayerMask mask = LayerMask.GetMask("Raycastable");
        if(Physics.Raycast(playerCamera.transform.position,playerCamera.transform.forward,out interactRay,raycastRange,mask)){

            GameObject other = interactRay.transform.gameObject;

            var pickable = other.gameObject.GetComponent<Pickable>();
            if(pickable != null){
                if(carryItem != null){//Exchange item
                    carryItem.transform.position = transform.position + (transform.rotation*Vector3.forward);
                    var carryItemRigidBody = carryItem.GetComponent<Rigidbody>();
                    carryItemRigidBody.isKinematic = false;
                    carryItem = null;

                    carryItem = pickable.gameObject;
                    carryItemRigidBody = carryItem.GetComponent<Rigidbody>();
                    carryItemRigidBody.isKinematic = true;

                }
                else{ //Pick up item
                    carryItem = pickable.gameObject;
                    var carryItemRigidBody = carryItem.GetComponent<Rigidbody>();
                    carryItemRigidBody.isKinematic = true;
                }
            }
    
        }
        else if(carryItem != null){ //Drop item
            carryItem.transform.position = transform.position + (transform.rotation*Vector3.forward);
            var carryItemRigidBody = carryItem.GetComponent<Rigidbody>();
            carryItemRigidBody.isKinematic = false;
            carryItem = null;
        }


    }

    public void OnThrow(){ //Right click pressed
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
