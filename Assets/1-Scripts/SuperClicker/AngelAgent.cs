using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAgent : Agent
{
    // Start is called before the first frame update

    // Update is called once per frame
    #region Properties
    #endregion

    #region Fields;

    #endregion

    #region Unity Callbacks
    private void Start()
    {
       base.Start();
        InvokeRepeating(nameof(Click), 1, RepeatRate);
        SlotButtonUI.OnSlotClicked += SetDestiny;
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    protected override void Click()
    {
        
            destiny.Click(1, true);
    }
    private void SetDestiny(SlotButtonUI newDestiny)
    {
        destiny = newDestiny;
        Movement();
    }
    #endregion
}
