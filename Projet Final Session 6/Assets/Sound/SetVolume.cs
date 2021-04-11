using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioM;
    [SerializeField] private string nameParameter;

    private Slider slide;//permet d'update le slider selon le pourcentage
    // Start is called before the first frame update
    void Start()
    {
        slide = GetComponent<Slider>();
        float v = PlayerPrefs.GetFloat(nameParameter, 0);//sauvegarder les options de sons, si rien 0
        SetVol(v);
    }

    public void SetVol(float vol)
    {
        //cette fonction va changer le volume de l audio mixer
        audioM.SetFloat(nameParameter, vol);
        slide.value = vol;
        PlayerPrefs.SetFloat(nameParameter, vol);
    }

}
