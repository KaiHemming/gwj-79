using Godot;
using System;

public class Camera2D : Godot.Camera2D
{
	float ZOOM_SPEED = 0.15f;
	float MOVEMENT_SPEED = 0.02f;
	float MIN_ZOOM = 0.5f;
	float MAX_ZOOM = 4f;
	public Vector2 START_POS = new Vector2(9,3);
	public TileMap tileMap;

	public override void _Ready()
	{
		tileMap = GetNode<TileMap>("TileMap");
		CenterScreenOnTileMap();
	}

	public void CenterScreenOnTileMap() {
		GD.Print(START_POS);
		GD.Print(tileMap.GetTileGlobalPosition(START_POS));
		var center = tileMap.GetTileGlobalPosition(START_POS) + (tileMap.CellSize/2 * Scale);
		center.y -= 45 * Scale.y;
		center.x -= tileMap.xDiff * Scale.x;
		Position = center - GetViewportRect().Size/2;

		GD.Print("pos " + Position + " center " + center);
	}
	
	public override void _Process(float delta) {
		if (Input.IsActionPressed("right_click")) {
			Vector2 movement = Input.GetLastMouseSpeed();
			movement *= MOVEMENT_SPEED;
			Position += movement;
		}
		else if (Input.IsActionPressed("snap")) {
			CenterScreenOnTileMap();
		}
	}
	public override void _Input(InputEvent inputEvent) {
		if (GetParent().GetNode<Sprite>("Sprite").paused) return;
		if (inputEvent.IsActionPressed("zoom_in")) {
			var newScale = Mathf.Clamp(Scale.x + ZOOM_SPEED, MIN_ZOOM, MAX_ZOOM);
			this.Scale = new Vector2(newScale, newScale);
		}
		else if (inputEvent.IsActionPressed("zoom_out")) {
			var newScale = Mathf.Clamp(Scale.x - ZOOM_SPEED, MIN_ZOOM, MAX_ZOOM);
			this.Scale = new Vector2(newScale, newScale);
		}
	}
}
