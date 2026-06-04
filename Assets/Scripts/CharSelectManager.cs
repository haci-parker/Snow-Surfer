using UnityEngine;
using UnityEngine.UI;

public class CharSelectManager : MonoBehaviour
{
    [Header("Frog Unlock Settings")]
    [SerializeField] private GameObject frogLock;
    [SerializeField] private int frogCost = 5;

    [Header("Character Selection References")]
    [SerializeField] private GameObject canvasCharSelection;
    [SerializeField] private GameObject scoreText;
    [SerializeField] private GameObject dinoSprite;
    [SerializeField] private GameObject frogSprite;

    private bool IsFrogUnlocked
    {
        get { return PlayerPrefs.GetInt("FrogUnlocked", 0) == 1; }
        set { PlayerPrefs.SetInt("FrogUnlocked", value ? 1 : 0); }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 0;

        // Auto-find references if not assigned in Inspector
        FindReferences();

        // Rewire buttons: replace inspector-assigned persistent events with our methods
        RewireButtons();

        // Set initial state for character selection screen
        if (scoreText != null) scoreText.SetActive(false);
        if (dinoSprite != null) dinoSprite.SetActive(false);
        if (frogSprite != null) frogSprite.SetActive(false);
        if (canvasCharSelection != null) canvasCharSelection.SetActive(true);

        // Initialize frog lock object visibility
        if (frogLock != null)
        {
            frogLock.SetActive(!IsFrogUnlocked);
        }
    }

    private void FindReferences()
    {
        if (canvasCharSelection == null)
            canvasCharSelection = FindObjectByName("Canvas for Char Selection");
        if (scoreText == null)
            scoreText = FindObjectByName("Score Text");
        if (dinoSprite == null)
            dinoSprite = FindObjectByName("Dino Sprite");
        if (frogSprite == null)
            frogSprite = FindObjectByName("Frog Sprite");
        if (frogLock == null)
            frogLock = FindObjectByName("FrogLock");
    }

    // Finds a GameObject by name even if it is inactive
    private GameObject FindObjectByName(string name)
    {
        foreach (GameObject root in UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects())
        {
            Transform result = SearchChildren(root.transform, name);
            if (result != null) return result.gameObject;
        }
        return null;
    }

    private Transform SearchChildren(Transform parent, string name)
    {
        if (parent.name == name) return parent;
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform result = SearchChildren(parent.GetChild(i), name);
            if (result != null) return result;
        }
        return null;
    }

    private void RewireButtons()
    {
        GameObject buttonFrogObj = GameObject.Find("ButtonFrog");
        if (buttonFrogObj != null)
        {
            Button frogBtn = buttonFrogObj.GetComponent<Button>();
            if (frogBtn != null)
            {
                frogBtn.onClick = new Button.ButtonClickedEvent();
                frogBtn.onClick.AddListener(SelectFrog);
            }
        }

        GameObject buttonDinoObj = GameObject.Find("ButtonDino");
        if (buttonDinoObj != null)
        {
            Button dinoBtn = buttonDinoObj.GetComponent<Button>();
            if (dinoBtn != null)
            {
                dinoBtn.onClick = new Button.ButtonClickedEvent();
                dinoBtn.onClick.AddListener(SelectDino);
            }
        }
    }

    public void BeginGame()
    {
        Time.timeScale = 1f;
    }

    public void SelectDino()
    {
        if (canvasCharSelection != null) canvasCharSelection.SetActive(false);
        if (dinoSprite != null) dinoSprite.SetActive(true);
        if (frogSprite != null) frogSprite.SetActive(false);
        if (scoreText != null) scoreText.SetActive(true);
        BeginGame();
    }

    public void SelectFrog()
    {
        if (IsFrogUnlocked)
        {
            if (canvasCharSelection != null) canvasCharSelection.SetActive(false);
            if (dinoSprite != null) dinoSprite.SetActive(false);
            if (frogSprite != null) frogSprite.SetActive(true);
            if (scoreText != null) scoreText.SetActive(true);
            BeginGame();
        }
        else
        {
            // Try to unlock
            if (MoneyManager.Instance != null && MoneyManager.Instance.GetTotalMoney() >= frogCost)
            {
                if (MoneyManager.Instance.SpendMoney(frogCost))
                {
                    IsFrogUnlocked = true;
                    if (frogLock != null)
                    {
                        frogLock.SetActive(false);
                    }
                }
            }
        }
    }
}
