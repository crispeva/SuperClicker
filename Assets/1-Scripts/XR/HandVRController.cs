
using UnityEngine;

public class HandVRController : MonoBehaviour
{
     #region Properties
    #endregion

    #region Fields
    private LineRenderer _line; //Lina para el rayo
    [SerializeField] private GameObject _teleportmark; //Marca de teletransporte
    [SerializeField] private Transform _player; //La mano del jugador
    [SerializeField] private OVRInput.Button _buttonPressed; //Boton para teletransportarse
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        TeleportRay();
    }

    private void TeleportRay()
    {
       _line.SetPosition(0, transform.position); //Posición inicial del rayo
        RaycastHit hit;
        Debug.DrawRay(transform.position  ,transform.right);
        if (Physics.Raycast(transform.position, transform.right, out hit)) //Lanzamos el rayo
        {
            if (hit.collider.CompareTag("Floor")) //Si el rayo choca con el suelo
            {
                //Actualizamos la posición del rayo y la marca de teletransporte
                _line.SetPosition(1, hit.point);
                _teleportmark.SetActive(true);
                _teleportmark.transform.position = hit.point;
                if (OVRInput.GetUp(_buttonPressed))
                {
                    //Teletransportamos al jugador
                    _player.transform.position = hit.point + Vector3.up;
                }
            }
            else
            {
                //Si no choca con el suelo desactivamos la marca de teletransporte
                _teleportmark.SetActive(false);
                _line.SetPosition(1, transform.position);
            }
        }
       
    }
    #endregion

    #region Public Methods
    #endregion

    #region Private Methods
    #endregion
}
