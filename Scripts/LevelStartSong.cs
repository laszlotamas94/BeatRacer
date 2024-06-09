using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelStartSong : MonoBehaviour
{
	[SerializeField]
	private AudioSource levelSong;

	[SerializeField]
	private TextMeshProUGUI attemptText;

	[SerializeField]
	private GameObject outOfTime;

	[SerializeField]
	private int songLength;

	[SerializeField]
	private Button exitButton;

	private bool alreadyFinished;

	private Rigidbody carObject;

	[SerializeField]
	private GameObject FinishObject;

	public static int x;

	private string tempString = "Level" + ChangeTextColor.x + "Progress";

	private void FadeSongIn()
	{
		if (levelSong.volume < 1f)
		{
			levelSong.volume += 0.04f;
			Invoke("FadeSongIn", 0.06f);
		}
	}

	private void StartLevelSong()
	{
		levelSong.Play();
		levelSong.volume = 0f;
		FadeSongIn();
		StartCoroutine(FinishedSonk(x));
	}

	private void RaiseMusic()
	{
		carObject.constraints = RigidbodyConstraints.None;
		levelSong.volume += 0.04f;
		if (levelSong.volume < 1f)
		{
			Invoke("RaiseMusic", 0.02f);
		}
	}

	private void restartMusic()
	{
		levelSong.volume -= 0.04f;
		if (levelSong.volume > 0f)
		{
			Invoke("restartMusic", 0.02f);
		}
		if (levelSong.volume == 0f)
		{
			levelSong.Play();
			Invoke("RaiseMusic", 0.5f);
		}
		StartCoroutine(FinishedSonk(x));
	}

	private IEnumerator FinishedSonk(int attempt)
	{
		yield return new WaitForSeconds(songLength);
		if (attempt == x && !alreadyFinished)
		{
			Debug.Log("F I N I S H E D");
			alreadyFinished = true;
			outOfTime.SetActive(value: true);
			carObject.constraints = RigidbodyConstraints.FreezeAll;
			yield return new WaitForSeconds(4f);
			outOfTime.SetActive(value: false);
			onRestart();
		}
	}

	private void Start()
	{
		carObject = GetComponent<Rigidbody>();
		Invoke("StartLevelSong", 1f);
		x = 1;
		alreadyFinished = false;
		outOfTime.SetActive(value: false);
	}

	public void UpdatePrefs()
	{
		int num = (int)levelSong.time * 100 / songLength;
		if (num > PlayerPrefs.GetInt(tempString))
		{
			PlayerPrefs.SetInt(tempString, num);
		}
	}

	private void onRestart()
	{
		StopAllCoroutines();
		carObject.transform.SetPositionAndRotation(new Vector3(0f, 2f, 0f), new Quaternion(0f, 0f, 0f, 0f));
		stopVelocity();
		carObject.constraints = RigidbodyConstraints.FreezeAll;
		FinishObject.SetActive(value: false);
		FinishCurrentLevel.done = false;
		x++;
		attemptText.SetText("Attempt " + x);
		restartMusic();
		UpdatePrefs();
		alreadyFinished = false;
	}

	private void FixedUpdate()
	{
		if (x > 0 && carObject.transform.position.y < -42f) {
			onRestart();
		}
	}

	private void OnGUI() {
		Event e = Event.current;
		if (e.type == EventType.KeyDown && e.isKey) {
			if (e.keyCode == KeyCode.R && carObject.constraints == RigidbodyConstraints.None) {
				onRestart();
			}
			else if (e.keyCode == KeyCode.Escape) {
				exitButton.onClick.Invoke();
			}
		}
	}

	private void stopVelocity()
	{
		carObject.velocity = new Vector3(0f, 0f, 0f);
	}
}
