using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchManager : MonoBehaviour
{
	
	public static MatchManager instance;
	
	private GameObject[] batting_team;
	private GameObject[] fielding_team;
	
	public List<Sprite> playerSprites = new List<Sprite>();
	
	public int pitchHeight, pitchWidth;
	
	public GameObject player;
	
    // Start is called before the first frame update
    void Start()
    {
		instance = GetComponent<MatchManager>();
		GenerateFieldingTeam();
    }
	
	private void GenerateFieldingTeam(){
		fielding_team = new GameObject[11];
		
		for (int i = 0; i < fielding_team.Length; i++) {
			int xPosition = i * 1;
			int yPosition = i * 1;
			GameObject newPlayer = Instantiate(
				player,
				new Vector3(xPosition, yPosition, 0),
				player.transform.rotation
			);
			
			fielding_team[i] = newPlayer;
			
			newPlayer.transform.parent = transform;
			newPlayer.GetComponent<SpriteRenderer>().sprite = playerSprites[Random.Range(0, playerSprites.Count)];
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
