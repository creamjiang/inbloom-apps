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
    public class EducationOrganization
    {
        public List<AccountabilityRating> accountabilityRatings { get; set; }
        
        public List<Address> addresses { get; set; }
        
        public string agencyHierarchyName { get; set; }
        
        public List<EducationOrgIdentificationCode> educationOrgIdentificationCode { get; set; }

        [StringLength(75)]
        public string nameOfInstituation { get; set; }

        public string oparationalStatus { get; set; }

        public List<string> organizationCategories { get; set; }

        public List<string> programReferences { get; set; }

        [StringLength(75)]
        public string shortNameOfInstituation { get; set; }

        [StringLength(60)]
        public string stateOrganizationId { get; set; }

        public InstitutionTelephone telephone { get; set; }

        [StringLength(80)] //(5-80)
        public string website { get; set; }

        public EducationOrganization parentEducationAgency { get; set; }
    }
}