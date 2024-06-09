using Unity.VisualScripting;
using UnityEngine;

public class BoosterRamp : MonoBehaviour
{
	public Rigidbody carBody;

	public GameObject carObject;

	[SerializeField]
	private int multip;

	private bool f = true;

	private void Start() {
		f = true;
	}

	private void OnCollisionEnter(Collision col)
	{
		Debug.Log("Collision!");
		if (f)
		{
			carBody.velocity *= (float)multip;
			f = false;
			Debug.Log(carBody.velocity);
		}
	}

	private void FixedUpdate() {
		if (carBody.transform.position.y <= -40f) {
			f = true;
		}
	}

	private void OnGUI() {
		Event e = Event.current;
		if (e.type == EventType.KeyDown && e.isKey && e.keyCode == KeyCode.R) {
			// Debug.Log("Ramp reset!");
			f = true;
			
		}
	}
}
