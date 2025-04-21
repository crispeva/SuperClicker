using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThunderAgent : Agent
{
    // Start is called before the first frame update
    SlotButtonUI[] allButtons;
    protected override void Click()
    {
       

        // Itera sobre cada botón y realiza un clic
        foreach (var button in allButtons)
        {
            button.Click(1, true); // Realiza un clic con el agente
        }

        // Opcional: Destruir el agente si es necesario
        //Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        allButtons = FindObjectsOfType<SlotButtonUI>();
    }
}
