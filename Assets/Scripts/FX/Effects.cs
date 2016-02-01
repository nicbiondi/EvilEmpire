using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour {
	public bool startAnimationOnCelebration = false;
	public bool animateOnStart = false;
	public bool stopAnimationOnLevelEnd = true;
	Animator animator;
	void OnStart()
	{
		Initialize();
	}
	void Initialize()
	{
		animator = GetComponent<Animator>();
	}
	void OnEnable()
	{
		if(startAnimationOnCelebration)
			EventManager.StartListening("Celebrate", Celebrate);
		if(stopAnimationOnLevelEnd)
			EventManager.StartListening("LevelEnded", LevelEnded);
		if(animateOnStart)
			EventManager.StartListening("NewLevel", NewLevel);	
	}
	
	void OnDisable()
	{
		if(startAnimationOnCelebration)
			EventManager.StopListening("Celebrate", Celebrate);
		if(stopAnimationOnLevelEnd)
			EventManager.StopListening("LevelEnded", LevelEnded);
		if(animateOnStart)
			EventManager.StopListening("NewLevel", NewLevel);
	}
	
	void Celebrate()
	{
		if(!animator)
			Initialize();
		animator.SetBool("enableAnimation", true);
	}
	void NewLevel()
	{
		if(!animator)
			Initialize();
		animator.SetBool("enableAnimation", true);
	}
	void LevelEnded()
	{
		animator.SetBool("enableAnimation", false);
	}
}
