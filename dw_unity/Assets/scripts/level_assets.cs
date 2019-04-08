using UnityEngine;

[System.Serializable]
public class TileSet
{
    public string name;
    public GameObject flat;
    public GameObject[] blocked;
    public GameObject side_left;
    public GameObject side_right;
}

public class level_assets : MonoBehaviour
{
    static level_assets Instance = null;

    [SerializeField]
    private TileSet[] tileSet;

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    /// <summary>
    /// This function returns a defined tile type from a tileset 
    /// </summary>
    /// <param name="name">String of the name of the tileset</param>
    /// <param name="type">Either "flat", "blocked", "left" or "right"</param>
    /// <returns></returns>
    public GameObject GetTileFromSet(string name, string type)
    {
        GameObject foundTile = null;

        for (int i = 0; i < tileSet.Length; i++)
        {
            if (tileSet[i].name == name)
            {
                switch (type)
                {
                    case "flat":
                        foundTile = tileSet[i].flat;
                        break;
                    case "blocking":
						if (tileSet[i].blocked.Length > 0)
						{
							foundTile = tileSet[i].blocked[Random.Range(0, tileSet[i].blocked.Length)];
						}
						else
						{
							return null;
						}
                        break;
                    case "left":
                        foundTile = tileSet[i].side_left;
                        break;
                    case "right":
                        foundTile = tileSet[i].side_right;
                        break;
                }
            }
        }

        return foundTile;
    }

    public static level_assets GetInstance()
    {
        return Instance;
    }
}
