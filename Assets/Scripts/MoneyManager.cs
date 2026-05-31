using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    [SerializeField] private string moneyTextName = "Money Text";
    private int totalMoney = 0;
    private TextMeshProUGUI moneyTextComponent;

    private void Awake()
    {
        // Singleton pattern: ensure only one instance of MoneyManager exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Register to the sceneLoaded event to automatically find the Money Text UI in new scenes
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }

    private void Start()
    {
        // Perform an initial search for the Money Text in the current scene
        FindAndUpdateMoneyText();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-find the new Money Text UI in the newly loaded scene and update it
        FindAndUpdateMoneyText();
    }

    public void AddMoney(int amount)
    {
        totalMoney += amount;
        UpdateUI();
    }

    public int GetTotalMoney()
    {
        return totalMoney;
    }

    private void FindAndUpdateMoneyText()
    {
        GameObject moneyTextObj = GameObject.Find(moneyTextName);

        // Fallback search: find any text containing "Money" in its name if "Money Text" is not found
        if (moneyTextObj == null)
        {
            TextMeshProUGUI[] allTexts = FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);
            foreach (var t in allTexts)
            {
                if (t.gameObject.name.Contains("Money"))
                {
                    moneyTextObj = t.gameObject;
                    break;
                }
            }
        }

        if (moneyTextObj != null)
        {
            moneyTextComponent = moneyTextObj.GetComponent<TextMeshProUGUI>();
            UpdateUI();
        }
        else
        {
            Debug.LogWarning("MoneyManager: Could not find Money Text object in the loaded scene.");
        }
    }

    private void UpdateUI()
    {
        if (moneyTextComponent != null)
        {
            moneyTextComponent.text = "Money: " + totalMoney;
        }
    }
}
