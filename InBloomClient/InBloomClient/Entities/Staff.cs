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

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using InBloomClient.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InBloomClient.Entities
{
    /// <summary>
    /// This entity represents an individual who performs specified activities for any public or private education institution or 
    /// agency that provides instructional and/or support services to students or staff at the early childhood level through high school completion. 
    /// For example, this includes: 1) an "employee" who performs services under the direction of the employing institution or agency, 
    /// is compensated for such services by the employer, and is eligible for employee benefits and wage or salary tax withholdings; 
    /// 2) a "contractor" or "consultant" who performs services for an agreed upon fee, or an employee of a management service contracted to work on site; 
    /// 3) a "volunteer" who performs services on a voluntary and uncompensated basis; 4) an in kind service provider; or 
    /// 5) an independent contractor or businessperson working at a school site.
    /// </summary>
    public class Staff
    {
        /// <summary>
        /// The set of elements that describes an address, including the street address, city, state, and ZIP code.
        /// </summary>
        public List<Address> address { get; set; }

        /// <summary>
        /// The month, day, and year on which an individual was born.
        /// </summary>
        public DateTime birthDate { get; set; }

        /// <summary>
        /// The legal document or authorization giving authorization to perform teaching assignment services.
        /// </summary>
        public List<Credential> credentials { get; set; }

        /// <summary>
        /// The numbers, letters, and symbols used to identify an electronic mail (e-mail) user within the network to which the individual or organization belongs.
        /// </summary>
        public List<ElectronicMail> electronicMail { get; set; }

        /// <summary>
        /// The extent of formal instruction an individual has received (e.g., the highest grade in school completed or its equivalent or the highest degree received).
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LevelOfEducationType highestLevelOfEducationCompleted { get; set; }

        /// <summary>
        /// An indication that the individual traces his or her origin or descent to Mexico, Puerto Rico, Cuba, 
        /// Central and South America, and other Spanish cultures, regardless of race. The term, "Spanish origin," 
        /// can be used in addition to "Hispanic or Latino."
        /// </summary>
        public bool hispanicLatinoEthnicity { get; set; }

        /// <summary>
        /// The login ID for the user; used for security access control interface.
        /// </summary>
        [StringLength(60)]
        public string loginId { get; set; }

        /// <summary>
        /// Full legal name of the person.
        /// </summary>
        public Name name { get; set; }

        /// <summary>
        /// Previous definition of Ethnicity combining Hispanic/Latino and race: 
        /// 1 - American Indian or Alaskan Native 2 - Asian or Pacific Islander 
        /// 3 - Black, not of Hispanic origin 4 - Hispanic 5 - White, not of Hispanic origin
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public OldEthnicityType oldEthnicity { get; set; }

        /// <summary>
        /// Other names (e.g., alias, nickname, previous legal name) associated with a person.
        /// </summary>
        public List<OtherName> otherName { get; set; }

        /// <summary>
        /// The general racial category which most clearly reflects the individual's recognition of his or her community or with which the individual most identifies. 
        /// The way this data element is listed, it must allow for multiple entries so that each individual can specify all appropriate races.
        /// </summary>
        //[JsonConverter(typeof(StringEnumConverter))]
        //public List<RaceItemType> race { get; set; }
        public List<string> race { get; set; }

        /// <summary>
        /// A person's gender.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SexType sex { get; set; }

        /// <summary>
        /// A unique number or alphanumeric code assigned to a staff member by a school, school system, a state, or other agency or entity.
        /// </summary>
        public List<StaffIdentificationCode> staffIdentificationCode { get; set; }

        /// <summary>
        /// A unique number or alphanumeric code assigned to a staff by a state education agency.
        /// </summary>
        [StringLength(30)]
        public string staffUniqueStateId { get; set; }

        /// <summary>
        /// The 10-digit telephone number, including the area code, for the person.
        /// </summary>
        public List<Telephone> telephone { get; set; }

        /// <summary>
        /// The total number of years that an individual has previously held a similar professional position in one or more education institutions.
        /// </summary>
        public int yearsOfPriorProfessionalExperience { get; set; }

        /// <summary>
        /// The total number of years that an individual has previously held a teaching position in one or more education institutions.
        /// </summary>
        public int yearsOfPriorTeachingExperience { get; set; }
    }
}