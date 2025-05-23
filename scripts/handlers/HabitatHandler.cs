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
	public static HarvesterAnts harvesterAnts = new HarvesterAnts();
	public static BlueButterfly blueButterfly = new BlueButterfly();
	public static KingFisher kingFisher = new KingFisher();
	public static Newt newt = new Newt();
	public static Worm worm = new Worm();
	public static GreenTigerBeetle greenTigerBeetle = new GreenTigerBeetle();
	public static SmallWhiteButterfly smallWhiteButterfly = new SmallWhiteButterfly();


	// Tuples of habitat, requirements of land, requirements of icons, and required tile to be placed on.
	public static (Habitat, LandRequirement[], IconRequirement[], Vector2[])[] requirementMapping = {
		( badger, 
			new LandRequirement[]{new LandRequirement(TileHandler.grassScene,1)}, 
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
			new Vector2[]{new Vector2(2,0)}),
		(harvesterAnts,
			new LandRequirement[]{},
			new IconRequirement[]{},
			new Vector2[]{new Vector2(6,0)}),
		(blueButterfly,
			new LandRequirement[]{},
			new IconRequirement[]{ new IconRequirement(new Vector2(5,0), 1)},
			new Vector2[]{new Vector2(5,0)}),
		(kingFisher,
			new LandRequirement[]{new LandRequirement(TileHandler.waterScene, 1)},
			new IconRequirement[]{new IconRequirement(fish.atlasCoord,1)},
			new Vector2[]{new Vector2(6,0)}),
		(newt,
			new LandRequirement[]{new LandRequirement(TileHandler.marshScene,1), new LandRequirement(TileHandler.grassScene,1)},
			new IconRequirement[]{},
			new Vector2[]{new Vector2(2,0)}),
		(worm, 
			new LandRequirement[]{},
			new IconRequirement[]{},
			new Vector2[]{new Vector2(0,0)}),
		(smallWhiteButterfly,
			new LandRequirement[]{},
			new IconRequirement[]{},
			new Vector2[]{new Vector2(5,0)}),
		(greenTigerBeetle,
			new LandRequirement[]{},
			new IconRequirement[]{new IconRequirement(blueButterfly.atlasCoord, 1)},
			new Vector2[]{new Vector2(1,0), new Vector2(5,0)}),
		(greenTigerBeetle,
			new LandRequirement[]{},
			new IconRequirement[]{new IconRequirement(smallWhiteButterfly.atlasCoord, 1)},
			new Vector2[]{new Vector2(1,0), new Vector2(5,0)})
	};

	// converting atlas coordinates to habitats
	// atlas coordinate 
	public static Dictionary<Vector2, Habitat> habitats = new Dictionary<Vector2, Habitat>{
		{new Vector2(0,0), badger},
		{new Vector2(1,0), bee},
		{new Vector2(2,0), dragonfly},
		{new Vector2(3,0), fish},
		{new Vector2(5,0), harvesterAnts},
		{new Vector2(9,0), blueButterfly},
		{new Vector2(10,0), kingFisher},
		{new Vector2(11,0), newt},
		{new Vector2(7,0), worm},
		{new Vector2(8,0), smallWhiteButterfly},
		{new Vector2(6,0), greenTigerBeetle}
	};

	public static Habitat GetHabitat(Vector2 atlasCoord) {
		return habitats[atlasCoord];
	}
}
