using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] goldNumbers;
    public Sprite[] redNumbers;
    public Sprite[] healthSliderSprites;

    public GameObject waveTens;
    public GameObject waveOnes;

    public GameObject moneyHundreds;
    public GameObject moneyTens;
    public GameObject moneyOnes;
    public GameObject healthSlider;
    public RectTransform waveSlider;

    void Update()
    {
        // update wave number
        waveTens.GetComponent<Image>().sprite = redNumbers[RoundManager.GetWave() / 10];
        waveOnes.GetComponent<Image>().sprite = redNumbers[RoundManager.GetWave() % 10];
        // update health
        healthSlider.GetComponent<Image>().sprite = healthSliderSprites[5 - RoundManager.GetCrops()];
        // update wave slider
        waveSlider.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 48 * SpawnerManager.Completion());

        // update money
        GameObject player = GameObject.FindWithTag("Player");
        int money = player.GetComponent<PlayerController>().money;
        moneyHundreds.GetComponent<Image>().sprite = goldNumbers[money / 100];
        moneyTens.GetComponent<Image>().sprite = goldNumbers[money / 10 % 10];
        moneyOnes.GetComponent<Image>().sprite = goldNumbers[money % 10];
    }
}
