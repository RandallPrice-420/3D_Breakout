using TMPro;


public class UIManager : Singleton<UIManager>
{
    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   TextBallsValue
    //   TextBricksValue
    //   TextLevelValue
    //   TextScoreValue
    //   TextSpeedValue
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public TMP_Text TextBallsValue;
    public TMP_Text TextBricksValue;
    public TMP_Text TextLevelValue;
    public TMP_Text TextScoreValue;
    public TMP_Text TextSpeedValue;

    #endregion


    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   BrickDestroyed()
    //   LevelOoaded()
    //   OnDisable()
    //   OnEnable()
    //   UpdateBallCountValue()
    //   UpdateBrickCountValue()
    //   UpdateLevelCountValue()
    //   UpdateScoreValue()
    //   UpdateSpeedValue()
    // -------------------------------------------------------------------------

    #region .  OnBrickDestroyed()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnBrickDestroyed()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnBrickDestroyed(Brick brick)
    {
        this.UpdateBrickCountValue();
        this.UpdateScoreValue();

    }   // OnBrickDestroyed()
    #endregion


    #region .  OnLevelLoaded()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnLevelLoaded()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnLevelLoaded()
    {
        this.UpdateBrickCountValue();
        this.UpdateLevelCountValue();

    }   // OnLevelLoaded()
    #endregion


    #region .  OnDisable()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnDisable()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnDisable()
    {
        Brick        .OnBrickDestroyed -= this.OnBrickDestroyed;
        BricksManager.OnLevelLoaded    += this.OnLevelLoaded;

        EventManager.OnBallCountChanged -= this.UpdateBallCountValue;
        EventManager.OnScoreChanged     -= this.UpdateScoreValue;
        EventManager.OnSpeedChanged     -= this.UpdateSpeedValue;
        //EventManager.OnLevelCountChanged -= this.UpdateLevelCountValue;

    }   // OnDisable()
    #endregion


    #region .  OnEnable()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnEnable()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnEnable()
    {
        Brick        .OnBrickDestroyed += this.OnBrickDestroyed;
        BricksManager.OnLevelLoaded    += this.OnLevelLoaded;

        EventManager.OnBallCountChanged += this.UpdateBallCountValue;
        EventManager.OnScoreChanged     += this.UpdateScoreValue;
        EventManager.OnSpeedChanged     += this.UpdateSpeedValue;
        //EventManager.OnLevelCountChanged += this.UpdateLevelCountValue;

    }   // OnEnable()
    #endregion


    #region .  UpdateBallCountValue()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateBallCountValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void UpdateBallCountValue()
    {
        this.TextBallsValue.text = GameManager.Instance.BallCount.ToString();

    }   // UpdateBallCountValue()
    #endregion


    #region .  UpdateBrickCountValue()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateBrickCountValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void UpdateBrickCountValue()
    {
        this.TextBricksValue.text = $"{BricksManager.Instance.BrickCount} / {BricksManager.Instance.InitialBrickCount}";

    }   // UpdateBrickCountValue()
    #endregion


    #region .  UpdateLevelCountValue()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateLevelCountValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void UpdateLevelCountValue()
    {
        this.TextLevelValue.text = GameManager.Instance.Level.ToString();

    }   // UpdateLevelCountValue()
    #endregion


    #region .  UpdateScoreValue()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateScoreValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void UpdateScoreValue()
    {
        this.TextScoreValue.text = GameManager.Instance.Score.ToString();

    }   // UpdateScoreValue()
    #endregion


    #region .  UpdateSpeedValue()  .
    // -------------------------------------------------------------------------
    //   Method.......:  UpdateSpeedValue()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void UpdateSpeedValue()
    {
        this.TextSpeedValue.text = GameManager.Instance.Speed.ToString();

    }   // UpdateSpeedValue()
    #endregion


}   // class UIManager
