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
    /// <summary>
    /// The data element to display for students
    /// </summary>
    public class DataElement
    {
        /// <summary>
        /// name of the data element
        /// </summary>
        public string attributeName { get; set; }

        /// <summary>
        /// id of the data element
        /// </summary>
        public string attributeId { get; set; }
    }
}