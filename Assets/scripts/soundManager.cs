using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = System.Random;

public class soundManager : MonoBehaviour
{
    public AudioClip sndClick = Resources.Load<AudioClip>("click");
    public AudioClip sndCoin = Resources.Load<AudioClip>("coin");
    public AudioClip[] radioClips = Resources.LoadAll<AudioClip>("Radio/");
    public AudioSource audio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Pressed primary button.");
            audio.clip = sndClick;
            playSound();
        }
    }

    public void playCoinSound() {
        audio.clip = sndCoin;
        playSound();
    }

    public void playRadio() {
        int radioFM = UnityEngine.Random.Range(0,radioClips.Length);
        audio.clip = radioClips[radioFM];
        playSound();
    }

    public IEnumerator playSound() {
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length);
    }
}
