using UnityEngine;
using System.Collections;



public class Instantiation : MonoBehaviour {
	
	public Transform startMarker;
	public Vector3 endMarker;
	private int _totalCorrect = 2;
	public float speed = 1.0F;
	private float startTime;
	public Transform target;
	//public float smooth = 5.0F;	
	public int flag=0,createobj=1;
	int count = 0;
	public float smooth;
	private Vector3 newPosition;
	GameObject candy;
	Color[] col;
	//Draw a new Color band after _totalCorrect correct candies
	int numCorrectCandies = 0;
	//The color band number. 
	int colorBand = 1;
	//Toggle when game is over
	int gameOver = 0;
	
	public Texture _texture;
	
	
	void SetText(string text){
		GameObject textBox = GameObject.Find("Text");
		TextMesh t = (TextMesh)textBox.GetComponent(typeof(TextMesh));
		t.text = text;
	}
	
	void CreateCandy(){
		candy = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		candy.transform.position = new Vector3(443,205,225);
		candy.transform.localScale = new Vector3(5,5,5);
		int colnum = Random.Range(0,6);
		//Debug.Log(colnum);
		candy.renderer.material.color= col[colnum];
		//Debug.Log(candy.transform.position.x);
		candy.renderer.castShadows = false;
		//Debug.Log("\nCandy Created!");
		//Debug.Log (candy.name);
		//newPosition=candy.transform.position;	
		//Debug.Log(newPosition.x + newPosition.y + newPosition.z);
		createobj=0;
		
	}

	void InitRainbowColors(){
		col = new Color[7];
		col[0] = new Color(143/255.0F,0,1,1); //violet
		col[1] = new Color(75/255.0F,0,130/255.0F,1); //indigo
		col[2] = Color.blue; //blue
		col[3] = Color.green;//green
		col[4] = Color.yellow; //yellow
		col[5] = new Color(1,127/255.0F,0,1); //orange
		col[6] = Color.red; //red
	}
	
	// Use this for initialization
	void Start () {
		Debug.Log("in start numcorrect Candies = "+numCorrectCandies+" colorBand = "+colorBand);
		GameObject rainbowPlane = GameObject.Find("RainbowPlane");
		rainbowPlane.renderer.enabled = false;
		/*string text = "Nice Job!";
		SetText(text);*/
		startTime = Time.time;

		speed = 2.0F;
		smooth = 3.0F;
		count++;
			
		InitRainbowColors();
		CreateCandy();
	}
	
	void SetRainbow(int code){
		GameObject rainbowPlane = GameObject.Find("RainbowPlane");
		if(rainbowPlane.renderer.enabled ==  false)
			rainbowPlane.renderer.enabled = true;
		Material rainbow;
		switch(code){
		case 1 :
			rainbow = new Material(Resources.Load("R", typeof(Material)) as Material);
			rainbowPlane.renderer.material = rainbow;		
			break;
			
		case 2 :
			rainbow = new Material(Resources.Load("OR", typeof(Material)) as Material);
			rainbowPlane.renderer.material = rainbow;		
			break;
			
		case 3 :
			rainbow = new Material(Resources.Load("YOR", typeof(Material)) as Material);
			rainbowPlane.renderer.material = rainbow;		
			break;
			
		case 4 :
			rainbow = new Material(Resources.Load("GYOR", typeof(Material)) as Material);
			rainbowPlane.renderer.material = rainbow;		
			break;
			
		case 5 :
			rainbow = new Material(Resources.Load("BGYOR", typeof(Material)) as Material);
			rainbowPlane.renderer.material = rainbow;		
			break;
			
		case 6 :
			rainbow = new Material(Resources.Load("IBGYOR", typeof(Material)) as Material);
			rainbowPlane.renderer.material = rainbow;		
			break;
			
		case 7 :
			rainbow = new Material(Resources.Load("VIBGYOR", typeof(Material)) as Material);
			rainbowPlane.renderer.material = rainbow;		
			break;
			
		}
		numCorrectCandies = 0;
		colorBand++;
	}
	
	// Update is called once per frame
	void Update () {
		
		//	PositionChange();
		if(gameOver == 0)
		{
			if(createobj == 1)
			{		
				CreateCandy();
			}
			if(Input.GetMouseButton(0))
			{
				initialise();			
			}
			animate();
		
	}
}

	void DecrementCandyCount(){
		numCorrectCandies--;
		if(numCorrectCandies<0)
			numCorrectCandies = 0;
	}
	
	void PlayWrongColorAudio(){
		GameObject audioObj;
		audioObj = GameObject.Find("WrongColorAudio");
		
		Debug.Log("Found the audio!");
		audioObj.audio.Play();
	}

	void PlayCorrectColorAudio(){
		GameObject audioObj;
		audioObj = GameObject.Find("CorrectColorAudio");
		
		Debug.Log("Found the audio!");
		audioObj.audio.Play();
	}

	void PlayNurseryRhyme(){
		GameObject audioObj;
		audioObj = GameObject.Find("NurseryRhymeAudio");
		
		Debug.Log("Found the audio!");
		audioObj.audio.Play();
	}
	void initialise()
	{
			RaycastHit hitinfo= new RaycastHit();
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitinfo))
			{
				
				/*if(hitinfo.collider.name=="Sphere")
					{
					//startMarker.position=GameObject.Find("Sphere").transform.position;
					flag1=1;
	//				Debug.Log(startMarker.position.x);
					}*/
				switch(hitinfo.collider.name)
				{
				case "VioletJar":
					if(candy.renderer.material.color == col[0]){
						endMarker=GameObject.Find("VioletJar").transform.position;
						flag=1;
					}
					else{
						DecrementCandyCount();
						PlayWrongColorAudio();
					}	
					break;
					
				case "IndigoJar":
					if(candy.renderer.material.color == col[1]){
						//Debug.Log("lerping indigo");
						endMarker=GameObject.Find("IndigoJar").transform.position;
						flag=1;
					}
					else{
						DecrementCandyCount();
						PlayWrongColorAudio();
						Debug.Log("Wrong Color!!!");
					}
					break;
					
				case "BlueJar":
					if(candy.renderer.material.color == col[2]){
						Debug.Log("lerping blue");
						endMarker=GameObject.Find("BlueJar").transform.position;
						flag=1;
					}
					else{
						DecrementCandyCount();
						PlayWrongColorAudio();
						Debug.Log("Wrong Color!!!");
						
					}
					break;
					
				case "GreenJar":
					if(candy.renderer.material.color == col[3]){
						Debug.Log("lerping green");
						endMarker=GameObject.Find("GreenJar").transform.position;
						flag=1;
					}
					else{
						DecrementCandyCount();
						PlayWrongColorAudio();
						Debug.Log("Wrong Color!!!");
					}
					break;
					
				case "YellowJar":
					if(candy.renderer.material.color == col[4]){
						Debug.Log("lerping yellow");
						endMarker=GameObject.Find("YellowJar").transform.position;
						flag=1;
						
					}
					else{
						DecrementCandyCount();	
						PlayWrongColorAudio();
						Debug.Log("Wrong Color!!!");
					}
					break;
					
				case "OrangeJar":
					if(candy.renderer.material.color == col[5]){
						Debug.Log("lerping orange");
						endMarker=GameObject.Find("OrangeJar").transform.position;
						flag=1;
						
					}
					else{
						DecrementCandyCount();
						PlayWrongColorAudio();
						Debug.Log("Wrong Color!!!");
					}
					break;
					
				case "RedJar":
					if(candy.renderer.material.color == col[6]){
						Debug.Log("lerping red");
						endMarker=GameObject.Find("RedJar").transform.position;
						flag=1;
						
					}
					else{
						DecrementCandyCount();
						PlayWrongColorAudio();
						Debug.Log("Wrong Color!!!");
					}
					break;
				}
				/*if(hitinfo.collider.name=="GreenJar")
					{
					endMarker=GameObject.Find("GreenJar").transform.position;
					flag2=1;
					flag1=1;
					Debug.Log(endMarker.x);
					}*/
			}
			//Reset the mouse-click flag. waiting for the next click now.
		//	executed = 1;
		//}			
	}
	
	
	void animate()
	{
		
		//Debug.Log("IN ANIMATE");
		if(flag==1)
		{
			//journeyLength=Vector3.Distance(startMarker.position, endMarker);
			//Debug.Log ("Inside startmarker");
			//Debug.Log (Mathf.Abs(candy.transform.position.x - (endMarker.x)));
			if(Mathf.Abs(candy.transform.position.x - (endMarker.x)) > 0.1)
			{
			
			/*float distCovered = (Time.time - startTime) * speed;
			        float fracJourney = distCovered / journeyLength;
					Vector3 TempVector = startMarker.position;*/
			//startMarker.position = Vector3.Lerp(startMarker.position, endMarker, smooth * Time.deltaTime);
			
				newPosition = endMarker;
				candy.transform.position = Vector3.Lerp(candy.transform.position, newPosition, smooth * Time.deltaTime);
				PlayCorrectColorAudio();

			}
		else
			{
				flag=0;
				createobj=1;
				Destroy (candy);
				numCorrectCandies++;
				//Check the number of correct candies. Draw a rainbow band if reached _totalCorrect
				if(numCorrectCandies == _totalCorrect)
				{
					Debug.Log("reached max");
					if(colorBand == 7)
					{
						gameOver = 1;
						PlayNurseryRhyme();
						SetText("Nice Job!");					
					}
					else{
						SetRainbow(colorBand);
					}
					
					
				}
			}
			//executed = 0;
		}

	}
	
	
	
	/*void PositionChange()
	{
		Vector3 dstpos=GameObject.Find("Sphere").transform.position;
		if(Input.GetMouseButton(0))
		{
			RaycastHit hitinfo= new RaycastHit();
			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hitinfo))
			{
				
				if(hitinfo.collider.name=="Sphere")
				{
					flag=1;
					Debug.Log("Me ithe aloy");
				}
				else
				{
					if(flag==1)
					{
						animate ();
					}
				}
					//Debug.Log(hitinfo.collider.name+" Found!!\n");
			
			}
		}
	
	}
	*/
	
	/*
void pchange()
{
	
        Vector3 positionA = new Vector3(500, 333, 200);
        Vector3 positionB = new Vector3(500, 300, 100);
        //Debug.Log ("inside pchange");
        if(Input.GetKeyDown(KeyCode.Q))
		{
			Debug.Log ("inside q");
			newPosition = positionA;
		}
		if(Input.GetKeyDown(KeyCode.E))
            newPosition = positionB;
        
		
        candy.transform.position = Vector3.Lerp(candy.transform.position, newPosition, smooth * Time.deltaTime);
    
}
*/
	
}



