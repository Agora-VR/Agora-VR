using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;
using System.Text;


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
    private List<string[]> HRSPO2Data = new List<string[]>();

    [SerializeField]
    private MainMenu menu;
    [SerializeField]
    private float HRMSaveInterval = 4f;

    private PluginWrapper BLEHRMSPO2;
    private Coroutine mSaveData;
    private int miliseconds;
    private object len;
    private int HR;
    private float SPO2;

    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.buildIndex);

        switch (scene.buildIndex){
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

    private void saveToCSV(string filePath)
    {
        string[][] output = new string[HRSPO2Data.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = HRSPO2Data[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    private IEnumerator Menu()
    {
        menu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();
        BLEHRMSPO2 = GameObject.FindGameObjectWithTag("Plugin").GetComponent<PluginWrapper>();
        yield return null;

        //yield return new WaitForSeconds(10);
        //SceneManager.LoadScene(1);
    }

    private IEnumerator Auditorium()
    {
        menu.sessionSettings.TryGetValue("SessionLength", out len);

        IEnumerator mGetData = BLEHRMSPO2.getData(); // Keep reference to this to stop it when session ends.
        StartCoroutine(mGetData);

        // Reset Variables for new session.
        miliseconds = 0;
        HRSPO2Data.Clear();

        StartCoroutine(saveHRSPO2()); // this must go before StartCoroutine(saveData(HRMSaveInterval))

        mSaveData = StartCoroutine(saveData(HRMSaveInterval));

        int minutes = (int) len * 60;

        yield return new WaitForSeconds(minutes+30);
        //StopCoroutine(mSaveData);
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
        if (BLEHRMSPO2.curHR < 40 || BLEHRMSPO2.curHR > 200)
            HR = 0;
        else
            HR = BLEHRMSPO2.curHR;

        if (BLEHRMSPO2.curSpO2 < 70 || BLEHRMSPO2.curSpO2 > 100)
            SPO2 = 0;
        else
            SPO2 = BLEHRMSPO2.curSpO2;

        string[] HRSPO2Entry = new string[] {miliseconds.ToString(), HR.ToString(), SPO2.ToString()};

        HRSPO2Data.Add(HRSPO2Entry);

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

    private IEnumerator saveHRSPO2()
    {
        string[] firstRow = new string[3];
        firstRow[0] = "Timestamp";
        firstRow[1] = "Heartrate";
        firstRow[2] = "spo2";
        HRSPO2Data.Add(firstRow);

        HR = 0;
        SPO2 = 0;

        yield return new WaitForSeconds(60 * (int)len - 2);

        saveToCSV(Application.persistentDataPath + "/HRSPO2Data.csv");
    }

}
