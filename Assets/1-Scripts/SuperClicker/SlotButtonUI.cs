using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class SlotButtonUI : MonoBehaviour
{
	#region Properties
	[field: SerializeField] public Reward Reward;
    public int ClicksLeft {
		get {
			return _clicksLeft;
		}
		set {
			_clicksLeft = value;
            //REWARD TIME!!!
            if (_clicksLeft <= 0)
			{

				//Reward Event
				OnSlotReward?.Invoke(Reward);
                _stock--;
				if (_stock > 0)
				{
                    _clicksLeft = _initialClics; 
                }
				else
				{
                    //No more Stock!
                    _audioSource.PlayOneShot(_stockAudio);
					 GetComponent<Image>().enabled = false;
					_clickButton.interactable = false;
					_clicksText.enabled = false;
					Allslot++;
                }
                _clicksLeft = Mathf.RoundToInt(_clicksLeft * 1.15f);//Incrementar un 15% los initial clicks cada vez que se gaste un stock
                Win();
                RefreshClicksText();
			}
		} 
	}

	//Only one event for all Slots
	public static event Action<Reward> OnSlotReward;
	public static event Action<SlotButtonUI> OnSlotClicked;
	#endregion

	#region Fields
	[Header("clicks")]
	[SerializeField] private int _initialClics = 10;
	[Header("UI")]
	[SerializeField] private Button _clickButton;
	[SerializeField] private TextMeshProUGUI _clicksText;
	[SerializeField] private ParticleSystem _particles;
	[SerializeField] private int _materialParticleIndex;
	[Header("Preafb points")]
	[SerializeField] private PointsElementUI _pointsPrefab;
	[Header("Audio")]
	[SerializeField] private AudioClip _hitAudio;
	[SerializeField] private AudioClip _stockAudio;
    private AudioSource _audioSource;


    private GameController _game;
	private int Allslot=0;
	private int _stock = 5;
	private int _clicksLeft = 0;
	#endregion

	#region Unity Callbacks
	private void Awake()
	{
		_game = FindObjectOfType<GameController>();
		_audioSource=GetComponent<AudioSource>();

        Reward.ObjectReward = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		

		_clickButton.onClick.AddListener(Click);
        Initialize();
        RefreshClicksText();
    }

	private void Initialize()
	{
		ClicksLeft = _initialClics;

		//Particle frame
		float segment = 1f / 12f;
		float frame = segment * _materialParticleIndex;
		var tex = _particles.textureSheetAnimation;
		tex.startFrame = frame;
	}

    #endregion

    #region Public Methods
    [Obsolete]
    public void Click(int clickCount, bool agent = false)
	{
        //Para asegurar que el clic no se ejecute si el botón no es interactuable
            
        if (_clickButton.interactable)
		{
            _particles.startSpeed = Mathf.Clamp(clickCount / 2, 1, 5);
            _particles.Emit(Mathf.Clamp(clickCount, 1, 5));
            ClicksLeft -= clickCount;
            RefreshClicksText();
            _audioSource.PlayOneShot(_hitAudio);
            if (!agent)
            {
                PointsElementUI newPoints = _game.Pool.GetPoints();
                newPoints.Initialize(transform);
                Camera.main.DOShakePosition(Mathf.Clamp(0.01f * clickCount, 0, 2));
                _game.RainParticles();
            }
        }
       
    }

	private void RefreshClicksText()
	{
		_clicksText.text = ClicksLeft.ToString();
	}
	private void Win()
	{
		if(Allslot == 12)
            AchievementManager.UnlockAchievement("¡FELICIDADES NOS DEJASTE SIN SNACKS!");
    }
	#endregion

	#region Private Methods
	private void Click()
	{
        //Invoca el evento de clic
        OnSlotClicked?.Invoke(this);
		int clickRatio = Mathf.RoundToInt(_game.ClickRatio);
		Click(clickRatio);
	}
    #endregion
}
