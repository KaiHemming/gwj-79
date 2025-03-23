using Godot;
using System;

public class ScoreAddition : Control
{
	private void _on_Timer_timeout()
	{
		QueueFree();
	}
	public void SetScoreAddition(int score) {
		GetNode<Label>("Label").Text = "+" + score;
	}
	public void SetScoreReduction(int score) {
		GetNode<Label>("Label").Text = "-" + score;
	}
}
