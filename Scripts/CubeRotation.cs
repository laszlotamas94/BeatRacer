using UnityEngine;

public class CubeRotation : MonoBehaviour
{
	[SerializeField]
	private float angleSpeed;

	private void FixedUpdate()
	{
		base.transform.Rotate(Vector3.up, angleSpeed);
	}
}
