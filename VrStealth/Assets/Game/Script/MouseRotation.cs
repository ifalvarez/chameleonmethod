using UnityEngine;

public class MouseRotation : MonoBehaviour
{
    public float mouseSensitivity = 100.0f;
    public float clampAngle = 80.0f;

    private float rotY = 0.0f;
    private float rotX = 0.0f;

    float mouseX = 0.0f;
    float mouseY = 0.0f;
    Quaternion myRotation;

    public enum MouseAxes
    {
        xAndYAxis,
        xAxis,
        yAxis,
        autoX
    }
    public MouseAxes selectedAxes = MouseAxes.xAndYAxis;

    void Start()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
    }

    void Update()
    {
        switch(selectedAxes)
        {
            case MouseAxes.xAndYAxis:
                mouseX = Input.GetAxis("Mouse X");
                mouseY = -Input.GetAxis("Mouse Y");
                rotY += mouseX * mouseSensitivity * Time.deltaTime;
                rotX += mouseY * mouseSensitivity * Time.deltaTime;
                rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
                myRotation = Quaternion.Euler(rotX, rotY, transform.rotation.z);
                transform.localRotation = myRotation;
                break;

            case MouseAxes.xAxis:
                mouseY = -Input.GetAxis("Mouse Y");
                rotX += mouseY * mouseSensitivity * Time.deltaTime;
                rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
                myRotation = Quaternion.Euler(rotX, transform.rotation.y, transform.rotation.z);
                transform.localRotation = myRotation;
                break;

            case MouseAxes.yAxis:
                mouseX = Input.GetAxis("Mouse X");
                rotY += mouseX * mouseSensitivity * Time.deltaTime;
                rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);
                myRotation = Quaternion.Euler(transform.rotation.x, rotY, transform.rotation.z);
                transform.localRotation = myRotation;
                break;
            case MouseAxes.autoX:
                transform.Rotate(0.0f, 0.1f, 0.0f);
                break;
        }        
    }
}
    