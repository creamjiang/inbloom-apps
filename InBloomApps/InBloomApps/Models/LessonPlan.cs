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
    public class LessonPlan
    {
        /// <summary>
        /// the base64 string lesson plan to be stored in the custom entity
        /// </summary>
        /// We have decided to use FTP instead
        //public string content { get; set; }

        /// <summary>
        /// file type. e.g. application/pdf
        /// </summary>
        public string type { get; set; }

        /// <summary>
        /// name of the file
        /// </summary>
        public string name { get; set; }
    }
}