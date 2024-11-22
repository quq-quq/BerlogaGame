using UnityEngine;

public class SounderStart : MonoBehaviour
{
    [SerializeField] private AudioClip _backgroundSound, _logoSound;
    [SerializeField] private float _volume;

    private void Start()
    {
        AudioListener.pause = false;
        SoundController.sounder.SetSound(_backgroundSound, true, gameObject.name, _volume);
    }
    public void GetLogoSound()
    {
        SoundController.sounder.SetSound(_logoSound, false, gameObject.name, _volume);
    }
}
