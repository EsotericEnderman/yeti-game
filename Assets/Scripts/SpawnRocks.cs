#nullable enable
using UnityEngine;

using Random = System.Random;

public class SpawnRocks : MonoBehaviour
{

#nullable disable
    private static SpawnRocks instance;

    public static SpawnRocks Instance { get => instance; }

    public GameObject breakingRockPrefab;
    public GameObject rockPrefab;
    public GameObject unbreakableRockPrefab;
#nullable enable

    private readonly float startingRockSpeedPerSecond = 1.25F;
    private readonly float rockAccelerationPerSecond = 0.0125F;
    private readonly float maximumRockSpeedPerSecond = 3.25F;

    private readonly float startingRockIntervalSeconds = 3.1F;
    private readonly float rockIntervalDecreasePerSecond = 0.0015F;
    private readonly float minimumRockIntervalSeconds = 0.45F;

    private readonly float rockXPositionRange = 1.4F;

    private readonly float rockChance = 0.1F;
    private readonly float breakingRockChance = 0.1F;

    private float timeWithoutRockSpawnSeconds = 0F;
    private float rockSpeedPerSecond;
    public float RockSpeedPerSecond { get => rockSpeedPerSecond; }
    private float rockSpawnIntervalSeconds;
    private int rockNumber = 0;

    public void Awake()
    {
        instance = this;

        rockSpeedPerSecond = startingRockSpeedPerSecond;
        rockSpawnIntervalSeconds = startingRockIntervalSeconds;
    }

    public void Update()
    {
        if (GameManager.Instance.HasGameStarted())
        {
            timeWithoutRockSpawnSeconds += Time.deltaTime;

            if (timeWithoutRockSpawnSeconds >= rockSpawnIntervalSeconds)
            {
                Random random = new();
                double randomNumber = random.NextDouble();
                GameObject rock;

                if (randomNumber <= rockChance)
                {
                    rock = rockPrefab;
                }
                else if (randomNumber <= rockChance + breakingRockChance)
                {
                    rock = breakingRockPrefab;
                }
                else
                {
                    rock = unbreakableRockPrefab;
                }

                GameObject rockCopy = Instantiate(rock);
                rockCopy.transform.position = new Vector2(GetRockXPosition(rockNumber), 5);

                Destroy(rockCopy, 15);

                rockNumber++;
                timeWithoutRockSpawnSeconds = 0;
            }

            rockSpeedPerSecond += rockAccelerationPerSecond * Time.deltaTime;
            rockSpawnIntervalSeconds -= rockIntervalDecreasePerSecond * Time.deltaTime;

            rockSpeedPerSecond = Mathf.Min(rockSpeedPerSecond, maximumRockSpeedPerSecond);
            rockSpawnIntervalSeconds = Mathf.Max(rockSpawnIntervalSeconds, minimumRockIntervalSeconds);
        }
    }

    private float GetRockXPosition(int rockNumber)
    {
        return rockXPositionRange * Mathf.Sin(rockNumber * rockNumber);
    }
}
