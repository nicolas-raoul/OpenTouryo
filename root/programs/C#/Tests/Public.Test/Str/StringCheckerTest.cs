﻿//**********************************************************************************
//* Copyright (C) 2007,2014 Hitachi Solutions,Ltd.
//**********************************************************************************

#region Apache License

// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License. 
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

//**********************************************************************************
//* クラス名        ：StringCheckerTest
//* クラス日本語名  ：Test of the class to Check the String
//*
//* 作成者          ：Rituparna
//* 更新履歴        ：
//* 
//*  Date:        Author:          Comments:
//*  ----------  ----------------  -------------------------------------------------
//*  05/16/2014    Rituparna       Testcode development for StringChecker.
//*
//**********************************************************************************

#region Includes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Touryo.Infrastructure.Public.Str;
using System.Text.RegularExpressions;
#endregion

namespace Public.Test.Str
{
    public class StringCheckerTest
    {
        [TestFixtureSetUp]
        public void Init()
        {
            // This is a test pre-processing.
            // This is done only once at the beginning.
        }

        /// <summary>Test case pre-processing.</summary>
        [SetUp]
        public void SetUp()
        {
            // This is a test case pre-processing.
            // It runs for each test case.
        }

        #region Test data

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsZenkaku.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsZenkakuTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "貴社ますますご盛栄のこととお慶び申し上げます。平素は格別のご高配を賜り、厚く御礼申し上げます。", true);
                yield return new TestCaseData("TestID-001N", "貴社ますますご盛栄のこととお慶び申し上げます。平素は格別のお引き立てをいただき、厚く御礼申し上げます。", true);
                yield return new TestCaseData("TestID-002N", "１２３４５", true);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-004A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-005A", "abcde", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", true);
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008A", "0.1 0.2", true).Throws(typeof(NUnit.Framework.AssertionException));
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsHankaku.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsHankakuTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "abcde", true);
                yield return new TestCaseData("TestID-001N", "12345", true);
                yield return new TestCaseData("TestID-002N", "ｦﾀ", true);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008A", "0.1 0.2", true);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsNumeric.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsNumericTest
        {
            get
            {
                this.SetUp();
                yield return new TestCaseData("TestID-000N", "12345", true);
                yield return new TestCaseData("TestID-001A", "abcde", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-002A", "ｦﾀ", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-003A", string.Empty, true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-004A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-005A", "ｪｫｬｭｮ", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-006N", "12.78", true);
                yield return new TestCaseData("TestID-007A", 12.78, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008N", "１２３４５", true);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsNumbers.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsNumbersTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "12345", true);
                yield return new TestCaseData("TestID-001A", "abcde", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-002N", "ｦﾀ", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006A", "ｪｫｬｭｮ", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-007A", "一二三四五六七八九十", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-008N", 1, true).Throws(typeof(ArgumentException));
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsNumbersZenkaku.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsNumbersZenkakuTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "１２３４５", true);
                yield return new TestCaseData("TestID-001L", string.Empty, true);
                yield return new TestCaseData("TestID-002A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-003A", "abcde", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-004N", "一二三四五六七八九十", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-005A", "貴社ますますご盛栄のこととお慶び申し上げます。平素は格別のご高配を賜り、厚く御礼申し上げます。", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-006A", "貴社ますますご盛栄のこととお慶び申し上げます。平素は格別のお引き立てをいただき、厚く御礼申し上げます。", true).Throws(typeof(NUnit.Framework.AssertionException));
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsAlphabet.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsAlphabetTest
        {
            get
            {
                this.SetUp();
                yield return new TestCaseData("TestID-000L", string.Empty, true);
                yield return new TestCaseData("TestID-001A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-002A", "abcde", true);
                yield return new TestCaseData("TestID-003A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-004N", "一二三四五六七八九十", false);
                yield return new TestCaseData("TestID-005N", "12345", false);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsNumbersHankaku.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsNumbersHankakuTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "abcde", false);
                yield return new TestCaseData("TestID-001N", "12345", true);
                yield return new TestCaseData("TestID-002N", "ｦﾀ", false);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", false);
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008A", "0.1 0.2", false);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsAlphabetHankaku.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsAlphabetHankakuTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "abcde", true);
                yield return new TestCaseData("TestID-001N", "12345", false);
                yield return new TestCaseData("TestID-002N", "ｦﾀ", false);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", false);
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008N", "0.1 0.2", false);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsAlphabetZenkaku.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsAlphabetZenkakuTest
        {
            get
            {
                this.SetUp();
                yield return new TestCaseData("TestID-000N", "abcde", false);
                yield return new TestCaseData("TestID-001N", "12345", false);
                yield return new TestCaseData("TestID-002N", "ｦﾀ", false);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", false);
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008N", "0.1 0.2", false);
                yield return new TestCaseData("TestID-009N", "Ａ", true);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsHiragana.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsHiraganaTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "abcde", false);
                yield return new TestCaseData("TestID-001N", "12345", false);
                yield return new TestCaseData("TestID-002N", "ｦﾀ", false);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", false);
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008N", "0.1 0.2", false);
                yield return new TestCaseData("TestID-009N", "さん", true);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsKatakana.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsKatakanaTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "abcde", false);
                yield return new TestCaseData("TestID-001N", "12345", false);
                yield return new TestCaseData("TestID-002N", "ｦﾀ", true);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", false);
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008N", "0.1 0.2", false);
                yield return new TestCaseData("TestID-009N", "ナルト", true);
            }
        }

        public IEnumerable<TestCaseData> TestIsKatakanaZenkakuTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "abcde", false);
                yield return new TestCaseData("TestID-001N", "12345", false);
                yield return new TestCaseData("TestID-002N", "ｦﾀ", false);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", false);
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008N", "0.1 0.2", false);
                yield return new TestCaseData("TestID-009N", "ナルト", true);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsKatakanaHankaku.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsKatakanaHankakuTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "abcde", false);
                yield return new TestCaseData("TestID-001N", "12345", false);
                yield return new TestCaseData("TestID-002N", "ｦﾀ", true);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", false);
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008N", "0.1 0.2", false);
                yield return new TestCaseData("TestID-009N", "ナルト", false);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsKanji.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsKanjiTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "達磨", true);
                yield return new TestCaseData("TestID-001N", "12345", false);
                yield return new TestCaseData("TestID-002N", "ｦﾀ", false);
                yield return new TestCaseData("TestID-003L", string.Empty, true);
                yield return new TestCaseData("TestID-005A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-006N", "一二三四五六七八九十", true);
                yield return new TestCaseData("TestID-007A", 1, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-008N", "0.1 0.2", false);
                yield return new TestCaseData("TestID-009N", "ナルト", false);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsInCodePage.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsInCodePageTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", string.Empty, 1200, true);
                yield return new TestCaseData("TestID-001N", "abcde", CustomEncode.UTF_16LE, true);
                yield return new TestCaseData("TestID-002N", "abcde", CustomEncode.UTF_8, true);
                yield return new TestCaseData("TestID-003A", null, CustomEncode.UTF_8, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-004A", "abcd", string.Empty, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-005A", "abcd", CustomEncode._iso_2022_jp_Dollar_ESC, true);
                yield return new TestCaseData("TestID-006N", "abcd", null, true);
                yield return new TestCaseData("TestID-007N", "\\\\@!", 20108, false);
                yield return new TestCaseData("TestID-008N", "\\\\*&", 20108, false);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsShiftJis.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsShiftJisTest
        {
            get
            {

                this.SetUp();

                yield return new TestCaseData("TestID-000N", string.Empty, true);
                yield return new TestCaseData("TestID-001N", "abcde", true);
                yield return new TestCaseData("TestID-002A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-003A", "ƒ`ƒƒƒlƒ‹ƒp[ƒgƒi[‚Ì‘I‘ð", false);
                yield return new TestCaseData("TestID-004A", "#", true);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsShiftJisZenkaku.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsShiftJisZenkakuTest
        {
            get
            {

                this.SetUp();

                yield return new TestCaseData("TestID-000N", string.Empty, true);
                yield return new TestCaseData("TestID-001A", "ナルト", true);
                yield return new TestCaseData("TestID-002N", "abcde", false);
                yield return new TestCaseData("TestID-003A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-004A", "ƒ`ƒƒƒlƒ‹ƒp[ƒgƒi[‚Ì‘I‘ð", false);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method IsShiftJisHankaku.
        /// </summary>
        public IEnumerable<TestCaseData> TestIsShiftJisHankakuTest
        {
            get
            {
                this.SetUp();
                yield return new TestCaseData("TestID-000N", string.Empty, true);
                yield return new TestCaseData("TestID-001A", "ナルト", false);
                yield return new TestCaseData("TestID-002N", "abcde", true);
                yield return new TestCaseData("TestID-003A", null, true).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-004A", "ƒ`ƒƒƒlƒ‹ƒp[ƒgƒi[‚Ì‘I‘ð", false);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method Match.
        /// </summary>
        public IEnumerable<TestCaseData> TestMatch1Test
        {
            get
            {
                this.SetUp();
                yield return new TestCaseData("TestID-000N", "Hello World", @"d \w+ \s", false);
                yield return new TestCaseData("TestID-001N", "Hello World", @"d \w+ \s", false);
                yield return new TestCaseData("TestID-002N", "The the quick brown fox  fox jumped over the lazy dog dog.", @"\b(?<word>\w+)\s+(\k<word>)\b", true);
                yield return new TestCaseData("TestID-003N", "Hello World", string.Empty, true);
                yield return new TestCaseData("TestID-004N", "Hello World", @"d \w+ \s", false);
                yield return new TestCaseData("TestID-005N", "Hello World", @"d \w+ \s", false);
                yield return new TestCaseData("TestID-006N", "Hello World", string.Empty, true);
                yield return new TestCaseData("TestID-007N", string.Empty, @"d \w+ \s", false);
                yield return new TestCaseData("TestID-008A", string.Empty, null, false).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-009A", null, @"d \w+ \s", false).Throws(typeof(ArgumentNullException)); ;
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method Match.
        /// </summary>
        public IEnumerable<TestCaseData> TestMatchTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "Hello World", @"d \w+ \s", RegexOptions.Singleline, false);
                yield return new TestCaseData("TestID-001N", "Hello World", @"d \w+ \s", RegexOptions.RightToLeft, false);
                yield return new TestCaseData("TestID-002N", "The the quick brown fox  fox jumped over the lazy dog dog.", @"\b(?<word>\w+)\s+(\k<word>)\b", RegexOptions.Compiled, true);
                yield return new TestCaseData("TestID-003N", "Hello World", string.Empty, RegexOptions.Singleline, true);
                yield return new TestCaseData("TestID-004A", "Hello World", @"d \w+ \s", string.Empty, true).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-005N", "Hello World", @"d \w+ \s", null, false);
                yield return new TestCaseData("TestID-006N", "Hello World", string.Empty, null, true);
                yield return new TestCaseData("TestID-007N", string.Empty, @"d \w+ \s", RegexOptions.Singleline, false);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method Matches.
        /// </summary>
        public IEnumerable<TestCaseData> TestMatches1Test
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "Hello World", @"d \w+ \s", 0);
                yield return new TestCaseData("TestID-001N", "The the quick brown fox  fox jumped over the lazy dog dog.", @"\b(?<word>\w+)\s+(\k<word>)\b", 2);
                yield return new TestCaseData("TestID-002N", string.Empty, @"d \w+ \s", 0);
                yield return new TestCaseData("TestID-003N", null, @"d \w+ \s", 0).Throws(typeof(ArgumentNullException));
                yield return new TestCaseData("TestID-004N", "hi", string.Empty, 3);
            }
        }

        /// <summary>
        /// This method to generate test cases. 
        /// This method to generate test data to be passed to the method Matches.
        /// </summary>
        public IEnumerable<TestCaseData> TestMatchesTest
        {
            get
            {
                this.SetUp();

                yield return new TestCaseData("TestID-000N", "Hello World", @"d \w+ \s", RegexOptions.Singleline, 0);
                yield return new TestCaseData("TestID-001N", "Hello World", @"d \w+ \s", RegexOptions.RightToLeft, 0);
                yield return new TestCaseData("TestID-002N", "The the quick brown fox  fox jumped over the lazy dog dog.", @"\b(?<word>\w+)\s+(\k<word>)\b", RegexOptions.Compiled, 2);
                yield return new TestCaseData("TestID-003N", "Hello World", string.Empty, RegexOptions.Singleline, 12);
                yield return new TestCaseData("TestID-004A", "Hello World", @"d \w+ \s", string.Empty, 2).Throws(typeof(ArgumentException));
                yield return new TestCaseData("TestID-005N", "Hello World", @"d \w+ \s", null, 0);
                yield return new TestCaseData("TestID-006N", "Hello World", string.Empty, null, 12);
                yield return new TestCaseData("TestID-007N", string.Empty, @"d \w+ \s", RegexOptions.Singleline, 0);
            }
        }

        #endregion

        /// <summary>
        /// IsZenkakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>        
        [TestCaseSource("TestIsZenkakuTest")]
        public static void IsZenkakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsZenkaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsHankakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsHankakuTest")]
        public static void IsHankakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsHankaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsNumericTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsNumericTest")]
        public static void IsNumericTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsNumeric(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsNumbersTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsNumbersTest")]
        public static void IsNumbersTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsNumbers(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsNumbers_ZenkakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsNumbersZenkakuTest")]
        public static void IsNumbers_ZenkakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsNumbers_Zenkaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsAlphabetTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsAlphabetTest")]
        public static void IsAlphabetTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsAlphabet(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsNumbers_HankakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsNumbersHankakuTest")]
        public static void IsNumbers_HankakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsNumbers_Hankaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsAlphabet_HankakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsAlphabetHankakuTest")]
        public static void IsAlphabet_HankakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsAlphabet_Hankaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsAlphabet_ZenkakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsAlphabetZenkakuTest")]
        public static void IsAlphabet_ZenkakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsAlphabet_Zenkaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsHiraganaTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsHiraganaTest")]
        public static void IsHiraganaTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsHiragana(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsKatakanaTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsKatakanaTest")]
        public static void IsKatakanaTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsKatakana(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsKatakana_ZenkakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsKatakanaZenkakuTest")]
        public static void IsKatakana_ZenkakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsKatakana_Zenkaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsKatakana_HankakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsKatakanaHankakuTest")]
        public static void IsKatakana_HankakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsKatakana_Hankaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsKanjiTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsKanjiTest")]
        public static void IsKanjiTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsKanji(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsInCodePageTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="codePageNum">codePageNum</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsInCodePageTest")]
        public static void IsInCodePageTest(string testCaseID, string input, int codePageNum, bool result)
        {
            bool output = StringChecker.IsInCodePage(input, codePageNum);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsShiftJisTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsShiftJisTest")]
        public static void IsShiftJisTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsShift_Jis(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsIsShift_Jis_ZenkakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsShiftJisZenkakuTest")]
        public static void IsIsShift_Jis_ZenkakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsShift_Jis_Zenkaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// IsShift_Jis_HankakuTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestIsShiftJisHankakuTest")]
        public static void IsShift_Jis_HankakuTest(string testCaseID, string input, bool result)
        {
            bool output = StringChecker.IsShift_Jis_Hankaku(input);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// MatchTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="pattern">pattern</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestMatch1Test")]
        public static void MatchTest(string testCaseID, string input, string pattern, bool result)
        {
            bool output = StringChecker.Match(input, pattern);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// MatchTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="pattern">pattern</param>
        /// <param name="options">options</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestMatchTest")]
        public static void MatchTest(string testCaseID, string input, string pattern, RegexOptions options, bool result)
        {
            bool output = StringChecker.Match(input, pattern, options);
            Assert.AreEqual(output, result);
        }

        /// <summary>
        /// MatchesTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="pattern">pattern</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestMatches1Test")]
        public static void MatchesTest(string testCaseID, string input, string pattern, int result)
        {
            MatchCollection output = StringChecker.Matches(input, pattern);
            Assert.NotNull(output);
            Assert.AreEqual(output.Count, result);
        }

        /// <summary>
        /// MatchesTest Method
        /// </summary>
        /// <param name="testCaseID">testCaseID</param>
        /// <param name="input">input</param>
        /// <param name="pattern">pattern</param>
        /// <param name="options">options</param>
        /// <param name="result">result</param>
        [TestCaseSource("TestMatchesTest")]
        public static void MatchesTest(string testCaseID, string input, string pattern, RegexOptions options, int result)
        {
            MatchCollection output = StringChecker.Matches(input, pattern, options);
            Assert.NotNull(output);
            Assert.AreEqual(output.Count, result);
        }

        [TearDown]
        public void TearDown()
        {
            // This is a test case post-processing.
            // It runs for each test case.
        }

        /// <summary>Test post-processing.</summary>
        [TestFixtureTearDown]
        public void CleanUp()
        {
            // This is a test post-processing.
            // This is done only once at the ending.
        }
    }
}
