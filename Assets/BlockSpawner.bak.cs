using SimpleJSON;
using System.Collections;
using System;
using UnityEngine;
//using WebSocketSharp;
using WebSocketCSharp;

public class BlockSpawner2 : MonoBehaviour
{

  public GameObject block;
  public GameObject transaction;

  public GameObject txPrefab;

  private Rigidbody rb;
  private int count;

  private string blockUrl = "https://blockchain.info/latestblock";

  private string wsUrl = "ws://localhost:8080/";

  //em-websocket-proxy -p 3000 -r ws.blockchain.info -q 80

  private WebSocket ws;

  //IEnumerator Start()
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    count = 0;

    var ws = new WebSocket(wsUrl);

    // using (ws = new WebSocket(wsUrl))
    //ws.EmitOnPing = true;

    ws.OnError += (sender, e) =>
      Console.WriteLine("error");

    ws.OnOpen += (sender, e) =>
      Console.WriteLine("open");

    // ws.OnMessage += (sender, e) =>
    //  Console.WriteLine("e.Data");

    ws.Connect();

    ws.OnMessage += Ws_OnMessage;

    //ws.Send("{\"op\": \"unity_start\"}");
    //ws.Send("{\"op\":\"ping\"}");
    //ws.Send("{\"op\":\"unconfirmed_sub\"}");

    //print(ws.IsAlive);
    //ws.Ping();

    //doStuff = DoEvent();
    //StartCoroutine(doStuff);
  }

  private void Ws_OnMessage(object sender, MessageEventArgs evt)
  {
    print("received message");

    string text = evt.Data;
    //print(text);

    var blockData = JSON.Parse(text);

    //string value = blockData["op"].Value;
    //print(value);

    //string value = blockData["x"][...]Value;
    //print(value);

    //print(blockData.Value);
    //print(blockData["block_index"].Value);


  }

  // private IEnumerator doStuff;
  //
  // private IEnumerator DoEvent()
  // {
  //   yield return new WaitForSeconds(3f);
  //   ws.Send("asd");
  //   print("added");
  // }

}
