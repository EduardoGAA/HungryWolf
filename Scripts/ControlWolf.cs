using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControlWolf : MonoBehaviour {

//	Collider2D colliderEnem = null;
	Rigidbody2D rgb; 
	Animator anim;
	public float maxVel = 5f;
	bool haciaDerecha = true;

	public Slider slider;
	public Text txt;

	public int energy = 100;
	//ControlSoldado ctrs = null;
	//public GameObject soldado = null;


	public int costoMordidaAlAire = 1;
	public int costoMordidaGallina = 3;
	public int premioGallina = 15;
	public int costoBala = 20;
	public int costoDaño = 10;
	bool enFire1 = false;

	public bool jumping = false;
	public float yJumpForce = 350;
	Vector2 jumpForce;
	public bool isOnTheFloor = false;

	AudioSource aSource;
	//public GameObject retroalimentacionEnergiaPrefab;
	//Transform retroalimentacionSpawnPoint;
	public AudioClip recibiendoBala;
	public AudioClip aullar;
	//public AudioClip ouch;
	//public AudioClip dying;

	// Use this for initialization
	void Start () {
		rgb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		//ctrs = GetComponent<ControlSoldado> ();
		aSource = GetComponent<AudioSource> ();

		energy = 100;
		//retroalimentacionSpawnPoint = GameObject.Find ("spawnPoint").transform;
		jumpForce = new Vector2 (0, 0);
		rgb.freezeRotation = true;
	}

	void Update(){
		if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("DieFront")) {
			if (energy <= 0) {
				energy = 0;
				//anim.Play ("DieFront");
				anim.SetTrigger ("morir");
				//return;
				//aSource.PlayOneShot (dying);
				siguienteEscena();
			}
		} else
			//anim.gameObject.GetComponent<Animator>().enabled = false;
			return;
		/*if (Input.GetKey().Equals("Fire2")){
			anim.SetTrigger ("Kick");
		}*/
		if (Mathf.Abs (Input.GetAxis ("Fire2")) > 0.01f) {
			anim.SetTrigger ("Kick"); 

		}
		if (Mathf.Abs (Input.GetAxis ("Fire1")) > 0.01f) {
			if (enFire1 == false) {
				enFire1 = true;
				//hacha.GetComponent<CircleCollider2D> ().enabled = false;
				anim.SetTrigger ("Attack3");
				//if (ctrArbol != null) {
					//if (ctrArbol.golpeOrco ()) {
						//IncrementarEnergia(premioArbol);
						//energy += premioArbol;
						if (energy > 100)
							energy = 100;
					//} else {
						energy -= costoMordidaGallina;
						//IncrementarEnergia(costoGolpeAlArbol * -1);
						//aSource.PlayOneShot (cortandoUnArbol);
					//}
				//} else {
					//energy -= costoMordidaAlAire;
					//IncrementarEnergia(costoGolpeAlAire * -1);
				//}
			}
		} else {
			enFire1 = false;
		}
		/*if (energy < 0) {
			energy = 0;
			anim.SetTrigger ("morir");
			anim.ResetTrigger ("Walk");
			anim.ResetTrigger ("Idle");

		}*/
		slider.value = energy;
		txt.text = energy.ToString ();
	}

	private void IncrementarEnergia(int incremento) {
		energy += incremento;
		//InstanciarRetroalimentacionEnergia(incremento);
	}
	/*private void InstanciarRetroalimentacionEnergia(int incremento) {
		GameObject retroalimentcionGO = null;
		if (retroalimentacionSpawnPoint!=null)
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, retroalimentacionSpawnPoint.position, retroalimentacionSpawnPoint.rotation);
		else
			retroalimentcionGO = (GameObject)Instantiate(retroalimentacionEnergiaPrefab, transform.position, transform.rotation);

		retroalimentcionGO.GetComponent<RetroalimentacionEnergia>().cantidadCambiodeEnergia = incremento;
	}*/

	private void verificarInputParaSaltar(){
		isOnTheFloor = rgb.velocity.y == 0;

		if (Input.GetAxis ("Jump") > 0.01f) {
			if (!jumping && isOnTheFloor) {
				jumping = true;
				jumpForce.x = 0f;
				jumpForce.y = yJumpForce;
				rgb.AddForce (jumpForce);
			}
		} else {
			jumping = false;
		}
	}

	/*public void habilitarTriggerHacha(){
		hacha.GetComponent<CircleCollider2D> ().enabled = true;
	}*/

	void FixedUpdate () {
		if (energy > 0) {
			verificarInputParaCaminar ();
			verificarInputParaSaltar ();   
		} else
			return;
	}

	private void verificarInputParaCaminar() {
		float h = Input.GetAxis("Horizontal");
		Vector2 vel = new Vector2(0, rgb.velocity.y);
		h *= maxVel;
		vel.x = h;
		rgb.velocity = vel;

		anim.SetFloat ("speed", vel.x);
		if (haciaDerecha && h < 0) 
			flip ();
		if (!haciaDerecha && h > 0) 
			flip ();
		if(!haciaDerecha && h < 0)
			anim.SetFloat ("speed", (vel.x)*-1);
	}

	void flip(){
		var s = transform.localScale;
		s.x *= -1;
		transform.localScale = s;
		haciaDerecha = !haciaDerecha;
	}



	//public void setControlArbol(ControlArbol ctr){
		//ctrArbol = ctr;
	//}

	public void recibirBala(){
		//energy -= costoBala;
		if(energy > 0){
		IncrementarEnergia(costoBala * -1);
		anim.SetTrigger ("Idle3");
		aSource.PlayOneShot (aullar);
		aSource.PlayOneShot (recibiendoBala);
		}

	}

	private void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name.Equals ("Enemigo") && energy > 0) {
			anim.SetTrigger ("Kick");
			ControlSoldado ctr = other.gameObject.GetComponent<ControlSoldado> ();
			if (ctr != null)
				ctr.BajarPuntosPorPatada ();
			//Destroy (gameObject);
		}
		if (other.gameObject.name.Equals ("Gallina")) {
			//anim.SetTrigger ("Idle4");
			anim.SetTrigger ("2To4");
			anim.SetTrigger ("4Attack");
		}
	}

	public void recibirDaño(){
		if (energy > 0) {
			IncrementarEnergia (costoDaño * -1);
			anim.SetTrigger ("Idle3");
			aSource.PlayOneShot (aullar);
			//aSource.PlayOneShot (ouch);
		} 
	}

	public void siguienteEscena(){
		Invoke ("cargarFin", 5);
	}

	public void cargarFin(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
	}
		
}
