using UnityEngine;

[ExecuteAlways]
public class SkyboxRotator : MonoBehaviour
{
    [Tooltip("Speed of rotation in degrees per second")]
    public float rotationSpeed = 0.5f;

    private float rotation;

    void Update()
    {
        rotation += rotationSpeed * Time.deltaTime;
        if (rotation > 360f) rotation -= 360f;

        RenderSettings.skybox.SetFloat("_Rotation", rotation);
        DynamicGI.UpdateEnvironment();
    }
}
