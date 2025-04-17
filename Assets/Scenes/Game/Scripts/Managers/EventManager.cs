using System;
using UnityEngine;


public class EventManager : MonoBehaviour
{
    // -------------------------------------------------------------------------
    // Public Delegates and Events:
    // ----------------------------
    //   OnBallCollisionEnter
    //   OnBallCountChanged
    //   OnHighScoreChanged
    //   OnLevelCountChanged
    //   OnLevelLoaded
    //   OnScoreChanged
    //   OnSpeedChanged
    //   OnToggleBallTrailChanged
    //   OnTogglePaddleChanged
    // -------------------------------------------------------------------------

    #region .  Public Delegates and Events  .

    public delegate void  onBallCollisionEnter();
    public static   event Action<SoundManager.Sounds> OnBallCollisionEnter;

    public delegate void  onBallCountChanged();
    public static   event onBallCountChanged          OnBallCountChanged;

    public delegate void  onHighScoreChanged();
    public static   event onHighScoreChanged          OnHighScoreChanged;

    public delegate void  onLevelCountChanged();
    public static   event onLevelCountChanged         OnLevelCountChanged;

    public delegate void  onLevelLoaded();
    public static   event Action<int>                 OnLevelLoaded;

    public delegate void  onScoreChanged();
    public static   event onScoreChanged              OnScoreChanged;

    public delegate void  onSpeedChanged();
    public static   event onSpeedChanged              OnSpeedChanged;

    public delegate void  onToggleBallTrailChanged();
    public static   event onToggleBallTrailChanged    OnToggleBallTrailChanged;

    public delegate void  onTogglePaddleChanged();
    public static   event onTogglePaddleChanged       OnTogglePaddleChanged;

    #endregion


    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   RaiseOnBallCollisionEnter()
    //   RaiseOnBallCountChanged()
    //   RaiseOnHighScoreChanged()
    //   RaiseOnLevelCountChanged()
    //   RaiseOnLevelLoaded()
    //   RaiseOnScoreChanged()
    //   RaiseOnSpeedChanged()
    //   RaiseOnToggleBallTrailChanged()
    //   RaiseOnTogglePaddleChanged()
    // -------------------------------------------------------------------------

    #region .  RaiseOnBallCollisionEnter()  .
    // -------------------------------------------------------------------------
    //   Method.......:  RaiseOnBallCollisionEnter()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static void RaiseOnBallCollisionEnter(SoundManager.Sounds sound)
    {
        // Invoke the Event.
        OnBallCollisionEnter?.Invoke(sound);

    }   // RaiseOnBallCollisionEnter()
    #endregion


    #region .  RaiseOnBallCountChanged()  .
    // -------------------------------------------------------------------------
    //   Method.......:  RaiseOnBallCountChanged()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static void RaiseOnBallCountChanged()
    {
        // Invoke the Event.
        OnBallCountChanged?.Invoke();

    }   // RaiseOnBallCountChanged()
    #endregion


    #region .  RaiseOnHighScoreChanged()  .
    // -------------------------------------------------------------------------
    //   Method.......:  RaiseOnHighScoreChanged()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static void RaiseOnHighScoreChanged()
    {
        // Invoke the Event.
        OnHighScoreChanged?.Invoke();

    }   // RaiseOnHighScoreChanged()
    #endregion


    #region .  RaiseOnLevelCountChanged()  .
    // -------------------------------------------------------------------------
    //   Method.......:  RaiseOnLevelCountChanged()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static void RaiseOnLevelCountChanged()
    {
        // Invoke the Event.
        OnLevelCountChanged?.Invoke();

    }   // RaiseOnLevelCountChanged()
    #endregion


    #region .  RaiseOnLevelLoaded()  .
    // -------------------------------------------------------------------------
    //   Method.......:  RaiseOnLevelLoaded()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static void RaiseOnLevelLoaded(int level)
    {
        // Invoke the Event.
        OnLevelLoaded?.Invoke(level);

    }   // RaiseOnLevelLoaded()
    #endregion


    #region .  RaiseOnScoreChanged()  .
    // -------------------------------------------------------------------------
    //   Method.......:  RaiseOnScoreChanged()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static void RaiseOnScoreChanged()
    {
        // Invoke the Event.
        OnScoreChanged?.Invoke();

    }   // RaiseOnScoreChanged()
    #endregion


    #region .  RaiseOnSpeedChanged()  .
    // -------------------------------------------------------------------------
    //   Method.......:  RaiseOnSpeedChanged()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static void RaiseOnSpeedChanged()
    {
        // Invoke the Event.
        OnSpeedChanged?.Invoke();

    }   // RaiseOnSpeedChanged()
    #endregion


    #region .  RaiseOnToggleBallTrailChanged()  .
    // -------------------------------------------------------------------------
    //   Method.......:  RaiseOnToggleBallTrailChanged()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static void RaiseOnToggleBallTrailChanged()
    {
        // Invoke the Event.
        OnToggleBallTrailChanged?.Invoke();

    }   // RaiseOnToggleBallTrailChanged()
    #endregion


    #region .  RaiseOnTogglePaddleChanged()  .
    // -------------------------------------------------------------------------
    //   Method.......:  RaiseOnTogglePaddleChanged()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public static void RaiseOnTogglePaddleChanged()
    {
        // Invoke the Event.
        OnTogglePaddleChanged?.Invoke();

    }   // RaiseOnTogglePaddleChanged()
    #endregion


}   // class EventManager