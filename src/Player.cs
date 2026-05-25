using System.Collections;
using System.Dynamic;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;

class Player
{
    private Game game;
 
    //fields
    public Inventory backpack { get;}
    public int health;
    public Room CurrentRoom { get; set; }
    // constructor
    public Player(Game game)
    {
        this.game = game;
        CurrentRoom = null;
        health = 100;
        backpack = new Inventory(25);
    }
    public string Use(string itemName, Inventory inventory)
    {
        if (itemName == null)
        return "Use what?";

        if (!inventory.Has(itemName))
        return "You don't have that item.";

        itemName = itemName.ToLower();

        if (itemName == "flashlight")
        return "You turn on the flashlight. The room is now bright.";

        if (itemName == "rum")
        return game.win();

        if (itemName== "key")
        return "the key feels inportant";

        if (itemName == "coin")
        return Sound();

        if (itemName == "chainsaw")
        return "You start the chainsaw and remove an obstacle.";   

        if (itemName == "potion")
        return heal();

        if (itemName == "bomb")
        return Damage();

        return "Nothing happens.";
    }

    // commamnds
    public string heal()
    {
    if (game != null)
        {
            game.healplayer(20);
            return "You drink the potion and feel rejuvenated!";
        }
            return "You don't have a potion to heal yourself.";
    }
    public string Damage()
    {
    if (game != null)
        {
            game.Blowup(99);
            return "You take damage! Ouch!";
        }
            return "You can't take damage right now.";}
public string Blow()
    {
    if (game != null)
        {
            game.Blowup(999);
            return "You blow on the coin. A powerful force is unleashed!";
        }
            return "You blow on the coin, but nothing happens.";
    }
    public string Sound()
    {
            if (game != null)
        {
            game.sound("C:\\Users\\victushp\\Documents\\Zuul\\Zuul\\sounds\\coin.mp3");
            return "You play a sound effect.";
        }
            return "";
    }   
    

    


}
