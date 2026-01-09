using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    public float ElapsedTime { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        ElapsedTime += Time.deltaTime;
    }

    public void ResetDifficulty()
    {
        ElapsedTime = 0f;
    }
}
