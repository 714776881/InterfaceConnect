using System;
using System.Collections.Generic;

namespace InterfaceMediator
{
    public interface INodeHelper
    {
        string GetListFirstNode(string data, string path);
        string AddListNode(string data, string path, string value);
        string GetNode(string data, string path);
        List<string> GetChildrenNodes(string data, string path);
        string SetNode(string data, string path, string value);
    }

}