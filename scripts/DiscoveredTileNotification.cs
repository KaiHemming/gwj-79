using Godot;
using System;

public class DiscoveredTileNotification : Control
{
	private void _on_Timer_timeout()
	{
		QueueFree();
	}
}
