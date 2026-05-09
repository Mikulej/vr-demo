using UnityEngine;

public class AppleCounter : MonoBehaviour
{
    [SerializeField] GameObject doorToUnlock;
    Door door;
    bool doorIsLocked = true;
    int count = 0;
    void Start()
    {
        door = doorToUnlock.GetComponent<Door>();
    }
    public void OnTriggerEnter(Collider other){
        if(other.tag == "Apple"){
            count++;
            if(count==5 && doorIsLocked){
                door.Unlock();
                doorIsLocked = false;
            }
        }
    }

    public void OnTriggerExit(Collider other){
        if(other.tag == "Apple"){
            count--;
        }
    }
}
