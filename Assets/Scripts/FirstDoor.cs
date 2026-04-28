using UnityEngine;

public class FirstDoor : Interactable
{
    public bool open = true;
    override public void Interact(){
        if (open){
            transform.localRotation = Quaternion.AngleAxis(0.0f, Vector3.up);
        }
        else{
            transform.localRotation = Quaternion.AngleAxis(90.0f, Vector3.up);
        }
        open = !open;
    }
}
