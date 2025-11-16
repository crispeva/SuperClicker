using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MachineController : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [Header("Prefabs para inicializar UI")]
    [SerializeField] private TextMeshProUGUI _rewardTextPrefab;
    [SerializeField] private TextMeshProUGUI _clicksTextPrefab;
    [SerializeField] private TextMeshProUGUI _lastClickedTextPrefab;
    [SerializeField] private ParticleSystem _particlesRain;
    [Header("Prefab para popupParent de AchievementManager")]
    [SerializeField] private Transform _popupParent;

    #endregion

    #region Unity Callbacks
    void Start()
    {
        AddReferencesGameController();
        AddReferencesAchiementManager();
    }

    private void AddReferencesAchiementManager()
    {
        AchievementManager achievementManager = FindObjectOfType<AchievementManager>();
            achievementManager.SetUIReferences(_popupParent);

    }

    public void AddReferencesGameController()
    {

            GameController.Instance.SetUIReferences(_rewardTextPrefab, _clicksTextPrefab,_lastClickedTextPrefab, _particlesRain);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
