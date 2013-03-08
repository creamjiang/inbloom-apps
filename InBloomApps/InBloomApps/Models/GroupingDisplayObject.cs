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

namespace InBloomApps.Models
{
    public class GroupingDisplayObject
    {
        /// <summary>
        /// all the cohorts needed to display
        /// </summary>
        public IEnumerable<CohortDisplayObject> cohorts { get; set; }

        /// <summary>
        /// this includes all section every student is in
        /// </summary>
        public IEnumerable<SectionDisplayObject> sections { get; set; }

        /// <summary>
        /// a list of filters to contruct a filter for students
        /// </summary>
        public IEnumerable<Filter> filters { get; set; }

        /// <summary>
        /// A list of colors to be used to assign to groups. This list is specified in the /Data/Colors.txt file
        /// </summary>
        public IEnumerable<Color> colors { get; set; }

        /// <summary>
        /// a list of all students
        /// </summary>
        public IEnumerable<StudentDisplayObject> students { get; set; }

        public IEnumerable<DataElement> dataElements { get; set; }
    }
}