using UnityEngine;

public class CarControl : MonoBehaviour
{
	private float horizontalInput;

	private float verticalInput;

	private float brakeInput;

	[SerializeField]
	private float motorForce;

	[SerializeField]
	private float brakeForce;

	private float steerAngle;

	[SerializeField]
	private float maxSteerAngle;

	private bool isBraking;

	[SerializeField]
	private WheelCollider FLWCollider;

	[SerializeField]
	private WheelCollider FRWCollider;

	[SerializeField]
	private WheelCollider RLWCollider;

	[SerializeField]
	private WheelCollider RRWCollider;

	[SerializeField]
	private Transform FLWTransform;

	[SerializeField]
	private Transform FRWTransform;

	[SerializeField]
	private Transform RLWTransform;

	[SerializeField]
	private Transform RRWTransform;

	[SerializeField]
	private Rigidbody carBody;

	private void Start()
	{
		carBody.centerOfMass = new Vector3(0f, -0.29f, -0.1f);
	}

	private void FixedUpdate()
	{
		GetInput();
		HandleMotor();
		HandleSteering();
		UpdateWheels();
		if (Input.GetKey(KeyCode.LeftShift))
		{
			carBody.transform.Rotate(new Vector3(1f, 0f, 0f), 1f);
		}
		if (Input.GetKey(KeyCode.LeftControl))
		{
			carBody.transform.Rotate(new Vector3(1f, 0f, 0f), -1f);
		}
		if (carBody.position.y <= -40f)
		{
			StopAllTorques();
		}
	}

	private void HandleSteering()
	{
		steerAngle = maxSteerAngle * horizontalInput;
		FLWCollider.steerAngle = steerAngle;
		FRWCollider.steerAngle = steerAngle;
	}

	private void HandleMotor()
	{
		FLWCollider.motorTorque = verticalInput * motorForce;
		FRWCollider.motorTorque = verticalInput * motorForce;
		// RRWCollider.motorTorque = verticalInput * motorForce;
		// RLWCollider.motorTorque = verticalInput * motorForce;
		if (isBraking)
		{
			brakeInput = brakeForce;
		}
		else
		{
			brakeInput = 0f;
		}
		ApplyBraking();
	}

	private void StopBraking()
	{
		FRWCollider.brakeTorque = 0f;
		FLWCollider.brakeTorque = 0f;
		RRWCollider.brakeTorque = 0f;
		RLWCollider.brakeTorque = 0f;
	}

	private void ApplyBraking()
	{

		// FLWCollider.motorTorque /= 2;
		// FRWCollider.motorTorque /= 2;
		// RRWCollider.motorTorque /= 2;
		// RLWCollider.motorTorque /= 2;

		FRWCollider.brakeTorque = brakeInput;
		FLWCollider.brakeTorque = brakeInput;
		RRWCollider.brakeTorque = brakeInput;
		RLWCollider.brakeTorque = brakeInput;
	}

	private void GetInput()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");
		isBraking = Input.GetKey(KeyCode.Space);
	}

	private void UpdateWheels()
	{
		UpdateSingleWheel(FRWCollider, FRWTransform);
		UpdateSingleWheel(FLWCollider, FLWTransform);
		UpdateSingleWheel(RRWCollider, RRWTransform);
		UpdateSingleWheel(RLWCollider, RLWTransform);
	}

	private void UpdateSingleWheel(WheelCollider WCollider, Transform WTransform)
	{
		WCollider.GetWorldPose(out var pos, out var quat);
		// Debug.Log(pos);
		WTransform.rotation = quat;
		// WTransform.position = pos;
	}

	private void StopAllTorques()
	{
		FRWCollider.brakeTorque = 0f;
		FLWCollider.brakeTorque = 0f;
		RRWCollider.brakeTorque = 0f;
		RLWCollider.brakeTorque = 0f;
		FLWCollider.motorTorque = 0f;
		FRWCollider.motorTorque = 0f;
	}
}
