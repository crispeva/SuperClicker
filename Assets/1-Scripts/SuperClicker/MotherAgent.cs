using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotherAgent : Agent 
{
    // Start is called before the first frame update
    #region Properties
    #endregion

    #region Fields;
    #endregion

    #region Unity Callbacks
      void Start()
    {
        base.Start(); // Llama al Start de la clase base si es necesario
        InvokeRepeating(nameof(Click), 1, 15f); // Llama a PerformAction cada 5 segundos
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
                button.Click((int)game.ClickRatio, true);
            }// Realiza un clic con el agente
        }
    }
    #endregion
}
