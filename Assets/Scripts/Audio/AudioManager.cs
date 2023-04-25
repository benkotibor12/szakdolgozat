using UnityEngine;
using System;

public class AudioManager : MonoBehaviour, IDataPersistence
{
    public Sound[] sounds;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }

    private void Start()
    {
        Play("AmbientMusic");
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.name == name);
        if (sound == null)
        {
            Debug.LogWarning("Can't find audio file named: " + name);
        }
        sound?.source.Play();
    }

    public void LoadData(GameData gameData)
    {
        AudioListener.volume = gameData.audioVolume;
    }

    public void SaveData(ref GameData gameData)
    {
        //
    }
}
