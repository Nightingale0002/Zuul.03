using System;
using System.Transactions;
using NAudio.Wave;

class Game
{
	// Private fields
	private WaveOutEvent outputDevice;
	private AudioFileReader audioFile;

	private Player player ;
	private Parser parser ;

	// Constructor
	public Game()
	{
		parser = new Parser();
		player = new Player(this);	
		CreateRooms();
	}



	// Initialise the Rooms (and the Items)
	private void CreateRooms()
	{
		// Create the rooms
		Room outside = new Room("are outside the main entrance of the university"," outside");
		Room theatre = new Room("are in a lecture theatre","theatre");
		Room pub = new Room("are in the campus pub","pub");
		Room lab = new Room("are in a computing lab","lab");
		Room office = new Room("are in the computing admin office","office");
        Room test = new Room ("are in the test room","test"); 
		Room the_woods = new Room ("are in a thick forest , all alone","the woods");
		Room house = new Room ("see a log cabin","house");
		Room living_room= new Room ("are in a living room ","living room");
		Room bathroom= new Room ("need to pee or something , what are you doing in the bathroom","bathroom");
		Room bedroom= new Room ("are in a bedroom.","bedroom");    
		Room empty = new Room ("mpty","MT");

		// Initialise room exits
		outside.AddExit("east", theatre);
		outside.AddExit("south", lab);
		outside.AddExit("west", pub);
        outside.AddExit("up", test);

        test.AddExit("west", pub);
        test.AddExit("down", outside);

		theatre.AddExit("west", outside);
		theatre.AddExit("east",the_woods);


		pub.AddExit("east", outside);
      
		lab.AddExit("north", outside);
		lab.AddExit("east", office);

		the_woods.AddExit("north", house);

		house.AddExit("outside",the_woods);
		house.AddExit("bathroom",bathroom);
		house.AddExit("bedroom",bedroom);
		house.AddExit("livingroom",living_room);

		office.AddExit("west", lab);
		
		// Create your Items here
	Item chainsaw = new Item (5,"Denji I never went to school either");
	Item coin = new Item (1,"A gold coin glimmering with value");
	Item bomb = new Item (10,"A big red button that says DO NOT PRESS");
	Item bigdumbell = new Item (30,"A heavy dumbell, looks like you could lift it");
	Item potion = new Item (1,"A mysterious potion that smells like strawberries");
	Item key = new Item (1,"A small key, it might open a door");
	Item rum = new Item (2,"A bottle of rum, it smells like it could get you drunk");

		// And add them to the Rooms
	living_room.chest.Put("chainsaw", chainsaw);
	player.backpack.Put("coin", coin);
	lab.chest.Put("bomb", bomb);
	pub.chest.Put("big dumbell", bigdumbell);
	house.chest.Put("potion", potion);
	the_woods.chest.Put("key", key);
	pub.chest.Put("rum", rum);

		// Start game outside
	  player.CurrentRoom = outside;
	}

	//  Main play routine. Loops until end of play.
	public void Play()
	{
		PrintWelcome();

		// Enter the main command loop. Here we repeatedly read commands and
		// execute them until the player wants to quit.
		bool finished = false;
		while (!finished)
		{
			Command command = parser.GetCommand();
			finished = ProcessCommand(command);
		}
		Console.WriteLine("Thank you for playing.");
		Console.WriteLine("Press [Enter] to continue.");
		Console.ReadLine();
	}

	// Print out the opening message for the player.
	private void PrintWelcome()
	{
		Console.WriteLine();
		Console.WriteLine("Welcome to Zuul!");
		Console.WriteLine("Zuul is a new, incredibly boring adventure game.");
		Console.WriteLine("Type 'help' if you need help.");
		Console.WriteLine();
	Console.WriteLine(player.CurrentRoom.GetLongDescription());
	sound("C:\\Users\\victushp\\Documents\\Zuul\\Zuul\\sounds\\welcome.mp3");
	}

	// Given a command, process (that is: execute) the command.
	// If this command ends the game, it returns true.
	// Otherwise false is returned.
	private bool ProcessCommand(Command command)
	{
		bool wantToQuit = false;

		if(command.IsUnknown())
		{
			Console.WriteLine("I don't know what you mean...");
			return wantToQuit; // false
		}

		switch 	(command.CommandWord)
		{	
			case "mute":
				StopSound();
				break;
			case "use":
				Use(command.SecondWord);
				break;
			case "bag":
				show();
				break;
			case "drop":
				Put(command.SecondWord);
				break;
			case "take":
				get(command.SecondWord);
				break; 
			case "dmg":
				damageplayer(20);
				break;
			case "heal":
				healplayer(20);
				break;
			case "status":
				status() ;
				break;
			case "look": 
				look();
				break;
			case "Blow": 
			    Blowup(99);
			    break;
			case "help":
				PrintHelp();
				break;
			case "go":
				GoRoom(command);
				break;
			case "quit":
				wantToQuit = true;
				break;
				
			

		}

		return wantToQuit;
	}

	// ######################################
	// implementations of user commands:
	// ######################################
	
	// Print out some help information.
	// Here we print the mission and a list of the command words.
	private void PrintHelp()
	{
		Console.WriteLine("You are lost. You are alone.");
		Console.WriteLine("You wander around at the university.");
		Console.WriteLine();
		// let the parser print the commands
		parser.PrintValidCommands();
	}

	// Try to go to one direction. If there is an exit, enter the new
	// room, otherwise print an error message.
	public	void GoRoom(Command command)
	{
		if(!command.HasSecondWord())
		{
			// if there is no second word, we don't know where to go...
			Console.WriteLine("Go where?");
			sound("C:\\Users\\victushp\\Documents\\Zuul\\Zuul\\sounds\\dum.mp3");
			return;
			
		}

		string direction = command.SecondWord;

		// Try to go to the next room.
		Room nextRoom = player.CurrentRoom.GetExit(direction);
		if (nextRoom == null)
		{
			Console.WriteLine("There is no door to "+direction+"!");
			sound("C:\\Users\\victushp\\Documents\\Zuul\\Zuul\\sounds\\bump.mp3");
			return;
		}

		player.CurrentRoom = nextRoom;
		Console.WriteLine(player.CurrentRoom.GetLongDescription());
		sound("C:\\Users\\victushp\\Documents\\Zuul\\Zuul\\sounds\\walk.mp3");
	} 
	
	public void Blowup(int damage)
	{
		player.health -= damage;
		if (player.health <= 0)
		{	
			Console.WriteLine("you exploded");
			Console.WriteLine("Game Over");
			Environment.Exit(0);	
			sound("C:\\Users\\victushp\\Documents\\Zuul\\Zuul\\sounds\\New_Project.wav");	 
		} else {
			Console.WriteLine("how are you still alive after that?");
			sound("C:\\Users\\victushp\\Documents\\Zuul\\Zuul\\sounds\\New_Project.wav");
		}
	}
	
	private void look()
	{
		Console.WriteLine ("you " +player.CurrentRoom.GetShortDescription());
		player.CurrentRoom.chest.showroom();
	} 
	
	private void status()
	{
	
		Console.WriteLine("you have " +player.health+" Life points");
    }

	public void damageplayer(int damage)
	{
		player.health -= damage;
		if (player.health <= 0)
		{
			Console.WriteLine("you died");
			Console.WriteLine("Game Over");
			Environment.Exit(0);
		}else {
			Console.WriteLine("you have " +player.health+" Life points left");
		}
	}
	public void healplayer(int heal)
	{
		player.health += heal;
		Console.WriteLine("you have " +player.health+" Life points now");
	}

	public void show()
	{
		player.backpack.show();
	}
	
	private void get(string itemName)
	{
    Item item = player.CurrentRoom.chest.Get(itemName);

    if (item == null)
    	{
        Console.WriteLine("That item is not here.");
        return;
    	}

    if (player.backpack.Put(itemName, item))
    	{
        Console.WriteLine("You picked up the " + itemName);
    	}
    else
   		{
        // terugleggen als inventory vol is
        player.CurrentRoom.chest.Put(itemName, item);
		Console.WriteLine("You are too weak."); 
		}
	}
	private void Put(string itemName)
	{
    	Item item = player.backpack.Get(itemName);

    	if (item == null)
    	{
        Console.WriteLine("You don't have that item.");
        return;
    	}

    	player.CurrentRoom.chest.Put(itemName, item);
    	Console.WriteLine("You dropped the " + itemName);
	}
	
	private void Use(string itemName )
	{
    	string result = player.Use(itemName, player.backpack);
    	Console.WriteLine(result);
	}
	public void sound(string soundfile)
	{
		StopSound();
		audioFile = new AudioFileReader(soundfile);
		outputDevice = new WaveOutEvent();
		outputDevice.Init(audioFile);
		outputDevice.Play();
	}

	public void StopSound()
	{
	if (outputDevice != null)
		{
		outputDevice.Stop();
		outputDevice.Dispose();
		outputDevice = null;
		}
	if (audioFile != null)
		{
		audioFile.Dispose();
		audioFile = null;
		}
	}
	private void inspect(Command command)
	{
		if (!command.HasSecondWord())
		{
			Console.WriteLine("Inspect what?");
			return;
		}
		string item = command.SecondWord;
		if (player.CurrentRoom.chest.Has(item))
		{
			Console.WriteLine(player.CurrentRoom.chest.Get(item).GetDescription());
		}
		else
		{
			Console.WriteLine("There is no " + item + " here.");
		}
		
	}
	public string win()
	{
		Console.WriteLine("you drank the rum and got drunk");
		Console.WriteLine("you win");
		Environment.Exit(0);
		return "you win";
	}



}



