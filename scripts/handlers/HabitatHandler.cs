using Godot;
using System.Collections.Generic;

public class HabitatHandler
{
	// Habitats	
	public static Fox fox = new Fox();
	public static Badger badger = new Badger();
	public static Bee bee = new Bee();
	public static Dragonfly dragonfly = new Dragonfly();
	public static Fish fish = new Fish();


	// Tuples of habitat, requirements of land, requirements of icons, and required tile to be placed on.
	public static (Habitat, LandRequirement[], IconRequirement[], Vector2[])[] requirementMapping = {
		( badger, 
			new LandRequirement[]{}, 
			new IconRequirement[]{}, 
			new Vector2[]{new Vector2(0,0)}
		),
		( bee,
			new LandRequirement[]{},
			new IconRequirement[]{},
			new Vector2[]{new Vector2(5,0), new Vector2(1,0)}),
		(dragonfly,
			new LandRequirement[]{},
			new IconRequirement[]{},
			new Vector2[] {new Vector2(3,0)}),
		(fish, 
			new LandRequirement[]{},
			new IconRequirement[]{},
			new Vector2[]{new Vector2(2,0)})
	};

	// converting atlas coordinates to habitats
	// atlas coordinate 
	private static Dictionary<Vector2, Habitat> habitats = new Dictionary<Vector2,Habitat>{
		{new Vector2(0,0), badger},
		{new Vector2(1,0), bee},
		{new Vector2(2,0), dragonfly},
		{new Vector2(3,0), fish}
	};

	public static Habitat GetHabitat(Vector2 atlasCoord) {
		return habitats[atlasCoord];
	}
}
