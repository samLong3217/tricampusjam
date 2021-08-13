using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    private AudioSource _musicSource;

    private void Awake()
    {
        Destroy(_instance);
        _instance = this;
    }

    public static void PlaySound(AudioClip clip, Vector2 position)
    {
        GameObject go = new GameObject("Sound Effect");
        go.transform.position = position;
        SoundObject so = go.AddComponent<SoundObject>();
        so.SetClip(clip);
    }

    public static void PlayMusic(AudioClip clip)
    {
        if (_instance._musicSource == null)
        {
            GameObject go = new GameObject("Music");
            _instance._musicSource = go.AddComponent<AudioSource>();
            _instance._musicSource.loop = true;
        }

        _instance._musicSource.clip = clip;
        _instance._musicSource.Play();
    }
}
