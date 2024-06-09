using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeTextColor : MonoBehaviour
{
	[SerializeField]
	private GameObject groundOutline;

	[SerializeField]
	private Material[] outlineMaterials = new Material[4];

	[SerializeField]
	private GameObject panelObject;

	[SerializeField]
	private TextMeshProUGUI levelSelectText;

	[SerializeField]
	private TextMeshProUGUI levelNameText;

	[SerializeField]
	private TextMeshProUGUI buttonText;

	[SerializeField]
	private TextMeshProUGUI attemptsText;

	[SerializeField]
	private TextMeshProUGUI attemptsCount;

	[SerializeField]
	private TextMeshProUGUI progressText;

	[SerializeField]
	private TextMeshProUGUI progressCount;

	[SerializeField]
	private Button nextButton;

	[SerializeField]
	private Button prevButton;

	[SerializeField]
	private Button selectButton;

	[SerializeField]
	private Button spectateButton;

	[SerializeField]
	private Light directionalLight;

	[SerializeField]
	private AudioSource[] songs = new AudioSource[4];

	[SerializeField]
	private Button exitButton;

	public static Color[] textColors = new Color[4];

	private int temp_x;

	public static int x = 0;

	public static string[] levelNames = new string[5];

	[SerializeField]
	private Material[] skyboxMaterials = new Material[4];

	[SerializeField]
	private GameObject carBody;

	[SerializeField]
	private Material carsMaterial;

	private static string[] spectateNames = new string[4];

	private void Start()
	{
		x = 0;
		attemptsCount.SetText(PlayerPrefs.GetInt("Level0Attempts").ToString());
		progressCount.SetText(PlayerPrefs.GetInt("Level0Progress") + "%");
		textColors[0] = new Color32(10, 10, 255, 255);
		textColors[1] = Color.green;
		textColors[2] = Color.yellow;
		textColors[3] = Color.red;
		carsMaterial.color = Color.blue;
		levelNames[0] = "Benihana";
		levelNames[1] = "Cold";
		levelNames[2] = "Breakdown";
		levelNames[3] = "Buster";
		levelNames[4] = "LevelSelect";

		spectateNames[0] = "BenihanaSpectate";
		spectateNames[1] = "ColdSpectate";
		spectateNames[2] = "BreakdownSpectate";
		spectateNames[3] = "BusterSpectate";
		RenderSettings.skybox = skyboxMaterials[0];
		for (int i = 1; i <= 3; i++)
		{
			songs[i].volume = 0f;
		}
		ChangeStringColor();
	}

	private void LowerVolume()
	{
		if (songs[temp_x].volume == 0f)
		{
			songs[temp_x].Stop();
			return;
		}
		songs[temp_x].volume -= 0.05f;
		Invoke("LowerVolume", 0.05f);
	}

	private void RaiseVolume()
	{
		if (songs[x].volume != 1f)
		{
			songs[x].volume += 0.05f;
			Invoke("RaiseVolume", 0.05f);
		}
	}

	private void DisableElseMusic()
	{
		for (int i = 0; i <= 3; i++)
		{
			if (i != x && i != temp_x)
			{
				songs[i].volume = 0f;
			}
		}
	}

	public void LowerIndex()
	{
		temp_x = x;
		Invoke("LowerVolume", 0.3f);
		if (x > 0)
		{
			x--;
		}
		else
		{
			x = 3;
		}
		RaiseVolume();
		DisableElseMusic();
	}

	public void GrowIndex()
	{
		temp_x = x;
		Invoke("LowerVolume", 0.3f);
		if (x < 3)
		{
			x++;
		}
		else
		{
			x = 0;
		}
		RaiseVolume();
		DisableElseMusic();
	}

	public void ChangeStringColor()
	{
		songs[x].Play();
		RenderSettings.skybox = skyboxMaterials[x];
		levelSelectText.color = textColors[x];
		buttonText.color = textColors[x];
		attemptsText.color = textColors[x];
		progressText.color = textColors[x];

		Transform[] lines = groundOutline.GetComponentsInChildren<Transform>(false);
		foreach (Transform line in lines) {
			MeshRenderer renderer = line.GetComponent<MeshRenderer>();
			if (renderer != null) {
				renderer.material = outlineMaterials[x];
			}
			// line.GetComponent<MeshRenderer>().material = outlineMaterials[x];
		}
		
		Color color = textColors[x];
		color.a = 0.35f;
		panelObject.GetComponent<Image>().color = color;
		nextButton.GetComponent<Image>().color = textColors[x];
		prevButton.GetComponent<Image>().color = textColors[x];
		selectButton.GetComponent<Image>().color = textColors[x];
		exitButton.GetComponent<Image>().color = textColors[x];
		spectateButton.GetComponent<Image>().color = textColors[x];

		levelNameText.SetText(levelNames[x]);
		levelNameText.color = textColors[x];
		directionalLight.color = textColors[x];
		carsMaterial.color = textColors[x];
		string key = "Level" + x + "Attempts";
		attemptsCount.SetText(PlayerPrefs.GetInt(key).ToString());
		attemptsCount.color = textColors[x];
		key = "Level" + x + "Progress";
		progressCount.SetText(PlayerPrefs.GetInt(key) + "%");
		progressCount.color = textColors[x];
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			exitButton.onClick.Invoke();
		}
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene(levelNames[x]);
	}

	public void LoadSpectate() {
		SceneManager.LoadScene(spectateNames[x]);
	}
}
