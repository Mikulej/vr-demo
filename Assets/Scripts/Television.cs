using UnityEngine;
using System.Collections;

public class Television : Interactable
{
    AudioSource[] soundEmitters = new AudioSource[3];
    [SerializeField] GameObject tvScreen;
    bool tvPoweredOn = false;
    void Start()
    {
        soundEmitters = GetComponents<AudioSource>();
        StartCoroutine(playCode());
    }
    override public void Interact(){
        if(tvPoweredOn){
            tvScreen.SetActive(false);
            soundEmitters[0].Pause();   
        }
        else{
            tvScreen.SetActive(true);
            soundEmitters[0].Play();
        }
        tvPoweredOn = !tvPoweredOn;
        soundEmitters[2].Play();
    }
    private IEnumerator playCode(){
        while(true){

            for(int i = 0; i < 4; i++){
                if(tvPoweredOn) soundEmitters[1].Play();
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(2);

            for(int i = 0; i < 2; i++){
                if(tvPoweredOn) soundEmitters[1].Play();
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(2);

            for(int i = 0; i < 3; i++){
                if(tvPoweredOn) soundEmitters[1].Play();
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(2);

            for(int i = 0; i < 1; i++){
                if(tvPoweredOn) soundEmitters[1].Play();
                yield return new WaitForSeconds(1);
            }

            yield return new WaitForSeconds(6);
        }
    }
}
