using UnityEngine;


public class SoundManager : Singleton<SoundManager>
{
    // -------------------------------------------------------------------------
    // Public Enumerations:
    // --------------------
    //   Sounds
    // -------------------------------------------------------------------------

    #region .  Public Enumerations  .

    public enum Sounds
    {
        Ball_Hit_Paddle      = 0,
        Ball_Hit_Wall_Bottom = 1,
        Ball_Hit_Wall_Left   = 2,
        Ball_Hit_Wall_Right  = 3,
        Ball_Hit_Wall_Top    = 4,
        Brick_Crack_1        = 5,
        Brick_Crack_2        = 6,
        Brick_Destroyed_1    = 7,
        Brick_Destroyed_2    = 8,
        Explosion_1          = 9,
        Game_Over_1          = 10,
        Game_Over_2          = 11,
        Game_Over_3          = 12,
        Level_Completed_1    = 13,
        New_High_Score       = 14,
        Pop_1                = 15,
        Pop_2                = 16,
        Pop_3                = 17,
        Pop_4                = 18,
        Pop_5                = 19,
        Pop_6                = 20,
        Pop_7                = 21,
        Pop_8                = 22,
        Victory              = 23
    }

    #endregion


    // -------------------------------------------------------------------------
    // Public Properties:
    // ------------------
    //   Audio
    //   SoundAudioClips
    // -------------------------------------------------------------------------

    #region .  Public Properties  .

    public AudioSource      Audio;
    public SoundAudioClip[] SoundAudioClips;

    #endregion


    // -------------------------------------------------------------------------
    // Public Methods:
    // ---------------
    //   PlaySound() - sound
    //   PlaySound() - name
    // -------------------------------------------------------------------------

    #region .  PlaySound() - sound  .
    // -------------------------------------------------------------------------
    //   Method.......:  PlaySound()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void PlaySound(Sounds sound)
    {
        try
        {
            Audio.PlayOneShot(this.GetAudioClip(sound));
        }
        catch
        {
            Debug.Log($"SoundManager.PlaySound(sound) called with a null sound.");
        }

    }   // PlaySound()
    #endregion


    #region .  PlaySound() - name  .
    // -------------------------------------------------------------------------
    //   Method.......:  PlaySound()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    public void PlaySound(string name)
    {
        try
        {
            Audio.PlayOneShot(this.GetAudioClip(name));
        }
        catch
        {
            Debug.Log($"SoundManager.PlaySound(name) called with a null name.");
        }
    
    }   // PlaySound()
    #endregion


    // -------------------------------------------------------------------------
    // Private Methods:
    // ----------------
    //   GetAudioClip() - sound
    //   GetAudioClip() - name
    // -------------------------------------------------------------------------

    #region .  GetAudioClip() - sound  .
    // -------------------------------------------------------------------------
    //   Method.......:  GetAudioClip()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  Nothing
    // -------------------------------------------------------------------------
    private AudioClip GetAudioClip(Sounds sound)
    {
        foreach (SoundAudioClip soundAudioClip in SoundAudioClips)
        {
            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.Log($"SoundManager.GetAudioClip({sound}) not found or is null.");
        return null;

    }   // GetAudioClip()
    #endregion


    #region .  GetAudioClip() - name  .
    // -------------------------------------------------------------------------
    //   Method.......:  GetAudioClip()
    //   Description..:  
    //   Parameters...:  None
    //   Returns......:  AudioClip
    // -------------------------------------------------------------------------
    private AudioClip GetAudioClip(string name)
    {
        foreach (SoundAudioClip soundAudioClip in SoundAudioClips)
        {
            if (soundAudioClip.sound.ToString() == name)
            {
                return soundAudioClip.audioClip;
            }
        }

        Debug.Log($"SoundManager.GetAudioClip({name}) not found or is null.");
        return null;

    }   // GetAudioClip()
    #endregion


    // --------------------------------------------------------------
    // Public Classes:
    // ---------------
    //   SoundAudioClip
    // --------------------------------------------------------------

    #region .  Public Classes  .

    [System.Serializable]
    public class SoundAudioClip
    {
        public SoundManager.Sounds sound;
        public AudioClip           audioClip;

    }   // class SoundAudioClip

    #endregion


}   // class SoundManager
