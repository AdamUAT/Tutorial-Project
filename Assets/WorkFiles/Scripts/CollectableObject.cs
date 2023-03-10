using UnityEngine;
using System.Collections;

public class CollectableObject : MonoBehaviour 
{
	private ParticleSystem glow;
	[SerializeField]
	private GameObject model;

	void Start () 
	{
		this.gameObject.GetComponent<Collider>().isTrigger = true;//Make sure the box collider we are using is a trigger so we get OnTrigger messages
		glow = GetComponentInChildren<ParticleSystem>();
	}

    /// <summary>
    /// Raises the trigger enter event, this happens when colliders hit & one of them was a trigger
    /// </summary>
    /// <param name='objectHit'>Object hit.</param>
    public void OnTriggerEnter(Collider objectHit)
	{
		if(objectHit.CompareTag("Player"))//If we hit the player object, move it to spawn	
		{
			this.AddToCollectablesCount();
			this.KillMe();
		}
	}

	public void AddToCollectablesCount()
	{
		GameManager.instance.AddCollectable();
	}
	
	public void KillMe()
	{
		Destroy(this.gameObject);
	}

	private void Update()
	{
		if(glow != null)
			glow.GetComponent<Transform>().LookAt(GameManager.instance.player.transform);
        //model.transform.rotation = Quaternion.Euler(new Vector3(33 * Mathf.Sin(Time.time), transform.rotation.y, 33 * Mathf.Sin(Time.time)));
        model.transform.Rotate(new Vector3(10 * Mathf.Cos(Time.time), 0, 10 * Mathf.Sin(Time.time)) * Time.deltaTime);
		model.transform.Rotate(new Vector3(0, 33, 0) * Time.deltaTime, Space.World);
	}
}
