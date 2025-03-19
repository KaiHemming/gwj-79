using Godot;
using System.Collections.Generic;

public class HabitatHandler : Node
{
	// Habitats	
	static Fox fox = new Fox();

	// Tuples of habitat to requirements of land and requirements of icons
	public static (Habitat, LandRequirement[], IconRequirement[])[] requirementMapping = {
		( fox, new LandRequirement[] { new LandRequirement(TileHandler.grassScene, 2)}, new IconRequirement[]{})
	};

	// converting atlas coordinates to habitats
	// atlas coordinate 
	private static Dictionary<Vector2, Habitat> habitats = new Dictionary<Vector2,Habitat>{
		{new Vector2(0,0), fox}
	};
	
	// TODO:
	public static Habitat GetHabitat(Dictionary<Vector2, int> countedLandNeighboursOfType, Dictionary<Vector2, int> countedIconNeighboursOfType, Tile curTile) {
		return null;
	}

	public static Habitat GetTileScene(Vector2 atlasCoord) {
		return habitats[atlasCoord];
	}
}
