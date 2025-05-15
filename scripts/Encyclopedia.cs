using Godot;
using System;
using System.Collections.Generic;

public class Encyclopedia : CanvasLayer
{
	public static GridContainer iconsContainer;
	public static TileSet tileSet;
	public static TileSet iconTileSet;
	[Export]
	public static int TILE_SIZE { get; set; } = 300;
	[Export]
	public static float IMAGE_SCALE { get; set; } = 0.5f;
	[Export]
	public static int ATLAS_SOURCE_SPACING { get; set; } = 0;
	public static Texture TILES_ATLAS_SOURCE = (Texture)GD.Load("res://assets/tiles.png");
	public static Texture HABITATS_ATLAS_SOURCE = (Texture)GD.Load("res://assets/hab tiles.png");
	public override void _Ready()
	{
		iconsContainer = GetNode("ScrollContainer").GetNode<GridContainer>("IconsContainer");
		tileSet = GetTree().Root.GetNode("Main").GetNode<TileMap>("TileMap").TileSet;
		iconTileSet = GetTree().Root.GetNode("Main").GetNode<TileMap>("IconTileMap").TileSet;
		

		GetImages();
	}
	public override void _Process(float delta) {
		Vector2 pageSize = OS.GetScreenSize()*0.8f;
		pageSize.x = pageSize.x/2;
		iconsContainer.SetSize(pageSize);
	}

	private void GetImages()
	{
		// Habitats
		foreach (KeyValuePair<Vector2, Habitat> habitat in HabitatHandler.habitats) {
			TextureRect rect = new TextureRect();
			rect.Texture = ImageFromAtlas(true, habitat.Key);
			iconsContainer.AddChild(rect);
			GD.Print("Added habitat " + habitat.Key);
		}
		foreach(KeyValuePair<Vector2, PackedScene> tile in TileHandler.tiles) {
			if (tile.Key == new Vector2(-1,-1)) continue;
			TextureRect rect = new TextureRect();
			rect.Texture = ImageFromAtlas(false, tile.Key);
			// rect.Modulate = rgba(252525);
			iconsContainer.AddChild(rect);
			// GD.Print("Added tile " + tile.Key):
		}
	}

	private ImageTexture ImageFromAtlas(Boolean isIconMap, Vector2 atlasCoord) {
		Texture atlasTexture;
		if (isIconMap) {
			atlasTexture = HABITATS_ATLAS_SOURCE;
		} else {
			atlasTexture = TILES_ATLAS_SOURCE;
		}

		// Convert the texture to an Image
		Image atlasImage = atlasTexture.GetData();
		atlasImage.Lock(); // Needed to access pixel data

		// Calculate the pixel position of the tile
		Vector2 pixelPosition = new Vector2(
			atlasCoord.x * (TILE_SIZE + ATLAS_SOURCE_SPACING),
			atlasCoord.y * (TILE_SIZE + ATLAS_SOURCE_SPACING)
		);

		ImageTexture texture = new ImageTexture();
		Image tileImage = new Image();
		tileImage.Create(TILE_SIZE, TILE_SIZE, false, atlasImage.GetFormat());
		tileImage.BlitRect(atlasImage, new Rect2(pixelPosition, new Vector2(TILE_SIZE, TILE_SIZE)), Vector2.Zero);
		tileImage.Resize((int) Math.Round(TILE_SIZE * IMAGE_SCALE), (int) Math.Round(TILE_SIZE * IMAGE_SCALE));
		tileImage.Unlock();
		texture.CreateFromImage(tileImage);
		return texture;
		
	}
}
