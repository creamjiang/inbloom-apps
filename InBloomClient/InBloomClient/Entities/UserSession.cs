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
using System.Text;
using System.Threading.Tasks;

namespace InBloomClient.Entities
{
    public class UserSession
    {
        public bool authenticated { get; set; }

        public EducationOrganization edOrg { get; set; }

        public string edOrgId { get; set; }

        public string email { get; set; }

        public string external_id { get; set; }

        public string full_name { get; set; }

        public List<string> granted_authorities { get; set; }

        public string realm { get; set; }

        public List<string> rights { get; set; }

        public List<string> sliRoles { get; set; }

        public string tenantId { get; set; }

        public string user_id { get; set; }

        public string redirect_user { get; set; } //when authenticated is false

        public List<string> all_roles { get; set; }
    }
}
