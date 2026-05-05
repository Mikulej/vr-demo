using UnityEngine;

public class FirstDoor : Interactable
{
    public bool open = true;
    Animator anim;
    Animation a;
    void Start(){
        anim = GetComponent<Animator>();
    }
    override public void Interact(){
        if (open){
            anim.SetTrigger("open");
        }
        else{
            anim.SetTrigger("close");
        }
        open = !open;
    }
}
