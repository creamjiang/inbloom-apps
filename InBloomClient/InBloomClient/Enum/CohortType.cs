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
    public enum CohortType
    {
        [EnumMember(Value = "Academic Intervention")]
        AcademicIntervention,
        [EnumMember(Value = "Attendance Intervention")]
        AttendanceIntervention,
        [EnumMember(Value = "Discipline Intervention")]
        DisciplineIntervention,
        [EnumMember(Value = "Classroom Pullout")]
        ClassroomPullout,
        [EnumMember(Value = "Extracurricular Activity")]
        ExtracurricularActivity,
        [EnumMember(Value = "Field Trip")]
        FieldTrip,
        [EnumMember(Value = "Principal Watch List")]
        PrincipalWatchList,
        [EnumMember(Value = "Counselor List")]
        CounselorList,
        [EnumMember(Value = "In-school Suspension")]
        InschoolSuspension,
        [EnumMember(Value = "Study Hall")]
        StudyHall,
        [EnumMember(Value = "Other")]
        Other
    }
}