/************************************************************************************
Copyright : Copyright (c) Facebook Technologies, LLC and its affiliates. All rights reserved.

Licensed under the Oculus Utilities SDK License Version 1.31 (the "License"); you may not use
the Utilities SDK except in compliance with the License, which is provided at the time of installation
or download, or which otherwise accompanies this software in either electronic or hard copy form.

You may obtain a copy of the License at
https://developer.oculus.com/licenses/utilities-1.31

Unless required by applicable law or agreed to in writing, the Utilities SDK distributed
under the License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF
ANY KIND, either express or implied. See the License for the specific language governing
permissions and limitations under the License.
************************************************************************************/

// Modified by Daniel Nguyen

using UnityEngine;
using System.Collections;

[AddComponentMenu("Agora VR/3rd Party/Oculus Hand Helper")]

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
