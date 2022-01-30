using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using Random = System.Random;

public class soundManager : MonoBehaviour
{
    public float lerpDuration = 3f;
    public GameObject knob;
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
            playSound();
        }
    }

    public void playCoinSound() {
        audio.clip = sndCoin;
        playSound();
    }

    public float timeElapsed = 0;
    public float knobRotation;
    private void FixedUpdate()
    {
        //TODO: fix rotation start
        timeElapsed += Time.deltaTime;
        float z = knob.transform.rotation.z;
        knob.transform.localRotation = Quaternion.Euler(0,0,Mathf.Lerp( z,knobRotation, timeElapsed / lerpDuration));
        // print(z);
    }

    public void playRadio() {
        StartCoroutine(radio());
    }

    private IEnumerator radio()
    {
        yield return new WaitForSeconds(0.5f);
        timeElapsed = 0;
        int radioFM = UnityEngine.Random.Range(0,radioClips.Length);
        knobRotation = UnityEngine.Random.Range(-50f, 50f);
        audio.clip = radioClips[radioFM];
        audio.Play();
    }

    public void playSound() {
        audio.PlayOneShot(audio.clip);
        //yield return new WaitForSeconds(audio.clip.length);
    }
}
