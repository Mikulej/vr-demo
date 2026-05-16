using UnityEngine;
using System.Collections;

public class GobletReset : MonoBehaviour
{
    [SerializeField] GameObject roomDoor;
    Door door;
    bool resetPossible = false;
    bool playerInside = false;
    bool performingReset = false;
    [SerializeField] AudioSource[] soundEmitters = new AudioSource[3];
    [SerializeField] AudioClip lockDoorSound;
    [SerializeField] AudioClip[] gobletSounds = new AudioClip[5];
    [SerializeField] AudioClip[] monsterSounds = new AudioClip[5];
    void Start()
    {
        door = roomDoor.GetComponent<Door>();
    }

    void Update()
    {
        if(performingReset == false && resetPossible && playerInside == false && door.isMoving == false && door.open == false){
            door.locked = true;
            Debug.Log("Goblet Reset!");
            StartCoroutine(GobletResetSequence());
        }
    }
    public void OnTriggerEnter(Collider other){
        if(other.tag == "Player"){
            resetPossible = true;
            playerInside = true;
        }
    }

    public void OnTriggerExit(Collider other){
        if(other.tag == "Player"){
            playerInside = false;
        }
    }

    private IEnumerator GobletResetSequence(){
        //Start
        performingReset = true;
        door.soundEmitter.PlayOneShot(lockDoorSound);
        //Shenanigans
        yield return new WaitForSeconds(1);
        soundEmitters[0].PlayOneShot(monsterSounds[0]);
        yield return new WaitForSeconds(4);
        soundEmitters[0].PlayOneShot(monsterSounds[1]);
        yield return new WaitForSeconds(2);
        soundEmitters[1].PlayOneShot(monsterSounds[0]);
        yield return new WaitForSeconds(4);
        int r = Random.Range(0,gobletSounds.Length);
        soundEmitters[2].PlayOneShot(gobletSounds[r]);
        yield return new WaitForSeconds(1);
        r = Random.Range(0,gobletSounds.Length);
        soundEmitters[2].PlayOneShot(gobletSounds[r]);
        yield return new WaitForSeconds(1);
        r = Random.Range(0,gobletSounds.Length);
        soundEmitters[2].PlayOneShot(gobletSounds[r]);
        yield return new WaitForSeconds(1);
        soundEmitters[1].PlayOneShot(monsterSounds[3]);
        yield return new WaitForSeconds(2);
        soundEmitters[1].PlayOneShot(monsterSounds[0]);
        yield return new WaitForSeconds(4);
        soundEmitters[0].PlayOneShot(monsterSounds[4]);
        yield return new WaitForSeconds(2);

        //Finish
        door.Unlock();
        resetPossible = false;
        performingReset = false;
    }

}
