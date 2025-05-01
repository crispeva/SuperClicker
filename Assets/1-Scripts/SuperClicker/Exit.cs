using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Exit : MonoBehaviour
{
    #region Properties
    #endregion

    #region Fields
    [SerializeField] private Button _exitButton;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _exitButton.onClick.AddListener(OnclickExit);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    public void OnclickExit()
    {
        this.gameObject.SetActive(false);
    }
    #endregion

    #region Private Methods

    #endregion
}
