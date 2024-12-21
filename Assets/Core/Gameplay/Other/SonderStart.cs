using UnityEngine;

public class SounderStart : MonoBehaviour
{
    [SerializeField] private AudioClip _logoSound;
    [SerializeField] private float _volume;

    private void Start()
    {
        AudioListener.pause = false;
    }
    public void GetLogoSound()
    {
        SoundController.sounder.SetSound(_logoSound, false, gameObject.name, _volume);
    }
}
