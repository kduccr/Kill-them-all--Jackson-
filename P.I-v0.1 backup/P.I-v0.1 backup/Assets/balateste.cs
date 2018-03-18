using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balateste : MonoBehaviour {

   public Transform StarPos;
   public GameObject prefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject novoTiro = (GameObject)Instantiate(prefab, StarPos.transform.position, transform.rotation);	
	}
}
