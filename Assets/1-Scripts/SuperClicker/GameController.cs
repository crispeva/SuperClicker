using UnityEngine;
using System;
using TMPro;
using DG.Tweening;
using System.Collections;
using static UnityEngine.InputSystem.LowLevel.InputStateHistory;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
	#region Properties
	[field:SerializeField] public float ClickRatio { get; set; }
	[field:SerializeField] public PoolSystem Pool { get; set; }
	[field: SerializeField] public List<Agent> _activeAgents = new List<Agent>();

	#endregion

	#region Fields
	[SerializeField] private Agent[] _agents;
	[SerializeField] private TextMeshProUGUI _rewardText;
	[SerializeField] private TextMeshProUGUI _clicksText;
    [SerializeField] private TextMeshProUGUI _lastClickedScoreText;

    [SerializeField] private ParticleSystem _particlesRain;
	 private AudioSource _audioSource;
	[SerializeField] private AudioClip _audioReward;
	private int _achievementSuma=5;
	private int _achievementMulti=100;
	private bool _activeachievement=false;
	private int []_achievementAgent = {1,5,20};
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
    private void Awake()
    {
        //Initialization
        _audioSource = GetComponent<AudioSource>();
    }
    void Start()
    {
		SlotButtonUI.OnSlotReward += GetReward;
        SlotButtonUI.OnSlotClicked += UpdateLastClickedScore;


    }
	private void OnDestroy()
	{
		SlotButtonUI.OnSlotReward -= GetReward;
        SlotButtonUI.OnSlotClicked -= UpdateLastClickedScore;
    }

	#endregion

	#region Public Methods
	public void RainParticles()
	{
		_particlesRain.Emit(Mathf.Clamp((int)ClickRatio, 0, 13));
	}
    //Setea el texto de los clicks 
    private void UpdateLastClickedScore(SlotButtonUI clickedButton)
    {
        if (_lastClickedScoreText != null)
        {
            _lastClickedScoreText.text = $"Último slot: {clickedButton.ClicksLeft}";
        }
    }
    #endregion

    #region Private Methods
    /***
 *       ____    U _____ u                 _       ____     ____    _    
 *    U |  _"\ u \| ___"|/__        __ U  /"\  uU |  _"\ u |  _"\ U|"|u  
 *     \| |_) |/  |  _|"  \"\      /"/  \/ _ \/  \| |_) |//| | | |\| |/  
 *      |  _ <    | |___  /\ \ /\ / /\  / ___ \   |  _ <  U| |_| |\|_|   
 *      |_| \_\   |_____|U  \ V  V /  U/_/   \_\  |_| \_\  |____/ u(_)   
 *      //   \\_  <<   >>.-,_\ /\ /_,-. \\    >>  //   \\_  |||_   |||_  
 *     (__)  (__)(__) (__)\_)-'  '-(_/ (__)  (__)(__)  (__)(__)_) (__)_) 
 */
    private void GetReward(Reward reward)
	{
		ShowReward(reward);

		//Apply rewards
		if (reward.RewardType == RewardType.Plus)
		{
			ClickRatio += reward.Value;
			_clicksText.text = "x" + ClickRatio;
			if(ClickRatio == _achievementSuma)
            {
                AchievementManager.UnlockAchievement("¡Suma y sigue!");
            }
            return;
		}
		
		if (reward.RewardType == RewardType.Multi)
		{
			ClickRatio *= reward.Value;
			_clicksText.text = "x" + ClickRatio;
			if(ClickRatio >= _achievementMulti && _activeachievement)
            {
				AchievementManager.UnlockAchievement("¡El REY !");
				_activeachievement = false;
            }
            return;
		}

		if (reward.RewardType == RewardType.Agent)
		{
            Agent newAgent = Instantiate(_agents[(int)reward.Value], transform.position, Quaternion.identity);
            newAgent.destiny = reward.ObjectReward;
            //Se añade el nuevo agente a la lista de agentes activos
            _activeAgents.Add(newAgent); 
			if(_activeAgents.Count== _achievementAgent[0])
			{
                AchievementManager.UnlockAchievement("¡Has invocado un agente por primera vez!");
			}
			if (_activeAgents.Count == _achievementAgent[1])
			{
                AchievementManager.UnlockAchievement("¡El amigo de los agentes!");
            }
            if (_activeAgents.Count == _achievementAgent[2])
            {
                AchievementManager.UnlockAchievement("¡Consiguelo a todos!");
            }
            return;
		}
	}

    private void ShowReward(Reward reward)
	{
		//Initialziation
		if (!_rewardText.gameObject.activeSelf)
		{
            _rewardText.gameObject.SetActive(true);
			_rewardText.transform.localScale = Vector3.zero;
		}
        _audioSource.PlayOneShot(_audioReward);
        //Update text
		if(reward.RewardType == RewardType.Plus)
		{
            _rewardText.text = "REWARD\n " + reward.RewardType + " " + reward.Value + " Clicks";
        }
		if(reward.RewardType == RewardType.Agent)
		{
            _rewardText.text = "REWARD\n " + reward.RewardType + " " + reward.Value;
        }
  

		// Crear una secuencia
		Sequence mySequence = DOTween.Sequence();

		// Añadir el primer efecto de escala
		mySequence.Append(_rewardText.transform.DOScale(1, 1));

		// Añadir el efecto de sacudida en la rotación
		mySequence.Append(_rewardText.transform.DOShakeRotation(1, new Vector3(0, 0, 30)));

		// Añadir el segundo efecto de escala
		mySequence.Append(_rewardText.transform.DOScale(0, 1));

		// Iniciar la secuencia
		mySequence.Play();

	}

    #endregion
}
