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
using System.ComponentModel.DataAnnotations;
using InBloomClient.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace InBloomClient.Entities
{
    //[DataContract]
    public class Student
    {
        public string id { get; set; }

        /// <summary>
        /// The set of elements that describes an address, including the street address, city, state, and ZIP code.
        /// </summary>
        public List<Address> address { get; set; }

        /// <summary>
        /// The set of elements that capture relevant data regarding a person's birth, including birth date and place of birth.
        /// </summary>
        public BirthData birthData { get; set; }

        /// <summary>
        /// The type and year of a cohort (e.g., 9th grade) the student belongs to as determined by the year that student entered a specific grade.
        /// </summary>
        public List<CohortYear> cohortYears { get; set; }

        /// <summary>
        /// The disability condition(s) that best describes an individual's impairment.
        /// </summary>
        public List<Disability> disabilities { get; set; }

        /// <summary>
        /// Indicates a state health or weather related event that displaces a group of students, and may require additional funding, educational, or social services.
        /// </summary>
        [StringLength(30)]
        public string displacementStatus { get; set; }

        /// <summary>
        /// An indication of inadequate financial condition of an individual's family, as determined by family income, number of family members/dependents, 
        /// participation in public assistance programs, and/or other characteristics considered relevant by federal, state, and local policy.
        /// </summary>
        public bool economicDisadvantaged { get; set; }

        /// <summary>
        /// The numbers, letters, and symbols used to identify an electronic mail (e-mail) user within the network to which the individual or organization belongs.
        /// </summary>
        public List<ElectronicMail> electronicMails { get; set; }

        /// <summary>
        /// Current grade level
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GradeLevelType gradeLevel { get; set; }

        /// <summary>
        /// An indication that the individual traces his or her origin or descent to Mexico, Puerto Rico, Cuba, Central and South America, 
        /// and other Spanish cultures, regardless of race. The term, "Spanish origin," can be used in addition to "Hispanic or Latino."
        /// </summary>
        public bool hispanicLatinoEthnicity { get; set; }

        /// <summary>
        /// The language or dialect routinely spoken in an individual's home. This language or dialect may or may not be an individual's native language.
        /// </summary>
        // [JsonConverter(typeof(List<StringEnumConverter>))]
        //public List<LanguageItemType> homeLanguages { get; set; }
        public List<string> homeLanguages { get; set; }

        /// <summary>
        /// Language(s) the individual uses to communicate
        /// </summary>
        //[JsonConverter(typeof(List<StringEnumConverter>))]
        //public List<LanguageItemType> languages { get; set; }
        public List<string> languages { get; set; }

        /// <summary>
        /// The student's relative preference to visual, auditory and tactile learning expressed as percentages.
        /// </summary>
        public LearningStyles learningStyles { get; set; }

        /// <summary>
        /// An indication that the student has been identified as limited English proficient by the Language Proficiency Assessment Committee (LPAC), or English proficient.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public LimitedEnglishProficiencyType limitedEnglishProficiency { get; set; }

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
        /// Locator for the student photo.
        /// </summary>
        [StringLength(59)]
        public string profileThumbnail { get; set; }

        /// <summary>
        /// Key programs the student is participating in or receives services.
        /// </summary>
        public List<ProgramParticipation> programParticipations { get; set; }

        /// <summary>
        /// The general racial category which most clearly reflects the individual's recognition of his or her community 
        /// or with which the individual most identifies. The way this data element is listed, it must allow for multiple 
        /// entries so that each individual can specify all appropriate races.
        /// </summary>
        //[JsonConverter(typeof(StringEnumConverter))]
        //public List<RaceItemType> race { get; set; }
        public List<string> race { get; set; }

        /// <summary>
        /// An indication of a student's level of eligibility for breakfast, lunch, snack, supper, and milk programs.
        /// </summary>
        //[JsonConverter(typeof(StringEnumConverter))]
        public SchoolFoodServicesEligibilityType schoolFoodServicesEligiblity { get; set; }

        /// <summary>
        /// Current school
        /// </summary>
        public string schoolId { get; set; }

        /// <summary>
        /// A categorization of the disabilities associated with a student pursuant to Section 504.
        /// </summary>
        //[JsonConverter(typeof(StringEnumConverter))]
        //public List<Section504DisabilityItemType> section504Disablities { get; set; }
        public List<string> section504Disablities { get; set; }

        /// <summary>
        /// A person's gender.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SexType sex { get; set; }

        /// <summary>
        /// Reflects important characteristics of the student's home situation: such as Displaced Homemaker, 
        /// Immigrant, Migratory, Military Parent, Pregnant Teen, Single Parent, Unaccompanied Youth, etc.
        /// </summary>
        public List<StudentCharacteristic> studentCharacteristics { get; set; }

        /// <summary>
        /// A coding scheme that is used for identification and record-keeping purposes by schools, social services, or other agencies to refer to a student.
        /// </summary>
        public List<StudentIdentificationCode> studentIdentificationCodes { get; set; }

        /// <summary>
        /// Indicator(s) or metric(s) computed for the student (e.g., at risk) to influence more effective education or direct specific interventions.
        /// </summary>
        public List<StudentIndicator> studentIndicators { get; set; }

        /// <summary>
        /// A unique number or alphanumeric code assigned to a student by a state education agency.
        /// </summary>
        [StringLength(30)]
        public string studentUniqueStateId { get; set; }

        /// <summary>
        /// The 10-digit telephone number, including the area code, for the person.
        /// </summary>
        public List<Telephone> telephones { get; set; }
    }
}