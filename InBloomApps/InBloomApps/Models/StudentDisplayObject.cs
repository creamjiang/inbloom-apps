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

using InBloomClient.Entities;
using InBloomClient.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InBloomApps.Models
{
    public class StudentDisplayObject : IComparable<StudentDisplayObject>
    {
        public string id { get; set; }

        /// <summary>
        /// first name + last name
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// list of disability names
        /// </summary>
        public IEnumerable<string> disabilities { get; set; }

        public string displacementStatus { get; set; }

        public bool economicDisadvantaged { get; set; }

        public string gradeLevel { get; set; }

        public bool hispanicLatinoEthnicity { get; set; }

        public IEnumerable<string> homeLanguages { get; set; }

        public IEnumerable<string> languages { get; set; }

        public string limitedEnglishProficiency { get; set; }

        public string oldEthnicity { get; set; }

        public IEnumerable<string> otherName { get; set; }

        public string sex { get; set; }

        public IEnumerable<string> studentCharacteristics { get; set; }

        public IEnumerable<string> studentIndicators { get; set; }

        public IEnumerable<Telephone> telephones { get; set; }

        public string birthDate { get; set; }

        public LearningStyles learningStyles { get; set; }

        public double auditoryLearning { get; set; }

        public double tactileLearning { get; set; }

        public double visualLearning { get; set; }

        public string profileThumbnail { get; set; }

        public IEnumerable<string> race { get; set; }

        public string schoolFoodServicesEligiblity { get; set; }

        public IEnumerable<string> section504Disablities { get; set; }

        /// <summary>
        /// GPA
        /// </summary>
        public double cumulativeGradePointAverage { get; set; }

        /// <summary>
        /// list of sectionId's the students belong to
        /// </summary>
        public IEnumerable<string> sections { get; set; }

        /// <summary>
        /// assessments that the student in context has taken
        /// </summary>
        public IEnumerable<Assessment> assessments { get; set; }

        /// <summary>
        /// Used to sort a list of students by name alphabetically
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(StudentDisplayObject other)
        {
            return this.name.CompareTo(other.name);
        }
    }
}