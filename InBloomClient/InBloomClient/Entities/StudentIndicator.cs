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
using System.ComponentModel.DataAnnotations;

namespace InBloomClient.Entities
{
    public class StudentIndicator
    {
        /// <summary>
        /// The date when the indicator was assigned or computed.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime beginDate { get; set; }

        /// <summary>
        /// The person, organization, or department that designated the program association.
        /// </summary>
        [StringLength(60)]
        public string designatedBy { get; set; }

        /// <summary>
        /// The date the indicator or metric was sunset or removed.
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime endDate { get; set; }

        /// <summary>
        /// The value of the indicator or metric. e.g at risk
        /// </summary>
        [StringLength(35)]
        public string indicator { get; set; } 

        /// <summary>
        /// The name for a group of indicators. e.g at risk
        /// </summary>
        [StringLength(60)]
        public string indicatorGroup { get; set; }

        /// <summary>
        /// The name of the indicator or metric. e.g at risk
        /// </summary>
        [StringLength(60)]
        public string indicatorName { get; set; }
    }
}