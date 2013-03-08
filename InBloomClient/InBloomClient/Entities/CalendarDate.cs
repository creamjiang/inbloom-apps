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
    public class CalendarDate
    {
        /// <summary>
        /// The type of scheduled or unscheduled event for the day. 
        /// For example: Instructional day Teacher only day Holiday Make-up day Weather day Student late arrival/early dismissal ...
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public CalendarEventType calendarEvent { get; set; }

        /// <summary>
        /// The month, day and year of the calendar date.
        /// </summary>
        public DateTime date { get; set; }
    }
}