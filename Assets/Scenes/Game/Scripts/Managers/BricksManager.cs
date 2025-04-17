using System;
using System.Collections.Generic;
using UnityEngine;


public class BricksManager : Singleton<BricksManager>
{
    // -------------------------------------------------------------------------
    // Public Delegates and Static Events:
    // -----------------------------------
    //   OnBrickCountChanged
    //   OnLevelCompleted
    //   OnLevelLoaded
    // -------------------------------------------------------------------------

    #region .  Public Events  .

    public static event Action<int> OnBrickCountChanged = delegate { };
    public static event Action      OnLevelCompleted    = delegate { };
    public static event Action      OnLevelLoaded       = delegate { };

    #endregion


    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   BrickPrefab
    //   Bricks
    //   Materials
    //   BrickCount
    //   InitialBrickCount
    //   RandomPop
    //
    // These are for the SampleScreen - delete if not using the SampleScreen.
    //   FirstColumn
    //   LastColumn
    //   Step
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public Brick            BrickPrefab;
    public List<GameObject> Bricks;
    public Material[]       Materials;
    public int              BrickCount;
    public int              InitialBrickCount;
    public bool             RandomPop;

    // These are for the SampleScreen.
    public int FirstColumn = -32;
    public int LastColumn  =  32;
    public int Step        =   2;

    #endregion


    // -------------------------------------------------------------------------
    // Private Properties:
    // -------------------
    //   _currentLevel
    //   _listOfChildren
    // -------------------------------------------------------------------------

    #region .  Private Properties  .

    private GameObject  _currentLevel;
    private List<Brick> _listOfChildren;

    #endregion


    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   LoadLevel()
    //   StartSampleScreen()  --  COMMENTED OUT
    // -------------------------------------------------------------------------

    #region .  LoadLevel()  .
    // -------------------------------------------------------------------------
    //   Method.......:  LoadLevel()
    //   Description..:  
    //   Parameters...:  int - the index for level object list to load.
    //   Returns......:  GameObject, the list of bricks objects for the level.
    // -------------------------------------------------------------------------
    public GameObject LoadLevel(int level)
    {
        _listOfChildren = new List<Brick>();

        this._currentLevel     = Instantiate(GameManager.Instance.Levels[level]);
        this._listOfChildren   = this.GetAllChildComponents(this._currentLevel);

        this.InitialBrickCount = _listOfChildren.Count;
        this.BrickCount        = _listOfChildren.Count;

        // Fire the event.
        OnLevelLoaded?.Invoke();

        return this._currentLevel;

    }   // LoadLevel()
    #endregion


    #region .  StartSampleScreen()  --  COMMENTED OUT  .
    //// -------------------------------------------------------------------------
    ////   Method.......:  StartSampleScreen()
    ////   Description..:  
    ////   Parameters...:  None
    ////   Returns......:  Nothing
    //// -------------------------------------------------------------------------
    //public void StartSampleScreen()
    //{
    //    SampleScreen(this.FirstColumn, this.LastColumn, this.Step);
    //
    //}   // StartSampleScreen()
    #endregion


    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   GetAllChildComponents()
    //   GetAllChildRecursive()
    //   OnBrickDestroyed()
    //   OnDisable()
    //   OnEnable()
    //   SampleScreen()  --  COMMENTED OUT
    // -------------------------------------------------------------------------

    #region .  GetAllChildComponents()  .
    // -------------------------------------------------------------------------
    //   Method.......:  GetAllChildComponents()
    //   Description..:  
    //   Parameters...:  GameObject - the parent GameObject for the level.
    //                                The child components are the bricks
    //   Returns......:  List<Brick> - a list of all the child components (the
    //                                 bricks) for the lvel.
    // -------------------------------------------------------------------------
    public List<Brick> GetAllChildComponents(GameObject parentObject)
    {
        GetAllChildRecursive(parentObject);

        return _listOfChildren;

    }// GetAllChildComponents()
    #endregion


    #region .  GetAllChildRecursive()  .
    // -------------------------------------------------------------------------
    //   Method.......:  GetAllChildRecursive()
    //   Description..:  
    //   Parameters...:  GameObject - the parent GameObject for the level.
    //                                The child components are the bricks
    //   Returns......:  Nothing, the GameObject (brick) is added to the private
    //                   property _listOfCildren.
    // -------------------------------------------------------------------------
    private void GetAllChildRecursive(GameObject parentObject)
    {
        if (parentObject == null)
            return;

        foreach (Transform child in parentObject.transform)
        {
            if (child == null)
                continue;

            if (child.name.StartsWith("Brick"))
            {
                this._listOfChildren.Add(child.gameObject.GetComponent<Brick>());
            }

            GetAllChildRecursive(child.gameObject);
        }

    }   // GetAllChildRecursive()
    #endregion


    #region .  OnBrickDestroyed()  .
    // -------------------------------------------------------------------------
    //   Method.......:  OnBrickDestroyed()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private void OnBrickDestroyed(Brick brick)
    {
        // Fire this Event.
        OnBrickCountChanged?.Invoke(this.BrickCount);       

        this.BrickCount--;

        // Is the last brick for the level is destroyed?
        if (this.BrickCount == 0)
        {
            // Fire the event.
            OnLevelCompleted?.Invoke();
        }

    }   // OnBrickDestroyed()
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
        Brick.OnBrickDestroyed -= this.OnBrickDestroyed;

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
        Brick.OnBrickDestroyed += this.OnBrickDestroyed;

    }   // OnEnable()
    #endregion


    #region .  SampleScreen()  --  COMMENTED OUT  .
    //// -------------------------------------------------------------------------
    ////   Method.......:  SampleScreen()
    ////   Description..:  
    ////   Parameters...:  None
    ////   Returns......:  Nothing
    //// -------------------------------------------------------------------------
    //private void SampleScreen(int firstColumn = -32, int lastColumn = 32, int step = 2)
    //{
    //    Brick newBrick;
    //    int[] rows = { 14, 13, 12, 9, 8, 7 };
    //
    //    for (int row = 0; row < rows.Length; row++)
    //    {
    //        for (int column = firstColumn; column <= lastColumn; column += step)
    //        {
    //            newBrick = Instantiate(BrickPrefab, new Vector3(column, rows[row], 0), Quaternion.identity);
    //            newBrick.GetComponent<MeshRenderer>().material = this.Materials[row];
    //            this.Bricks.Add(newBrick);
    //        }
    //    }
    //
    //    this.InitialBrickCount = this.Bricks.Count;
    //
    //    EventManager.RaiseOnLevelLoaded(GameManager.Instance.Level);
    //
    //}   // SampleScreen()
    #endregion


}   // class BricksManager
