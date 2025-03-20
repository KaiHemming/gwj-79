using Godot;
using System;

public class UI : Control
{
	public override void _Process(float delta) {
		GD.Print(GetViewportRect().Size);
		this.RectSize = GetViewportRect().Size;
	}
}
