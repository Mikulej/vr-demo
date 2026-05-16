using UnityEngine;
using System.Collections;

public class Goblet : MonoBehaviour
{
    AudioSource soundEmitter;
    [SerializeField] AudioClip[] hitSounds = new AudioClip[5];
    bool canPlaySound = false;
    void Start()
    {
        soundEmitter = GetComponent<AudioSource>();
        StartCoroutine(enableSoundAfterDelay(1.0f));
    }

    void OnCollisionEnter(Collision other){
        if(canPlaySound ){
            int r = Random.Range(0,hitSounds.Length);
            soundEmitter.PlayOneShot(hitSounds[r]);
        }
    }
    void OnCollisionExit(){

    }

    private IEnumerator enableSoundAfterDelay(float waitTime){
        yield return new WaitForSeconds(waitTime);
        canPlaySound = true;
    }
}
