using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
	[SerializeField]
	private Button nextButton;

	[SerializeField]
	private Button prevButton;

	[SerializeField]
	private Button selectButton;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			nextButton.onClick.Invoke();
		}
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			prevButton.onClick.Invoke();
		}
		if (Input.GetKeyDown(KeyCode.Return))
		{
			selectButton.onClick.Invoke();
		}
	}
}
