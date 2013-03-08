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
using System.ComponentModel;

namespace InBloomClient.Enum
{
    [DataContract]
    public enum OldEthnicityType
    {
        [EnumMember(Value = "American Indian Or Alaskan Native")]
        [Description("American Indian Or Alaskan Native")]
        AmericanIndianOrAlaskanNative,
        [EnumMember(Value = "Asian Or Pacific Islander")]
        [Description("Asian Or Pacific Islander")]
        AsianOrPacificIslander,
        [EnumMember(Value = "Black, Not Of Hispanic Origin")]
        [Description("Black, Not Of Hispanic Origin")]
        BlackNotOfHispanicOrigin,
        [EnumMember(Value = "Hispanic")]
        [Description("Hispanic")]
        Hispanic,
        [EnumMember(Value = "White, Not Of Hispanic Origin")]
        [Description("White, Not Of Hispanic Origin")]
        WhiteNotOfHispanicOrigin
    }
}