using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlay : MonoBehaviour
{
    [SerializeField] private MeshRenderer quadPlayer;
    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private GameObject videoBoard;
    [SerializeField] private PhotoTrigger photoTrigger;

    private void Awake()
    {
        videoPlayer.Pause();
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    private void OnDestroy()
    {
        videoPlayer.loopPointReached -= OnVideoEnd;
    }

    public void PlayVideo()
    {
        videoBoard.SetActive(true);
        videoPlayer.Play();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        photoTrigger.TriggerDialogue();
    }
}
