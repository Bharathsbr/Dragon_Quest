using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string texIntro;
    private Text txt;

    private void Awake()
    {
        txt=GetComponent<Text>();
        
    }

    private void Update()
    {
        UpadteVolume();
    }
    private void UpadteVolume(){
        float vc=PlayerPrefs.GetFloat(volumeName)*100;
        txt.text=texIntro+vc.ToString();
    }
}
