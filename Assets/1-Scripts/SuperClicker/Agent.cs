using UnityEngine;
using System;
using DG.Tweening;
using System.Collections;

public  class Agent : MonoBehaviour
{
	#region Properties
	public SlotButtonUI destiny { get; set; }
    [field: SerializeField] public float RepeatRate {
        get {
            return _repeatRate; 
        }
        set
        {
            // Si el valor es menor que 0.2f, se establece en 0.2f
            _repeatRate = Mathf.Max(value, 0.2f);
        }
    }
              
   public float _repeatRate = 1.5f;
    #endregion

    #region Fields
    protected GameController game;
    protected SlotButtonUI[] allSlotButtons;
    #endregion

    #region Unity Callbacks
    protected virtual void Start() //De este modo las subclases heredan start y con virtual las otras clases puedes anadir y no sobrescriben
   {
        game = GameObject.FindObjectOfType<GameController>();
        allSlotButtons = GameObject.FindObjectsOfType<SlotButtonUI>();
        Movement();
        RepeatRate = 1.5f;
    }
    protected virtual void Click()
    {
    }
    #endregion

    #region Public Methods
    protected virtual  IEnumerator ComboSpeed()
    {
        while (true)
        {
            foreach (var agent in game._activeAgents)
            {
                if (agent != null && agent.RepeatRate> 0.2f && agent.destiny != null ) 
                {
                    agent.RepeatRate -= 1.3f;
                    Debug.Log("Agente " + agent.name + " Repeat rate: antes" + agent.RepeatRate);
                }
            }

            yield return new WaitForSeconds(3f);

            foreach (var agent in game._activeAgents)
            {
                if (agent != null && agent.RepeatRate<0.3f && agent.destiny != null)
                {
                    agent.RepeatRate += 1.3f;
                    Debug.Log("Agente " + agent.name + " Repeat rate: despues" + agent.RepeatRate);
                }
            }

            yield return new WaitForSeconds(15f);
        }
    }

    #endregion

    #region Private Methods
    protected void Movement()
	{
		transform.DOMove(destiny.transform.position, 1);
	}
	#endregion   
}
