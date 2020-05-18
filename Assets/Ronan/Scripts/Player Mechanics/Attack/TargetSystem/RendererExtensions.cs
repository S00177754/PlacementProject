using UnityEngine;

//Credit to Author: Michael Garforth
//Solution from Unity fourms/community to solve issue with built in ISVisible as it checks against all camera including the unity editor camera, this focuses on a single camera.

public static class RendererExtensions
{
	public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}
