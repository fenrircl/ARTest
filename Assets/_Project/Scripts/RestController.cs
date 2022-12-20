using UnityEngine;
using UnityEditor;
using Models;
using Proyecto26;
using System.Collections.Generic;
using UnityEngine.Networking;

public class RestController : MonoBehaviour
{
    private void LogMessage(string title, string message) {
    #if UNITY_EDITOR
            EditorUtility.DisplayDialog (title, message, "Ok");
    #else
            Debug.Log(message);
    #endif
        }
	public void Get()
    {

		// We can add default request headers for all requests
		RestClient.DefaultRequestHeaders["Authorization"] = "Bearer ...";

		RequestHelper requestOptions = null;

		RestClient.GetArray<Post>("https://api.gateway.integritic.cl/je/getDatosColegio/").Then(res => {
			this.LogMessage("Posts", JsonHelper.ArrayToJsonString<Post>(res, true));
			return RestClient.GetArray<Todo>("https://api.gateway.integritic.cl/je/getDatosColegio/");
		}).Catch(err => this.LogMessage("Error", err.Message));
	}


}
