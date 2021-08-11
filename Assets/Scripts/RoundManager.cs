using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    private static int wave = 1;
    private enum State {Crops, Wave}; // Can only place health crops in the crop phase
    private static State state;
    private static RoundManager _instance;
    private static int cropsLeft = 0;

    private void Awake()
    {
        Destroy(_instance);
        _instance = this;
        state = State.Crops;
    }

    /// <summary>
    /// Kicks off the wave phase
    /// </summary>
    public static void StartWavePhase() {
        state = State.Wave;
        SpawnerManager.SetAllWaves(0, 0.0f);
    }

    public static void incrementCrops() {
        cropsLeft++;
    }

    public static void decrementCrops() {
        cropsLeft--;
    }

    public static int GetCrops() {
        return cropsLeft;
    }

    public static int GetWave() {
        return wave;
    }

    private void Update()
    {
        if (state == State.Crops) {
            return;
        }
        if (SpawnerManager.AllSpawnersDoneCurrent() && GameObject.FindGameObjectWithTag("enemy") == null) {
            if (SpawnerManager.AllSpawnersHaveAnother()) {
                Debug.Log("Starting next wave");
                GivePlayerMoney();
                SpawnerManager.IncrementAllWaves(3.0f);
                wave++;
            } else { // we beat all waves, restart
                SceneManager.LoadScene( SceneManager.GetActiveScene().name );
            }
        }
        if (cropsLeft <= 0) { // we lost :(
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }

    private void GivePlayerMoney() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) {
            player.GetComponent<PlayerController>().AwardRoundMoney();
        }
    }
}
