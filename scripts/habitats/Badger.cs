using Godot;
using System;

public class Badger : Habitat
{
	public Badger() {
		atlasCoord = new Vector2(0,0);
		name = "Badger";
        score = 2;
		discoveryAddition = 1;
		discoveryTitle = "A badger has moved in! +2 points";
        discoveryDescription = "+1 habitat token and +1 point per grass neighbour.";
	}

    public override Vector2 GetUpdatedTile(Vector2 atlasCoord) {
        if (atlasCoord == new Vector2(1,0)) {
            AddToScore(1);
        }
		return atlasCoord;
	}
}
