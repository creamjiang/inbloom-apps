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
    /// The identifier for the learning standard.
    /// </summary>
    public class LearningStandardId
    {
        /// <summary>
        /// The name of the content standard from which the learning standard was derived.
        /// </summary>
        [StringLength(65)]
        public string contentStandardName { get; set; }

        /// <summary>
        /// A unique number or alphanumeric code assigned to a learning Standard.
        /// </summary>
        [StringLength(60)]
        public string identificationCode { get; set; }
    }
}