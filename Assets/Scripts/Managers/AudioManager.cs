using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

[Serializable]
public class audioFiles {

    public string name;
    public AudioClip audioFile;
    public AudioMixerGroup mixer;
}

public class AudioManager : MonoBehaviour {

    #region instance

    private static AudioManager _instance;
    public static AudioManager Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<AudioManager>();
                DontDestroyOnLoad(_instance);
            }
            return _instance;
        }
    }

    #endregion

    [SerializeField] private List<AudioSource> audioSources = new List<AudioSource>();
    [SerializeField] private List<audioFiles> files = new List<audioFiles>();

    [SerializeField] private Dictionary<string, audioFiles> _items = new Dictionary<string, audioFiles>();


    public void SetAudio(string audio) {

        playAudio(audio);
	}

	public bool playAudio(string audio){

		for (int i = 0;i< audioSources.Count;i++){

            if (audioSources[i].clip == null || !audioSources[i].isPlaying) {
                audioSources[i].clip = _items[audio].audioFile;
                audioSources[i].outputAudioMixerGroup = _items[audio].mixer;
                audioSources[i].volume = 1;
                audioSources[i].pitch = 1;
                audioSources[i].Play();
                return true;
            }
		}
        return false;
    }

    public bool playAudio(string audio, float volume) {

        for (int i = 0; i < audioSources.Count; i++) {

            if (audioSources[i].clip == null || !audioSources[i].isPlaying) {

                audioSources[i].clip = _items[audio].audioFile;
                audioSources[i].outputAudioMixerGroup = _items[audio].mixer;
                audioSources[i].volume = volume;
                audioSources[i].pitch = 1;
                audioSources[i].Play();
                return true;
            }
        }
        return false;
    }

    public void PauseSong(string audio) {

        for (int i = 0; i < audioSources.Count; i++) {

            if (audioSources[i].clip != null && audioSources[i].isPlaying) {

                audioSources[i].clip = _items[audio].audioFile;
                audioSources[i].Pause();
            }
        }
    }

    public void StopSong(string audio) {

        for (int i = 0; i < audioSources.Count; i++) {

            if (audioSources[i].clip != null || audioSources[i].isPlaying) {

                audioSources[i].clip = _items[audio].audioFile;
                audioSources[i].Stop();
            }
        }
    }

    public void PlaySongFaster(string audio, float  multiple) {

        for (int i = 0; i < audioSources.Count; i++) {

            if (audioSources[i].clip != null || audioSources[i].isPlaying) {

                audioSources[i].clip = _items[audio].audioFile;
                audioSources[i].pitch = multiple;
            }
        }
    }

	private void Awake() {

        for (int i = 0;i<files.Count;i++) {

            _items.Add(files[i].name, files[i]);
        }

        StopAll();
	}

    public void StopAll() {

        for (int i = 0; i < audioSources.Count; i++) {

            audioSources[i].Stop();
        }
    }
}
