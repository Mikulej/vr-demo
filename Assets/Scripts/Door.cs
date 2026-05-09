using UnityEngine;

public class FirstDoor : Interactable
{
    public bool open = true;
    [SerializeField] bool locked = false;
    AudioSource soundEmitter;
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
        if(locked){
            soundEmitter.PlayOneShot(lockedSound);
        }
        else{
            if (open){
            anim.SetTrigger("open");
            }
            else{
                anim.SetTrigger("close");
            }
            soundEmitter.PlayOneShot(interactSound);
            open = !open;
        }
    }
    public void Unlock(){
        open = true;
        locked = false;
        anim.SetTrigger("open");
        soundEmitter.PlayOneShot(unlockSound);
    }
}
