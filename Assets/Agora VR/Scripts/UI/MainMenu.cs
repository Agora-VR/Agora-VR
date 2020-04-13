using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("Agora VR/UI/Main Menu")]
public class MainMenu : MonoBehaviour
{
    public Dictionary<string, object> sessionSettings = new Dictionary<string, object>() {
        { "SceneName", sceneName.Auditorium },
        { "SessionLength", 1 },
    };

    private Text sliderText;
    private bool inMenu;
    private SessionManager mSessionManager;
    public GameObject UICanvas;

    public GameObject DDOL; // TEMP

    public enum sceneName { // Important that these names line up with the scene index.
        MainMenu,
        Auditorium,
        MeetingRoom,
        Review,
    };

	void Start ()
    {
        init();
        // StartCoroutine(test());
	}

    void Update()
    {
        if(OVRInput.GetDown(OVRInput.Button.Two) || OVRInput.GetDown(OVRInput.Button.Start))
        {
            if (inMenu) UIBuilder.instance.Hide();
            else UIBuilder.instance.Show();
            inMenu = !inMenu;
        }
    }

    private void init() // Initial Menu UI
    {
        /* Center Panel (index 0) */
        UIBuilder.instance.AddSpacer();
        UIBuilder.instance.AddLogo();
        UIBuilder.instance.AddLabel("Scene Selection");
        UIBuilder.instance.AddDivider();
        UIBuilder.instance.AddButton("Auditorium", AuditoriumSetup);
        UIBuilder.instance.AddButton("Meeting Room", MeetingSetup);

        /* Right Panel (index 1) */
        UIBuilder.instance.AddLabel2("Capstone Group S20-61", 1);
        UIBuilder.instance.AddLabel2("Members:", 1);
        UIBuilder.instance.AddBody("Aryeh Ness\nDaniel Nguyen\nTed Moseley\nMichaelTruong", "center", 1);
        UIBuilder.instance.AddLabel2("Advisor:", 1);
        UIBuilder.instance.AddBody("Dr. Grigore Burdea", "center", 1);

        /* Left Panel (index 2) */
        UIBuilder.instance.AddLabel2("Agora VR Main Menu", 2);
        UIBuilder.instance.AddSpacer(2);
        UIBuilder.instance.AddBody("Welcome to Agora VR, a virtual reality application designed " +
            "specifically for people who struggle with Agoraphobia or have social anxiety disorder(s). ", "justified", 2);
        UIBuilder.instance.AddSpacer(2); UIBuilder.instance.AddSpacer(2);
        UIBuilder.instance.AddBody("Lorem ipsum adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore " +
            "magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco " +
            "laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " +
            "reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.", "justified", 2);


        UIBuilder.instance.Show();
        inMenu = true;
    }

    private void Login()
    {
        UIBuilder.instance.Destroy();
    }

    private void AuditoriumSetup()
    {
        UIBuilder.instance.Destroy();

        sessionSettings["SceneName"] = sceneName.Auditorium;

        /* Center Panel (index 0) */
        UIBuilder.instance.AddSpacer();
        UIBuilder.instance.AddLabel("Auditorium Setup");
        UIBuilder.instance.AddDivider();

        var sliderPrefab = UIBuilder.instance.AddSlider("Session Length", 1f, 10f, SliderPressed, true);
        var textElementsInSlider = sliderPrefab.GetComponentsInChildren<Text>();
        sliderText = textElementsInSlider[1];
        sliderText.text = sliderPrefab.GetComponentInChildren<Slider>().value.ToString();

        UIBuilder.instance.AddDivider();
        UIBuilder.instance.AddButton("START", AuditoriumStart);

        UIBuilder.instance.Show();

        UICanvas.transform.position = new Vector3(UICanvas.transform.position.x, 2.5f, UICanvas.transform.position.z); // TEMP
    }

    private void AuditoriumStart()
    {
        SceneManager.LoadScene(1); //Assumes Auditorium Scene is at Index 1 in Build
        DDOL.transform.position = new Vector3(0.155f, 1.49f, -13.45f); // TEMP
    }

    private void MeetingSetup()
    {
        UIBuilder.instance.Destroy();

        sessionSettings["SceneName"] = sceneName.MeetingRoom;

        /* Center Panel (index 0) */
        UIBuilder.instance.AddSpacer();
        UIBuilder.instance.AddLabel("Meeting Setup");
        UIBuilder.instance.AddDivider();

        var sliderPrefab = UIBuilder.instance.AddSlider("Session Length", 1f, 10f, SliderPressed, true);
        var textElementsInSlider = sliderPrefab.GetComponentsInChildren<Text>();
        sliderText = textElementsInSlider[1];
        sliderText.text = sliderPrefab.GetComponentInChildren<Slider>().value.ToString();

        UIBuilder.instance.AddDivider();
        UIBuilder.instance.AddButton("START", MeetingStart);

        UIBuilder.instance.Show();

        UICanvas.transform.position = new Vector3(0f, 2.5f, 1.75f); // TEMP
    }

    private void MeetingStart()
    {
        SceneManager.LoadScene(2); //Assumes Meeting Room Scene is at Index 2 in Build
    }

    public void SliderPressed(float f)
    {
        int val = Mathf.FloorToInt(f);
        sessionSettings["SessionLength"] = val;
        sliderText.text = val.ToString();
    }

    public void TogglePressed(Toggle t)
    {
        Debug.Log("Toggle pressed. Is on? "+t.isOn);
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(5);
        AuditoriumSetup();
    }
}
