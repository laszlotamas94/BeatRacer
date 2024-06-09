using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinishCurrentLevel : MonoBehaviour
{
	[SerializeField]
	private GameObject carBody;

	[SerializeField]
	private GameObject canvasObject;

	[SerializeField]
	private TextMeshProUGUI levelNameText;

	[SerializeField]
	private Image panelObject;

	private string tempString = "Level" + ChangeTextColor.x + "Attempts";

	public static bool done;

	private void Start()
	{
		levelNameText.SetText(ChangeTextColor.levelNames[ChangeTextColor.x]);
		done = false;
		Color color = ChangeTextColor.textColors[ChangeTextColor.x];
		color.a = 0.4f;
		panelObject.color = color;
		canvasObject.SetActive(value: false);
		_ = "Level" + ChangeTextColor.x + "Attempts";
	}

	private void OnCollisionEnter(Collision col)
	{
		if (!done && col.gameObject == carBody)
		{
			Debug.Log(col.gameObject);
			canvasObject.SetActive(value: true);
			done = true;
			PlayerPrefs.SetInt(tempString, PlayerPrefs.GetInt(tempString) + LevelStartSong.x);
			PlayerPrefs.SetInt("Level" + ChangeTextColor.x + "Progress", 100);
			LevelStartSong.x = 0;
		}
	}

	private void Update()
	{
	}
}
