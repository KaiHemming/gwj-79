using Godot;
using System;

public class EndScreen : Control
{
	public void SetScore(int score) {
		GetNode("VBoxContainer").GetNode("HBoxContainer").GetNode<Label>("Score").Text = "" + score;
	}
}
