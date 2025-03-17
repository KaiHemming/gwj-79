using Godot;
using System;

public class Camera2D : Godot.Camera2D
{
	float ZOOM_SPEED = 0.2f;
	float MOVEMENT_SPEED = 0.02f;
	
	public override void _Process(float delta) {
		if (Input.IsActionPressed("right_click")) {
			Vector2 movement = Input.GetLastMouseSpeed();
			movement *= MOVEMENT_SPEED;
			Position += movement;
		}
		else if (Input.IsActionPressed("snap")) {
			Position = Vector2.Zero;
		}
	}
	public override void _Input(InputEvent inputEvent) {
		if (inputEvent.IsActionPressed("zoom_in")) {
			var newScale = Mathf.Clamp(Scale.x - ZOOM_SPEED, 0.6f, 4f);
			this.Scale = new Vector2(newScale, newScale);
		}
		else if (inputEvent.IsActionPressed("zoom_out")) {
			var newScale = Mathf.Clamp(Scale.x + ZOOM_SPEED, 0.6f, 4f);
			this.Scale = new Vector2(newScale, newScale);
		}
	}
}
