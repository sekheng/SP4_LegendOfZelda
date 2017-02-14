using UnityEngine;
using System.Collections;
using System.Linq; 

public class CSVReader : MonoBehaviour {

    public TextAsset file;

    public GameObject WallPrefab;
    public GameObject PlayerPrefab;
    public GameObject StonePrefab;
    public GameObject TreasurePrefab;
    public GameObject EnemyPrefab;
    public GameObject floorPrefab;

    private string[,] LevelLayoutArray;
    private string [] textRow;
    private GameObject lvlHolder;

	// Use this for initialization
	void Start () {
        lvlHolder = new GameObject("LevelHolder");

	    LevelLayoutArray = CSVReader.SplitCsvGrid(file.text);
        textRow = new string[LevelLayoutArray.GetUpperBound(0)];  // Instantiate the textRow array with the number of rows in csvData array.

        for (int y = 0; y < ((LevelLayoutArray.Length / textRow.Length) - 1) ; y++) // height
        {
            for (int x = 0; x < textRow.Length - 1; x++)
            {
                Vector3 objPos = new Vector3(x, -y, 0);
                GameObject result;
                Debug.Log(LevelLayoutArray[x, y]);
                if (LevelLayoutArray[x, y] == "1")
                {
                    result = Instantiate(WallPrefab, objPos, Quaternion.identity) as GameObject;
                }
                else if (LevelLayoutArray[x, y] == "2")
                {
                    result = Instantiate(StonePrefab, objPos, Quaternion.identity) as GameObject;
                }
                else if (LevelLayoutArray[x, y] == "3")
                {
                    result = Instantiate(PlayerPrefab, objPos, Quaternion.identity) as GameObject;
                }
                //else if (LevelLayoutArray[x, y] == "4")
                //{
                //    result = Instantiate(TreasurePrefab, objPos, Quaternion.identity) as GameObject;
                //}
                //else if (LevelLayoutArray[x, y] == "5")
                //{
                //    result = Instantiate(enemyPrefab, objPos, Quaternion.identity) as GameObject;
                //}
                else
                {
                    result = Instantiate(floorPrefab, objPos, Quaternion.identity) as GameObject;
                }
                if(!result.CompareTag("Player"))
                {
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
