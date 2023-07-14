#nullable enable
using UnityEngine;

public class Wall : MonoBehaviour
{
    public void OnTriggerEnter2D()
    {
        GameManager.Instance.EndGame();
    }
}
