using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource soundSource;
    private AudioSource musicSource;

    float baseVolume=1;

    private void Awake()
    {
        soundSource=GetComponent<AudioSource>();
        musicSource=transform.GetChild(0).GetComponent<AudioSource>();

        if(instance==null){
            instance=this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance!=null && instance!=this){
            Destroy(gameObject);
        }

        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip audio)
    {
        soundSource.PlayOneShot(audio);
    }

    public void ChangeSoundVolume(float change)
    {
        ChangeSourceVolume(1,"sv",change,soundSource);

    }

    public void ChangeMusicVolume(float change)
    {
        ChangeSourceVolume(0.3f,"mv",change,musicSource);
    }

    private void ChangeSourceVolume(float baseVolume,string volumeName,float change,AudioSource source){
        float currentVolume=PlayerPrefs.GetFloat(volumeName,1);
        currentVolume+=change;

        if(currentVolume<0){
            currentVolume=1;
        }else if(currentVolume>1){
            currentVolume=0;
        }

      float finalVolume=baseVolume*currentVolume;
      source.volume=finalVolume;
      PlayerPrefs.SetFloat(volumeName,currentVolume);

    }
}
