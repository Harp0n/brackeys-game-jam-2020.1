using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioSystem : MonoBehaviour
{
    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip[] clips;
    }

    public Sound[] sounds;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(string soundName)
    {
        foreach(Sound s in sounds)
        {
            if(s.name.Equals(soundName))
            {
                audioSource.clip = s.clips[Random.Range(0, s.clips.Length)];
                audioSource.Play();
                return;
            }
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }
}
