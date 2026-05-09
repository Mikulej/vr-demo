using UnityEngine;
using TMPro;
using System.Collections;
public class Keypad : MonoBehaviour
{
    [SerializeField] GameObject doorToUnlock;
    Door door;
    bool keypadLocked = false;
    AudioSource soundEmitter;
    [SerializeField] AudioClip[] buttonSounds = new AudioClip[3];
    [SerializeField] AudioClip[] keypadSounds = new AudioClip[2];
    [SerializeField] TMP_Text displayText;
    [SerializeField] Material[] screenColors = new Material[3];
    [SerializeField] GameObject displayScreen;
    MeshRenderer mr; 
    
    void Start()
    {
        soundEmitter = GetComponent<AudioSource>();
        door = doorToUnlock.GetComponent<Door>();
        mr = displayScreen.GetComponent<MeshRenderer>();
    }

    public void AddNumber(int number){
        int r = Random.Range(0,3);
        soundEmitter.PlayOneShot(buttonSounds[r]);

        if(keypadLocked == false){
            displayText.text += number.ToString();

            if(displayText.text.Length == 4){
                if(displayText.text == "4231"){
                    StartCoroutine(correctCode());
                }
                else{
                    StartCoroutine(incorrectCode());
                }
            }
        }
    }

    private IEnumerator correctCode(){
        keypadLocked = true;
        mr.material = screenColors[2];
        soundEmitter.PlayOneShot(keypadSounds[1]);
        yield return new WaitForSeconds(2); 
        door.Unlock();
    }

    private IEnumerator incorrectCode(){
        keypadLocked = true;
        mr.material = screenColors[1];
        soundEmitter.PlayOneShot(keypadSounds[0]);
        yield return new WaitForSeconds(2); 
        keypadLocked = false;
        displayText.text = "";
        mr.material = screenColors[0];
    }
}
