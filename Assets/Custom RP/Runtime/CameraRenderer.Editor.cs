﻿using UnityEngine;
using UnityEngine.Rendering;

partial class CameraRenderer
{
	partial void PrepareForSceneWindow();

#if UNITY_EDITOR

	partial void PrepareForSceneWindow()
	{
		if (camera.cameraType == CameraType.SceneView)
		{
			ScriptableRenderContext.EmitWorldGeometryForSceneView(camera);
			useScaledRendering = false;
		}
	}

#endif
}
