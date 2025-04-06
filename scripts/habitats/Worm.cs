using Godot;
using System;

public class Worm : Habitat
{
	public Worm() {
		atlasCoord = new Vector2(7,0);
		name = "Worm";
        score = 1;
        discoveryAddition = 4;
		discoveryTitle = "A Worm has moved in! +1 point";
        discoveryDescription = "+4 habitat tokens.";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		return atlasCoord;
	}
}
