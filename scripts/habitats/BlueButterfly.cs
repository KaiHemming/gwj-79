using Godot;
using System;

public class BlueButterfly : Habitat
{
	public BlueButterfly() {
		atlasCoord = new Vector2(9,0);
		name = "Blue Butterfly";
        score = 6;
        discoveryAddition = 3;
		discoveryTitle = "A Blue Butterfly has moved in! +6 points";
        discoveryDescription = "A rare find! +1 per wildflower neighbour + 3 habitat tokens.";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
		if (atlasCoord == new Vector2(5,0)) {
            AddToScore(1);
        }
		return atlasCoord;
	}
}
