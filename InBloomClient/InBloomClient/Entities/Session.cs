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
    public class Session
    {
        /// <summary>
        /// Month, day and year of the first day of the session.
        /// </summary>
        public DateTime beginDate { get; set; }

        /// <summary>
        /// Month, day and year of the last day of the session.
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        /// The identifier for the school year.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public SchoolYearType schoolYear { get; set; }

        /// <summary>
        /// The identifier for the calendar for the academic session (e.g., 2010/11, 2011 Summer).
        /// </summary>
        [StringLength(60)]
        public string sessionName { get; set; }

        /// <summary>
        /// The term for the session during the school year.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public TermType term { get; set; }

        /// <summary>
        /// The total number of instructional days in the school calendar.
        /// </summary>
        public int totalInstructionalDays { get; set; }

        /// <summary>
        /// The grading periods that take place during the session.
        /// </summary>
        public List<GradingPeriod> gradingPeriods { get; set; }
    }
}