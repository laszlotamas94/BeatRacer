using UnityEngine;

public class CameraControl : MonoBehaviour
{
	[SerializeField]
	private GameObject carBody;

	[SerializeField] private GameObject followObject;

	private float carBodyX;

	private float carBodyY;

	private float carBodyZ;

	private void Update()
	{
		carBodyX = carBody.transform.eulerAngles.x;
		carBodyY = carBody.transform.eulerAngles.y;
		carBodyZ = carBody.transform.eulerAngles.z;

		followObject.transform.eulerAngles = new Vector3(0, carBodyY, 0);
		base.transform.eulerAngles = new Vector3(20f, carBodyY, 0f);
	}
}
