using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField]private GameObject achievementPopupPrefab; // Prefab del popup
    [SerializeField] private Transform popupParent; // Donde instanciarlo 
    #endregion

    #region Unity Callbacks
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    public void ShowAchievement(string achievementText)
    {
        
        GameObject popup = Instantiate(achievementPopupPrefab, popupParent);
        AchiviementPopupUI popupUI = popup.GetComponent<AchiviementPopupUI>();
        AnimatePopup(popup);
        if (popupUI.AchievementText.text != null)
        {
            popupUI.AchievementText.text = achievementText;

        }

        Destroy(popup, 5f); // Se destruye solo a los 5 segundos
    }
    public static event Action<string> OnAchievementUnlocked;

    public static void UnlockAchievement(string text)
    {
        OnAchievementUnlocked?.Invoke(text);
    }



    #endregion

    #region Private Methods
    private void AnimatePopup(GameObject popup)
    {

        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(popup.transform.DOMoveY(popup.transform.position.y + 80f, 1f)); // mueve 100 unidades hacia arriba en 1 segundo
        mySequence.Join(popup.transform.DOScale(1f, 1f)); // escala a 1.5 en 1 segundo
        mySequence.Play();
    }
    private void OnEnable()
    {
        OnAchievementUnlocked += ShowAchievement;
    }

    private void OnDisable()
    {
        OnAchievementUnlocked -= ShowAchievement;
    }
    #endregion
}
