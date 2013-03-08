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
    public enum SpecialAccommodationItemType
    {
        [EnumMember(Value = "Presentation")]
        Presentation,
        [EnumMember(Value = "Response")]
        Response,
        [EnumMember(Value = "Setting")]
        Setting,
        [EnumMember(Value = "Timing and Scheduling")]
        TimingAndScheduling,
        [EnumMember(Value = "Large Print")]
        LargePrint,
        [EnumMember(Value = "Dyslexia Bundled")]
        DyslexiaBundled,
        [EnumMember(Value = "Oral Administration")]
        OralAdministration,
        [EnumMember(Value = "Adjustable swivel arm")]
        AdjustableSwivelArm,
        [EnumMember(Value = "Adjustable table height")]
        AdjustableTableHeight,
        [EnumMember(Value = "Braille")]
        Braille,
        [EnumMember(Value = "Enlarged monitor view")]
        EnlargedMonitorView,
        [EnumMember(Value = "Extra time")]
        ExtraTime,
        [EnumMember(Value = "Large print booklet")]
        LargePrintBooklet,
        [EnumMember(Value = "Multi-day administration")]
        MultiDayAdministration,
        [EnumMember(Value = "Recorder or amanuensis")]
        RecorderOrAmanuensis,
        [EnumMember(Value = "Separate room")]
        SeparateRoom,
        [EnumMember(Value = "Signer/sign language interpreter")]
        SignerSignLanguageInterpreter,
        [EnumMember(Value = "Special furniture")]
        SpecialFurniture,
        [EnumMember(Value = "Special lighting")]
        SpecialLighting,
        [EnumMember(Value = "Track ball")]
        TrackBall,
        [EnumMember(Value = "Untimed")]
        Untimed,
        [EnumMember(Value = "Enlarged keyboard")]
        EnlargedKeyboard,
        [EnumMember(Value = "Additional example items/tasks")]
        AdditionalExampleItemsTasks,
        [EnumMember(Value = "Administration in several sessions")]
        AdministrationInSeveralSessions,
        [EnumMember(Value = "Answers written directly in test booklet")]
        AnswersWrittenDirectlyInTestBooklet,
        [EnumMember(Value = "Assessment in native language")]
        AssessmentInNativeLanguage,
        [EnumMember(Value = "Arithmetic table (math or science)")]
        ArithmeticTableMathOrScience,
        [EnumMember(Value = "Assistive device that does not interfere with independent work of the student")]
        AssistiveDeviceThatDoesNotInterfereWithIndependentWorkOfTheStudent,
        [EnumMember(Value = "Assistive device that does interfere with independent work of the student")]
        AssistiveDeviceThatDoesInterfereWithIndependentWorkOfTheStudent,
        [EnumMember(Value = "Audiotape or CD")]
        AudiotapeOrCD,
        [EnumMember(Value = "Audio cassette player")]
        AudioCassettePlayer,
        [EnumMember(Value = "Braille writer, no thesaurus, spell- or grammar-checker")]
        BrailleWriterNoThesaurusSpellOrGrammarChecker,
        [EnumMember(Value = "Breaks during testing")]
        BreaksDuringTesting,
        [EnumMember(Value = "Calculator (math or science)")]
        CalculatorMathOrScience,
        [EnumMember(Value = "Clarify directions")]
        ClarifyDirections,
        [EnumMember(Value = "Colored lenses")]
        ColoredLenses,
        [EnumMember(Value = "Computer administration")]
        ComputerAdministration,
        [EnumMember(Value = "Cranmer abacus")]
        CranmerAbacus,
        [EnumMember(Value = "Cueing")]
        Cueing,
        [EnumMember(Value = "Dictionary in English")]
        DictionaryInEnglish,
        [EnumMember(Value = "Dictionary in native language")]
        DictionaryInNativeLanguage,
        [EnumMember(Value = "Dictated oral repsonse (to a proctor)")]
        DictatedOralRepsonseToAProctor,
        [EnumMember(Value = "Directions read aloud or explained")]
        DirectionsReadAloudOrExplained,
        [EnumMember(Value = "Examiner familiarity")]
        ExaminerFamiliarity,
        [EnumMember(Value = "Font enlarged beyond print version requirements")]
        FontEnlargedBeyondPrintVersionRequirements,
        [EnumMember(Value = "Foreign language interpreter")]
        ForeignLanguageInterpreter,
        [EnumMember(Value = "Foreign language interpreter for instructions, ask questions")]
        ForeignLanguageInterpreterForInstructionsAskQuestions,
        [EnumMember(Value = "Format")]
        Format,
        [EnumMember(Value = "Hospital/home testing")]
        HospitalHomeTesting,
        [EnumMember(Value = "Linguistic modification of test directions")]
        LinguisticModificationOfTestDirections,
        [EnumMember(Value = "Magnification device")]
        MagnificationDevice,
        [EnumMember(Value = "Manually coded English or American Sign Language to present test questions")]
        ManuallyCodedEnglishOrAmericanSignLanguageToPresentTestQuestions,
        [EnumMember(Value = "Math manipulatives (math or science)")]
        MathManipulativesMathOrScience,
        [EnumMember(Value = "Modification of linguistic complexity")]
        ModificationOfLinguisticComplexity,
        [EnumMember(Value = "Multiple test sessions")]
        MultipleTestSessions,
        [EnumMember(Value = "Oral directions in the native language")]
        OralDirectionsInTheNativeLanguage,
        [EnumMember(Value = "Paraphrasing")]
        Paraphrasing,
        [EnumMember(Value = "Physical supports")]
        PhysicalSupports,
        [EnumMember(Value = "Response dictated in American Sign Language")]
        ResponseDictatedInAmericanSignLanguage,
        [EnumMember(Value = "Response in native language")]
        ResponseInNativeLanguage,
        [EnumMember(Value = "Scheduled extended time")]
        ScheduledExtendedTime,
        [EnumMember(Value = "Separate room with other English Learners under supervision of district employee")]
        SeparateRoomWithOtherEnglishLearnersUnderSupervisionOfDistrictEmployee,
        [EnumMember(Value = "Signer/sign language for instructions, ask questions")]
        SignerSignLanguageForInstructionsAskQuestions,
        [EnumMember(Value = "Simplified language")]
        SimplifiedLanguage,
        [EnumMember(Value = "Small-group or individual administration")]
        SmallGroupOrIndividualAdministration,
        [EnumMember(Value = "Speech recognition system")]
        SpeechRecognitionSystem,
        [EnumMember(Value = "Spell-checker")]
        SpellChecker,
        [EnumMember(Value = "Specialized setting")]
        SpecializedSetting,
        [EnumMember(Value = "Student read aloud")]
        StudentReadAloud,
        [EnumMember(Value = "Student-requested extended time")]
        StudentRequestedExtendedTime,
        [EnumMember(Value = "Supervised test breaks")]
        SupervisedTestBreaks,
        [EnumMember(Value = "Technological aid")]
        TechnologicalAid,
        [EnumMember(Value = "Test administrator marked / wrote test at student's direction")]
        TestAdministratorMarkedWroteTestAtStudentsDirection,
        [EnumMember(Value = "Test adminstrator read questions aloud")]
        TestAdminstratorReadQuestionsAloud,
        [EnumMember(Value = "Test administered at best time of day for student handwriting issues")]
        TestAdministeredAtBestTimeOfDayForStudentHandwritingIssues,
        [EnumMember(Value = "Text changes in vocabulary")]
        TextChangesInVocabulary,
        [EnumMember(Value = "Translation dictionary")]
        TranslationDictionary,
        [EnumMember(Value = "Verbalized problem-solving")]
        VerbalizedProblemSolving,
        [EnumMember(Value = "Video cassette")]
        VideoCassette,
        [EnumMember(Value = "Visual cues")]
        VisualCues,
        [EnumMember(Value = "Word processor")]
        WordProcessor,
        [EnumMember(Value = "Word processor / typewriter / computer with thesaurus / spell-checker / grammar-checker turned off")]
        WordProcessorTypewriterComputerWithThesaurusSpellCheckerGrammarCheckerTurnedOff,
        [EnumMember(Value = "Word processor / typewriter / computer with thesaurus/ spell-checker/ grammar-checker enabled on essay response portion of test")]
        WordProcessorTypewriterComputerWithThesaurusSpellCheckeGrammarCheckeEnabledOnEssayResponsePortionOfTest,
        [EnumMember(Value = "Unknown")]
        Unknown,
        [EnumMember(Value = "Other")]
        Other
    }
}