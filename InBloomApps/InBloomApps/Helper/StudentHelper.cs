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
using InBloomClient.Entities;
using InBloomClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Web.UI;
using InBloomApps.Models;

namespace InBloomApps.Helper
{
    public class StudentHelper
    {
        const string ACADEMIC_RECORDS_CACHE_KEY = "academicRecords";
        const string STUDENTS_CACHE_KEY = "allStudents";

        public static async Task<StudentDisplayObject> GetStudentDisplayObject(StudentService ss, Student student)
        {
            //check to see if the item is already in cache. if so, return the cache item
            var cache = (StudentDisplayObject)HttpContext.Current.Cache[student.id];
            if (cache != null)
                return cache;

            var sections = GetSectionsByStudentId(ss, student.id);
            var assessments = GetStudentAssessmentsByStudentId(ss, student.id);
            var academicRecords = GetAllStudentsAcademicRecords(ss);

            await Task.WhenAll(sections, assessments, academicRecords);           

            var sdo = MapStudentToStudentDisplayObject(student, sections.Result, assessments.Result, academicRecords.Result);
            HttpContext.Current.Cache.Insert(student.id, sdo);
            return sdo;
        }

        public static async Task<IEnumerable<StudentAcademicRecord>> GetAllStudentsAcademicRecords(StudentService ss)
        {
            var records = await ss.GetAllStudentsAcademicRecords();
            return records;
        }

        private static async Task<IEnumerable<Section>> GetSectionsByStudentId(StudentService cs, string studentId)
        {
            var result = await cs.GetStudentSectionsByStudentId(studentId);
            return result;
        }

        private static async Task<IEnumerable<Assessment>> GetStudentAssessmentsByStudentId(StudentService cs, string studentId)
        {
            var result = await cs.GetStudentAssessmentsByStudentId(studentId);
            return result;            
        }

        public static StudentDisplayObject MapStudentToStudentDisplayObject(Student student, IEnumerable<Section> sections, IEnumerable<Assessment> assessments, IEnumerable<StudentAcademicRecord> academicRecords)
        {
            
            var newStudent = new StudentDisplayObject();
            try
            {
                newStudent.id = student.id;
                newStudent.name = string.Format("{0} {1}", student.name.firstName, student.name.lastSurName);
                newStudent.sections = from s in sections select s.id;
                newStudent.disabilities = from d in student.disabilities select FilterHelper.GetEnumDescription(d.disability);

                //sometime there's no learning style data
                if (student.learningStyles != null)
                {
                    newStudent.auditoryLearning = student.learningStyles.auditoryLearning;
                    newStudent.tactileLearning = student.learningStyles.tactileLearning;
                    newStudent.visualLearning = student.learningStyles.visualLearning;
                }
                
                newStudent.birthDate = student.birthData.birthDate.ToShortDateString();
                newStudent.profileThumbnail = student.profileThumbnail;
                newStudent.race = student.race;
                newStudent.schoolFoodServicesEligiblity = FilterHelper.GetEnumDescription(student.schoolFoodServicesEligiblity);
                newStudent.section504Disablities = student.section504Disablities;
                newStudent.studentCharacteristics = from sc in student.studentCharacteristics select FilterHelper.GetEnumDescription(sc.characteristic);
                newStudent.languages = student.languages;
                newStudent.homeLanguages = student.homeLanguages;
                newStudent.learningStyles = student.learningStyles;
                newStudent.gradeLevel = FilterHelper.GetEnumDescription(student.gradeLevel);
                newStudent.economicDisadvantaged = student.economicDisadvantaged;
                newStudent.hispanicLatinoEthnicity = student.hispanicLatinoEthnicity;
                newStudent.oldEthnicity = FilterHelper.GetEnumDescription(student.oldEthnicity);
                newStudent.limitedEnglishProficiency = FilterHelper.GetEnumDescription(student.limitedEnglishProficiency);
                newStudent.otherName = from son in student.otherName select string.Format("{0} {1}", son.firstName, son.lastSurName);
                newStudent.studentCharacteristics = from sc in student.studentCharacteristics select FilterHelper.GetEnumDescription(sc.characteristic);
                newStudent.studentIndicators = from si in student.studentIndicators select si.indicator;
                newStudent.telephones = student.telephones;
                newStudent.sex = FilterHelper.GetEnumDescription(student.sex);
                newStudent.displacementStatus = student.displacementStatus;

                //get the gpa
                var studentAcademicRecord = academicRecords.FirstOrDefault(a => a.studentId == student.id);
                newStudent.cumulativeGradePointAverage = studentAcademicRecord != null ? studentAcademicRecord.cumulativeGradePointAverage : 0;

                newStudent.assessments = assessments;
            }
            catch (Exception e)
            {
                ExceptionHelper.LogCaughtException(e);
            }
            
            return newStudent;
        }

        private static IEnumerable<SectionDisplayObject> MapSectionToSectionDisplayObject(IEnumerable<Section> sections)
        {
            var newSections = new List<SectionDisplayObject>();
            foreach (var s in sections)
            {
                var newSection = new SectionDisplayObject();
                newSection.courseTitle = s.course.courseTitle;
                newSection.courseDescription = s.course.courseDescription;
                newSection.courseLevel = s.course.courseLevel;
                newSection.subjectArea = s.course.subjectArea;
                newSections.Add(newSection);
            }
            return newSections;
        }
    }
}