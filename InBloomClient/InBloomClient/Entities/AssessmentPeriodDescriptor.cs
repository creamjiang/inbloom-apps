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
    /// This descriptor holds the period or window in which an assessment is supposed to be administered (e.g., Beginning of Year, Middle of Year, End of Year).
    /// </summary>
    public class AssessmentPeriodDescriptor
    {
        /// <summary>
        /// The first date the assessment is to be administered.
        /// </summary>
        public DateTime beginDate { get; set; }

        /// <summary>
        /// A code or abbreviation that is used to refer to an Assessment Period.
        /// </summary>
        [StringLength(20)]
        public string codeValue { get; set; }

        /// <summary>
        /// The description of the Assessment Period.
        /// </summary>
        [StringLength(1024)]
        public string description { get; set; }

        /// <summary>
        /// The last date the assessment is to be administered.
        /// </summary>
        public DateTime endDate { get; set; }

        /// <summary>
        /// A shortened description for reference.
        /// </summary>
        [StringLength(50)]
        public string shortDescription { get; set; }

    }
}