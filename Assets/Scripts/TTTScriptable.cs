using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="ShrineData", menuName = "Scriptalbe/Mode")]
public class TTTScriptable : ScriptableObject
{
    public List<string> modeString;
    public List<string> languageString;
    public Color black;
    public Color white;
}
