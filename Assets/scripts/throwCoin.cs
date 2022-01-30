using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwCoin : MonoBehaviour
{
    public Sprite coinUp;
    public Sprite coinDown;
    public Sprite coinFlip;
    public Sprite currentSprite;
    public GameObject radio;
    private soundManager playSound;
    private Animation anim;

    private void Start() {
        anim = this.GetComponent<Animation>();
        playSound = radio.GetComponent<soundManager>();
        this.GetComponent<SpriteRenderer>().sprite = coinFlip;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.S)) {
            Debug.Log("Spinning");
            StartCoroutine(makeDecision());
        }
    }
    IEnumerator makeDecision(bool decision = false)
    {
        this.GetComponent<SpriteRenderer>().sprite = coinFlip;
        playSound.playCoinSound();
        if (decision) {
            currentSprite = coinUp;
        }
        else {
            currentSprite = coinDown;
        }
        anim.Play();
        yield return new WaitForSeconds(playSound.sndCoin.length-0.1f);
        this.GetComponent<SpriteRenderer>().sprite = currentSprite;
    }
}
