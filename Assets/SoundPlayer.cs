using CarInput;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Unity.VisualScripting.Member;

public class SoundPlayer : MonoBehaviour
{
    public static SoundPlayer Instance; private void Awake() { if (Instance != null) Destroy(this.gameObject); else DontDestroyOnLoad(this.gameObject); Instance = this; }

    public AudioSource[] Sources;

    [SerializeField] private MMF_Player mmfPlayer;

    /// <summary>
    /// Plays sound: AudioClip, Speaker number
    /// </summary>
    /// <param name="sfx"></param>
    /// <param name="source"></param>
    public void PlaySound(AudioClip sfx, int source)
    {
        MMSoundManagerPlayOptions options;

        options = MMSoundManagerPlayOptions.Default;

        options.SpatialBlend = 1;
        options.Location = Sources[source].transform.position;

        MMSoundManagerSoundPlayEvent.Trigger(sfx, options);
    }

    //UI Sound management
    //[Header("Player Inputs")]
    //[SerializeField] private GameObject Player1_GO;//Player inputs
    //private PlayerInput player1;
    //[SerializeField] private GameObject Player2_GO;
    //private PlayerInput player2;
    //[SerializeField] private GameObject Player3_GO;
    //private PlayerInput player3;
    //[SerializeField] private GameObject Player4_GO;
    //private PlayerInput player4;

    [Header("Keyboard Screen Inputs")]
    [SerializeField] private KeyboardInput player1KeyInput;//OnUpdateText + OnEnterKeyboard
    [SerializeField] private KeyboardInput player2KeyInput;
    [SerializeField] private KeyboardInput player3KeyInput;
    [SerializeField] private KeyboardInput player4KeyInput;

    [Header("System Sound Files")]
    [SerializeField] AudioClip RC_PAUSE;
    [SerializeField] AudioClip RC_UNPAUSE;
    [SerializeField] AudioClip SYS_NAME_SELECT;
    [SerializeField] AudioClip SYS_NAME_PICK;
    [SerializeField] AudioClip SYS_NAME_CONFIRM;
    [SerializeField] AudioClip SYS_UI_SELECT;
    [SerializeField] AudioClip SYS_UI_CONFIRM;
    //to anyone reading this, it was 3:31 am honestly at this point fuck naming conventions
    private void Update()
    {
        player1KeyInput.OnUpdateText?.AddListener(BitchSound1);//Confirm Character
        player2KeyInput.OnUpdateText?.AddListener(BitchSound2);
        player3KeyInput.OnUpdateText?.AddListener(BitchSound3);
        player4KeyInput.OnUpdateText?.AddListener(BitchSound4);

        player1KeyInput.OnEnterKeyboard?.AddListener(BitchSound1);//Confirm Name
        player2KeyInput.OnEnterKeyboard?.AddListener(BitchSound2);
        player3KeyInput.OnEnterKeyboard?.AddListener(BitchSound3);
        player4KeyInput.OnEnterKeyboard?.AddListener(BitchSound4);

        player1KeyInput.OnApplyMovement?.AddListener(bitchSound1);//Move to different character
        player2KeyInput.OnApplyMovement?.AddListener(bitchSound2);
        player3KeyInput.OnApplyMovement?.AddListener(bitchSound3);
        player4KeyInput.OnApplyMovement?.AddListener(bitchSound4);
    }
    void bitchSound1()
    {
        PlaySound(SYS_NAME_SELECT, 1);
    }
    void bitchSound2()
    {
        PlaySound(SYS_NAME_SELECT, 2);
    }
    void bitchSound3()
    {
        PlaySound(SYS_NAME_SELECT, 3);
    }
    void bitchSound4()
    {
        PlaySound(SYS_NAME_SELECT, 4);
    }
    void BitchSound1(string e)
    {
        PlaySound(SYS_NAME_PICK, 1);
    }
    void BitchSound1()
    {
        PlaySound(SYS_NAME_CONFIRM, 1);
    }
    void BitchSound2(string e)
    {
        PlaySound(SYS_NAME_PICK, 2);
    }
    void BitchSound2()
    {
        PlaySound(SYS_NAME_CONFIRM, 2);
    }
    void BitchSound3(string e)
    {
        PlaySound(SYS_NAME_PICK, 3);
    }
    void BitchSound3()
    {
        PlaySound(SYS_NAME_CONFIRM, 3);
    }
    void BitchSound4(string e)
    {
        PlaySound(SYS_NAME_PICK, 4);
    }
    void BitchSound4()
    {
        PlaySound(SYS_NAME_CONFIRM, 4);
    }
    private void Start()
    {
        mmfPlayer.PlayFeedbacks();
    }
}
