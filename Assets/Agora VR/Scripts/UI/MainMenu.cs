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
        UIBuilder.instance.AddLogo();
        UIBuilder.instance.AddLabel("Welcome To Agora VR");
        UIBuilder.instance.AddButton("Auditorium", StartAuditorium);
        UIBuilder.instance.AddButton("Meeting Room", StartMeetingRoom);

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
