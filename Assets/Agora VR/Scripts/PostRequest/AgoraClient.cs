using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.IO;
using System;

public class AgoraClient
{
    // For running on the deployed server
    private static string apiUrl = "https://agoravr.online/api";
    // For running a local deployment
    // private static string apiUrl = "http://localhost:8080"; //

    public string token;

    public static async Task<HttpResponseMessage> PostJson<T>(string url, T payload)
    {
        var payloadString = JsonConvert.SerializeObject(payload);

        var content = new StringContent(payloadString, Encoding.UTF8, "application/json");

        using (var httpClient = new HttpClient())
        {
            return await httpClient.PostAsync(url, content);
        }
    }

    public async Task<HttpResponseMessage> GetJsonWithAuth(string endpoint)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);

            return await client.GetAsync(apiUrl + endpoint);
        }
    }

    public async Task<HttpResponseMessage> PostJsonWithAuth<T>(string url, T payload)
    {
        var payloadString = JsonConvert.SerializeObject(payload);

        var content = new StringContent(payloadString, Encoding.UTF8, "application/json");

        Debug.Log(token);

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", token);

            return await client.PostAsync(url, content);
        }
    }

    public async Task<string> authenticate(string username, string password)
    {
        var postPayload = new Dictionary<string, object>();

        postPayload.Add("user_name", username);
        postPayload.Add("user_pass", password);

        var response = await PostJson(apiUrl + "/authenticate", postPayload);

        var responseString = await response.Content.ReadAsStringAsync();

        if (response.StatusCode == HttpStatusCode.OK)
            token = responseString;

        return responseString;
    }

    public class session
    {
        public string session_id
        {
            get;
            set;
        } 
    }

    public async Task<string> CreateSession(string datetime, string typename)
    {
        var postPayload = new Dictionary<string, object>();

        postPayload.Add("datetime", datetime);
        postPayload.Add("type_name", typename);
       
        var response = await PostJsonWithAuth(apiUrl + "/session", postPayload);

        var responseString = await response.Content.ReadAsStringAsync();

        string temp;

        if (response.StatusCode == HttpStatusCode.OK)
            temp = responseString;

        var mySession = JsonConvert.DeserializeObject<session>(responseString);

        return mySession.session_id;
    }


    public async Task<string> PostFile(string filePath, string data_type, string session)
    {
        string metadata = null;
        string content_type = null;

        if (data_type == "audio_session")
        {
            content_type = "audio/mpeg";
            metadata = "{\"data_type\":\"audio_session\"}";
        }

        else if (data_type == "heart_rate_session")
        {
            content_type = "text/csv";
            metadata = "{\"data_type\":\"heart_rate_session\"}";
        }


        else if (data_type == "volume_session")
        {
            content_type = "text/csv";
            metadata = "{\"data_type\":\"volume_session\"}";
        }

        else if (data_type == "text_script")
        {
            content_type = "text/plain";
            metadata = "{\"data_type\":\"text_script\"}";
        }

        //byte[] metadtaByte = Encoding.UTF8.GetBytes(value);

        //HttpResponseMessage responseForm = Request.CreateResponse();
        try
        {
            var filestream = new FileStream(filePath, FileMode.Open);
            var fileName = System.IO.Path.GetFileName(filePath);

            using (var client = new HttpClient())
            {
                using (var form = new MultipartFormDataContent("application/json"))
                {
                    var metacontent = new StringContent(metadata, Encoding.UTF8, "application/json");
                    metacontent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    StreamContent content = new StreamContent(filestream);
                    content.Headers.ContentType = new MediaTypeHeaderValue(content_type);

                    form.Add(metacontent, "application/json", "application/json");
                    form.Add(content, "testFile", fileName);

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

                    var response = await client.PostAsync(apiUrl + "/session/" + session + "/files", form);
                    var responseString = await response.Content.ReadAsStringAsync();

                    string temp = "FAILED";

                    if (response.StatusCode == HttpStatusCode.OK)
                        temp = responseString;

                    return responseString;
                }
            }


            /*client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            HttpResponseMessage response = await client.SendAsync(requestMessage);
            
            var responseString = await response.Content.ReadAsStringAsync();

            string temp = "FAILED";

            if (response.StatusCode == HttpStatusCode.OK)
                temp = responseString;

            return responseString;*/
            
        }
        catch (Exception e)
        {
            throw;
        }

        /*
        using (var client = new HttpClient())
        {
            using (var form = new MultipartFormDataContent())
            {
                StreamContent content = new StreamContent(stream);
                content.Headers.ContentType = new MediaTypeHeaderValue(content_type);

                form.Add(metacontent, "application/json");
                //form.Add(content, "audiofile", "test.mp3");

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
                var response = await client.PostAsync(apiUrl + "/session/" + session + "/files", form);
                var responseString = await response.Content.ReadAsStringAsync();

                string temp = "FAILED";

                if (response.StatusCode == HttpStatusCode.OK)
                    temp = responseString;

                return responseString;
            }
        }*/
    }




}