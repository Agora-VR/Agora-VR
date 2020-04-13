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

    [SerializeField]
    private MainMenu menu;
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
        yield return null;
    }

    private IEnumerator Auditorium()
    {
        menu.sessionSettings.TryGetValue("SessionLength", out len);
        int minutes = (int) len * 60;

        yield return new WaitForSeconds(minutes);
        SceneManager.LoadScene(3); // Loads Review Scene
    }

    private IEnumerator MeetingRoom()
    {
        menu.sessionSettings.TryGetValue("SessionLength", out len);
        int minutes = (int) len * 60;

        yield return new WaitForSeconds(minutes);
        SceneManager.LoadScene(3); // Loads Review Scene
    }

    private IEnumerator Review()
    {
        GameObject.Find("Session Time").GetComponent<TextMeshPro>().text += len.ToString() + " Minute(s)";
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(0); // Loads Main Menu
    }
}
