using TMPro;
using UnityEngine;

public class LeaderboardPlayerView : MonoBehaviour
{
    [SerializeField] private TMP_Text _number;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _score;

    public void Init(string number, string name, string score)
    {
        _number.text = number;
        _name.text = name;
        _score.text = score;
    }
}
