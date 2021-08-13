using UnityEngine;
 using UnityEngine.SceneManagement;

public class RoundManager : MonoBehaviour
{
    private int _wave = 1;
    public enum State {Crops, Wave} // Can only place health crops in the crop phase
    private State _state;
    private static RoundManager _instance;
    private int _cropsLeft = 0;

    private void Awake()
    {
        Destroy(_instance);
        _instance = this;
        _state = State.Crops;
        SoundManager.PlayMusic(Bootstrapper.Instance.MapData.PrewaveMusic);
    }

    /// <summary>
    /// Kicks off the wave phase
    /// </summary>
    public static void StartWavePhase() {
        _instance._state = State.Wave;
        SpawnerManager.SetAllWaves(0, 0.0f);
        SoundManager.PlayMusic(Bootstrapper.Instance.MapData.WaveMusic);
    }

    public static void IncrementCrops() {
        _instance._cropsLeft++;
        if (_instance._cropsLeft >= Bootstrapper.Instance.MapData.MaxCrops)
        {
            PlacementController.ReplaceCrop();
            StartWavePhase();
        }
    }

    public static void DecrementCrops() {
        _instance._cropsLeft--;
    }

    public static int GetCrops() {
        return _instance._cropsLeft;
    }

    public static int GetWave() {
        return _instance._wave;
    }

    public static State GetState()
    {
        return _instance._state;
    }

    private void Update()
    {
        if (_instance._state == State.Crops) {
            return;
        }
        if (SpawnerManager.AllSpawnersDoneCurrent() && GameObject.FindGameObjectWithTag("enemy") == null) {
            if (SpawnerManager.AllSpawnersHaveAnother()) {
                Debug.Log("Starting next wave");
                GivePlayerMoney();
                SpawnerManager.IncrementAllWaves(3.0f);
                _instance._wave++;
            } else { // we beat all waves, restart
                SceneManager.LoadScene( SceneManager.GetActiveScene().name );
            }
        }
        if (_instance._cropsLeft <= 0) { // we lost :(
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
