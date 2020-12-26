using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace com.Origin.CommonLibrary.Framework
{
    /// <summary>
    /// xml普通操作类
    /// 2012-11-14 HuangChao
    /// </summary>
    public class XmlHelper
    {
        XElement xElm;

        public XmlHelper(string xmlString, bool isStr)
        {
            xElm = XElement.Parse(xmlString);
        }
        public XmlHelper(string xmlPath)
        {
            xElm = XElement.Load(xmlPath);
        }

        /// <summary>
        /// 返回xml中的一个节点的集合
        /// </summary>
        /// <param name="node">节点名</param>
        /// <returns></returns>
        public List<XElement> GetNodeAttribute(string node)
        {
            List<XElement> list = xElm.Element(node).Descendants("Menu").ToList();
            return list;
        }

        /// <summary>
        /// 返回xml中的一个节点的json
        /// </summary>
        /// <param name="node">节点名</param>
        /// <returns></returns>
        public string GetNodeToJson(string node)
        {
            var theFrist = xElm.Element(node);

            if (theFrist != null)
            {
                return PluSoft.Utils.JSON.Encode(theFrist).Replace("@", "");
            }

            return "";
        }

        public string GetNodeToListJson(string node, string childNode)
        {
            var theFristList = xElm.Elements(node).Elements(childNode).Elements("set").ToList();
            if (theFristList != null)
            {
                return PluSoft.Utils.JSON.Encode(theFristList).Replace("@", "");
            }
            return "";
        }

        /// <summary>
        /// 得到指定节点下的子节点的json字符串
        /// </summary>
        /// <param name="node">节点名</param>
        /// <param name="attribute">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <param name="childNodeName">子节点名</param>
        /// <returns></returns>
        public string GetNodeChildrenToJson(string node, string attribute, string attributeValue, string childNodeName)
        {
            var theFrist = xElm.Descendants(node).Where(p => p.Attribute(attribute).Value == attributeValue);

            if (theFrist.Count() > 0)
            {
                theFrist = theFrist.Single().Elements(childNodeName);
                return PluSoft.Utils.JSON.Encode(theFrist).Replace("@", "");
            }

            return "";
        }

        /// <summary>
        /// 得到指定节点下的子节点的json字符串
        /// </summary>
        /// <param name="node">节点名</param>
        /// <param name="attribute">属性名</param>
        /// <param name="attributeValue">属性值</param>
        /// <returns></returns>
        public string GetNodeChildrenToJson(string node, string attribute, string attributeValue)
        {
            var theFrist = xElm.Descendants(node).Where(p => p.Attribute(attribute).Value == attributeValue).ElementAtOrDefault(0);
            if (theFrist != null)
            {
                return PluSoft.Utils.JSON.Encode(theFrist).Replace("@", "");
            }

            return "";
        }

        /// <summary>
        /// 返回xml中的一个节点的属性值
        /// </summary>
        /// <param name="node">节点名</param>
        /// <param name="attribute">属性名</param>
        /// <returns></returns>
        public string GetNodeToJson(string node, string attribute)
        {
            var theFrist = xElm.Element(node);

            if (theFrist != null && theFrist.Attribute(attribute) != null)
            {
                return theFrist.Attribute(attribute).Value;
            }

            return "";
        }

    }
}