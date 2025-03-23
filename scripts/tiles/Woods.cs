using Godot;
using System;

public class Woods : Tile
{
	public Woods() {
		name = "Woods";
		score = 3;
		atlasCoord = new Vector2 (6,0);
		discoveryTitle = "You discovered woods! +3 points";
		discoveryAddition = 0;
		discoveryDescription = "By placing grass next to 3 grass.";
	}
}
