using UnityEngine;
using System.Collections;

public class Goblet : MonoBehaviour
{
    AudioSource soundEmitter;
    [SerializeField] AudioClip[] hitSounds = new AudioClip[5];
    bool canPlaySound = false;
    public Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
        soundEmitter = GetComponent<AudioSource>();
        TemporaryMute();
    }

    void OnCollisionEnter(Collision other){
        if(canPlaySound ){
            int r = Random.Range(0,hitSounds.Length);
            soundEmitter.PlayOneShot(hitSounds[r]);
        }
    }
    void OnCollisionExit(){

    }
    public void TemporaryMute(){
        StartCoroutine(enableSoundAfterDelay(1.0f));
    }

    private IEnumerator enableSoundAfterDelay(float waitTime){
        canPlaySound = false;
        yield return new WaitForSeconds(waitTime);
        canPlaySound = true;
    }
}
