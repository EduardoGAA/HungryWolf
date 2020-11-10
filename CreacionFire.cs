using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreacionFire : MonoBehaviour {

	[SerializeField]
	public GameObject objectToCreate;
	[SerializeField]
	public GameObject position;

	public GameObject createdObject;
	//public ControlSoldado ctrs;
	// Use this for initialization


	void Start () {
		//ctrs = FindObjectOfType<ControlSoldado> ();
		//createObject ();
	}

	public void createObject(){
		    
			createdObject = Instantiate (objectToCreate, position.transform);
			//Invoke ("destroyObject", 7f);

	}

	public void destroyObject(){
		//Destroy (createdObject);

		Invoke ("createObject", 4f);
	}
	// Update is called once per frame
	void Update () {
		//if (ctrs.energy <= 0)
		//	createObject ();

	}
}
