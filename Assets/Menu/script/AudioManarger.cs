using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
 
public class AudioManarger : MonoBehaviour
{               
    public AudioMixer audioMixer; //声音混合器

    //控制主音量
    public void ControlMasterVolume(float v)
    {
        audioMixer.SetFloat("MastterVolume", v);
    }
    //控制背景音量
    public void ControlBGMVolume(float v)
    {
        audioMixer.SetFloat("BGMVolume",v);
    }
    //控制特效音量
    public void ControlSoundEffectVolume(float v)
    {
        audioMixer.SetFloat("SoundEffectVolume", v);
    }
}