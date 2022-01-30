using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = System.Random;

public class soundManager : MonoBehaviour
{

    public AudioClip sndClick;
    public AudioClip sndCoin;
    public AudioClip[] radioClips;
    public AudioSource audio;

    private void Awake()
    {
        sndClick = Resources.Load<AudioClip>("click");
        sndCoin = Resources.Load<AudioClip>("coin");
        radioClips = Resources.LoadAll<AudioClip>("Radio/");
    }

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    
    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Pressed primary button.");
            audio.clip = sndClick;
            playRadio();
        }
    }

    public void playCoinSound() {
        audio.clip = sndCoin;
        playSound();
    }

    public IEnumerator playRadio() {
        int radioFM = UnityEngine.Random.Range(0,radioClips.Length);
        audio.clip = radioClips[radioFM];
        audio.PlayOneShot(audio.clip);
        yield return new WaitForSeconds(3f);
    }

    public void playSound() {
        audio.PlayOneShot(audio.clip);
        //yield return new WaitForSeconds(audio.clip.length);
    }
}
