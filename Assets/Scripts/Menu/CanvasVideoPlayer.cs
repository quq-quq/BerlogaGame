using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CanvasVideoPlayer : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private VideoPlayer _videoPlayer;

    private void Awake()
    {
        StartCoroutine(PlayingVideo());
    }

    private IEnumerator PlayingVideo()
    {
        _videoPlayer.Prepare();

        while (!_videoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.01f);
            break;
        }

        _rawImage.texture = _videoPlayer.texture;
        _videoPlayer.Play();
    }
}
