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
using System.Runtime.Serialization;

namespace InBloomClient.Enum
{
    [DataContract]
    public enum SchoolYearType
    {
        [EnumMember(Value = "1996-1997")]
        Y19961997,
        [EnumMember(Value = "1997-1998")]
        Y19971998,
        [EnumMember(Value = "1998-1999")]
        Y19981999,
        [EnumMember(Value = "1999-2000")]
        Y19992000,
        [EnumMember(Value = "2000-2001")]
        Y20002001,
        [EnumMember(Value = "2001-2002")]
        Y20012002,
        [EnumMember(Value = "2002-2003")]
        Y20022003,
        [EnumMember(Value = "2003-2004")]
        Y20032004,
        [EnumMember(Value = "2004-2005")]
        Y20042005,
        [EnumMember(Value = "2005-2006")]
        Y20052006,
        [EnumMember(Value = "2006-2007")]
        Y20062007,
        [EnumMember(Value = "2007-2008")]
        Y20072008,
        [EnumMember(Value = "2008-2009")]
        Y20082009,
        [EnumMember(Value = "2009-2010")]
        Y20092010,
        [EnumMember(Value = "2010-2011")]
        Y20102011,
        [EnumMember(Value = "2011-2012")]
        Y20112012,
        [EnumMember(Value = "2012-2013")]
        Y20122013,
        [EnumMember(Value = "2013-2014")]
        Y20132014,
        [EnumMember(Value = "2014-2015")]
        Y20142015,
        [EnumMember(Value = "2015-2016")]
        Y20152016,
        [EnumMember(Value = "2016-2017")]
        Y20162017,
        [EnumMember(Value = "2017-2018")]
        Y20172018,
        [EnumMember(Value = "2018-2019")]
        Y20182019,
        [EnumMember(Value = "2019-2020")]
        Y20192020,
        [EnumMember(Value = "2021-2022")]
        Y20212022,
        [EnumMember(Value = "2022-2023")]
        Y20222023,
        [EnumMember(Value = "2023-2024")]
        Y20232024,
        [EnumMember(Value = "2024-2025")]
        Y20242025,
        [EnumMember(Value = "2025-2026")]
        Y20252026,
        [EnumMember(Value = "2026-2027")]
        Y20262027,
        [EnumMember(Value = "2027-2028")]
        Y20272028,
        [EnumMember(Value = "2028-2029")]
        Y20282029,
        [EnumMember(Value = "2029-2030")]
        Y20292030
    }
}