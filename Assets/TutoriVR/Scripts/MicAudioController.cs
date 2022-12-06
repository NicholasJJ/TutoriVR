using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gets a specific audio file, and has functions for playing it
/// </summary>
public class MicAudioController : MonoBehaviour
{
    public const string audioName = "mic.wav";

    public AudioSource audioSource;
    public AudioClip audioClip;
    public string soundPath;

    /// <summary>
    /// Creates the audio source that can later have an audio clip attached to it
    /// </summary>
    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        //soundPath = Application.persistentDataPath + "/recording/capture/";
        //StartCoroutine(LoadAudio());
    }

    private void Update()
    {
        //Debug.Log(audioSource.time);
    }

    /// <summary>
    /// Gets the audio from the specific file, and plays it
    /// starting from the next frame.
    /// </summary>
    public IEnumerator LoadAudio()
    {
        WWW request = GetAudioFromFile("file://" + soundPath, audioName);
        yield return request;
        audioClip = request.GetAudioClip(false, true);
        audioClip.name = audioName;
        audioSource.clip = audioClip;
        Debug.Log(audioSource.clip);
        PlayAudioFile();
    }

    /// <summary>
    /// Reformats the filename with the other part of the path, and makes the request to get the file
    /// </summary>
    /// <returns>The file from the specified path</returns>
    private WWW GetAudioFromFile(string path, string filename)
    {
        string audioToLoad = string.Format(path + "{0}", filename);
        Debug.Log(audioToLoad);
        WWW request = new WWW(audioToLoad);
        return request;
    }

    /// <summary>
    /// Plays the whole audio clip starting from the beginning.
    /// </summary>
    public void PlayAudioFile()
    {
        Debug.Log("in play audio");
        //audioSource.clip = audioClip;
        SetTime(0);
        audioSource.Play();
    }

    /// <summary>
    /// Pauses the video and sets the time for that audio source to start at the paused time.
    /// </summary>
    public void SetTime(float t)
    {
        //Debug.Log(t);
        audioSource.Pause();
        audioSource.time = t;
        //Debug.Log(audioSource.time);
        //Debug.Log("settime");
    }

}
