using UnityEngine;
using System.Collections;
using System.Linq; 

public class CSVReader : MonoBehaviour {

    public TextAsset file;
    public int offset = 0;
    //0
    public GameObject PlayerPrefab;
    
    //From 1 to 10.
    public GameObject[] OuterWallPrefabs;
    public GameObject[] WallPrefabs;
    public GameObject StonePrefab;
    public GameObject[] RelicPrefabs;
    public GameObject PickableCompassPrefab;

    //Enemy ID will start from 10
    public GameObject SlimePrefab;

    //BOSS from 20
    public GameObject GolemPrefab;

    //NPC FROM 50
    public GameObject DragonPrefab;

    //NO ID NEEDED HERE.
    public GameObject[] floorPrefabs;
    public GameObject[] environmentOBJ;

    private string[,] LevelLayoutArray;
    private string [] textRow;
    private GameObject lvlHolder;

	// Use this for initialization
	void Start () {
        lvlHolder = new GameObject("LevelHolder");

	    LevelLayoutArray = CSVReader.SplitCsvGrid(file.text);
        textRow = new string[LevelLayoutArray.GetUpperBound(0)];  // Instantiate the textRow array with the number of rows in csvData array.

        int height = ((LevelLayoutArray.Length / textRow.Length) - 1) + offset;
        int width = textRow.Length - 1;

        //Debug.Log(width.ToString() + " " + height.ToString());

        float offsetX = width % 2 == 0 ? 0.5f : 0.0f;
        float offsetY = -0.5f; //compulsory

        for (int y = height - 1; y >= 0; y--) // height
        {
            for (int x = 0; x < width; x++)
            {
                Vector3 objPos = new Vector3(x - (width >> 1) + offsetX, (-y + (height >> 1) + offsetY), 0);
                if (LevelLayoutArray[x, y] == "0")
                {
                    Instantiate(PlayerPrefab, objPos, Quaternion.identity);

                    int randomIndex2 = Random.Range(0, floorPrefabs.Length);
                    GameObject result = Instantiate(floorPrefabs[randomIndex2], objPos, Quaternion.identity) as GameObject;
                    result.transform.SetParent(lvlHolder.transform);
                }
                else if (LevelLayoutArray[x, y] == "1")
                {
                    int randomIndex = Random.Range(0, OuterWallPrefabs.Length);
                    GameObject result = Instantiate(OuterWallPrefabs[randomIndex], objPos, Quaternion.identity) as GameObject;
                    result.transform.SetParent(lvlHolder.transform);
                }
                else if (LevelLayoutArray[x, y] == "2")
                {
                    int randomIndex = Random.Range(0, WallPrefabs.Length);
                    GameObject result = Instantiate(WallPrefabs[randomIndex], objPos, Quaternion.identity) as GameObject;
                    result.transform.SetParent(lvlHolder.transform);

                    if(environmentOBJ.Length > 0)
                    {
                        int randomIndex2 = Random.Range(0, environmentOBJ.Length);
                        GameObject result2 = Instantiate(environmentOBJ[randomIndex2], objPos, Quaternion.identity) as GameObject;
                        result2.transform.SetParent(lvlHolder.transform);
                    }
                }
                else if (LevelLayoutArray[x, y] == "3")
                {
                    Instantiate(StonePrefab, objPos, Quaternion.identity);

                    int randomIndex2 = Random.Range(0, floorPrefabs.Length);
                    GameObject result = Instantiate(floorPrefabs[randomIndex2], objPos, Quaternion.identity) as GameObject;
                    result.transform.SetParent(lvlHolder.transform);
                }
                else if (LevelLayoutArray[x, y] == "4")
                {
                    int randomIndex = Random.Range(0, RelicPrefabs.Length);
                    GameObject result = Instantiate(RelicPrefabs[randomIndex], objPos, Quaternion.identity) as GameObject;
                    result.transform.SetParent(lvlHolder.transform);
                }
                else if (LevelLayoutArray[x, y] == "5")
                {
                    Instantiate(PickableCompassPrefab, objPos, Quaternion.identity);
                }
                else if(LevelLayoutArray[x, y] == "10")
                {
                    Instantiate(SlimePrefab, objPos, Quaternion.identity);

                    int randomIndex2 = Random.Range(0, floorPrefabs.Length);
                    GameObject result = Instantiate(floorPrefabs[randomIndex2], objPos, Quaternion.identity) as GameObject;

                    result.transform.SetParent(lvlHolder.transform);
                }
                //else if (LevelLayoutArray[x, y] == "20")
                //{
                //    result = Instantiate(GolemPrefab, objPos, Quaternion.identity) as GameObject;
                //}
                else if (LevelLayoutArray[x, y] == "50")
                {
                    Instantiate(DragonPrefab, objPos, Quaternion.identity);

                    int randomIndex2 = Random.Range(0, floorPrefabs.Length);
                    GameObject result = Instantiate(floorPrefabs[randomIndex2], objPos, Quaternion.identity) as GameObject;

                    result.transform.SetParent(lvlHolder.transform);
                }
                else
                {
                    int randomIndex2 = Random.Range(0, floorPrefabs.Length);
                    GameObject result = Instantiate(floorPrefabs[randomIndex2], objPos, Quaternion.identity) as GameObject;

                    result.transform.SetParent(lvlHolder.transform);
                }
                
            }
        }
	}

    // splits a CSV file into a 2D string array
	static public string[,] SplitCsvGrid(string csvText)
	{
		string[] lines = csvText.Split("\n"[0]); 
 
		// finds the max width of row
		int width = 0; 
		for (int i = 0; i < lines.Length; i++)
		{
			string[] row = SplitCsvLine( lines[i] ); 
			width = Mathf.Max(width, row.Length); 
		}
 
		// creates new 2D string grid to output to
		string[,] outputGrid = new string[width + 1, lines.Length + 1]; 
		for (int y = 0; y < lines.Length; y++)
		{
			string[] row = SplitCsvLine( lines[y] ); 
			for (int x = 0; x < row.Length; x++) 
			{
				outputGrid[x,y] = row[x]; 
 
				// This line was to replace "" with " in my output. 
				// Include or edit it as you wish.
				outputGrid[x,y] = outputGrid[x,y].Replace("\"\"", "\"");
			}
		}
 
		return outputGrid; 
	}
    // splits a CSV row 
    static public string[] SplitCsvLine(string line)
    {
        return (from System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line,
        @"(((?<x>(?=[,\r\n]+))|""(?<x>([^""]|"""")+)""|(?<x>[^,\r\n]+)),?)",
        System.Text.RegularExpressions.RegexOptions.ExplicitCapture)
                select m.Groups[1].Value).ToArray();
    }
}
