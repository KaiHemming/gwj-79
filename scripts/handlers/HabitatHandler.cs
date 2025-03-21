using Godot;
using System.Collections.Generic;

public class HabitatHandler
{
	// Habitats	
	public static Fox fox = new Fox();

	// Tuples of habitat, requirements of land, requirements of icons, and required tile to be placed on.
	public static (Habitat, LandRequirement[], IconRequirement[], Vector2[])[] requirementMapping = {
		( fox, 
			new LandRequirement[] { new LandRequirement(TileHandler.grassScene, 2)}, 
			new IconRequirement[]{}, 
			new Vector2[]{new Vector2(0,0)}
		)
	};

	// converting atlas coordinates to habitats
	// atlas coordinate 
	private static Dictionary<Vector2, Habitat> habitats = new Dictionary<Vector2,Habitat>{
		{new Vector2(0,0), fox}
	};

	public static Habitat GetTileScene(Vector2 atlasCoord) {
		return habitats[atlasCoord];
	}
}
