using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace InterfaceConnect
{
    public class HttpSend
    {
        // 访问地址
        private string _url;
        // 访问方法
        private Method _type;
        // 请求消息头
        private List<KeyValue> _headers;
        // 请求参数
        private List<KeyValue> _parameter;
        // 超时时间
        private int _timeout = 10000;

        /// <summary>
        /// 生成HTTP发送对象
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="type"></param>
        /// <param name="headers"></param>
        /// <param name="parameter"></param>
        /// <exception cref="Exception"></exception>
        public HttpSend(string url, Method type, List<KeyValue> headers, List<KeyValue> parameter, int timeout)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("HTTP请求参数不足！");
            }
            _url = url;
            _type = type;
            _headers = headers;
            _parameter = parameter;
            _timeout = timeout;
        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="body">信息体,可为空</param>
        /// <returns></returns>
        public string Send(string body = "")
        {
            var url = GetCompleteUrl(_url, _parameter);
            Logger.LogInfo("请求地址：" + url);
            string result = string.Empty;
            try
            {
                var client = new RestClient(url);
                client.Timeout = _timeout;
                var request = new RestRequest(_type);

                // 组建Header
                var headersStr = string.Empty;
                var contentType = "application/json";
                if (_headers != null)
                {
                    foreach (KeyValue item in _headers)
                    {
                        if(item.Key == "Content-Type")
                        {
                            contentType = item.Value;
                        }
                        request.AddHeader(item.Key, item.Value);
                        headersStr += item.ToString();
                    }
                }
                Logger.LogInfo("消息头：" + headersStr);

                request.AddParameter(contentType, body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if(response.ResponseStatus == ResponseStatus.Error)
                {
                    throw response.ErrorException;
                }
                result = response.Content;
            }
            catch(TimeoutException ex)
            {
                Logger.LogInfo("超时限制：" + _timeout/1000 + "s");
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        /// <summary>
        /// 网址后拼接参数
        /// 例如：http://baidu.com?name=zhangsan&age=18
        /// </summary>
        /// <param name="url">网址</param>
        /// <param name="parameters">拼接参数</param>
        /// <returns></returns>
        public static string GetCompleteUrl(string url, List<KeyValue> parameters)
        {
            if (parameters == null || parameters.Count == 0)
            {
                return url;
            }
            List<string> tempList = new List<string>();
            foreach (KeyValue item in parameters)
            {
                if (string.IsNullOrEmpty(item.Key)) continue;
                tempList.Add(item.Key + "=" + item.Value);
            }
            if (tempList.Count == 0) return url;
            return url + "?" + string.Join("&", tempList);
        }
    }
}
