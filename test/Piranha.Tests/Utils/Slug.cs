﻿/*
 * Copyright (c) 2017 Håkan Edling
 *
 * This software may be modified and distributed under the terms
 * of the MIT license.  See the LICENSE file for details.
 * 
 * http://github.com/piranhacms/piranha
 * 
 */

using Xunit;

namespace Piranha.Tests.Utils
{
    public class Slug
    {
        [Fact]
        public void ToLowerCase() {
            Assert.Equal("mycamelcasestring", Piranha.Utils.GenerateSlug("MyCamelCaseString"));
        }

        [Fact]
        public void Trim() {
            Assert.Equal("trimmed", Piranha.Utils.GenerateSlug(" trimmed  "));
        }

        [Fact]
        public void ReplaceWhitespace() {
            Assert.Equal("no-whitespaces", Piranha.Utils.GenerateSlug("no whitespaces"));
        }

        [Fact]
        public void ReplaceDoubleDashes() {
            Assert.Equal("no-whitespaces", Piranha.Utils.GenerateSlug("no - whitespaces"));
            Assert.Equal("no-whitespaces", Piranha.Utils.GenerateSlug("no & whitespaces"));
        }

        [Fact]
        public void TrimDashes() {
            Assert.Equal("trimmed", Piranha.Utils.GenerateSlug("-trimmed-"));
        }

        [Fact]
        public void RemoveSwedishCharacters() {
            Assert.Equal("aaeoeaaeoe", Piranha.Utils.GenerateSlug("åäöÅÄÖ"));
        }

        [Fact]
        public void TranslateUnicodeCharacters()
        {
            Assert.Equal("bei-jing", Piranha.Utils.GenerateSlug("北亰"));
            Assert.Equal("bu-hao", Piranha.Utils.GenerateSlug("不好"));
            Assert.Equal("kak-dela", Piranha.Utils.GenerateSlug("Как дела"));
            Assert.Equal("ya-khotela-by-piva", Piranha.Utils.GenerateSlug("Я хотел(а) бы пива."));
        }

        [Fact]
        public void RemoveHyphenCharacters() {
            Assert.Equal("aaooeeiiaaooeeii", Piranha.Utils.GenerateSlug("áàóòéèíìÁÀÓÒÉÈÍÌ"));
        }

        [Fact]
        public void RemoveSlashesForNonHierarchical() {
            Assert.Equal("no-more-dashes", Piranha.Utils.GenerateSlug("no/more / dashes", false));
        }
    }
}
