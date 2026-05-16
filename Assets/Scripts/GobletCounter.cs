using UnityEngine;
using System.Collections;
public class GobletCounter : MonoBehaviour
{
    int count = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other){
        if(other.tag == "goblet"){
            count++;
        }
    }

    public void OnTriggerExit(Collider other){
        if(other.tag == "goblet"){
            count--;
            if(count==0){
                Debug.Log("No goblets!");
            }
        }
    }
}
