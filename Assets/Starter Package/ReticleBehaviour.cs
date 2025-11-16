/*
 * Copyright 2021 Google LLC
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ReticleBehaviour : MonoBehaviour
{
    public GameObject Child;
    public DrivingSurfaceManager DrivingSurfaceManager;

    public ARPlane CurrentPlane;

    private Vector3 _screenCenter;
    [SerializeField] private GameObject _machineprefab;

    // Start is called before the first frame update
    private void Start()
    {
        Child = transform.GetChild(0).gameObject;

        // Inicializar _screenCenter aquí (no en el inicializador de campo)
        if (Camera.main != null)
        {
            _screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        }
        else
        {
            // Si no hay Camera.main (ej. escena mal configurada), usar centro de pantalla por defecto
            _screenCenter = new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0f);
        }
    }

    private void Update()
    {
       List<ARRaycastHit> hits = new List<ARRaycastHit>();
        DrivingSurfaceManager.RaycastManager.Raycast(_screenCenter,hits,TrackableType.PlaneWithinBounds);
       CurrentPlane = null;
        ARRaycastHit? hit;
        if (hits.Count > 0)
        {
            CurrentPlane = DrivingSurfaceManager.LockedPlane;
            if (CurrentPlane == null)
            {
                hit = hits[0];

            }
            else
            {
                hit = hits.SingleOrDefault(x => x.trackableId == CurrentPlane.trackableId);

            }

            if (hit.HasValue)
            {
                CurrentPlane = DrivingSurfaceManager.PlaneManager.GetPlane(hit.Value.trackableId);
                transform.position=hit.Value.pose.position;
            }
            if(CurrentPlane != null)
            {
                Child.SetActive(true);
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                 GameObject machine = Instantiate(_machineprefab,transform.position + Vector3.up*0.7f, _machineprefab.transform.rotation);
                    machine.transform.forward = Camera.main != null ? Camera.main.transform.forward : Vector3.forward;
                    machine.transform.rotation = Quaternion.Euler(0, machine.transform.rotation.eulerAngles.y, 0);
                    Destroy(gameObject);
                }
            }
            else
            {
                Child.SetActive(false);
            }
        }
    }
}
