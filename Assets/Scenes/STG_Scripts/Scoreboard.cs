using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public TMP_Text scoreText;

    void Update()
    {
        string scoreStr = GameData.score.ToString("D10");

        string spriteText = "";

        foreach (char c in scoreStr)
        {
            spriteText += $"<sprite={c}>";
        }
        scoreText.text = spriteText;
    }
}
