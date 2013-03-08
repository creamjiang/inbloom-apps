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

namespace InBloomClient.Entities
{
    /// <summary>
    /// The academic rank information of a student in relation to his or her graduating class
    /// </summary>
    public class ClassRanking
    {
        /// <summary>
        /// The academic rank of a student in relation to his or her graduating class (e.g., 1st, 2nd, 3rd).
        /// </summary>
        public int classRank { get; set; }

        /// <summary>
        /// Date class ranking was determined.
        /// </summary>
        public DateTime classRankingDate { get; set; }

        /// <summary>
        /// The academic percentage rank of a student in relation to his or her graduating class (e.g., 95%, 80%, 50%).
        /// </summary>
        public int percentageRanking { get; set; }

        /// <summary>
        /// The total number of students in the student's graduating class.
        /// </summary>
        public int totalNumberInClass { get; set; }
    }
}