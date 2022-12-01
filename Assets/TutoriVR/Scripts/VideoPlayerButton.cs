using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using Evereal.VRVideoPlayer;

/// <summary>
/// Is able to to load the latest video in the /recording directory which includes audio and button recreation info
/// when run.
/// </summary>
// [RequireComponent(typeof(Camera))]
public class VideoPlayerButton : MonoBehaviour, IRunnable
{
    [SerializeField] Material showButton;
    [SerializeField] Material closeButton;
    [SerializeField] bool playFromFile;
    public bool readTutori;
    [SerializeField] GameObject buttonRecreation;
    // [SerializeField] RecordingEvent Event;

    private IAppInfo appInfo;
    private bool currentstate;
    public GameObject VideoPlayer;
    public GameObject VideoPlayer_stereo;
    public GameObject microphone_audio;
    public GameObject awareness_widget;
    public GameObject perspective_widget;
    public ReplicatorCam repCam;
    //public GameObject record_begin_button;

    // Start is called before the first frame update
    // [SerializeField] VideoCapture VC;
    void Start()
    {
        SetChildrenActive(false);
        currentstate = false;
         gameObject.GetComponent<Renderer>().material = showButton;   
    }
    
    /// <summary>
    /// Activates all children of this object as well as the video/audio player. If the setting
    /// is == True, the video is played. Otherwise deactivates awareness and perspective widget.
    /// If there is inputData, button recreation is activated and inputData is fed to it and played.
    /// </summary>
    private void SetChildrenActive(bool setting, buttonLedger inputsData = null)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(setting);
        }

        VideoPlayer.SetActive(setting);
        VideoPlayer_stereo.SetActive(setting);
        microphone_audio.SetActive(setting);

        if (setting)
        {
            VideoPlayer.GetComponent<VideoPlayerCtrl>().PlayVideo();
            VideoPlayer_stereo.GetComponent<VideoPlayerCtrl>().PlayVideo();
            StartCoroutine(microphone_audio.GetComponent<MicAudioController>().LoadAudio());
        } else
        {
            awareness_widget.SetActive(false);
            perspective_widget.SetActive(false);
        }


        if (inputsData != null)
        {
            buttonRecreation.SetActive(setting);
            buttonRecreation.GetComponent<trackController>().dataLedger = inputsData;
            buttonRecreation.GetComponent<trackController>().Play();
            //repCam.setTransform();
        }

        // for (int i = 0; i < transform.childCount; i++)
        // {
        //     transform.GetChild(i).gameObject.SetActive(setting);
        // }
    }

    /// <summary>
    /// When run, loads the audio/video/reader info of the first file in the /recording directory and
    /// changes the currentstate. Only loads info if playFromFile is set to true. Changes material
    /// of object button depending on current state.
    /// </summary>
    public void Run(Vector3 currentpoint)
    {
        if (!playFromFile)
        {
            SetChildrenActive(!currentstate);
        } 
        else
        { 
            string d = Application.persistentDataPath + "/recording";
            Debug.Log(d);
            var info = new DirectoryInfo(d);
            var fileInfo = info.GetDirectories();
            var f = fileInfo[0];
            var fileList = f.GetFiles();
            foreach (FileInfo fi in fileList)
            {
                Debug.Log(fi.Name);
            }
            if (readTutori)
            {
                VideoPlayer.GetComponent<VRVideoPlayer>().SetSource(VideoSource.ABSOLUTE_URL);
                VideoPlayer.GetComponent<VideoPlayerCtrl>().playlist[0] = f.FullName + "/MONO.mp4";

                VideoPlayer_stereo.GetComponent<VRVideoPlayer>().SetSource(VideoSource.ABSOLUTE_URL);
                VideoPlayer_stereo.GetComponent<VideoPlayerCtrl>().playlist[0] = f.FullName + "/STEREO.mp4";

                microphone_audio.GetComponent<MicAudioController>().soundPath = f.FullName + "/";
                //StartCoroutine(microphone_audio.GetComponent<MicAudioController>().LoadAudio());

                StreamReader readerAlerts = new StreamReader(f.FullName + "/alerts.json");
                string alertsJson = readerAlerts.ReadToEnd();
                readerAlerts.Close();

                ListLedger alertsData = JsonUtility.FromJson<ListLedger>(alertsJson);
                VideoPlayer.GetComponent<VideoPlayerCtrl>().loadAlerts(alertsData);

                StreamReader readerInputs = new StreamReader(f.FullName + "/inputs.json");
                string inputsJson = readerInputs.ReadToEnd();
                readerInputs.Close();

                buttonLedger inputsData = JsonUtility.FromJson<buttonLedger>(inputsJson);
                //GameObject br = Instantiate(buttonRecreation, Vector3.zero, Quaternion.identity, null);
                //br.GetComponent<trackController>().dataLedger = inputsData;
                //br.GetComponent<trackController>().Play();

                SetChildrenActive(!currentstate, inputsData);
            } else //read from file, only 1 video
            {
                VideoPlayer.GetComponent<VRVideoPlayer>().SetSource(VideoSource.ABSOLUTE_URL);
                VideoPlayer.GetComponent<VideoPlayerCtrl>().playlist[0] = f.FullName + "/" + fileList[0].Name;
                SetChildrenActive(!currentstate);
            }
        }

        currentstate = !currentstate;
        if (currentstate==false)
        {
            gameObject.GetComponent<Renderer>().material = showButton;
            //record_begin_button.SetActive(true);
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = closeButton;
            //record_begin_button.SetActive(false);
        }
    }
}
