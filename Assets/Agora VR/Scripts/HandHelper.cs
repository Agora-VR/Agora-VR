using UnityEngine;
using System.Collections;

/// <summary>
/// Simple helper script that conditionally enables rendering of a controller if it is connected.
/// </summary>
public class HandHelper : MonoBehaviour
{
	/// <summary>
	/// The root GameObject that represents the Left Hand model.
	/// </summary>
	public GameObject m_leftHand;

    	/// <summary>
	/// The root GameObject that represents the Right Hand model.
	/// </summary>
	public GameObject m_rightHand;

	/// <summary>
	/// The controller that determines whether or not to enable rendering of the controller model.
	/// </summary>
	public OVRInput.Controller m_controller;

	private bool m_prevControllerConnected = false;
	private bool m_prevControllerConnectedCached = false;

	void Start()
	{
        if (m_controller == OVRInput.Controller.LTrackedRemote) {
            m_controller = OVRInput.Controller.LTouch;
        } else if (m_controller == OVRInput.Controller.RTrackedRemote) {
            m_controller = OVRInput.Controller.RTouch;
        }
	}

	void Update()
	{
		bool controllerConnected = OVRInput.IsControllerConnected(m_controller);

		if ((controllerConnected != m_prevControllerConnected) || !m_prevControllerConnectedCached)
		{
			if (controllerConnected) {
				m_leftHand.SetActive(m_controller == OVRInput.Controller.LTouch);
				m_rightHand.SetActive(m_controller == OVRInput.Controller.RTouch);
			} else {
				m_leftHand.SetActive(false);
				m_rightHand.SetActive(false);
			}

			m_prevControllerConnected = controllerConnected;
			m_prevControllerConnectedCached = true;
		}

		if (!controllerConnected) {
			return;
		}
	}
}
