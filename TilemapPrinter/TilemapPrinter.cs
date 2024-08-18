using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPrinter : MonoBehaviour
{	
	//Public
	//IS the object we scan for later printing
	public Tilemap tilemapToPrint; 
	//target Tilemap to print on
	public Tilemap worldToWrite;
	//The printing position (Bottomleftmost as 0,0
	public Vector2Int printStartPos;

	//Private 
	private Dictionary<Vector2Int, TileBase> printingData;
	private List<Vector2Int> storedCoord;

	// Start is called before the first frame update
	private void Awake()
	{
		PrintTilemap();	
	}

	public void PrintTilemap()
	{
		//Initialise Printable Temporary references
		storedCoord = new List<Vector2Int>();
		printingData = new Dictionary<Vector2Int, TileBase>();
		Vector2Int printablePivotOfffset = Vector2Int.zero;
		bool offsetDone = false;
		//Scans and storess the printable, offseting it to center bottom left most block to 0,0 
		for (int y = tilemapToPrint.cellBounds.yMin; y < tilemapToPrint.cellBounds.yMax; y++)
		{
			for (int x = tilemapToPrint.cellBounds.xMin; x < tilemapToPrint.cellBounds.xMax; x++)
			{
				Vector2Int pos = new Vector2Int(x, y);
				TileBase tiletoprint = ReadTile(x, y, tilemapToPrint);
				if (tiletoprint != null)
				{
					if (offsetDone == false)
					{
						printablePivotOfffset = pos;
						offsetDone = true;
					}
					//After finding pivot, 
					//Get pivot pos - its own position to stay at 0,0
					//set all tiles position - the pivot pos , wich is equal to the offset. 
					pos -= printablePivotOfffset;
					StorePrintableData(pos, tiletoprint);
				}			
			}
		}
		//Perform printing
		PrintStoredData(printStartPos, worldToWrite);	
	}

	private void PrintStoredData(Vector2Int printlocation, Tilemap tilemap) 
	{
		foreach (var pos in storedCoord)
		{
			PlaceTile(pos, printlocation , tilemap);
		}
		printingData.Clear();
		storedCoord.Clear();
	}
	private void PlaceTile(Vector2Int pos, Vector2Int printPos, Tilemap tilemap) 
	{
		TileBase tile = printingData[pos];
		Vector2Int finalPos = printPos + pos;
		Vector3Int worldPos = new Vector3Int(finalPos.x, finalPos.y, 0);
		tilemap.SetTile(worldPos, tile);
	}
	private void StorePrintableData(Vector2Int pos, TileBase tile) 
	{
		storedCoord.Add(pos);
		printingData.Add(pos, tile);
	}
	private TileBase ReadTile(int x, int y, Tilemap tilemap) 
	{
		Vector3Int tilepos = new Vector3Int(x, y, 0);
		TileBase tile = tilemap.GetTile(tilepos);
		 return tile;
	}
}
