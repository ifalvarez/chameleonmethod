using UnityEngine.VR;
using UnityEngine;

public class DisableTracking : MonoBehaviour
{
    public bool disableCameraTracking = false;
    Camera cam;

    private void Awake()
    {
        if (disableCameraTracking == true)
        {
            cam = GetComponent<Camera>();
            VRDevice.DisableAutoVRCameraTracking(cam, true);
            print("Camera Disabled");
        }
    }
}