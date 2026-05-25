using System.Collections.Generic;
using System.Security.Principal;

class Room
{
	// Private fields
	public Inventory chest;
	private string description;
	private string isname { get; set; }
	private Dictionary<string, Room> exits; // stores exits of this room.
	public Inventory Chest
	{
		get { return chest; }
	}
	// Create a room described "description". Initially, it has no exits.
	// "description" is something like "in a kitchen" or "in a court yard".
	public Room(string desc,string name)
	{
		description = desc;
		exits = new Dictionary<string, Room>();

		isname = name;
	
		chest = new Inventory(999999);
	}

	// Define an exit for this room.
	public void AddExit(string direction, Room neighbor)
	{
		exits.Add(direction, neighbor);
	}

	// Return the description of the room.
	public string GetShortDescription()
	{
		return description;
	}

	// Return a long description of this room, in the form:
	//     You are in the kitchen.
	//     Exits: north, west
	public string GetLongDescription()
	{
		string str = "You ";
		str += description;
		str += ".\n";
		str += GetExitString();
		return str;
	}

	// Return the room that is reached if we go from this room in direction
	// "direction". If there is no room in that direction, return null.
	public Room GetExit(string direction)
	{
		if (exits.ContainsKey(direction))
		{
			return exits[direction];
		}
		return null;
	}

	// Return a string describing the room's exits, for example
	// "Exits: north, west".
	private string GetExitString()
	{
		string str = "you can go: ";
		str += String.Join(", ", exits.Keys);

		return str;
	}









}
