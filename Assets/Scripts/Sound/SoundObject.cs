using UnityEngine;

public class SoundObject : MonoBehaviour
{
    private AudioSource _source;
    
    public void SetClip(AudioClip clip)
    {
        _source = gameObject.AddComponent<AudioSource>();
        _source.clip = clip;
        _source.Play();
    }

    private void Update()
    {
        if (_source != null && !_source.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
