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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace InBloomClient.Entities
{
    /// <summary>
    /// This association contains the cumulative record of academic achievements and the collection of student grades for the student at the end of a semester or school year.
    /// </summary>
    public class StudentAcademicRecord
    {
        /// <summary>
        /// Academic distinctions earned by or awarded to the student.
        /// </summary>
        public List<AcademicHonor> academicHonors { get; set; }

        /// <summary>
        /// The academic rank information of a student in relation to his or her graduating class.
        /// </summary>
        public ClassRanking classRanking { get; set; }

        /// <summary>
        /// The cumulative number of credits an individual attempts to earn by taking courses during his or her enrollment 
        /// in the current school as well as those credits transferred from schools in which the individual had been previously enrolled.
        /// </summary>
        public Credits cumulativeCreditsAttempted { get; set; }

        /// <summary>
        /// The cumulative number of credits an individual earns by completing courses or examinations during his or her enrollment 
        /// in the current school as well as those credits transferred from schools in which the individual had been previously enrolled.
        /// </summary>
        public Credits cumulativeCreditsEarned { get; set; }

        /// <summary>
        /// A measure of average performance in all courses taken by an individual during his or her school career as determined for record-keeping purposes. 
        /// This is obtained by dividing the total grade points received by the total number of credits attempted. 
        /// This usually includes grade points received and credits attempted in his or her current school as well as those transferred from schools 
        /// in which the individual was previously enrolled.
        /// </summary>
        public double cumulativeGradePointAverage { get; set; }

        /// <summary>
        /// The cumulative number of grade points an individual earns by successfully completing courses or 
        /// examinations during his or her enrollment in the current school as well as those transferred from schools 
        /// in which the individual had been previously enrolled.
        /// </summary>
        public double cumulativeGradePointsEarned { get; set; }

        /// <summary>
        /// The scale of equivalents, if applicable, for grades awarded as indicators of performance in schoolwork. 
        /// For example, numerical equivalents for letter grades used in determining a student's Grade Point Average 
        /// (A=4, B=3, C=2, D=1 in a four-point system) or letter equivalents for percentage grades (90-100%=A, 80-90%=B, etc.).
        /// </summary>
        [StringLength(80)]
        public string gradeValueQualifier { get; set; }

        /// <summary>
        /// The month and year the student is projected to graduate.
        /// </summary>
        public DateTime projectedGraduationDate { get; set; }

        /// <summary>
        /// Recognitions given to the student for accomplishments in a co-curricular, or extra-curricular activity.
        /// </summary>
        public List<Recognition> recognitions { get; set; }
       
        /// <summary>
        /// Grades for the classes attended by the student for this grading period.
        /// </summary>
        public Session session { get; set; } //TODO: fix description

        /// <summary>
        /// The student who was enrolled for the session.
        /// </summary>
        public Student student { get; set; }

        public string studentId { get; set; }

        public string sessionId { get; set; }
    }
}