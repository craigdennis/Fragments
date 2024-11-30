using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneService : MonoBehaviour
{
    public static SceneService Instance { get; private set; }
    public PlayerInventory PlayerInventory { get; private set; }
    
    private SceneTransitionData transitionData;
    public bool IsTransitionPending => transitionData != null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Get or create PlayerInventory
            PlayerInventory = GetComponent<PlayerInventory>();
            if (PlayerInventory == null)
            {
                PlayerInventory = gameObject.AddComponent<PlayerInventory>();
                Debug.Log("Created new PlayerInventory component");
            }
        }
        else
        {
            Debug.Log("Destroying duplicate SceneService");
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void TransitionToCombat(SceneTransitionData data)
    {
        Debug.Log($"SceneService: Transitioning to combat scene with enemy type: {data.EnemyType}"); // Debug log
        transitionData = data;
        SceneManager.LoadScene(GameConstants.COMBAT_SCENE);
    }

    public void ReturnToExploration(Room nextRoom)
    {
        transitionData = new SceneTransitionData { NextRoom = nextRoom };
        SceneManager.LoadScene("MainGameScene");
    }

    public SceneTransitionData GetTransitionData() => transitionData;
    public void ClearTransitionData() => transitionData = null;
}