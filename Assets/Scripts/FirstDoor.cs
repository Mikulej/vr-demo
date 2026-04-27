using UnityEngine;

public class FirstDoor : MonoBehaviour
{
    public void OpenDoor(){
        transform.localRotation = Quaternion.AngleAxis(90.0f, Vector3.up);
    }
}
