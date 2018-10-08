using UnityEngine;
using System.Collections.Generic;
public class GameManager : MonoBehaviour
{
    #region Player Registry
    private static Dictionary<string, Player> playerDict = new Dictionary<string, Player>();
    private const string PLAYER_ID_PREFIX = "Player ";

    public static void RegisterPlayer(string _netID, Player _player)
    {
        string _playerID = PLAYER_ID_PREFIX + _netID;
        playerDict.Add(_playerID, _player);
        _player.transform.name = _playerID;

    }

    public static void UnregisterPlayer(string _playerId)
    {
        playerDict.Remove(_playerId);

    }

    public static Player GetPlayer (string _playerId)
    {
        return playerDict[_playerId];
    }

    //UI show player in GUI.
    //void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();
    //    foreach (string _playerId in playerDict.Keys)
    //    {
    //        GUILayout.Label(_playerId + " - " + playerDict[_playerId].transform.name);
    //    }
    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}
    #endregion

    public static GameManager instance;
    public MatchSettings matchSettings;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one GameManager in Scene");
        }
        else
        {
            instance = this;
        }
    }
}




