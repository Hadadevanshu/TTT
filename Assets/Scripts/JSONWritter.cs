using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class JSONWriter : MonoBehaviour
{
    List<string> n = new List<string>();
    List<string> data = new List<string>();
    void Start()
    {

        GetComponent<GameController>().jsonDataParseEvent += OnLastChance;

        // Save the JSON to a file
    }

    private void OnDestroy()
    {
        GetComponent<GameController>().jsonDataParseEvent -= OnLastChance;
        
    }

    void SaveJsonToFile(string jsonData, string fileName)
    {
        string path = Application.persistentDataPath + "/" + fileName;
        System.IO.File.WriteAllText(path, jsonData);
        Debug.Log("JSON saved to " + path);
    }

    public void OnLastChance(List<string> list)
    {
        Dictionary<string, string> moves = new Dictionary<string, string>();
        for (int i = 0; i < list.Count; i++)
        {
            string temp = "Move" + i.ToString();
            n.Add(temp);
        }
        for (int i = 0; i < list.Count; i++)
        {
            moves.Add(n[i], list[i]);
        }
        string jsonData = JsonConvert.SerializeObject(moves, Formatting.Indented);

        SaveJsonToFile(jsonData, "TicTacToeMoves.json");
        moves.Clear();
        n.Clear();

    }
}