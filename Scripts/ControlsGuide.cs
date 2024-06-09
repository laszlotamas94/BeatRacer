using UnityEngine;
using UnityEngine.UI;

public class ControlsGuide : MonoBehaviour
{

	[SerializeField]
	private Button returnButton;



	private void Start()
	{

	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			returnButton.onClick.Invoke();
		}
	}
}
