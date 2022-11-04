using UnityEngine;

public class Party : MonoBehaviour
{
    [SerializeField] public Player[] characters = new Player[4];
    public void UpdateData()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].UploadData();
        }
    }

    public void LoadData()
    {
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].LoadData();
        }
    }
}