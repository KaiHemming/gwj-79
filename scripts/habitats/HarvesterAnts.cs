using Godot;
using System;

public class HarvesterAnts : Habitat
{
	public HarvesterAnts() {
		atlasCoord = new Vector2(5,0);
		name = "Harvester Ants";
        score = 1;
        discoveryAddition = 3;
		discoveryTitle = "Harvester ants have moved in! +1 point";
        discoveryDescription = "+3 habitat tokens have been added to your bag.";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		return atlasCoord;
	}
}
