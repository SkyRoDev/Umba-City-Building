using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingControlerScript : MonoBehaviour {
	
	[SerializeField]
	private CityScript city;
	[SerializeField]
	private UIControllerScript uiController;
	[SerializeField]
	private BuildingScript[] buildings;
	[SerializeField]
	private BoardScript board;
	private BuildingScript selectedBuilding;

	private void Update()
	{
		if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift) && selectedBuilding != null)
		{
			InteractWithBoard(0);
		}
		else if (Input.GetMouseButtonDown(0) && selectedBuilding !=null)
		{
			InteractWithBoard(0);
		}

		if (Input.GetMouseButtonDown(1))
		{
			InteractWithBoard(1);
		}
	}

	void InteractWithBoard(int action)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray,out hit))
		{
			Vector3 gridPosition = board.CalculateGridPosition(hit.point);
			if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
			{
				if (action == 0 && board.CheckForBuildingAtPosition(gridPosition) == null)
				{
					if (city.Cash >= selectedBuilding.buildingCost)
					{
						city.DepositCash(-selectedBuilding.buildingCost);
						uiController.UpdateCityData();
						city.buildingCount[selectedBuilding.buildingId]++;
						board.AddBuilding(selectedBuilding, gridPosition);
					}
				}
			}
			else if (action == 1)
			{
				
				city.DepositCash(board.CheckForBuildingAtPosition(gridPosition).buildingCost/2);
				board.RemoveBuilding(gridPosition);
				uiController.UpdateCityData();
				city.buildingCount[selectedBuilding.buildingId]--;
			}
		}
	}

	public void EnableBuildilder(int building)
	{
		selectedBuilding = buildings[building];
		Debug.Log("Selected building: " + selectedBuilding.buldingName);
	}
	
}
