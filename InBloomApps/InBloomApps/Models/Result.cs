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
    public class Result
    {
        /// <summary>
        /// Indicates whether the entire action was completed succesfully
        /// </summary>
        public bool completedSuccessfully { get; set; }

        /// <summary>
        /// The main object action result
        /// </summary>
        public ActionResponseResult objectActionResult { get; set; }

        /// <summary>
        /// Indicates whether any association was created
        /// </summary>
        public bool partialCreateSuccess { get; set; }

        /// <summary>
        /// Indicates whether any assocation was deleted
        /// </summary>
        public bool partialDeleteSuccess { get; set; }

        /// <summary>
        /// this list contains all the Id's that the action failed to create new objects for
        /// </summary>
        public IEnumerable<ActionResponseResult> failToCreateAssociations { get; set; }

        /// <summary>
        /// this list contains all the Id's that the action failed to delete for
        /// </summary>
        public IEnumerable<ActionResponseResult> failToDeleteAssociations { get; set; }

        public ActionResponseResult customActionResult { get; set; }
    }
}