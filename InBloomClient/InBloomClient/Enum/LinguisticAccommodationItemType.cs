/*
 * Copyright 2012-2013 inBloom, Inc. and its affiliates.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace InBloomClient.Enum
{
    /// <summary>
    /// The item(s) denoting the linguistic accommodations used with the student for the test. 
    /// For example: Bilingual dictionary English dictonary Reading Aloud - Word or Phrase Reading Aloud - 
    /// Entire Test Item Oral Translation - Word or phrase Clarification - Word or phrase Like all enumerations in SLI, 
    /// LinguisticAccommodationItemType is derived from the W3C data type token.
    /// </summary>
    [DataContract]
    public enum LinguisticAccommodationItemType
    {
        [EnumMember(Value = "Bilingual Dictionary")]
        BilingualDictionary,
        [EnumMember(Value = "English Dictionary")]
        EnglishDictionary,
        [EnumMember(Value = "Reading Aloud - Word or Phrase")]
        ReadingAloudWordOrPhrase,
        [EnumMember(Value = "Reading Aloud - Entire Test Item")]
        ReadingAloudEntireTestItem,
        [EnumMember(Value = "Oral Translation - Word or Phrase")]
        OralTranslationWordOrPhrase,
        [EnumMember(Value = "Clarification - Word or Phrase")]
        ClarificationWordOrPhrase,
        [EnumMember(Value = "Linguistic Accommodations allowed but not used")]
        LinguisticAccommodationsAllowedButNotUsed,
        [EnumMember(Value = "Linguistic Simplification")]
        LinguisticSimplification,
        [EnumMember(Value = "Reading Assistance")]
        ReadingAssistance,
        [EnumMember(Value = "Bilingual Glossary")]
        BilingualGlossary,
        [EnumMember(Value = "English and Spanish tests side-by-side")]
        EnglishAndSpanishTestsSideBySide
    }
}