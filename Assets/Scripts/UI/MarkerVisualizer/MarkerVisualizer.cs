/*
 * Copyright (c) 2020 LG Electronics Inc.
 *
 * SPDX-License-Identifier: MIT
 */

using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public partial class MarkerVisualizer : MonoBehaviour
{
	private UnityEvent responseEvent;
	private const string targetRootName = "Markers";
	private const string mainCameraName = "Main Camera";
	private const string commonShaderName = "UI/Unlit/Text";
	private Shader commonShader = null;

	private Camera mainCamera = null;
	private GameObject rootMarkers = null;
	private Hashtable registeredMarkers = null;
	private Hashtable registeredObjectsForText = null;


#region Request
	private VisualMarkerRequest request = null;
#endregion

#region Response
	private VisualMarkerResponse response = null;
#endregion

	MarkerVisualizer()
	{
		registeredMarkers = new Hashtable();
		registeredObjectsForText = new Hashtable();
		response = new VisualMarkerResponse();
		InitializeList();
	}

	void Awake()
	{
		if (responseEvent == null)
		{
			// Debug.Log("UnityEvent");
			responseEvent = new UnityEvent();
		}

		rootMarkers = GameObject.Find(targetRootName);
		commonShader = Shader.Find(commonShaderName);
		mainCamera = GameObject.Find(mainCameraName).GetComponent<Camera>();
	}

	void Update()
	{
		if (request != null && !request.markerCommand.Equals(VisualMarkerRequest.MarkerCommands.Unknown))
		{
			StartCoroutine(HandleRequsetMarkers());
		}
	}

	void LateUpdate()
	{
		HandleFollowingText();
	}

	private void HandleFollowingText()
	{
		var texts = rootMarkers.GetComponentsInChildren<TextMeshPro>();
		foreach (var text in texts)
		{
			// Look at camera
			var textObject = text.gameObject;
			textObject.transform.LookAt(mainCamera.transform);

			// Following Objects
			var markerName = text.name;
			var followingObject = registeredObjectsForText[markerName] as GameObject;
			if (followingObject != null)
			{
				var rectTransform = textObject.GetComponent<RectTransform>();
				var followingObjectPosition = followingObject.transform.position;
				var textPosition = rectTransform.localPosition;
				var newPos = new Vector3(followingObjectPosition.x, textPosition.y, followingObjectPosition.z);
				rectTransform.position = newPos;
			}
		}
	}

	public void RegisterResponseAction(in UnityAction call)
	{
		if (responseEvent != null)
		{
			responseEvent.AddListener(call);
		}
		else
		{
			Debug.LogWarning("event is not ready!!");
		}
	}

	private void DoneMarkerRequested(in string command, in bool result)
	{
		request = null;

		response.command = command;
		response.result = (result)? SimulationService.SUCCESS : SimulationService.FAIL;

		if (result)
		{
			responseEvent.Invoke();
		}
	}

	private void SetDefaultMeshRenderer(in Renderer renderer)
	{
		if (renderer != null)
		{
			renderer.material = new Material(commonShader);
			renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			renderer.receiveShadows = false;
			renderer.lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
			renderer.reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
			renderer.motionVectorGenerationMode = MotionVectorGenerationMode.ForceNoMotion;
		}
	}

	private IEnumerator HandleRequsetMarkers()
	{
		var result = false;

		switch (request.markerCommand)
		{
			case VisualMarkerRequest.MarkerCommands.Add:
				result = AddMarkers();
				break;

			case VisualMarkerRequest.MarkerCommands.Modify:
				result = ModifyMarkers();
				break;

			case VisualMarkerRequest.MarkerCommands.Remove:
				result = RemoveMarkers();
				break;

			case VisualMarkerRequest.MarkerCommands.List:
				result = ListMarkers();
				break;

			case VisualMarkerRequest.MarkerCommands.Unknown:
				break;

			default:
				Debug.Log("invalid type");
				break;
		}

		var command = request.markerCommand.ToString().ToLower();
		DoneMarkerRequested(command, result);

		yield return null;
	}


	public bool PushRequsetMarkers(in VisualMarkerRequest markerRequest)
	{
		if (markerRequest.markerCommand.Equals(VisualMarkerRequest.MarkerCommands.List) && markerRequest.markers.Count > 0)
		{
			request = null;
			response.command = request.command;
			response.result = SimulationService.FAIL;
			response.lines = null;
			response.texts = null;
			response.boxes = null;
			response.spheres = null;
			return false;
		}

		request = markerRequest;

		return true;
	}

	public VisualMarkerResponse GetResponseMarkers()
	{
		return response;
	}
}