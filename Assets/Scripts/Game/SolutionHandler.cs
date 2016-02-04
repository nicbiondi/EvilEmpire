using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SimpleJSON;

//used to create the solution section of the game
public class SolutionHandler : MonoBehaviour {
	public GameObject PlayArea;
	public Text FirstSolutionText;
	public Text SecondSolutionText;
	public Transform FirstSolutionParent;
	public Transform SecondSolutionParent;
	Game game;
	public bool isOnline = false;
	string URL = "http://172.16.31.148:8080/FunService/rest/api/screen";
	string TestString =  "[{\"MAX_LINE_WIDTH\":6,\"MAX_ENTRY_LINES\":3,\"id\":0,\"firstHint\":\"mournful\",\"firstSolution\":\"sad\",\"playTiles\":\"das\"},{\"MAX_LINE_WIDTH\":6,\"MAX_ENTRY_LINES\":3,\"id\":1,\"firstHint\":\"cow\",\"firstSolution\":\"beef\",\"firstTileStart\":\"befe\"},{\"MAX_LINE_WIDTH\":6,\"MAX_ENTRY_LINES\":3,\"id\":2,\"firstHint\":\"insect\",\"firstSolution\":\"bug\",\"firstTileStart\":\"gbu\",\"secondHint\":\"hobo\",\"secondSolution\":\"bum\",\"secondTileStart\":\"mub\"},{\"MAX_LINE_WIDTH\":6,\"MAX_ENTRY_LINES\":3,\"id\":3,\"firstHint\":\"Howdy\",\"firstSolution\":\"hello\",\"firstTileStart\":\"ehlol\",\"secondHint\":\"planet\",\"secondSolution\":\"world\",\"secondTileStart\":\"orldw\"},{\"MAX_LINE_WIDTH\":6,\"MAX_ENTRY_LINES\":3,\"id\":4,\"firstHint\":\"beautiful\",\"firstSolution\":\"pretty\",\"firstTileStart\":\"ttpeyr\",\"secondHint\":\"homely\",\"secondSolution\":\"ugly\",\"secondTileStart\":\"lyug\"},{\"MAX_LINE_WIDTH\":6,\"MAX_ENTRY_LINES\":3,\"id\":5,\"firstHint\":\"bad\",\"firstSolution\":\"evil\",\"firstTileStart\":\"vlei\",\"secondHint\":\"kingdom\",\"secondSolution\":\"empire\",\"secondTileStart\":\"eriemp\"}]";
		
	

	void Start() {
	
		game = Game.Instance;
		
		if(isOnline)
			StartCoroutine("GetGameFromServer");
		else 
			StartOffline();
			
	}
	
	void StartOffline()
	{
		//Level 1 doesn't need a dynamic solution since we have a tutorial scene to show how to play which doesn't use the solution handler
		if(game.currentLevel==2)
			CreateSolution("beef?","COW","beef","","");
		if(game.currentLevel==3)
			CreateSolution("bugbum??","INSECT","bug","HOBO","bum");
		if(game.currentLevel==4)
			CreateSolution("helloworld??","HOWDY","hello","PLANET","world");
		if(game.currentLevel==5)
			CreateSolution("prettyugly","BEAUTIFUL","pretty","HOMELY","ugly");
		if(game.currentLevel==6)
			CreateSolution("evilempire??","BAD","evil","KINGDOM","empire");
	
	}
	
	IEnumerator getGameFromServer()
	{
		WWW www = new WWW(URL);
		yield return www;
		string jasonData = www.text;
		
		game = Game.Instance;
		
		
		var N = JSON.Parse(jasonData);
		//var N = JSON.Parse(TestString);

		string firstHint = N[0]["firstHint"];// name will be a string containing "sub object"
		string firstSolution = N[0]["firstSolution"];// name will be a string containing "sub object"
		string playTiles = N[0]["playTiles"];// name will be a string containing "sub object"
		
		
		if(game.currentLevel==1)
			CreateSolution(playTiles,firstHint,firstSolution,"","");
	}
	void CreateSolution(string playTiles, string hint1,string solution1,string hint2,string solution2)
	{
		CreateTiles(playTiles);
		FirstSolutionText.text = hint1;
		CreateSolution(solution1,FirstSolutionParent);
		SecondSolutionText.text = hint2;
		CreateSolution (solution2,SecondSolutionParent);
		
	}
	GameObject CreateTileSlot(string slotValue,Transform parentTransform)
	{
		GameObject TileSlot = Instantiate(Resources.Load("Tiles/TileSlot")) as GameObject;
		TileSlot.transform.parent = parentTransform;//set parent to correct
		TileSlot.GetComponentInChildren<DropZone>().answer = slotValue;//little hacky but I'm ok with it
		return gameObject;
	}
	void CreateSolution(string tileString, Transform parentTransform)
	{
		fixSpacing(tileString,parentTransform.gameObject);
		foreach(char c in tileString)
		{
			CreateTileSlot(c.ToString(),parentTransform);
		}
	}
	void CreateTiles(string tileString)
	{
		foreach(char c in tileString)
		{
			if(c.ToString().Equals("?"))
				CreateTile(RandomizeValue());
			else
		  		CreateTile(c.ToString());
		}
	}
	void CreateTile(string tileValue)
	{
		GameObject Tile = Instantiate(Resources.Load("Tiles/Tile")) as GameObject;
		Tile.transform.parent = PlayArea.transform;
		Text text = Tile.GetComponentInChildren<Text>();
		if(text)
			text.text = tileValue.ToUpper();
		//return gameObject;
	}
	
	//when the words get too big, the spacing needs to change to fit nicely in the tilespace
	void fixSpacing(string tileString,GameObject parentGo)
	{
		if(tileString.Length>=6)
		{
			GridLayoutGroup layoutGroup = parentGo.GetComponent<GridLayoutGroup>();
			layoutGroup.spacing = new Vector2(5f,layoutGroup.spacing.y);
		}
	}
	string RandomizeValue()
	{
		string st = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		char c = st[Random.Range(0,st.Length)];
		return c.ToString();
	}

		

}
