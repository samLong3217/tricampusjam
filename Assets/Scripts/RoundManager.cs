using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
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
        Debug.Log("Starting the first wave");
    }

    public static void incrementCrops() {
        cropsLeft++;
    }

    public static void decrementCrops() {
        cropsLeft--;
    }

    private void Update()
    {
        if (state == State.Crops) {
            return;
        }
        if (SpawnerManager.AllSpawnersDoneCurrent()) {
            Debug.Log("Finished with current wave");
            if (SpawnerManager.AllSpawnersHaveAnother()) {
                Debug.Log("Starting next wave");
                SpawnerManager.IncrementAllWaves(4.0f);
            } else { // we beat all waves, restart
                 SceneManager.LoadScene( SceneManager.GetActiveScene().name );
            }
        }
        if (cropsLeft <= 0) { // we lost :(
            Debug.Log("Out of crops");
            SceneManager.LoadScene( SceneManager.GetActiveScene().name );
        }
    }
}
