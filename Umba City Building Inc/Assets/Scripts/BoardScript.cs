using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardScript : MonoBehaviour {

	private BuildingScript[,] buildings = new BuildingScript[100, 100];
	
	public void AddBuilding(BuildingScript building, Vector3 position)
	{
		buildings[(int)position.x, (int)position.z] = Instantiate(building, position, Quaternion.identity);
		
	}

	public BuildingScript CheckForBuildingAtPosition(Vector3 position)
	{
		return buildings[(int)position.x, (int)position.z] ;
	}

	public void RemoveBuilding(Vector3 position)
	{
		Destroy(buildings[(int)position.x, (int)position.z].gameObject);
		buildings[(int)position.x, (int)position.z] = null;
	}

	public Vector3 CalculateGridPosition(Vector3 position)
	{
		return new Vector3(Mathf.Round (position.x), .5f, Mathf.Round(position.z));
	}
}
