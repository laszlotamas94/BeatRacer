using UnityEngine;

public class AudioBeats : MonoBehaviour
{
	[SerializeField]
	private AudioSource mumsic;

	[SerializeField]
	private Camera mainCamera;

	[SerializeField]
	private int minimumThreshold;

	[SerializeField]
	private int maximumFreqCounted;

	[SerializeField]
	private int minimumFreqCounted;

	[SerializeField]
	private float multipl;

	[SerializeField]
	private Rigidbody carBody;

	[SerializeField]
	private Light varLight;

	private Vector3[] directions = new Vector3[6];

	private const int DATA_LENGTH = 1024;

	private bool wasForced;

	private float clipLoudness;

	private float[] clipSampleData = new float[1024];

	private void Start()
	{
		directions[0] = Vector3.up;
		directions[1] = Vector3.down;
		directions[2] = Vector3.left;
		directions[3] = Vector3.right;
		directions[4] = Vector3.forward;
		directions[5] = Vector3.back;
	}

	private void Update()
	{
		if (mumsic.isPlaying)
		{
			AnalyzeMusic();
			if (mainCamera.fieldOfView < 60f)
			{
				mainCamera.fieldOfView += 1f;
			}
			varLight.intensity = (60f - mainCamera.fieldOfView) * 0.12f;
		}
	}

	private void AllowForce()
	{
		wasForced = false;
	}

	private void AnalyzeMusic()
	{
		mumsic.clip.GetData(clipSampleData, mumsic.timeSamples);
		clipLoudness = 0f;
		for (int i = minimumFreqCounted; i < maximumFreqCounted; i++)
		{
			clipLoudness += Mathf.Abs(clipSampleData[i]);
		}
			// Debug.Log(clipLoudness);
		if (clipLoudness > (float)minimumThreshold)
		{
			mainCamera.fieldOfView = 60f - multipl * clipLoudness;
			if (!wasForced)
			{
				carBody.AddForce(directions[Random.Range(0, 5)] * clipLoudness * mumsic.volume * 6000f);
				wasForced = true;
				Invoke("AllowForce", 0.1f);
			}
		}
	}
}
