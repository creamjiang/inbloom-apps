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
using System.ComponentModel;

namespace InBloomClient.Enum
{
    [DataContract]
    public enum LanguageItemType
    {
        [EnumMember(Value = "Spanish")]
        [Description("Spanish")]
        Spanish,
        [EnumMember(Value = "Vietnamese")]
        [Description("Vietnamese")]
        Vietnamese,
        [EnumMember(Value = "Laotian (Lao)")]
        [Description("Laotian (Lao)")]
        LaotianLao,
        [EnumMember(Value = "Cambodian (Khmer)")]
        [Description("Cambodian (Khmer)")]
        CambodianKhmer,
        [EnumMember(Value = "Korean")]
        [Description("Korean")]
        Korean,
        [EnumMember(Value = "Japanese")]
        [Description("Japanese")]
        Japanese,
        [EnumMember(Value = "French")]
        [Description("French")]
        French,
        [EnumMember(Value = "German")]
        [Description("German")]
        German,
        [EnumMember(Value = "English")]
        [Description("English")]
        English,
        [EnumMember(Value = "Other languages")]
        [Description("Other languages")]
        OtherLanguages,
        [EnumMember(Value = "Afrikaans (Taal)")]
        [Description("Afrikaans (Taal)")]
        AfrikaansTaal,
        [EnumMember(Value = "Akan (Fante, Asante)")]
        [Description("Akan (Fante, Asante)")]
        AkanFanteAsante,
        [EnumMember(Value = "Albanian, Gheg (Kossovo/Macedonia)")]
        [Description("Albanian, Gheg (Kossovo/Macedonia)")]
        AlbanianGhegKossovoMacedonia,
        [EnumMember(Value = "Albanian, Tosk (Albania)")]
        [Description("Albanian, Tosk (Albania)")]
        AlbanianToskAlbania,
        [EnumMember(Value = "Algonquin")]
        [Description("Algonquin")]
        Algonquin,
        [EnumMember(Value = "Amharic")]
        [Description("Amharic")]
        Amharic,
        [EnumMember(Value = "Apache")]
        [Description("Apache")]
        Apache,
        [EnumMember(Value = "Arabic")]
        [Description("Arabic")]
        Arabic,
        [EnumMember(Value = "Armenian")]
        [Description("Armenian")]
        Armenian,
        [EnumMember(Value = "Assyrian (Syriac, Aramaic)")]
        [Description("Assyrian (Syriac, Aramaic)")]
        AssyrianSyriacAramaic,
        [EnumMember(Value = "Balinese")]
        [Description("Balinese")]
        Balinese,
        [EnumMember(Value = "Bengali")]
        [Description("Bengali")]
        Bengali,
        [EnumMember(Value = "Bosnian")]
        [Description("Bosnian")]
        Bosnian,
        [EnumMember(Value = "Bulgarian")]
        [Description("Bulgarian")]
        Bulgarian,
        [EnumMember(Value = "Burmese")]
        [Description("Burmese")]
        Burmese,
        [EnumMember(Value = "Cantonese (Chinese)")]
        [Description("Cantonese (Chinese)")]
        CantoneseChinese,
        [EnumMember(Value = "Cebuano (Visayan)")]
        [Description("Cebuano (Visayan)")]
        CebuanoVisayan,
        [EnumMember(Value = "Chamorro")]
        [Description("Chamorro")]
        Chamorro,
        [EnumMember(Value = "Chaochow/Teochiu (Chinese)")]
        [Description("Chaochow/Teochiu (Chinese)")]
        ChaochowTeochiuChinese,
        [EnumMember(Value = "Cherokee")]
        [Description("Cherokee")]
        Cherokee,
        [EnumMember(Value = "Chippewa/Ojibawa/Ottawa")]
        [Description("Chippewa/Ojibawa/Ottawa")]
        ChippewaOjibawaOttawa,
        [EnumMember(Value = "Choctaw")]
        [Description("Choctaw")]
        Choctaw,
        [EnumMember(Value = "Comanche")]
        [Description("Comanche")]
        Comanche,
        [EnumMember(Value = "Coushatta")]
        [Description("Coushatta")]
        Coushatta,
        [EnumMember(Value = "Creek")]
        [Description("Creek")]
        Creek,
        [EnumMember(Value = "Croatian")]
        [Description("Croatian")]
        Croatian,
        [EnumMember(Value = "Crow")]
        [Description("Crow")]
        Crow,
        [EnumMember(Value = "Czech")]
        [Description("Czech")]
        Czech,
        [EnumMember(Value = "Danish")]
        [Description("Danish")]
        Danish,
        [EnumMember(Value = "Dard")]
        [Description("Dard")]
        Dard,
        [EnumMember(Value = "Dutch/Flemish")]
        [Description("Dutch/Flemish")]
        DutchFlemish,
        [EnumMember(Value = "Efik")]
        [Description("Efik")]
        Efik,
        [EnumMember(Value = "Eskimo")]
        [Description("Eskimo")]
        Eskimo,
        [EnumMember(Value = "Estonian")]
        [Description("Estonian")]
        Estonian,
        [EnumMember(Value = "Ethiopic")]
        [Description("Ethiopic")]
        Ethiopic,
        [EnumMember(Value = "Ewe")]
        [Description("Ewe")]
        Ewe,
        [EnumMember(Value = "Farsi (Persian)")]
        [Description("Farsi (Persian)")]
        FarsiPersian,
        [EnumMember(Value = "Finnish")]
        [Description("Finnish")]
        Finnish,
        [EnumMember(Value = "Fukien/Hokkien (Chinese)")]
        [Description("Fukien/Hokkien (Chinese)")]
        FukienHokkienChinese,
        [EnumMember(Value = "Gaelic (Irish)")]
        [Description("Gaelic (Irish)")]
        GaelicIrish,
        [EnumMember(Value = "Gaelic (Scottish)")]
        [Description("Gaelic (Scottish)")]
        GaelicScottish,
        [EnumMember(Value = "Greek")]
        [Description("Greek")]
        Greek,
        [EnumMember(Value = "Gujarati")]
        [Description("Gujarati")]
        Gujarati,
        [EnumMember(Value = "Guyanese")]
        [Description("Guyanese")]
        Guyanese,
        [EnumMember(Value = "Hainanese (Chinese)")]
        [Description("Hainanese (Chinese)")]
        HainaneseChinese,
        [EnumMember(Value = "Haitian-Creole")]
        [Description("Haitian-Creole")]
        HaitianCreole,
        [EnumMember(Value = "Hakka (Chinese)")]
        [Description("Hakka (Chinese)")]
        HakkaChinese,
        [EnumMember(Value = "Hausa")]
        [Description("Hausa")]
        Hausa,
        [EnumMember(Value = "Hebrew")]
        [Description("Hebrew")]
        Hebrew,
        [EnumMember(Value = "Hindi")]
        [Description("Hindi")]
        Hindi,
        [EnumMember(Value = "Hmong")]
        [Description("Hmong")]
        Hmong,
        [EnumMember(Value = "Hopi")]
        [Description("Hopi")]
        Hopi,
        [EnumMember(Value = "Hungarian")]
        [Description("Hungarian")]
        Hungarian,
        [EnumMember(Value = "Ibo/Igbo")]
        [Description("Ibo/Igbo")]
        IboIgbo,
        [EnumMember(Value = "Icelandic")]
        [Description("Icelandic")]
        Icelandic,
        [EnumMember(Value = "Ilonggo (Hiligaynon)")]
        [Description("Ilonggo (Hiligaynon)")]
        IlonggoHiligaynon,
        [EnumMember(Value = "Indonesian")]
        [Description("Indonesian")]
        Indonesian,
        [EnumMember(Value = "Italian")]
        [Description("Italian")]
        Italian,
        [EnumMember(Value = "Kache (Kaje, Jju)")]
        [Description("Kache (Kaje, Jju)")]
        KacheKajeJju,
        [EnumMember(Value = "Kannada (Kanarese)")]
        [Description("Kannada (Kanarese)")]
        KannadaKanarese,
        [EnumMember(Value = "Kanuri")]
        [Description("Kanuri")]
        Kanuri,
        [EnumMember(Value = "Kashmiri")]
        [Description("Kashmiri")]
        Kashmiri,
        [EnumMember(Value = "Kickapoo")]
        [Description("Kickapoo")]
        Kickapoo,
        [EnumMember(Value = "WoKonkanirk")]
        [Description("WoKonkanirk")]
        Konkani,
        [EnumMember(Value = "Kpelle")]
        [Description("Kpelle")]
        Kpelle,
        [EnumMember(Value = "Krio")]
        [Description("Krio")]
        Krio,
        [EnumMember(Value = "Kurdish")]
        [Description("Kurdish")]
        Kurdish,
        [EnumMember(Value = "Kwa")]
        [Description("Kwa")]
        Kwa,
        [EnumMember(Value = "Latvian")]
        [Description("Latvian")]
        Latvian,
        [EnumMember(Value = "Lingala")]
        [Description("Lingala")]
        Lingala,
        [EnumMember(Value = "Lithuanian")]
        [Description("Lithuanian")]
        Lithuanian,
        [EnumMember(Value = "Luganda")]
        [Description("Luganda")]
        Luganda,
        [EnumMember(Value = "Lunda")]
        [Description("Lunda")]
        Lunda,
        [EnumMember(Value = "Luyia (Luhya)")]
        [Description("Luyia (Luhya)")]
        LuyiaLuhya,
        [EnumMember(Value = "Macedonian")]
        [Description("Macedonian")]
        Macedonian,
        [EnumMember(Value = "Malay")]
        [Description("Malay")]
        Malay,
        [EnumMember(Value = "Malayalam")]
        [Description("Malayalam")]
        Malayalam,
        [EnumMember(Value = "Maltese")]
        [Description("Maltese")]
        Maltese,
        [EnumMember(Value = "Mandarin (Chinese)")]
        [Description("Mandarin (Chinese)")]
        MandarinChinese,
        [EnumMember(Value = "Mande")]
        [Description("Mande")]
        Mande,
        [EnumMember(Value = "Marathi")]
        [Description("Marathi")]
        Marathi,
        [EnumMember(Value = "Menominee")]
        [Description("Menominee")]
        Menominee,
        [EnumMember(Value = "Mien (Yao)")]
        [Description("Mien (Yao)")]
        MienYao,
        [EnumMember(Value = "Navajo")]
        [Description("Navajo")]
        Navajo,
        [EnumMember(Value = "Nepali")]
        [Description("Nepali")]
        Nepali,
        [EnumMember(Value = "Norwegian")]
        [Description("Norwegian")]
        Norwegian,
        [EnumMember(Value = "Okinawan")]
        [Description("Okinawan")]
        Okinawan,
        [EnumMember(Value = "Oneida")]
        [Description("Oneida")]
        Oneida,
        [EnumMember(Value = "Oriya")]
        [Description("Oriya")]
        Oriya,
        [EnumMember(Value = "Orri (Oring)")]
        [Description("Orri (Oring)")]
        OrriOring,
        [EnumMember(Value = "Pampangan")]
        [Description("Pampangan")]
        Pampangan,
        [EnumMember(Value = "Panjabi (Punjabi)")]
        [Description("Panjabi (Punjabi)")]
        PanjabiPunjabi,
        [EnumMember(Value = "Pashto (Pushto)")]
        [Description("Pashto (Pushto)")]
        PashtoPushto,
        [EnumMember(Value = "Pilipino (Tagalog)")]
        [Description("Pilipino (Tagalog)")]
        PilipinoTagalog,
        [EnumMember(Value = "Pima")]
        [Description("Pima")]
        Pima,
        [EnumMember(Value = "Polish")]
        [Description("Polish")]
        Polish,
        [EnumMember(Value = "Portuguese")]
        [Description("Portuguese")]
        Portuguese,
        [EnumMember(Value = "Pueblo")]
        [Description("Pueblo")]
        Pueblo,
        [EnumMember(Value = "Romanian")]
        [Description("Romanian")]
        Romanian,
        [EnumMember(Value = "Romany (Gypsy)")]
        [Description("Romany (Gypsy)")]
        RomanyGypsy,
        [EnumMember(Value = "Russian")]
        [Description("Russian")]
        Russian,
        [EnumMember(Value = "Samoan")]
        [Description("Samoan")]
        Samoan,
        [EnumMember(Value = "Serbian")]
        [Description("Serbian")]
        Serbian,
        [EnumMember(Value = "Shanghai (Chinese)")]
        [Description("Shanghai (Chinese)")]
        ShanghaiChinese,
        [EnumMember(Value = "Shona")]
        [Description("Shona")]
        Shona,
        [EnumMember(Value = "Sikkimese")]
        [Description("Sikkimese")]
        Sikkimese,
        [EnumMember(Value = "Sindhi")]
        [Description("Sindhi")]
        Sindhi,
        [EnumMember(Value = "Sinhalese (Sri Lanka)")]
        [Description("Sinhalese (Sri Lanka)")]
        SinhaleseSriLanka,
        [EnumMember(Value = "Sioux (Dakota)")]
        [Description("Sioux (Dakota)")]
        SiouxDakota,
        [EnumMember(Value = "Slavic")]
        [Description("Slavic")]
        Slavic,
        [EnumMember(Value = "Slovenian (Slovene)")]
        [Description("Slovenian (Slovene)")]
        SlovenianSlovene,
        [EnumMember(Value = "Somali")]
        [Description("Somali")]
        Somali,
        [EnumMember(Value = "Sotho")]
        [Description("Sotho")]
        Sotho,
        [EnumMember(Value = "Swahili")]
        [Description("Swahili")]
        Swahili,
        [EnumMember(Value = "Swedish")]
        [Description("Swedish")]
        Swedish,
        [EnumMember(Value = "Taiwanese/Formosan/Min Nan (Chinese)")]
        [Description("Taiwanese/Formosan/Min Nan (Chinese)")]
        TaiwaneseFormosanMinNanChinese,
        [EnumMember(Value = "Tamil")]
        [Description("Tamil")]
        Tamil,
        [EnumMember(Value = "Telugu (Telegu)")]
        [Description("Telugu (Telegu)")]
        TeluguTelegu,
        [EnumMember(Value = "Thai")]
        [Description("Thai")]
        Thai,
        [EnumMember(Value = "Tibetan")]
        [Description("Tibetan")]
        Tibetan,
        [EnumMember(Value = "Tigrinya")]
        [Description("Tigrinya")]
        Tigrinya,
        [EnumMember(Value = "Tiwa")]
        [Description("Tiwa")]
        Tiwa,
        [EnumMember(Value = "Tuluau")]
        [Description("Tuluau")]
        Tuluau,
        [EnumMember(Value = "Turkish")]
        [Description("Turkish")]
        Turkish,
        [EnumMember(Value = "Ukrainian")]
        [Description("Ukrainian")]
        Ukrainian,
        [EnumMember(Value = "Urdu")]
        [Description("Urdu")]
        Urdu,
        [EnumMember(Value = "Welsh")]
        [Description("Welsh")]
        Welsh,
        [EnumMember(Value = "Winnebago")]
        [Description("Winnebago")]
        Winnebago,
        [EnumMember(Value = "Yiddish")]
        [Description("Yiddish")]
        Yiddish,
        [EnumMember(Value = "Yombe")]
        [Description("Yombe")]
        Yombe,
        [EnumMember(Value = "Yoruba")]
        [Description("Yoruba")]
        Yoruba
    }
}