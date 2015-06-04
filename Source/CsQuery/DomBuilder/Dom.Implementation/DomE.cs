using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Text;
using System.IO;
using System.Diagnostics;
using CsQuery.StringScanner;
using CsQuery.HtmlParser;
using CsQuery.ExtensionMethods.Internal;
namespace CsQuery.Implementation
{
    //TODO: review here, should use builder, not static class
    public static class DomE
    {
        /// <summary>
        /// Creates a new element
        /// </summary>
        ///
        /// <param name="nodeName">
        /// The NodeName for the element (upper case).
        /// </param>
        ///
        /// <returns>
        /// A new element that inherits DomElement
        /// </returns>

        public static DomElement Create(string nodeName)
        {
            return Create(HtmlData.Tokenize(nodeName));
        }

        internal static DomElement Create(ushort nodeNameId)
        {

            switch (nodeNameId)
            {
                case HtmlData.tagA:
                    return new HtmlAnchorElement();
                case HtmlData.tagFORM:
                    return new HtmlFormElement();
                case HtmlData.tagBUTTON:
                    return new HTMLButtonElement();
                case HtmlData.tagINPUT:
                    return new HTMLInputElement();
                case HtmlData.tagLABEL:
                    return new HTMLLabelElement();
                case HtmlData.tagLI:
                    return new HTMLLIElement();
                case HtmlData.tagMETER:
                    return new HTMLMeterElement();
                case HtmlData.tagOPTION:
                    return new HTMLOptionElement();
                case HtmlData.tagPROGRESS:
                    return new HTMLProgressElement();
                case HtmlData.tagSELECT:
                    return new HTMLSelectElement();
                case HtmlData.tagTEXTAREA:
                    return new HTMLTextAreaElement();
                case HtmlData.tagSTYLE:
                    return new HTMLStyleElement();
                case HtmlData.tagSCRIPT:
                    return new HTMLScriptElement();
                default:
                    return new DomElement(nodeNameId);
            }
        }

        public static IDomCData CreateCData(string data)
        {
            return new DomCData();
        }
        public static IDomComment CreateComment(string content)
        {
            return new DomComment(content);
        }
        public static IDomText CreateTextNode(string text)
        {
            return new DomText(text);
        }
        public static IDomFragment CreateDomFragment()
        {
            return new Implementation.DomFragment();
        }
    }

    public static class DomEExt
    {

        /// <summary>
        /// Returns all child elements of a specific tag, cast to a type
        /// </summary>
        ///
        /// <typeparam name="T">
        /// Generic type parameter.
        /// </typeparam>
        /// <param name="nodeNameId">
        /// Backing field for NodeNameID property.
        /// </param>
        ///
        /// <returns>
        /// An enumerator.
        /// </returns>

        public static IEnumerable<T> ChildElementsOfTag<T>(this IDomElement elem, ushort nodeNameId)
        {
            return ChildElementsOfTag<T>(elem, nodeNameId);
        }

        private static IEnumerable<T> ChildElementsOfTag<T>(this IDomElement elem, IDomElement parent, ushort nodeNameId)
        {
            foreach (var el in elem.ChildNodes)
            {
                if (el.NodeType == NodeType.ELEMENT_NODE &&
                    el.NodeNameID == nodeNameId)
                {
                    yield return (T)el;
                }
                if (el.HasChildren)
                {
                    foreach (var child in ((DomElement)el).ChildElementsOfTag<T>(nodeNameId))
                    {
                        yield return child;
                    }
                }
            }
        }
    }
}