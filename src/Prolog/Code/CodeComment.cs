﻿/* Copyright © 2010 Richard G. Todd.
 * Licensed under the terms of the Microsoft Public License (Ms-PL).
 */

using System;
using System.Xml.Linq;

namespace Prolog.Code
{
    /// <summary>
    /// Represents a code comment.
    /// </summary>
    [Serializable]
    public sealed class CodeComment
    {
        public const string ElementName = "CodeComment";

        public CodeComment(string text)
        {
            if (text == null)
            {
                text = string.Empty;
            }

            Text = text;
        }

        public static CodeComment Create(XElement xCodeComment)
        {
            var text = xCodeComment.Value;
            return new CodeComment(text);
        }

        public string Text { get; private set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var rhs = obj as CodeComment;
            if (rhs == null) return false;

            return Text == rhs.Text;
        }

        public override int GetHashCode()
        {
            var result = 0;
            result ^= Text.GetHashCode();
            return result;
        }

        public static bool operator ==(CodeComment lhs, CodeComment rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;
            return lhs.Equals(rhs);
        }

        public static bool operator !=(CodeComment lhs, CodeComment rhs)
        {
            return !(lhs == rhs);
        }

        public XElement ToXElement()
        {
            return new XElement(ElementName, Text);
        }

        public bool Equals(CodeComment other)
        {
            if (ReferenceEquals(other, null)) return false;

            return Text == other.Text;
        }
    }
}
