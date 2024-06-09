using UnityEngine;
using UnityEngine.UI;

public class FreeCameraControl : MonoBehaviour
{
    [SerializeField]
    private Button exitButton;
    [SerializeField] private float speed = 25.0f;
    [SerializeField] private float mouseSensitivity = 800f;
    [SerializeField] private float ascentSpeed = 20.0f;

    private CharacterController controller;
    private Transform cameraTransform;
    private float xRotation = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float multiplier = 1.0f;
        if (Input.GetKey(KeyCode.LeftShift)) {
            multiplier = 3.0f;
        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;

 
        controller.Move(move * speed * multiplier * Time.deltaTime);


        if (Input.GetKey(KeyCode.Space))
        {
            controller.Move(Vector3.up * multiplier * ascentSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            controller.Move(Vector3.down * multiplier * ascentSpeed * Time.deltaTime);
        }
    }

    private void OnGUI() {
        Event e = Event.current;
		if (e.type == EventType.KeyDown && e.isKey && e.keyCode == KeyCode.Escape) {
            exitButton.onClick.Invoke();
        }
    }

    public void UnlockCursor() {
        Cursor.lockState = CursorLockMode.None;
    }

}

