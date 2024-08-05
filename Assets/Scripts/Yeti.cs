#nullable enable
using Unity.VisualScripting;
using UnityEngine;

public class Yeti : MonoBehaviour
{

#nullable disable
    private static Yeti instance;
    public static Yeti Instance { get => instance; }

    public Transform pivotLocation = null;
    public Transform previousPivotLocation = null;
    public Rigidbody2D yetiRigidbody = null;
#nullable enable

    private readonly KeyCode launchKey = KeyCode.Space;

    private readonly float startingAngularVelocityDegreesPerSecond = 120F;
    private readonly float angularVelocityDegreesIncreasePerSecond = 2.5F;
    private readonly float pivotRadius = 1.5F;
    private readonly float launchStrength = 15F;

    private float pivotAngleDegrees;
    public float PivotAngleDegrres { get => pivotAngleDegrees; }
    public float angularVelocityDegreesPerSecond;

    public void Awake()
    {
        instance = this;

        angularVelocityDegreesPerSecond = startingAngularVelocityDegreesPerSecond;
    }

    public void Update()
    {
        if ((Input.GetKey(launchKey) || Input.GetMouseButtonDown((int)MouseButton.Left) || Input.GetMouseButtonDown((int)MouseButton.Right)) && pivotLocation != null)
        {
            float pivotAngleRadians = Mathf.Deg2Rad * pivotAngleDegrees;

            float x = Mathf.Cos(pivotAngleRadians) * pivotRadius;
            float y = Mathf.Sin(pivotAngleRadians) * pivotRadius;

            yetiRigidbody.velocity = new Vector2(-y, x).normalized * launchStrength;

            previousPivotLocation = pivotLocation;
            pivotLocation = null;
        }

        if (pivotLocation != null)
        {
            float x_0 = pivotLocation.position.x;
            float y_0 = pivotLocation.position.y;

            pivotAngleDegrees += angularVelocityDegreesPerSecond * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0, -270 + pivotAngleDegrees);

            float newAngleRadians = pivotAngleDegrees * Mathf.Deg2Rad;

            float sine = Mathf.Sin(newAngleRadians);
            float cosine = Mathf.Cos(newAngleRadians);

            transform.position = new Vector2(x_0 + cosine * pivotRadius, y_0 + sine * pivotRadius);
        }

        if (GameManager.Instance.HasGameStarted())
        {
            angularVelocityDegreesPerSecond += angularVelocityDegreesIncreasePerSecond * Time.deltaTime;
        }
    }
}
