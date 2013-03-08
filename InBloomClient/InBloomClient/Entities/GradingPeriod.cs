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
using System.Linq;
using System.Web;

namespace InBloomClient.Entities
{
    public class GradingPeriod
    {
        /// <summary>
        /// Month, day, and year of the first day of the grading period.
        /// </summary>
        public DateTime beginDate { get; set; }

        /// <summary>
        /// Month, day, and year of the last day of the grading period.
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        /// The name of the period for which grades are reported
        /// values:First Six Weeks, Second Six Weeks, Third Six Weeks, Fourth Six Weeks, Fifth Six Weeks, Sixth Six Weeks, First Semester,
        /// Second Semester, First Summer Session, Second Summer Session, Third Summer Session, Summer Semester, First Nine Weeks, 
        /// Second Nine Weeks, Third Nine Weeks, Fourth Nine Weeks, First Trimester, Second Trimester, Third Trimester, End of Year
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public GradingPeriodType gradingPeriod { get; set; }

        /// <summary>
        /// Total days available for educational instruction during the grading period.
        /// </summary>
        public int totalInstructionalDays { get; set; }

        /// <summary>
        /// Days associated with the grading period.
        /// </summary>
        public List<CalendarDate> calendarDates { get; set; }
    }
}