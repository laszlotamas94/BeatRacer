using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	private void Start()
	{
		FirstTimePrefs();
		Screen.SetResolution(1920, 1080, fullscreen: true);
	}

	private void FirstTimePrefs()
	{
		if (!PlayerPrefs.HasKey("Level1Progress"))
		{
			PlayerPrefs.SetInt("Level1Attempts", 0);
			PlayerPrefs.SetInt("Level2Attempts", 0);
			PlayerPrefs.SetInt("Level3Attempts", 0);
			PlayerPrefs.SetInt("Level0Attempts", 0);
			PlayerPrefs.SetInt("Level1Progress", 0);
			PlayerPrefs.SetInt("Level2Progress", 0);
			PlayerPrefs.SetInt("Level3Progress", 0);
			PlayerPrefs.SetInt("Level0Progress", 0);
		}
	}

	public void PlayGame()
	{
		string key = "Level" + ChangeTextColor.x + "Attempts";
		PlayerPrefs.SetInt(key, PlayerPrefs.GetInt(key) + LevelStartSong.x);
		SceneManager.LoadScene("LevelSelect");
	}

	public void BackToMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}

	public void ToControls()
	{
		SceneManager.LoadScene("Controls");
	}

	public void ToGuide()
	{
		SceneManager.LoadScene("Guide");
	}

	public void ExitApp()
	{
		Application.Quit();
	}
}
