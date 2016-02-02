using UnityEngine;
using System.Collections;

public class Effects : MonoBehaviour {
	public bool startAnimationOnCelebration = false;
	public bool animateOnStart = false;
	public bool stopAnimationOnLevelEnd = true;
	public bool enableSpecialStartAnimation = false;
	Animator animator;
	void OnStart()
	{
		Initialize();
	}
	void Initialize()
	{
		animator = GetComponent<Animator>();
	}
	
	//set up messages
	void OnEnable()
	{
		if(startAnimationOnCelebration)
			EventManager.StartListening("Celebrate", Celebrate);
		if(stopAnimationOnLevelEnd)
			EventManager.StartListening("LevelEnded", LevelEnded);
		if(animateOnStart)
			EventManager.StartListening("NewLevel", NewLevel);	
	}
	
	//clean up messages
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
		//this special start animation is used for a special case "scrub in".  If we use more of these kinds of dual purpose animations we should refactore this.
		if(enableSpecialStartAnimation)
		{
							
			animator.SetBool("enableSpecialStartAnimation", true);
			Invoke("ResetAnimation",0.3f);//turn off the animation bool so it doesn't repeat
		}
		else
			animator.SetBool("enableAnimation", true);
	}
	void LevelEnded()
	{
		animator.SetBool("enableAnimation", false);
	}
	
	void ResetAnimation()
	{
		animator.SetBool("enableSpecialStartAnimation", false);
	}
}
