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
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace InBloomClient.Entities
{
    public enum StaffClassificationType
    {
        [EnumMember(Value = "Art Therapist")]
        [Description("Art Therapist")]
        ArtTherapist,

        [EnumMember(Value = "Athletic Trainer")]
        [Description("Athletic Trainer")]
        AthleticTrainer,

        [EnumMember(Value = "Assistant Principal")]
        [Description("Assistant Principal")]
        AssistantPrincipal,

        [EnumMember(Value = "Assistant Superintendent")]
        [Description("Assistant Superintendent")]
        AssistantSuperintendent,

        [EnumMember(Value = "Certified Interpreter")]
        [Description("Certified Interpreter")]
        CertifiedInterpreter,

        [EnumMember(Value = "High School Counselor")]
        [Description("High School Counselor")]
        HighSchoolCounselor,

        [EnumMember(Value = "Instructional Coordinator")]
        [Description("Instructional Coordinator")]
        InstructionalCoordinator,

        [EnumMember(Value = "Instructional Aide")]
        [Description("Instructional Aide")]
        InstructionalAide,

        [EnumMember(Value = "Librarians/Media Specialists")]
        [Description("Librarians/Media Specialists")]
        LibrariansMediaSpecialists,

        [EnumMember(Value = "LEA Administrator")]
        [Description("LEA Administrator")]
        LEAAdministrator,

        [EnumMember(Value = "LEA Specialist")]
        [Description("LEA Specialist")]
        LEASpecialist,

        [EnumMember(Value = "LEA System Administrator")]
        [Description("LEA System Administrator")]
        LEASystemAdministrator,

        [EnumMember(Value = "LEA Administrative Support Staff")]
        [Description("LEA Administrative Support Staff")]
        LEAAdministrativeSupportStaff,

        [EnumMember(Value = "Librarian")]
        [Description("Librarian")]
        Librarian,

        [EnumMember(Value = "Principal")]
        [Description("Principal")]
        Principal,

        [EnumMember(Value = "Physical Therapist")]
        [Description("Physical Therapist")]
        PhysicalTherapist,

        [EnumMember(Value = "Teacher")]
        [Description("Teacher")]
        Teacher,

        [EnumMember(Value = "Other")]
        [Description("Other")]
        Other,

        [EnumMember(Value = "Superintendent")]
        [Description("Superintendent")]
        Superintendent,

        [EnumMember(Value = "School Nurse")]
        [Description("School Nurse")]
        SchoolNurse,

        [EnumMember(Value = "Specialist/Consultant")]
        [Description("Specialist/Consultant")]
        SpecialistConsultant,

        [EnumMember(Value = "School Administrator")]
        [Description("School Administrator")]
        SchoolAdministrator,

        [EnumMember(Value = "School Administrative Support Staff")]
        [Description("School Administrative Support Staff")]
        SchoolAdministrativeSupportStaff,

        [EnumMember(Value = "Student Support Services Staff")]
        [Description("Student Support Services Staff")]
        StudentSupportServicesStaff,

        [EnumMember(Value = "School Leader")]
        [Description("School Leader")]
        SchoolLeader,

        [EnumMember(Value = "School Specialist")]
        [Description("School Specialist")]
        SchoolSpecialist,

        [EnumMember(Value = "Substitute Teacher")]
        [Description("Substitute Teacher")]
        SubstituteTeacher,
    }
}
