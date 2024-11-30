using UnityEngine;
using System.Collections.Generic;
using System;

public class PlayerDecisionManager : MonoBehaviour
{
    private static PlayerDecisionManager instance;
    public static PlayerDecisionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerDecisionManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("PlayerDecisionManager");
                    instance = go.AddComponent<PlayerDecisionManager>();
                    DontDestroyOnLoad(go);
                }
            }
            return instance;
        }
    }

    private Dictionary<string, bool> playerDecisions = new Dictionary<string, bool>();
    private const string SAVE_KEY = "PlayerDecisions";

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Clear decisions at the start of the game
        ResetAllDecisions();
        
        LoadDecisions();
    }

    public void RecordDecision(string decisionKey)
    {
        if (!string.IsNullOrEmpty(decisionKey))
        {
            playerDecisions[decisionKey] = true;
            SaveDecisions();
            Debug.Log($"Recorded and saved decision: {decisionKey}");
        }
    }

    public bool HasMadeDecision(string decisionKey)
    {
        return playerDecisions.ContainsKey(decisionKey) && playerDecisions[decisionKey];
    }

    public List<string> GetAllDecisions()
    {
        return new List<string>(playerDecisions.Keys);
    }

    private void SaveDecisions()
    {
        SerializableDecisions serializableDecisions = new SerializableDecisions(playerDecisions);
        string jsonData = JsonUtility.ToJson(serializableDecisions);
        PlayerPrefs.SetString(SAVE_KEY, jsonData);
        PlayerPrefs.Save();
    }

    private void LoadDecisions()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY))
        {
            string jsonData = PlayerPrefs.GetString(SAVE_KEY);
            SerializableDecisions serializableDecisions = JsonUtility.FromJson<SerializableDecisions>(jsonData);
            playerDecisions = serializableDecisions.ToDictionary();
            Debug.Log($"Loaded {playerDecisions.Count} decisions");
        }
    }

    public void ResetAllDecisions()
    {
        playerDecisions.Clear();
        PlayerPrefs.DeleteKey(SAVE_KEY);
        PlayerPrefs.Save();
        Debug.Log("All decisions have been reset");
    }

    [Serializable]
    private class SerializableDecisions
    {
        public List<string> keys = new List<string>();
        public List<bool> values = new List<bool>();

        public SerializableDecisions(Dictionary<string, bool> dictionary)
        {
            foreach (var kvp in dictionary)
            {
                keys.Add(kvp.Key);
                values.Add(kvp.Value);
            }
        }

        public Dictionary<string, bool> ToDictionary()
        {
            Dictionary<string, bool> dictionary = new Dictionary<string, bool>();
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
            return dictionary;
        }
    }
}