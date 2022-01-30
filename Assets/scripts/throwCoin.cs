using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwCoin : MonoBehaviour
{
    public Sprite coinUp;
    public Sprite coinDown;
    public GameObject radio;

    private void Start() {
        var playSound = radio.GetComponent<soundManager>();
        playSound.sndCoin;
    }

    void makeDecision(bool decision){
        if (decision) {
            this.GetComponent<SpriteRenderer>().sprite = coinUp;
        }
        else {
            this.GetComponent<SpriteRenderer>().sprite = coinDown;
        }
    }

    void coinAnim() {
        
    }

}
