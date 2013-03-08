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
using InBloomClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using InBloomApps.Models;

namespace InBloomApps.Helper
{
    public class SectionHelper
    {
        const string COURSES_CACHE_KEY = "allCourses";
        const string COURSE_OFFERINGS_CACHE_KEY = "allCourses";

        public static async Task<SectionDisplayObject> GetSectionDisplayObject(SectionService ss, Section section)
        {
            //check to see if the item is already in cache. if so, return the cache item
            var cache = (SectionDisplayObject)HttpContext.Current.Cache[section.id];
            if (cache != null)
                return cache;

            var courseOfferingCache = (CourseOffering)HttpContext.Current.Cache[section.courseOfferingId];
            if (courseOfferingCache == null){
                var courseOfferings = await GetAllCourseOfferings(ss);
                courseOfferingCache = courseOfferings.FirstOrDefault(co => co.id == section.courseOfferingId);
            }

            var courseCache = new Course();
            if (courseOfferingCache != null)
            {
                courseCache = (Course)HttpContext.Current.Cache[courseOfferingCache.courseId];
                if (courseCache == null)
                {
                    var courses = await GetAllCourses(ss);
                    courseCache = courses.FirstOrDefault(c => c.id == courseOfferingCache.courseId);
                }
            }
                                      
            var sdo = MapSectionToSectionDisplayObject(courseCache, section.id);

            HttpContext.Current.Cache.Insert(section.id, sdo);
            return sdo;
        }

        private static SectionDisplayObject MapSectionToSectionDisplayObject(Course c, string sectionId)
        {

            var newSection = new SectionDisplayObject();
            if (c != null)
            {
                newSection.id = sectionId;
                newSection.courseTitle = c.courseTitle;
                newSection.courseDescription = c.courseDescription;
                newSection.courseLevel = c.courseLevel;
                newSection.subjectArea = c.subjectArea;
            }            
            return newSection;
        }

        public static async Task<IEnumerable<Course>> GetAllCourses(SectionService ss)
        {
            try
            {
                //Get all courses
                var result = await ss.GetAllCourses();

                //cache each course by id
                foreach (var c in result)
                {
                    HttpContext.Current.Cache.Insert(c.id, c);
                }

                return result;
            }
            catch (Exception e)
            {
                ExceptionHelper.LogCaughtException(e);
                return new List<Course>();
            }           
        }

        public static async Task<IEnumerable<CourseOffering>> GetAllCourseOfferings(SectionService ss)
        {
            try
            {
                //Get all course offerings
                var result = await ss.GetAllCourseOfferings();

                //cache each course offering
                foreach (var co in result)
                {
                    HttpContext.Current.Cache.Insert(co.id, co);
                }
                return result;
            }
            catch (Exception e)
            {
                ExceptionHelper.LogCaughtException(e);
                return new List<CourseOffering>();
            }
            
        }

    }
}