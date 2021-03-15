using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayStroke : MonoBehaviour
{
	 [SerializeField]
     private LineRenderer trailPrefab = null;
 
     [SerializeField]
     private float distanceFromCamera = 1;
 
     private LineRenderer currentTrail;
     private List<Vector3> points = new List<Vector3>();
 
     private void Update () {
         if (Input.GetMouseButtonDown(0)) {
             DestroyCurrentTrail();
             CreateCurrentTrail();
             AddPoint();
         }
 
         if (Input.GetMouseButton(0)) { SetEnd(); }
         //if (Input.GetMouseButton(0)) { AddPoint(); }
 
         UpdateTrailPoints();

     }
 
     private void DestroyCurrentTrail () {
         if (currentTrail != null) {
             Destroy(currentTrail.gameObject);
             currentTrail = null;
             points.Clear();
         }
     }
 
     private void CreateCurrentTrail () {
         currentTrail = Instantiate(trailPrefab);
         currentTrail.transform.SetParent(transform, true);
     }
 
     private void AddPoint () {
         Vector3 mousePosition = Input.mousePosition;
         points.Add(Camera.main.ViewportToWorldPoint(new Vector3(mousePosition.x / Screen.width, mousePosition.y / Screen.height, distanceFromCamera)));
     }
	 
	 private void SetEnd() {
		 Vector3 mousePosition = Input.mousePosition;
		 var p = Camera.main.ViewportToWorldPoint(new Vector3(mousePosition.x / Screen.width, mousePosition.y / Screen.height, distanceFromCamera));
		 if (points.Count < 2) {
			 points.Add(p);
		 } else {
			points[1] = p;
		 }
	 }
 
     private void UpdateTrailPoints () {
         if (currentTrail != null && points.Count > 1) {
             currentTrail.positionCount = points.Count;
             currentTrail.SetPositions(points.ToArray());
         }
         else { DestroyCurrentTrail(); }
     }
 
 }
