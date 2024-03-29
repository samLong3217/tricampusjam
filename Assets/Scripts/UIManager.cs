using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Sprite[] goldNumbers;
    public Sprite[] redNumbers;
    public Sprite[] healthSliderSprites;
    public Sprite[] tutorialTextSprites;

    public GameObject waveTens;
    public GameObject waveOnes;

    public GameObject moneyHundreds;
    public GameObject moneyTens;
    public GameObject moneyOnes;
    public GameObject healthSlider;
    public RectTransform waveSlider;
    public GameObject player;
    public GameObject cropButton;
    public GameObject abilityButtons;
    public GameObject tutorialText;

    public GameObject controlText;
    public float tutorialTime = 10.0f;
    private PlayerController playerController;
    private float tutorialHalfPoint;
    private bool tutorialPart2Init = false;

    void Awake()
    {
        playerController = player.GetComponent<PlayerController>();
        tutorialHalfPoint = tutorialTime / 2;
    }

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
        int money = MoneyManager.GetMoney();
        moneyHundreds.GetComponent<Image>().sprite = goldNumbers[money / 100];
        moneyTens.GetComponent<Image>().sprite = goldNumbers[money / 10 % 10];
        moneyOnes.GetComponent<Image>().sprite = goldNumbers[money % 10];

        if (RoundManager.GetState() == RoundManager.State.Wave) {
            cropButton.SetActive(false);
            abilityButtons.SetActive(true);
            Debug.Log("Is this happenign?");
            tutorialTime -= Time.deltaTime;
            if (tutorialTime >= tutorialHalfPoint) {
                tutorialText.GetComponent<Image>().sprite = tutorialTextSprites[1];
            } else {
                tutorialText.GetComponent<Image>().sprite = tutorialTextSprites[2];
            }
            if (tutorialTime <= 0) {
                tutorialText.SetActive(false);
            }
            controlText.SetActive(false);
        } else {
            cropButton.SetActive(true);
            abilityButtons.SetActive(false);
        }
    }
}
