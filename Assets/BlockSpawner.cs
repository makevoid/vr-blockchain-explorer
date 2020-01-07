using SimpleJSON;
using System.Collections;
using System;
using UnityEngine;
//using WebSocketSharp;
using WebSocketCSharp;
using System.Collections.Generic;

public class BlockSpawner : MonoBehaviour
{

  public GameObject block;
  public GameObject transaction;

  public GameObject txPrefab;

  private Rigidbody rb;
  private int count;

  private string blockUrl = "https://blockchain.info/latestblock";

  private string wsUrl = "ws://localhost:8080/";

  Stack<string> TXs = new Stack<string>();
  Dictionary<string, string> TXValues = new Dictionary<string, string>();
  // TXValues[$"{txId}:hash:{hash}"]
  // TXValues[$"{txId}:size:{size}"]


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

    var renderBlocks = RenderBlocks();
    StartCoroutine(renderBlocks);
  }

  private IEnumerator RenderBlocks()
  {
    while (true)
    {
      yield return new WaitForSeconds(0.1f);

      bool txPresent = TXs.Count > 0;
      if (txPresent)
      {
        string txId = TXs.Pop();

        int dim = 10;
        int dimY = 50;

        string size = TXValues[txId+":size"];
        Debug.Log("txId: " + txId);
        Debug.Log("size: " + size + " bytes");
        Debug.Log("...");


        // BIP? - inizio dell'hex e' un colore
        System.Random rnd = new System.Random();
        float xRand = rnd.Next(-dim, dim);
        float yRand = rnd.Next(dimY, dimY * 2);
        float zRand = rnd.Next(-dim, dim);
        xRand = xRand + (float)rnd.NextDouble();
        yRand = yRand + (float)rnd.NextDouble();
        zRand = zRand + (float)rnd.NextDouble();

        Vector3 position = new Vector3(xRand, yRand, zRand);
        GameObject txInstance = Instantiate(txPrefab, position, Quaternion.identity);

        //Debug.Log(txId.Substring(0, 2));
        //Debug.Log(int.Parse(txId.Substring(0, 2), System.Globalization.NumberStyles.HexNumber));
        //Debug.Log("---");
        float red   = int.Parse(txId.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        float green = int.Parse(txId.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        float blue  = int.Parse(txId.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        //Debug.Log(">>>" + green / 255f);
        //Debug.Log(">>2" + txId.Substring(2, 4));
        
        txInstance.GetComponent<Renderer>().material.color = new Color(red / 255f, green / 255f, blue / 255f, 0.5f);

      }

    };

  }

  private void Ws_OnMessage(object sender, MessageEventArgs evt)
  {
    //Debug.Log("received message");
    string text = evt.Data;
    var txData = JSON.Parse(text);

    string value = txData["op"].Value;
    //Debug.Log(value);

    var tx = txData["x"];
    //Debug.Log("hash: "+tx["hash"].Value);   // hash
    //Debug.Log("size: "+tx["size"].Value);   // block size

    string id   = tx["hash"].Value; 
    string size = tx["size"].Value;

    TXs.Push(id);
    TXValues[id+":size"] = size;
  }

}


/// notes

// requires CS 6
//
// var size = tx["size"].Value;
// print($"size: {size}");   // block size


//string value = blockData["x"][...]Value;
//print(value);

//print(blockData.Value);
//print(blockData["block_index"].Value);

//GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
//txInstance.AddComponent<Rigidbody>();
//cube.transform.position = new Vector3(xRand, yRand, zRand);