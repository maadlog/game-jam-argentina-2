using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour, ITrackable
{
	// Start is called ;before the first frame update
	public float health;
    public Transform healthBar;
	public Image uiHealth;
    public GameObject explosion;
   
	GameManager gameManager;
	void Start()
	{
		gameManager = GameManager.getGameManager();
		healthBar.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 4, 0);
	}

	// Update is called once per frame
	void Update()
	{
		uiHealth.fillAmount = health / 100;
	}
	public void GetHit(int damage)
	{
		health -= damage;
        
		if (health <= 0)
		{
            // Create explosion on base positon
            Instantiate(explosion, transform.position, Quaternion.identity);
            uiHealth.fillAmount = 0;
            // Lost Level on game manager
            gameManager.LostLevel();
            gameManager.PlaySoundExplosion();

            tracker?.RemoveItem(gameObject);

            // Destroy base
            Destroy(gameObject);
		}

	}

    private CameraTracker tracker;
    public void SetTracker(CameraTracker tracker)
    {
        this.tracker = tracker;
    }


}
