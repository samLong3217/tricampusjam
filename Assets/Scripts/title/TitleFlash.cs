using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleFlash : MonoBehaviour
{
    private float curTime;
    public float interval = 4.0f;
    public float offInterval = 2.0f;  
    private AudioSource audioSource; 
    private bool reveal;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        curTime -= Time.deltaTime;
        if (curTime <= 0) {
            Debug.Log("In here");
            if (reveal) {
                curTime = interval;
                spriteRenderer.enabled = true;
            } else {
                curTime = offInterval;
                spriteRenderer.enabled = false;
            }
            reveal = !reveal;
        }
        if (Input.GetMouseButtonDown(0)){
            audioSource.Stop();
            SceneManager.LoadScene("level1");
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }
}
