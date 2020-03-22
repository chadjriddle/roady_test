using UnityEngine;
using WildbotLabs.Scriptables.References;

public class UpdateTopCameraRCC : MonoBehaviour
{
    public RCC_Camera RccCamera;
    public IntReference CameraZoom;
    public IntReference CameraXAngle;
    public IntReference CameraYAngle;

    // Start is called before the first frame update
    void Start()
    {
       UpdateTopCamera();
    }

    void OnEnable()
    {
        CameraZoom.ValueChanged += UpdateTopCamera;
        CameraXAngle.ValueChanged += UpdateTopCamera;
        CameraYAngle.ValueChanged += UpdateTopCamera;
    }

    void OnDisable()
    {
        CameraZoom.ValueChanged -= UpdateTopCamera;
        CameraXAngle.ValueChanged -= UpdateTopCamera;
        CameraYAngle.ValueChanged -= UpdateTopCamera;
    }


    private void UpdateTopCamera()
    {
        RccCamera.minimumOrtSize = CameraZoom.Value;
        RccCamera.maximumOrtSize = CameraZoom.Value;

        RccCamera.topCameraAngle = new Vector3(CameraXAngle.Value, CameraYAngle.Value, 0);
    }
}
