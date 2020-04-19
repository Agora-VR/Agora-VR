using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[AddComponentMenu("Agora VR/Sessions/Session Manager")]
public class SessionManager : MonoBehaviour
{
    [ReadOnly]
    public sceneName currentScene = sceneName.MainMenu;

    public enum sceneName { // Important that these names line up with the scene index.
        MainMenu,
        Auditorium,
        MeetingRoom,
        Review,
    };

    // Values Saved Here
    private List<string[]> heartRateData = new List<string[]>();
    private List<string[]> spo2Data = new List<string[]>();

    [SerializeField]
    private MainMenu menu;
    [SerializeField]
    private float HRMSaveInterval = 4f;

    private PluginWrapper BLEHRMSPO2;
    private Coroutine mSaveData;
    private int miliseconds;
    private object len;

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.buildIndex);

        switch(scene.buildIndex){
            case 0:
                currentScene = sceneName.MainMenu;
                StartCoroutine(Menu());
                break;
            case 1:
                currentScene = sceneName.Auditorium;
                StartCoroutine(Auditorium());
                break;
            case 2:
                currentScene = sceneName.MeetingRoom;
                StartCoroutine(MeetingRoom());
                break;
            case 3:
                currentScene = sceneName.Review;
                StartCoroutine(Review());
                break;
            default:
                break;
        }
    }

    private IEnumerator Menu()
    {
        menu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();
        BLEHRMSPO2 = GameObject.FindGameObjectWithTag("Plugin").GetComponent<PluginWrapper>();
        yield return null;
    }

    private IEnumerator Auditorium()
    {
        menu.sessionSettings.TryGetValue("SessionLength", out len);

        IEnumerator mGetData = BLEHRMSPO2.getData(); // Keep reference to this to stop it when session ends.
        StartCoroutine(mGetData);

        // Reset Variables for new session.
        miliseconds = 0;
        heartRateData.Clear();
        spo2Data.Clear();

        mSaveData = StartCoroutine(saveData(HRMSaveInterval));

        int minutes = (int) len * 60;

        yield return new WaitForSeconds(minutes);
        StopCoroutine(mSaveData);
        StopCoroutine(mGetData);
        SceneManager.LoadScene(3); // Loads Review Scene
    }

    private IEnumerator MeetingRoom()
    {
        menu.sessionSettings.TryGetValue("SessionLength", out len);

        int minutes = (int) len * 60;

        yield return new WaitForSeconds(minutes);
        SceneManager.LoadScene(3); // Loads Review Scene
    }

    private IEnumerator saveData(float interval)
    {
        string[] HREntry = new string[] {miliseconds.ToString(), BLEHRMSPO2.curHR.ToString()};
        string[] spo2Entry = new string[] {miliseconds.ToString(), BLEHRMSPO2.curSpO2.ToString()};

        heartRateData.Add(HREntry);
        spo2Data.Add(spo2Entry);

        yield return new WaitForSeconds(interval);
        miliseconds += (int) HRMSaveInterval*1000;
        mSaveData = StartCoroutine(saveData(HRMSaveInterval)); // Keep reference to this to stop it when session ends.
    }

    private IEnumerator Review()
    {
        GameObject.Find("Session Time").GetComponent<TextMeshPro>().text += len.ToString() + " Minute(s)";
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0); // Loads Main Menu
    }
}
