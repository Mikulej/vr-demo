using UnityEngine;

public class Goblet : MonoBehaviour
{
    AudioSource soundEmitter;
    [SerializeField] AudioClip[] hitSounds = new AudioClip[5];
    void Start()
    {
        soundEmitter = GetComponent<AudioSource>();
    }

    void OnCollisionEnter(){
        int r = Random.Range(0,hitSounds.Length);
        soundEmitter.PlayOneShot(hitSounds[r]);
        Debug.Log("Play sound!");
    }
    void OnCollisionExit(){

    }
}
