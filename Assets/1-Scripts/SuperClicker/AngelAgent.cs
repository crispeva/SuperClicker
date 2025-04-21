using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelAgent : Agent
{
    // Start is called before the first frame update
    protected override void Click()
    {
        destiny.Click(1, true);
        if (destiny.ClicksLeft < 0)
            Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
