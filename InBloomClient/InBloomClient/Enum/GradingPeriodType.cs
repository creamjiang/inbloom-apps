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
    [DataContract]
    public enum GradingPeriodType
    {
        [EnumMember(Value = "First Six Weeks")]
        FirstSixWeeks,
        [EnumMember(Value = "Second Six Weeks")]
        SecondSixWeeks,
        [EnumMember(Value = "Third Six Weeks")]
        ThirdSixWeeks,
        [EnumMember(Value = "Fourth Six Weeks")]
        FourthSixWeeks,
        [EnumMember(Value = "Fifth Six Weeks")]
        FifthSixWeeks,
        [EnumMember(Value = "Sixth Six Weeks")]
        SixthSixWeeks,
        [EnumMember(Value = "First Semester")]
        FirstSemester,
        [EnumMember(Value = "Second Semester")]
        SecondSemester,
        [EnumMember(Value = "First Summer Session")]
        FirstSummerSession,
        [EnumMember(Value = "Second Summer Session")]
        SecondSummerSession,
        [EnumMember(Value = "Third Summer Session")]
        ThirdSummerSession,
        [EnumMember(Value = "Summer Semester")]
        SummerSemester,
        [EnumMember(Value = "First Nine Weeks")]
        FirstNineWeeks,
        [EnumMember(Value = "Second Nine Weeks")]
        SecondNineWeeks,
        [EnumMember(Value = "Third Nine Weeks")]
        ThirdNineWeeks,
        [EnumMember(Value = "Fourth Nine Weeks")]
        FourthNineWeeks,
        [EnumMember(Value = "First Trimester")]
        FirstTrimester,
        [EnumMember(Value = "Second Trimester")]
        SecondTrimester,
        [EnumMember(Value = "Third Trimester")]
        ThirdTrimester,
        [EnumMember(Value = "End of Year")]
        EndOfYear
    }
}