using UnityEngine;
public class GobletCounter : MonoBehaviour
{
    public int count = 0;
    public void OnTriggerEnter(Collider other){
        if(other.tag == "goblet"){
            count++;
        }
    }

    public void OnTriggerExit(Collider other){
        if(other.tag == "goblet"){
            count--;
        }
    }
}
