﻿using UnityEngine;
using System.Collections;

public class GassyFlashBang : MonoBehaviour {
	
	SpriteRenderer spriteRenderer;
	
	public GameObject target;
	public GameObject gassProjectile;
	HeroSkillTrigger heroSkillTrigger;
	
	public int enemyHitLimit = 4;
	private int countEnemiesTargeted;
	private string[] enemiesRealName;
	public GameObject[] enemiesCaught;
	private string markedTargetName;    //All targeted enemies marked by this name
	private int countEnemiesHit;
	
	public Vector3 ground;
	bool groundClicked;
	bool reachGround;
	bool flag;
	public int spawnNum;
	public float explosionRadius;

	public float radius;
	public float speed;
	
	private bool isFindingTarget = false;
	private bool projectileVisible = false;
	
	public GameObject soundHitGO;
	public AudioClip soundHit;
	
	public GameObject[] enemiesOnRadius;
	
	
	
	
	public float slowandpoisonDelay;
	public int poison;
	Vector3 temp2;
	
	void Start()
	{
		//To mark the arrow not visible before launch
		spriteRenderer = GetComponent<SpriteRenderer>();
		spriteRenderer.enabled = false;
		groundClicked = false;
		reachGround = false;
		enemiesRealName = new string[enemyHitLimit];
		enemiesCaught = new GameObject[enemyHitLimit];	
		heroSkillTrigger = transform.parent.GetComponent<HeroSkillTrigger>();
		countEnemiesTargeted = 0;
		countEnemiesHit = 0;
		markedTargetName = "GrenadeArrowVictim";
	}
	
	void Update()
	{
		if (Input.GetButtonDown("Fire1") && !isFindingTarget)
			setFirstEnemyOnTap();
		
		//Set the object rotation
		if (groundClicked)
		{
			Quaternion direction = Quaternion.LookRotation(ground - this.transform.position, this.transform.TransformDirection(Vector3.up));
			this.transform.rotation = new Quaternion(0, 0, direction.z, direction.w);
		}
		
		//move projectile towards target
		if (isFindingTarget && groundClicked && !reachGround)
		{
			transform.position = Vector2.MoveTowards(transform.position, ground, Time.deltaTime * speed);
			if(Vector2.MoveTowards(transform.position, ground, Time.deltaTime * speed)==new Vector2(transform.position.x,transform.position.y))
			{
				
				reachGround = true;
			}
			
		}

		// hit the ground
		if (transform.position.x == ground.x && transform.position.y == ground.y) {
			reachGround = true;
		}

		// hit the ground and found enemy
		if(isFindingTarget && reachGround)
		{
			if(!flag)
			{
				disableProjectileVisulization();
				flag=true;
				spawnExplossionEffect();
				
			}
		}
	}
	
	void setFirstEnemyOnTap()
	{
		ground = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		groundClicked = true;
		//TargetAnEnemy(hitObject.collider);
		isFindingTarget = true;		
		//To re enable the sprite renderer when the projectile launched
		spriteRenderer.enabled = true;		
		//To continue the attacking animation
		heroSkillTrigger.ResumeHeroAnimation();
		//To play the sfx
		//GetComponent<AudioSource>().Play();
		

		//GameObject temp = Instantiate(gassProjectile,new Vector3(center.x,center.y,0), Quaternion.identity) as GameObject;
	
		
		/*
		RaycastHit2D hitObject = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
		if (hitObject.transform.tag == "Enemy" && Vector2.Distance(transform.position, hitObject.transform.position) <= radius)
		{
			TargetAnEnemy(hitObject.collider);
			isFindingTarget = true;
			
			//To re enable the sprite renderer when the projectile launched
			spriteRenderer.enabled = true;
			
			//To continue the attacking animation
			heroSkillTrigger.ResumeHeroAnimation();
			
			//To play the sfx
			GetComponent<AudioSource>().Play();
		}
		else
		{
			//To hide the skills button after clicking
			//heroSkillTrigger.HideSkillsHolder();
			heroSkillTrigger.ResumeHeroAnimation();
			heroSkillTrigger.RestartHeroAnimation();
			Destroy(gameObject);
		}
		*/
	}
	
	
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Enemy")
		{          
			//Invoke("spawnExplossionEffect",slowandpoisonDelay);
			//set explosion position
			//temp2 = other.gameObject.GetComponent<Transform>().position;
			//instantiate the explosion
			//Instantiate(explosion, transform.position, transform.rotation);
			//Projectile hit enemy
			//other.gameObject.GetComponent<Enemy>().AttackedV3();
			disableProjectileVisulization();
			//GetComponent<ProjectileSound>().hitTargetSound();
			GetComponent<AudioSource>().clip = soundHit;
			GetComponent<AudioSource>().Play();
			//other.gameObject.GetComponent<Enemy>().SlowAndPoison(slowandpoisonDelay,poison);
			//float waitToDestroy = GetComponent<ProjectileSound>().getSoundClipLength();
			//Destroy(gameObject, waitToDestroy);
		}
	}
	
	
	
	void spawnExplossionEffect()
	{
		// looping gass projectile
		for (int i = 0; i<spawnNum; i++)
		{
			// random in tap area
			Vector3 center = ground;
			center.x = center.x + Random.Range(-radius, radius);
			center.y = center.y + Random.Range(-radius, radius);
			GameObject temp =Instantiate(gassProjectile,new Vector3(center.x,center.y,0) , Quaternion.identity) as GameObject;
			//temp.GetComponent<Transform> ().localScale = new Vector3 (explosionRadius, explosionRadius, 1);
			// play the animation
			temp.GetComponent<Animator> ().Play ("Gass");
		}

		Destroy(gameObject);
		//set radius explosion
		//temp.GetComponent<Transform> ().localScale = new Vector3 (radius, radius, 1);
		//start explosion animation
		//temp.GetComponent<Animator> ().Play ("FadeIn");
		
	}
	
	private void disableProjectileVisulization()
	{
		//disabling projectile existence to maintain hit sound
		//while the proectile didn't affect anything in game world
		Destroy(GetComponent<Rigidbody2D>());
		Destroy(GetComponent<BoxCollider2D>());
		Destroy(GetComponent<SpriteRenderer>());
	}
	
	void TargetAnEnemy(Collider2D targetCollider)
	{
		//save the reference of the enemies in order
		enemiesCaught[countEnemiesTargeted] = targetCollider.gameObject;
		
		//save the name to be refund when the skills finished
		enemiesRealName[countEnemiesTargeted] = targetCollider.gameObject.name;
		targetCollider.gameObject.name = markedTargetName;
		
		//set the reference to current targeted enemy
		this.target = enemiesCaught[countEnemiesTargeted];
		
		countEnemiesTargeted++;
		countEnemiesHit++;
	}
	
	void TargetPreviousEnemy()
	{
		int tmpRandomTarget = Random.Range(0, countEnemiesTargeted);
		countEnemiesHit++;
	}
	
	//clasify off all enemies in the field to target in range of projectile
	void FindTargetOnRadius()
	{
		GameObject[] enemiesFound = GameObject.FindGameObjectsWithTag("Enemy");
		enemiesOnRadius = new GameObject[enemiesFound.Length];
		int enemiesOnRadiusCount = 0;
		
		for (int i = 0; i < enemiesFound.Length; i++)
		{
			if (enemiesFound[i].name != markedTargetName)
			{
				if (Vector2.Distance(transform.position, enemiesFound[i].transform.position) <= radius)
					enemiesOnRadius[enemiesOnRadiusCount++] = enemiesFound[i].gameObject;
			}
		}
		if (enemiesOnRadiusCount == 0)
		{
			TargetPreviousEnemy();
		}
		else
		{
			FindNearestTarget();
		}
	}
	
	//find the nearest target after clasified by FindTargetOnRadius() method
	void FindNearestTarget()
	{
		//temp variabel to be replace by nearest enemy found through looping
		GameObject tempNearestTarget = gameObject;
		
		//to make sure event the fartest enemy in radius got a chance
		float tempDistance = radius + 0.1f;
		
		for (int i = 0; i < enemiesOnRadius.Length; i++)
		{
			if (enemiesOnRadius[i] == null)     //because array is longer than it should
				continue;
			else
			{
				float newDistance = Vector2.Distance(transform.position, enemiesOnRadius[i].transform.position);
				//if the distance to the enemy closer than the previous distance then save it
				if (newDistance < tempDistance)
				{
					tempNearestTarget = enemiesOnRadius[i].gameObject;
					tempDistance = newDistance;
				}
			}
		}
		TargetAnEnemy(tempNearestTarget.GetComponent<Collider2D>());
	}
	
	void RefundEnemiesName()
	{
		//return the name
		for (int i = 0; i < enemiesCaught.Length; i++)
		{
			if (enemiesCaught[i] == null)
				continue;
			else
				enemiesCaught[i].name = enemiesRealName[i];
		}
	}
	
	
}
