#nullable enable
using UnityEngine;

public class Yeti : MonoBehaviour
{
    private static Yeti? instance;

    public static Yeti? Instance
    {
        get { return instance; }
    }

    public Transform? pivotLocation = null;
    public Transform? previousPivotLocation = null;
    private Rigidbody2D? yetiRigidbody = null;

    public static readonly KeyCode launchKey = KeyCode.Space;

    public static float angularVelocityDegreesPerSecond = 120f;
    public static readonly float angularVelocityDegreesIncreasePerSecond = 2.5f;

    public static readonly float pivotRadius = 1.5f;
    public static readonly float launchStrength = 15f;

    public static float? pivotAngleDegrees = null;

    public void Awake()
    {
        instance = this;
        yetiRigidbody = GetComponent<Rigidbody2D>();
        pivotAngleDegrees = null;
    }

    public void Update()
    {
        if ((Input.GetKey(launchKey) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && pivotLocation != null)
        {
            #nullable disable
            float pivotAngleRadians = Mathf.Deg2Rad * (float) pivotAngleDegrees;
            #nullable enable

            float x = Mathf.Cos(pivotAngleRadians) * pivotRadius;
            float y = Mathf.Sin(pivotAngleRadians) * pivotRadius;

            #nullable disable
            yetiRigidbody.velocity = new Vector2(-y, x).normalized * launchStrength;
            #nullable enable

            previousPivotLocation = pivotLocation;
            pivotLocation = null;
        }

        if (pivotLocation != null)
        {
            float x_0 = pivotLocation.position.x;
            float y_0 = pivotLocation.position.y;

            if (pivotAngleDegrees == null)
            {
                float x = transform.position.x;
                float y = transform.position.y;

                float xDifference = x - x_0;
                float yDifference = y - y_0;

                float radius = Vector2.Distance(transform.position, pivotLocation.position);

                float angleRadians = Mathf.Asin(xDifference / radius);

                float angleDegrees = Mathf.Rad2Deg * (
                    xDifference >= 0
                    ? (yDifference >= 0 ? angleRadians : -angleRadians)
                    : (yDifference >= 0 ? angleRadians + Mathf.PI / 2 : angleRadians + Mathf.PI)
                );

                pivotAngleDegrees = angleDegrees;
            }

            pivotAngleDegrees += angularVelocityDegreesPerSecond * Time.deltaTime;

            transform.rotation = Quaternion.Euler(0, 0, -270 + (float) pivotAngleDegrees);

            float newAngleRadians = (float) pivotAngleDegrees * Mathf.Deg2Rad;

            float sine = Mathf.Sin(newAngleRadians);
            float cosine = Mathf.Cos(newAngleRadians);

            transform.position = new Vector2(x_0 + cosine * pivotRadius, y_0 + sine * pivotRadius);
        }

        if (GameManager.Score > 0)
        {
            angularVelocityDegreesPerSecond += angularVelocityDegreesIncreasePerSecond * Time.deltaTime;
        }
    }
}
