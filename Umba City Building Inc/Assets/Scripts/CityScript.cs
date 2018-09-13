using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityScript : MonoBehaviour
{
	public int Cash { get; set; }
	public int Day { get; set; }
	public float PopulationCurrent { get; set; }
	public float PopulationCeiling { get; set; }
	public int JobsCurrent { get; set; }
	public int JobsCeiling { get; set; }
	public float Food { get; set; }

	public int[] buildingCount = new int[4];

	private UIControllerScript uiController;

	private void Start()
	{
		uiController = GetComponent<UIControllerScript>();
		Cash = 10000;
		Food = 6;
		JobsCeiling = 10;
	}
	public void EndTurn()
	{
		Day++;
		CalculatePopulation();
		CalculateJobs();
		CalculateCash();
		CalculateFood();
		Debug.Log("Day ended");
		uiController.UpdateCityData();
		uiController.UpdateDayCount();
		Debug.LogFormat
			("Jobs: {0}/{1}, Cash: {2}. Population: {3}/{4}, Food:{5}",
			JobsCurrent,JobsCeiling,Cash,PopulationCurrent,PopulationCeiling,Food);
	}

	void CalculateJobs()
	{
		JobsCeiling = buildingCount[3] * 10;
		JobsCurrent = Mathf.Min((int)PopulationCurrent, JobsCeiling);
	}

	private void CalculateCash()
	{
		Cash += JobsCurrent * 2;
	}
	public void DepositCash(int cash)
	{
		Cash += cash;
	}
	private void CalculateFood()
	{
		Food += buildingCount[2] * 4f;
	}

	private void CalculatePopulation()
	{
		PopulationCeiling = buildingCount[1] * 5;
		if (Food>=PopulationCurrent&&PopulationCurrent<PopulationCeiling)
		{
			Food -= PopulationCurrent*.25f;
			PopulationCurrent = Mathf.Min(PopulationCurrent += Food * .25f, PopulationCeiling);

		}else if (Food < PopulationCurrent)
		{
			PopulationCurrent -= (PopulationCurrent - Food) * 5f;
		}
	}
}
