using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Manages and displays money, health, and the different actions the player can make
public class UIManager : MonoBehaviour
{
    public int health = 20;
    public int[] abilityCost;

    public float towerSelectY = -30; // the upper position of the tower ui when selected
    private int wave = 1;

    private float towerOriginY; // the starting position of the tower ui

    public Sprite[] goldNumbers;
    public Sprite[] whiteNumbers;
    public GameObject waveTens;
    public GameObject waveOnes;

    public GameObject hpTens;
    public GameObject hpOnes;

    public GameObject moneyHundreds;
    public GameObject moneyTens;
    public GameObject moneyOnes;




    // Start is called before the first frame update
    void Start()
    {
        towerOriginY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        // update wave number
        waveTens.GetComponent<Image>().sprite = whiteNumbers[wave / 10];
        waveOnes.GetComponent<Image>().sprite = whiteNumbers[wave % 10];
        // update health
        hpTens.GetComponent<Image>().sprite = whiteNumbers[health / 10];
        hpOnes.GetComponent<Image>().sprite = whiteNumbers[health % 10];

        // update money
        GameObject player = GameObject.FindWithTag("Player");
        int money = player.GetComponent<PlayerController>().money;
        moneyHundreds.GetComponent<Image>().sprite = goldNumbers[money / 100];
        moneyTens.GetComponent<Image>().sprite = goldNumbers[money / 10 % 10];
        moneyOnes.GetComponent<Image>().sprite = goldNumbers[money % 10];
        // update selected ability
    }
}
