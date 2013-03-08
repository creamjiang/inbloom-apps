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
    /// A school that has been designed: 1) to attract students of different racial/ethnic backgrounds for the purpose of reducing, preventing, 
    /// or eliminating racial isolation; and/or 2)to provide an academic or social focus on a particular theme 
    /// (e.g., science/math, performing arts, gifted/talented, or foreign language). 
    /// </summary>
    [DataContract]
    public enum MagnetSpecialProgramEmphasisSchoolType
    {
        [EnumMember(Value = "All students participate")]
        AllStudentsParticipate,
        [EnumMember(Value = "No students participate")]
        NoStudentsParticipate,
        [EnumMember(Value = "Some, but not all, students participate")]
        SomeButNotAllStudentsParticipate
    }
}