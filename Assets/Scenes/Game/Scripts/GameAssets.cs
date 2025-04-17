using UnityEngine;


public class GameAssets : Singleton<GameAssets>
{
    // --------------------------------------------------------------
    // Public Properties:
    // ------------------
    //  SoundAudioClips
    // --------------------------------------------------------------

    #region .  Public Properties  .

    public SoundAudioClip[] SoundAudioClips;

    #endregion


    // --------------------------------------------------------------
    // Public Classes:
    // ---------------
    //  SoundAudioClip
    // --------------------------------------------------------------

    #region .  Public Classes  .

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sounds sound;
        public AudioClip           audioClip;
        public string              name;

    }   // class SoundAudioClip

    #endregion


}   // class GameAssewts