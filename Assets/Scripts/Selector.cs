using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : MonoBehaviour {
    GameObject parent;
    PlayerController pController;

    public float offset = 0.8f;

    void Start() {
        parent = transform.parent.gameObject;
        pController = parent.GetComponent<PlayerController>();
    }

    void Update() { 
        Debug.Log("We out here");
        if (pController != null) {
            Debug.Log("We in here");
            switch (pController.Dir()) {
                case 0:
                    transform.position = parent.transform.position + new Vector3(-offset, 0, 0);
                break;
                case 1:
                    transform.position = parent.transform.position + new Vector3(0, -offset, 0);
                break;
                case 2:
                    transform.position = parent.transform.position + new Vector3(offset, 0, 0);
                break;
                case 3:
                    transform.position = parent.transform.position + new Vector3(0, offset, 0);
                break;
            }
        }
    }
}
