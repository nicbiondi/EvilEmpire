using UnityEngine;
using System.Collections;
using System.IO;
public class AudioManager : MonoBehaviour {


//		Game game;
//		public string explosionSound = "boom";
//		public string thrusterSound = "thruster";
//		public string sideThrusterSound = "sideThruster";
//		public string winSound = "clapping";
//		public string roundBeginSound = "gong";
//		public string laserLoopSound = "LazerBeamloop";
//		public string electricityLoopSound = "electricityLoop";
//		public string landingGearCollisionSound = "thud";
//		public string UIFlipSound = "UIFlip";
//		public string UISelectSound = "UISelect";
//		public string UICurs0rMove = "CursorMove";
		
		
		// Use this for initialization
		void Start () {
			//game = Game.Instance;
		}
		void OnEnable()
		{
			EventManager.StartListening("PickUpTile", PickUpTile);
			EventManager.StartListening("DropTile", DropTile);
		}
		void OnDisable()
		{
			EventManager.StopListening("PickUpTile", PickUpTile);
			EventManager.StopListening("DropTile", DropTile);
		}
		
		void PickUpTile()
		{
			PlaySound("click0");
		}
		void DropTile()
		{
			PlaySound("click1");
		}
		public AudioClip GetAudioClipFromResources(string audioClipName)
		{
			AudioClip soundClip = (AudioClip) Resources.Load("Audio/"+audioClipName);
			return soundClip;
			
		}
		public bool PlaySound(string soundName)
		{return PlaySound(soundName,PitchType.Normal);}
		
		public bool PlaySound(string soundName, PitchType pitchType)
		{
			return PlaySound(soundName, pitchType, Vector3.zero, Vector2.zero);	
		}
		
		public bool PlaySound(string soundName, PitchType pitchType, Vector3 position,Vector2 pitchPosition)
		{
			
			AudioClip soundClip = (AudioClip) Resources.Load("Audio/"+soundName);
			
			if(soundClip)
			{
				PlayClipAtPoint(soundClip, position, 1f, GetPitch(pitchType,pitchPosition));
				return true;
			}
			else
			{
				Debug.Log("problem loading audio:" + soundName);
				return false;
			}
			
		}
		
		
		public float GetPitch(PitchType pitchType)
		{
			return GetPitch(pitchType, Vector2.zero);
		}
		public float GetPitch(PitchType pitchType, Vector2 pitchPosition)
		{
			float pitch = 1f;
			if(pitchType == PitchType.Random)
			{
				float randomizationRange = 0.07f;
				pitch = Random.Range(1-randomizationRange,1+randomizationRange);
			}
			else if (pitchType == PitchType.RandomRange)//define range of pitch randomization in pitchPosition vector
			{
				pitch = Random.Range(pitchPosition.x,pitchPosition.y);
			}
			else if(pitchType == PitchType.PositionToPitch)
			{
				float startingPitch = 1f;
				float shiftPerValue = 0.0002f;
				pitch = startingPitch + ((pitchPosition.x+ pitchPosition.y) * shiftPerValue);
				//Debug.Log("pitch is " + pitch);
			}
			return pitch;
		}
		
		GameObject PlayClipAtPoint(AudioClip clip, Vector3 position, float volume, float pitch){
			GameObject obj = new GameObject();
			obj.transform.position = position;
			AudioSource audiosource = obj.AddComponent<AudioSource>();
			audiosource.pitch = pitch;
			audiosource.PlayOneShot(clip, volume);
			Destroy (obj, clip.length / pitch);
			return obj;
		}
		
	}
	public enum PitchType {Normal,Random,PositionToPitch,RandomRange};
