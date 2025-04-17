using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


public class XMLManager : Singleton<XMLManager>
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   Leaderboard
    //   ScoresFilePath
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public LeaderBoard Leaderboard;
    public string      ScoresFilePath = "/HighScores/Highscores.xml";

    #endregion


    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   LoadScores()
    //   SaveScores()
    // -------------------------------------------------------------------------

    #region .  LoadScores()  .
    // -------------------------------------------------------------------------
    //   Method.......:  LoadScores()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  List<HighScoreEntry>
    // -------------------------------------------------------------------------
    public List<HighScoreEntry> LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + ScoresFilePath))
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeaderBoard));
            FileStream    fileStream    = new FileStream(Application.persistentDataPath + ScoresFilePath, FileMode.Open);

            Leaderboard = xmlSerializer.Deserialize(fileStream) as LeaderBoard;
        }

        return Leaderboard.PlayerList;

    }   // LoadScores()
    #endregion


    #region .  SaveScores()  .
    // -------------------------------------------------------------------------
    //   Method.......:  SaveScores()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void SaveScores(List<HighScoreEntry> scoresToSave)
    {
        Leaderboard.PlayerList = scoresToSave;

        XmlSerializer xmlSerializer = new XmlSerializer(typeof(LeaderBoard));
        FileStream    fileStream    = new FileStream(Application.persistentDataPath + ScoresFilePath, FileMode.Create);

        xmlSerializer.Serialize(fileStream, Leaderboard);
        fileStream.Close();

    }   // SaveScores()
    #endregion


    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   Awake()
    // -------------------------------------------------------------------------

    #region .  Awake()  .
    // -------------------------------------------------------------------------
    //   Method.......:  Awake()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  List<HighScoreEntry>
    // -------------------------------------------------------------------------
    void Awake()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/HighScores/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/HighScores/");
        }

    }   // Awake()
    #endregion  


}   // class XMLManager


// --------------------------------------------------------------
// Public Classes:
// ---------------
//   HighScoreEntry
//   LeaderBoard
// --------------------------------------------------------------

#region .  Public Classes  .

public class HighScoreEntry
{
    public string Name;
    public int    Score;
}


[System.Serializable]
public class LeaderBoard
{
    public List<HighScoreEntry> PlayerList = new List<HighScoreEntry>();
}

#endregion
