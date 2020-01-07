//----------------------------------------------
// WebSocketUnity
// Copyright (c) 2015, Jonathan Pavlou
// All rights reserved
//----------------------------------------------

using UnityEngine;
using System.Collections;

// Callbacks used by websocketunity
// A Game object (Monobehaviour) has to implement this interface

public interface WebSocketUnityDelegate {

	// Event when the connection has been opened
	void OnWebSocketUnityOpen(string sender);
	
	// Event when the connection has been closed
	void OnWebSocketUnityClose(string reason);
	
	// Event when the websocket receive a message
	void OnWebSocketUnityReceiveMessage(string message);
	
	// Event when the websocket receive data
	void OnWebSocketUnityReceiveData(byte[] data);
	
	// Event when an error occurs
	void OnWebSocketUnityError(string error);

}
