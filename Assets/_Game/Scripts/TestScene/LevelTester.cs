using Sirenix.OdinInspector;
using UnityEngine;

public class LevelTester : MonoBehaviour
{
    public Board board;

    void Start()
    {
        if (board == null)
        {
            Debug.LogWarning("Board is not setted");
        }
        board.OnInit();
    }
}
