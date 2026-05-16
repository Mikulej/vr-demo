using UnityEngine;
using System.Collections;

public class Door : Interactable
{
    public bool open = true;
    public bool isMoving = false;
    [SerializeField] public bool locked = false;
    public AudioSource soundEmitter;
    [SerializeField] AudioClip lockedSound;
    [SerializeField] AudioClip unlockSound;
    [SerializeField] AudioClip interactSound;
    Animator anim;
    Animation a;
    void Start(){
        anim = GetComponent<Animator>();
        soundEmitter = GetComponent<AudioSource>();
    }
    override public void Interact(){
        if(isMoving == false){
            if(locked){
                soundEmitter.PlayOneShot(lockedSound);
            }
            else{
                if (open){
                    anim.SetTrigger("close");
                }
                else{
                    anim.SetTrigger("open");
                }
                StartCoroutine(SetMovement(1.0f));
                soundEmitter.PlayOneShot(interactSound);
                open = !open;
            }
        }
    }
    public void Unlock(){
        open = true;
        locked = false;
        anim.SetTrigger("open");
        soundEmitter.PlayOneShot(unlockSound);
    }
    private IEnumerator SetMovement(float waitTime){
        isMoving = true;
        yield return new WaitForSeconds(waitTime);
        isMoving = false;
    }
}
