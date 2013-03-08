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
    /// <summary>
    /// This entity represents any program designed to work in conjunction with or to supplement the main academic program. 
    /// Programs may provide instruction, training, services or benefits through federal, state, or local agencies. 
    /// Programs may also include organized extracurricular activities for students.
    /// </summary>
    public class Program
    {
        [StringLength(20)]
        public string programId { get; set; }

        public string programSponsor { get; set; }

        public string programType { get; set; }

        public List<ServiceDescriptorType> services { get; set; }
    }
}