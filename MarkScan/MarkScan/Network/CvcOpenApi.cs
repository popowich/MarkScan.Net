using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows.Controls;
using MarkScan.Network.JsonWrapers;

namespace MarkScan.Network
{
    internal class CvcOpenApi
    {
        private const string Host = "https://c-v-c.ru";

        /// <summary>
        /// Singleton
        /// </summary>
        private static CvcOpenApi _cvcOpenApi;
        /// <summary>
        /// Токен авторизации при отправке данных
        /// </summary>
        private string _tokenAuth;

        /// <summary>
        /// Получить клиента API
        /// </summary>
        /// <returns></returns>
        internal static CvcOpenApi GetClientApi()
        {
            return _cvcOpenApi ?? (_cvcOpenApi = new CvcOpenApi());
        }
        /// <summary>
        /// Установить токен доступа
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        internal void SetTokenAuth(string login, string pass)
        {
            _tokenAuth = "Basic " + Tools.Base64.ToBase64(Encoding.ASCII, $"{login}:{pass}");
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="login"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        internal ResponseBase<AuthResult> Auth(string login, string pass)
        {
            SetTokenAuth(login, pass);

            var request = getHttpWebRequestt("GET", "/openapi/status", false);
            request.Headers.Add("Authorization", _tokenAuth);

            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                return (ResponseBase<AuthResult>)new DataContractJsonSerializer(typeof(ResponseBase<AuthResult>))
                    .ReadObject(responseStream);
            }
        }
        /// <summary>
        /// Отправка данных инвентаризации
        /// </summary>
        internal bool Remainings(ResultScanPosititon positions)
        {
            var request = getHttpWebRequestt("PATCH", "/openapi/remainings", true);

            new DataContractJsonSerializer(typeof(ResultScanPosititon))
                .WriteObject(request.GetRequestStream(), positions);

            var textResp = _getStringFromWebResponse(request.GetResponse());

            if (textResp.IndexOf("statusCode") > -1)
            {
                var result = (ResponseBase<SendDataResult>)new DataContractJsonSerializer(typeof(ResponseBase<SendDataResult>))
                    .ReadObject(_getMemoryStreamFromWebResponse(textResp));

                return result.Response.StatusCode == "200" || result.Response.StatusCode == "202";
            }
            else
            {
                var result = (ResponseBase<AuthResult>)new DataContractJsonSerializer(typeof(ResponseBase<AuthResult>))
                    .ReadObject(_getMemoryStreamFromWebResponse(textResp));

                throw new Exception(result.Response.LocalizedMessage);
            }
        }
        /// <summary>
        /// Отпрака данных списания
        /// </summary>да 
        internal bool Writeoff(ResultScanPosititon positions)
        {
            var request = getHttpWebRequestt("POST", "/openapi/writeoff", true);

            new DataContractJsonSerializer(typeof(ResultScanPosititon))
                .WriteObject(request.GetRequestStream(), positions);

            var textResp = _getStringFromWebResponse(request.GetResponse());

            if (textResp.IndexOf("statusCode") > -1)
            {
                var result = (ResponseBase<SendDataResult>)new DataContractJsonSerializer(typeof(ResponseBase<SendDataResult>))
                    .ReadObject(_getMemoryStreamFromWebResponse(textResp));

                return result.Response.StatusCode == "200" || result.Response.StatusCode == "202";
            }
            else
            {
                var result = (ResponseBase<AuthResult>)new DataContractJsonSerializer(typeof(ResponseBase<AuthResult>))
                    .ReadObject(_getMemoryStreamFromWebResponse(textResp));

                throw new Exception(result.Response.LocalizedMessage);
            }
        }
        /// <summary>
        /// Получить остатки по скалду
        /// </summary>
        internal ResultRemainings Remainings()
        {
            var request = getHttpWebRequestt("GET", "/openapi/remainings", true);

            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                return (ResultRemainings)new DataContractJsonSerializer(typeof(ResultRemainings)).ReadObject(responseStream);
            }
        }

        /// <summary>
        /// Тест соединения
        /// </summary>
        /// <returns></returns>
        internal bool TestConnect()
        {
            HttpWebRequest request = getHttpWebRequestt("GET", "/openapi/status", false);
            request.Headers.Add("Authorization", _tokenAuth);

            using (var responseStream = request.GetResponse().GetResponseStream())
            {
                return true;
            }
        }

        private string _getStringFromWebResponse(WebResponse webStream)
        {
            using (var responseStream = webStream.GetResponseStream())
            {
                if (responseStream != null)
                    using (var stream = new StreamReader(responseStream))
                    {
                        return stream.ReadToEnd();
                    }
            }

            return null;
        }

        private Stream _getMemoryStreamFromWebResponse(string textResp)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(textResp));
        }

        /// <summary>
        /// Получить объект запроса
        /// </summary>
        /// <param name="methods"></param>
        /// <param name="pathAndQuery"></param>
        /// <param name="addAuthToken"></param>
        /// <returns></returns>
        private HttpWebRequest getHttpWebRequestt(string methods, string pathAndQuery, bool addAuthToken)
        {
            var request = (HttpWebRequest)WebRequest.Create(Host + pathAndQuery);

            request.Method = methods;
            //request.UserAgent = ".NET Framework Client";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            if (addAuthToken)
                request.Headers.Add("Authorization", _tokenAuth);

            return request;
        }
    }
}
