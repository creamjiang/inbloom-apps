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

using InBloomClient.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Web;
using InBloomApps.Models;

namespace InBloomApps.Helper
{
    public class FilterHelper
    {
        static string[] containsOperator = { "contains" };
        static string[] logicalOperators = { "=", ">", "<", "<=", ">=" };
        static string[] equalOperator = { "equals" };

        static IEnumerable<FilterValue> trueFalse = new List<FilterValue> {
                                                new FilterValue () { id = "true", title = "true" },
                                                new FilterValue () { id = "false", title = "false"}
                };

        static IEnumerable<FilterValue> disabilityTypes = from DisabilityType s in Enum.GetValues(typeof(DisabilityType))
                                                          select GetEnumFilterValues(s);

        static IEnumerable<FilterValue> gradeLevelTypes = from GradeLevelType s in Enum.GetValues(typeof(GradeLevelType))
                                                          select GetEnumFilterValues(s);

        static IEnumerable<FilterValue> languageItemTypes = from LanguageItemType s in Enum.GetValues(typeof(LanguageItemType))
                                                            select GetEnumFilterValues(s);

        static IEnumerable<FilterValue> oldEthnicityTypes = from OldEthnicityType s in Enum.GetValues(typeof(OldEthnicityType))
                                                            select GetEnumFilterValues(s);

        static IEnumerable<FilterValue> raceItemTypes = from RaceItemType s in Enum.GetValues(typeof(RaceItemType))
                                                        select GetEnumFilterValues(s);

        static IEnumerable<FilterValue> schoolFoodServicesEligibilityTypes = from SchoolFoodServicesEligibilityType s in Enum.GetValues(typeof(SchoolFoodServicesEligibilityType))
                                                                             select GetEnumFilterValues(s);

        static IEnumerable<FilterValue> section504DisabilityItemTypes = from Section504DisabilityItemType s in Enum.GetValues(typeof(Section504DisabilityItemType))
                                                                        select GetEnumFilterValues(s);

        static IEnumerable<FilterValue> sexTypes = from SexType s in Enum.GetValues(typeof(SexType))
                                                   select GetEnumFilterValues(s);

        static IEnumerable<FilterValue> studentCharacteristicTypes = from StudentCharacteristicType s in Enum.GetValues(typeof(StudentCharacteristicType))
                                                   select GetEnumFilterValues(s);

        static IEnumerable<FilterValue> limitedEnglishProficiencyTypes = from LimitedEnglishProficiencyType s in Enum.GetValues(typeof(LimitedEnglishProficiencyType))
                                                                         select GetEnumFilterValues(s);

        public static IEnumerable<Filter> InitializeFilters(IEnumerable<SectionDisplayObject> sdos)
        {
            var filters = new List<Filter>();

            //enum filters
            var disabilities = new Filter("disabilities", "Disabilities", containsOperator, disabilityTypes);
            var gradeLevels = new Filter("gradeLevel", "Grade Level", containsOperator, gradeLevelTypes);
            var homeLanguageItems = new Filter("homeLanguages", "Home Languages", containsOperator, languageItemTypes);
            var languageItems = new Filter("languages", "Languages", containsOperator, languageItemTypes);
            var oldEthnicities = new Filter("oldEthnicity", "Old Ethnicity", containsOperator, oldEthnicityTypes);
            var raceItems = new Filter("race", "Race", containsOperator, raceItemTypes);
            var schoolFoodServicesEligibilities = new Filter("schoolFoodServicesEligiblity", "School Food Services Eligibility", containsOperator, schoolFoodServicesEligibilityTypes);
            var section504DisabilityItems = new Filter("section504Disablities", "Section 504 Disabilities", containsOperator, section504DisabilityItemTypes);
            var sex = new Filter("sex", "Gender", containsOperator, sexTypes);
            var studentCharacteristics = new Filter("studentCharacteristics", "Student Characteristics", containsOperator, studentCharacteristicTypes);
            var limitedEnglishProficiency = new Filter("limitedEnglishProficiency", "Limited English Proficiency", containsOperator, limitedEnglishProficiencyTypes);

            //student attribute filters
            var birthDate = new Filter("birthDate", "Birth Date", logicalOperators);
            var economicDisadvantaged = new Filter("economicDisadvantaged", "Economic Disadvantaged", equalOperator, trueFalse);
            var hispanicLatinoEthnicity = new Filter("hispanicLatinoEthnicity", "Hispanic Latino Ethnicity", equalOperator, trueFalse);
            var auditoryLearning = new Filter("auditoryLearning", "Auditory Learning", logicalOperators);
            var tactileLearning = new Filter("tactileLearning", "Tactile Learning", logicalOperators);
            var visualLearning = new Filter("visualLearning", "Visual Learning", logicalOperators);            
            var gpa = new Filter("cumulativeGradePointAverage","GPA", logicalOperators);

            //section filter
            var sectionValues = GetSectionsFilter(sdos);
            var section = new Filter("sections", "Section", containsOperator, sectionValues);

            return new List<Filter>() { disabilities, gradeLevels, languageItems, homeLanguageItems, 
                oldEthnicities, raceItems, schoolFoodServicesEligibilities, section504DisabilityItems, sex,
                studentCharacteristics, birthDate, economicDisadvantaged, hispanicLatinoEthnicity, 
                auditoryLearning, tactileLearning, visualLearning, limitedEnglishProficiency, gpa, section
            };
        }

        /// <summary>
        /// Get a FilterValue object from an Enum
        /// </summary>
        /// <param name="value">The enum to get the description value from</param>
        /// <returns>a FilterValue object</returns>
        public static FilterValue GetEnumFilterValues(Enum value)
        {
            var enumDescription = GetEnumDescription(value);
            return new FilterValue { id = enumDescription, title = enumDescription };
        }

        /// <summary>
        /// Get the description value of Enum values
        /// </summary>
        /// <param name="value">The enum to get the description value from</param>
        /// <returns>The description of the enum</returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        /// <summary>
        /// Contruct a filter for all sections
        /// </summary>
        /// <param name="sdos"></param>
        /// <returns></returns>
        public static IEnumerable<FilterValue> GetSectionsFilter(IEnumerable<SectionDisplayObject> sdos){
            var list = new List<FilterValue>();
            list.AddRange(from sdo in sdos select new FilterValue { id = sdo.id, title = sdo.courseTitle });
            return list;
        }
    }
}