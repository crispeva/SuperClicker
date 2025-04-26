using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThunderAgent : Agent
{
    // Start is called before the first frame update

    #region Properties
    #endregion

    #region Fields;
    #endregion

    #region Unity Callbacks
    private void Start()
    {
       base.Start();
        InvokeRepeating(nameof(Click), 1, RepeatRate);
    }
    void Update()
    {
        
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    protected override void Click()
    {
        // Itera sobre cada botón y realiza un clic
        foreach (var button in allSlotButtons)
        {
            if (button != null)
            {
                button.Click(1, true);
            }
        }
    }
    // Update is called once per frame
    #endregion
}
