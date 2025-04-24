using UnityEngine;
using System;
using DG.Tweening;
using System.Collections;

public  class Agent : MonoBehaviour
{
	#region Properties
	public SlotButtonUI destiny { get; set; }
    [field: SerializeField] public float RepeatRate { get; set; }
    #endregion

    #region Fields
    protected GameController game;
    #endregion

    #region Unity Callbacks
    // Start is called before the first frame update
     void Awake()
    {
        
    }
    protected virtual void Start() //De este modo las subclases heredan start y con virtual las otras clases puedes anadir
   {
        game = GameObject.FindObjectOfType<GameController>();
        Movement();
        RepeatRate = 1.5f;
        UpdateRepeatRate();
    }
    protected virtual void Click()
    {
        
    }
    // Update is called once per frame
    void Update()
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
                if (agent != null && agent.RepeatRate> 0.2f) 
                {
                    agent.RepeatRate -= 1.3f;
                    Debug.Log("Agente " + agent.name + " Repeat rate: antes" + agent.RepeatRate);

                    // Si el agente necesita actualizar su lógica basada en RepeatRate
                    agent.UpdateRepeatRate();
                }
            }

            yield return new WaitForSeconds(3f);

            foreach (var agent in game._activeAgents)
            {
                if (agent != null && agent.RepeatRate<0.3f)
                {
                    agent.RepeatRate += 1.3f;
                    Debug.Log("Agente " + agent.name + " Repeat rate: despues" + agent.RepeatRate);
                    agent.UpdateRepeatRate();
                }
            }

            yield return new WaitForSeconds(15f);
        }
    }

    private void UpdateRepeatRate()
    {
        // Actualiza el tiempo de invocación del clic y cancela el anterior
        if (destiny == null || !destiny.gameObject.activeInHierarchy)
        {
            CancelInvoke(nameof(Click));
            return;
        }
        CancelInvoke(nameof(Click));
        InvokeRepeating(nameof(Click), 1f, RepeatRate);
    }
    #endregion

    #region Private Methods
    protected void Movement()
	{
		transform.DOMove(destiny.transform.position, 1);
	}
	#endregion   
}
