using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[AddComponentMenu("Agora VR/UI/Main Menu")]
public class MainMenu : MonoBehaviour
{
    bool inMenu;
    private Text sliderText;

	void Start ()
    {
        /* Center Panel (index 0) */
        UIBuilder.instance.AddSpacer();
        UIBuilder.instance.AddLogo();
        UIBuilder.instance.AddLabel("Scene Selection");
        UIBuilder.instance.AddDivider();
        UIBuilder.instance.AddButton("Auditorium", StartAuditorium);
        UIBuilder.instance.AddButton("Meeting Room", StartMeetingRoom);

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

    public void SliderPressed(float f)
    {
        Debug.Log("Slider: " + f);
        sliderText.text = f.ToString();
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

    void StartAuditorium()
    {
        SceneManager.LoadScene(1); //Assumes Auditorium Scene is at Index 1 in Build
    }

    void StartMeetingRoom()
    {
        SceneManager.LoadScene(2); //Assumes Meeting Room Scene is at Index 2 in Build
    }
}
