using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AgoraUiBehaviorScript : MonoBehaviour
{
    private string folderPath;

    /*public TMP_InputField usernameField, passwordField;

    public Button authButton;

    public TMP_Text tokenText;

    public Button requestButton;

    public TMP_Text requestText;*/

    private string datetime;

    private AgoraClient client;

    [SerializeField]
    private string username;

    [SerializeField]
    private string password;

    [SerializeField]
    private float testTime;

    private IEnumerator TestSave()
    {
        yield return new WaitForSeconds(testTime);

        buttonPress();
    }

    public async void buttonPress()
    {
        //var authResponse = await client.authenticate(usernameField.text, passwordField.text);
        //tokenText.text = authResponse;

        var authResponse = await client.authenticate(username, password);
        Debug.Log(authResponse);

        var sessionID = await client.CreateSession(datetime, "room");

        var uploadResponse = await client.PostFile(folderPath + "/test.mp3", "audio_session", sessionID);
        Debug.Log(uploadResponse);

        uploadResponse = await client.PostFile(folderPath + "/Audio_Data.csv", "volume_session", sessionID);
        Debug.Log(uploadResponse);

        uploadResponse = await client.PostFile(folderPath + "/speech.txt", "text_script", sessionID);
        Debug.Log(uploadResponse);

        uploadResponse = await client.PostFile(folderPath + "/HRSPO2Data.csv", "heart_rate_session", sessionID);
        Debug.Log(uploadResponse);
        //tokenText.text = uploadResponse;
    }

    public async void requestPress()
    {
        var response = await client.GetJsonWithAuth("/patient/sessions");

        //requestText.text = await response.Content.ReadAsStringAsync();
    }

    // Start is called before the first frame update
    void Start()
    {
        #if UNITY_ANDROID && !UNITY_EDITOR
        folderPath = Application.persistentDataPath;
        #else
        folderPath = Application.dataPath;
        #endif

        StartCoroutine(TestSave());
        datetime = System.DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.ffffff");
        client = new AgoraClient();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
