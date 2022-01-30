using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throwCoin : MonoBehaviour
{
    public Sprite coinUp;
    public Sprite coinDown;

    // Start is called before the first frame update
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
