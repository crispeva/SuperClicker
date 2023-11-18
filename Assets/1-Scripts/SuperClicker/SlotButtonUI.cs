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
					GetComponent<Image>().enabled = false;
					_clickButton.interactable = false;
					_clicksText.text = "";
				}
				RefreshClicksText();
			}
		} 
	}

	//Only one event for all Slots
	public static event Action<Reward> OnSlotReward;
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
	
	private GameController _game;
	private int _stock = 5;
	private int _clicksLeft = 0;

	#endregion

	#region Unity Callbacks
	private void Awake()
	{
		_game = FindObjectOfType<GameController>();
	}

	// Start is called before the first frame update
	void Start()
    {
		Initialize();

		_clickButton.onClick.AddListener(Click);

		//_clickButton.onClick.AddListener(() =>
		//{
		//	int clickRatio = Mathf.RoundToInt(_game.ClickRatio);
		//	Click(clickRatio);
		//});

		RefreshClicksText();
    }

	private void Initialize()
	{
		ClicksLeft = _initialClics;

		//Particle frame
		float segment = 1f / 28f;
		float frame = segment * _materialParticleIndex;
		var tex = _particles.textureSheetAnimation;
		tex.startFrame = frame;
	}

	#endregion

	#region Public Methods
	public void Click(int clickCount)
	{
		_particles.startSpeed = Mathf.Clamp(clickCount / 2, 1, 30);
		_particles.Emit(Mathf.Clamp(clickCount,1, 15));
		ClicksLeft -= clickCount;
		RefreshClicksText();
		Instantiate(_pointsPrefab, transform);
		Camera.main.DOShakePosition(Mathf.Clamp(0.01f * clickCount, 0, 2));
		_game.RainParticles();
	}

	private void RefreshClicksText()
	{
		_clicksText.text = ClicksLeft.ToString();
	}

	#endregion

	#region Private Methods
	private void Click()
	{
		int clickRatio = Mathf.RoundToInt(_game.ClickRatio);
		Click(clickRatio);
	}
	#endregion
}
