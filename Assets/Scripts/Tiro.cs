using UnityEngine;
using System.Collections;

public class Tiro : MonoBehaviour {

	public enum Direcao
	{
		Cima,
		Baixo,
		Esquerda,
		Direita
	}

	public float velocidade = 10;
	public Direcao DirecaoTiro = Direcao.Cima;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		Movimentar ();
	}

	void OnCollisionEnter2D(Collision2D collisor)
	{
		Destroy(gameObject);
	}

	void Movimentar()
	{
		Vector3 _vetor;

		switch(DirecaoTiro) 
		{
			case Direcao.Cima:
				_vetor = Vector3.up * velocidade * Time.deltaTime;
				break;
			case Direcao.Baixo:
				_vetor = Vector3.down * velocidade * Time.deltaTime;
				break;
			case Direcao.Direita:
				_vetor = Vector3.right * velocidade * Time.deltaTime;
				break;
			case Direcao.Esquerda:
				_vetor = Vector3.left * velocidade * Time.deltaTime;
				break;
			default:
				_vetor = transform.transform.position;
				break;
		}

		transform.Translate(_vetor);
	}
}
