using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Meteor Spawn")]
    public GameObject meteorPrefab;
    public float startMeteorSpeedX = -6f;
    public float startMeteorSpeedY = -10f;

    [Header("State")]
    public bool isRunning = false;

    [Header("UI")]
    [SerializeField] private GameObject NOADButton;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject Score_Text;
    [SerializeField] private Text bestScore_Text;
    [SerializeField] private GameObject Noads;

    private Camera cam;
    private int score;
    private int bestScore;

    void Awake()
    {
        Instance = this;
        cam = Camera.main;
        score = 0;
        bestScore = PlayerPrefs.GetInt("BestScore");
        bestScore_Text.text = bestScore.ToString();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        Time.fixedDeltaTime = 1f / 60f;
        Application.runInBackground = false;

    }

    private void Start()
    {
        NoAdPurchasedSetting();
    }

    private void Update()
    {
        
    }

    public void StartGame()
    {
        SpawnStartMeteor();
        isRunning = true;
        NOADButton.SetActive(false);
        StartButton.SetActive(false);
        Score_Text.SetActive(true);
        Score_Text.GetComponent<Text>().text = "0";
        score = 0;
        
    }

    public void GameOver()
    {
        if(score > bestScore)
        {
            bestScore = score;
            bestScore_Text.text = bestScore.ToString();
            PlayerPrefs.SetInt("BestScore", score);
        }

        AdsManager.Instance.OnPlayerDied();

        __Init__();
    }

    public void __Init__()
    {
        isRunning = false;
        NOADButtonActive();
        StartButton.SetActive(true);
        Score_Text.SetActive(false);
        DifficultyManager.Instance.ResetDifficulty();
    }

    public void ScoreUp()
    {
        score++;
        Score_Text.GetComponent<Text>().text = score.ToString();
    }

    public void NoAdPurchased()
    {
        NOADButton.SetActive(false);
        Noads.SetActive(false);
    }

    void NoAdPurchasedSetting()
    {
        if(NoAdsManager.Instance.HasNoAds)
        {
            NOADButton.SetActive(false);
            Noads.SetActive(false);
        }
    }

    void NOADButtonActive()
    {
        if(!NoAdsManager.Instance.HasNoAds)
        {
            NOADButton.SetActive(true);
        }
    }

    void SpawnStartMeteor()
    {
        // 📍 우측 상단 (Viewport 기준)
        Vector3 spawnViewport = new Vector3(1.05f, 1.05f, 0f);
        Vector3 spawnPos = cam.ViewportToWorldPoint(spawnViewport);
        spawnPos.z = 0f;

        // 운석 생성
        GameObject meteor = Instantiate(meteorPrefab, spawnPos, Quaternion.identity);

        // 초기 방향 부여
        Rigidbody2D rb = meteor.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = new Vector2(startMeteorSpeedX, startMeteorSpeedY);
        }

        SoundManager.Instance.Play(SFXType.Falling);
    }
}
