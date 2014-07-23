﻿//BSD 2014, WinterDev 
//ArthurHub

using System;
using System.Collections.Generic;
using HtmlRenderer.Diagnostics;
using HtmlRenderer.Drawing;
using HtmlRenderer.WebDom;
using HtmlRenderer.Boxes;

namespace HtmlRenderer.Composers.BridgeHtml
{
    class BridgeHtmlElement : DomElement
    {
        CssBox principalBox;
        Css.BoxSpec boxSpec;        
        CssRuleSet elementRuleSet; 

        public BridgeHtmlElement(BridgeHtmlDocument owner, int prefix, int localNameIndex)
            : base(owner, prefix, localNameIndex)
        {
            this.boxSpec = new Css.BoxSpec();
        }
        public Css.BoxSpec Spec
        {
            get { return this.boxSpec; }
        }
        public WellknownElementName WellknownElementName { get; set; }
        public bool TryGetAttribute(WellknownElementName wellknownHtmlName, out DomAttribute result)
        {
            var found = base.FindAttribute((int)wellknownHtmlName);
            if (found != null)
            {
                result = found;
                return true;
            }
            else
            {
                result = null;
                return false;
            }
        }
        public bool TryGetAttribute(WellknownElementName wellknownHtmlName, out string value)
        {
            DomAttribute found;
            if (this.TryGetAttribute(wellknownHtmlName, out found))
            {
                value = found.Value;
                return true;
            }
            else
            {
                value = null;
                return false;
            }

        }

        //------------------------------------
        protected CssBox GetPrincipalBox()
        {
            return this.principalBox;
        }

        internal void SetPrincipalBox(CssBox box)
        {
            this.principalBox = box;
            this.SkipPrincipalBoxEvalulation = true;
        }

        internal bool SkipPrincipalBoxEvalulation
        {
            get;
            set;

        }
        internal static CssBox InternalGetPrincipalBox(BridgeHtmlElement element)
        {
            return element.principalBox;
        }
        //------------------------------------
        protected override void OnChangeInIdleState(ElementChangeKind changeKind)
        {
            //1. 
            this.OwnerDocument.SetDocumentState(DocumentState.ChangedAfterIdle);
            //2.
            this.SkipPrincipalBoxEvalulation = false;
            var cnode = this.ParentNode;
            while (cnode != null)
            {
                ((BridgeHtmlElement)cnode).SkipPrincipalBoxEvalulation = false;
                cnode = cnode.ParentNode;
            }
        } 

        internal static void InvokeNotifyChangeOnIdleState(BridgeHtmlElement elem, ElementChangeKind changeKind)
        {
            elem.OnChangeInIdleState(changeKind);
        }

        internal CssRuleSet ElementRuleSet
        {
            get
            {
                return this.elementRuleSet;
            }
            set
            {
                this.elementRuleSet = value;
            }
        }
        internal bool IsStyleEvaluated
        {
            get;
            set;
        }
    }

    sealed class BridgeRootElement : BridgeHtmlElement
    {
        public BridgeRootElement(BridgeHtmlDocument ownerDoc)
            : base(ownerDoc, 0, 0)
        {
        }
    }

    class BridgeHtmlTextNode : HtmlTextNode
    {
        //---------------------------------
        //this node may be simple text node  
        bool freeze;
        bool hasSomeChar;
        List<CssRun> runs;

        public BridgeHtmlTextNode(WebDocument ownerDoc, char[] buffer)
            : base(ownerDoc, buffer)
        {
        }
        public bool IsWhiteSpace
        {
            get
            {
                return !this.hasSomeChar;
            }
        }
        internal void SetSplitParts(List<CssRun> runs, bool hasSomeChar)
        {

            this.freeze = false;
            this.runs = runs;
            this.hasSomeChar = hasSomeChar;
        }
        public bool IsFreeze
        {
            get { return this.freeze; }
        }
#if DEBUG
        public override string ToString()
        {
            return new string(base.GetOriginalBuffer());
        }
#endif

        internal List<CssRun> InternalGetRuns()
        {
            this.freeze = true;
            return this.runs;
        }

    }

    enum TextSplitPartKind : byte
    {
        Text = 1,
        Whitespace,
        SingleWhitespace,
        LineBreak,
    }


    static class HtmlPredefineNames
    {

        static readonly ValueMap<WellknownElementName> _wellKnownHtmlNameMap =
            new ValueMap<WellknownElementName>();

        static UniqueStringTable htmlUniqueStringTableTemplate = new UniqueStringTable();

        static HtmlPredefineNames()
        {
            int j = _wellKnownHtmlNameMap.Count;
            for (int i = 0; i < j; ++i)
            {
                htmlUniqueStringTableTemplate.AddStringIfNotExist(_wellKnownHtmlNameMap.GetStringFromValue((WellknownElementName)(i + 1)));
            }
        }
        public static UniqueStringTable CreateUniqueStringTableClone()
        {
            return htmlUniqueStringTableTemplate.Clone();
        }

    }

}