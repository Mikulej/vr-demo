using UnityEngine;
using System.Collections;
public class AmbientTriggerKnock : MonoBehaviour
{
    [SerializeField] Door door;
    [SerializeField] AudioSource soundEmitter;
    bool triggered = false;

    public void OnTriggerEnter(Collider other){
        if(triggered == false && door.open == false && other.tag == "Player"){
            StartCoroutine(waitTillPlaying(15.0f));
        }
    }

    public void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            StopAllCoroutines();
        }
    }

    private IEnumerator waitTillPlaying(float waitTime){
        yield return new WaitForSeconds(waitTime);
        soundEmitter.Play();
        triggered = true;
    }
}
