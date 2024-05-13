using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private static PlayerManager instance;

    public static PlayerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = "PlayerManager";
                    instance = obj.AddComponent<PlayerManager>();
                }
            }
            return instance;
        }
    }

    private Vector3 playerPosition;

    public void UpdatePlayerPosition(Vector3 newPosition)
    {
        playerPosition = newPosition;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }
}
