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
 * 
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using InBloomClient.Entities;
using InBloomClient.Services;
using InBloomApps.Controllers;
using InBloomApps.Models;
using InBloomApps.Helper;
using System.Net.Http;
using System.Web.Caching;
using System.Text;
using System.IO;

namespace InBloomApps.Controllers
{
    [AsyncTimeout(5000)]
    [HandleError(ExceptionType = typeof(TimeoutException), View = "Timeout")]
    [HandleError(ExceptionType = typeof(NullReferenceException), View = "Error")]
    [HandleError(ExceptionType = typeof(HttpRequestException), View = "PermissionError")]
    public class HomeController : BaseController
    {
        const string MAIN = "/Home/GroupSelection";
        const string INBLOOM_USER_SESSION = "inBloom_user";
        const string INBLOOM_USER_ID = "inBloom_user_id";

        public ActionResult Index()
        {
            if (Session["access_token"] == null)
            {
                if (Request.QueryString["code"] == null || Request.QueryString["code"] == "")
                {
                    return View("LandingPage");
                }
                else
                {
                    return GetToken(MAIN);
                }
            }
            else
            {
                // We have an access token in session, let's redirect to app main page.
                return RedirectToAction(MAIN);
            }
        }

        public ActionResult Login()
        {
            if (Session["access_token"] == null)
            {
                return GetToken(MAIN);
            }
            else
            {
                // We have an access token in session, let's redirect to app main page.
                return RedirectToAction(MAIN);
            }
        }

        /// <summary>
        /// AJAX to this method to append the lesson plan info to the group (not the lesson plan itself)
        /// </summary>
        /// <param name="obj">data object to update cohort</param>
        /// <returns></returns>
        public ActionResult AttachFile(CohortActionObject obj)
        {
            try
            {
                var cohortResult = ProcessOneCohortFileUpload(obj);
                return Json(cohortResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //handle
                throw;
            }
        }

        /// <summary>
        /// Update one cohort custom
        /// </summary>
        /// <param name="obj">data object to update cohort</param>
        /// <returns>result of the action</returns>
        public async Task<Result> ProcessOneCohortFileUpload(CohortActionObject obj)
        {
            try
            {
                var accessToken = Session["access_token"];
                if (accessToken != null)
                {
                    var cohortResult = new Result();

                    var cs = new CohortService(accessToken.ToString());
                    // update cohort custom entity
                    var cohortCustom = CohortActionHelper.UpdateCustom(obj, cs);

                    var tasksToWaitFor = new List<Task>();                    
                    tasksToWaitFor.Add(cohortCustom);                    

                    await Task.WhenAll(tasksToWaitFor);
                 
                    //determine whether custom was created successfully
                    CohortActionHelper.ProcessCustomResult(cohortResult, cohortCustom, HttpStatusCode.NoContent, obj.custom, cs);

                    //remove cohort from cache after an update
                    HttpContext.Cache.Remove(obj.cohort.id);

                    return cohortResult;
                }
                else
                {
                    //session expired
                    return GlobalHelper.GetSessionExpiredResult(obj.cohort.id);
                }
            }
            catch (Exception e)
            {
                return GlobalHelper.GetExceptionResult(obj.cohort.id, e);
            }

        }

        [HttpPost]
        public async Task<ActionResult> UploadFiles()
        {
            try
            {
                var accessToken = Session["access_token"];
                if (accessToken != null)
                {
                    var results = new List<ViewDataUploadFilesResult>();
                    var groupIds = Request["groupId"].Split(',');
                    var files = Request.Files;

                    for (int i = 0; i < groupIds.Length; i++)
                    {
                        string groupId = groupIds[i];
                        bool isSuccess = false;
                        ViewDataUploadFilesResult res = new ViewDataUploadFilesResult() { CohortId = groupId };
                        try
                        {
                            HttpPostedFileBase hpf = files[i];
                            // IE passes in the entire path, so we got to make sure we only grab the file name
                            int startIdx = hpf.FileName.LastIndexOf("\\");
                            string fileName = startIdx < 0 ? hpf.FileName : hpf.FileName.Substring(startIdx + 1);
                            string filePath = string.Format("/{0}", fileName);
                            FTPHelper.uploadFileFromStream(groupId, filePath, hpf.InputStream);
                            isSuccess = true;

                            // update the cohort custom 
                            var cs = new CohortService(accessToken.ToString());
                            var cohort = CohortHelper.GetGroupById(accessToken.ToString(), groupId);
                            var group = CohortHelper.GetCohortDisplayObject(cs, cohort).Result;
                            var cohortActionObj = new CohortActionObject()
                            {
                                cohort = cohort,
                                custom = group.custom
                            };
                            // attach the lesson plan info
                            cohortActionObj.custom.lessonPlan = new LessonPlan()
                            {
                                name = fileName,
                                type = hpf.ContentType
                            };

                            var result = await ProcessOneCohortUpdate(cohortActionObj);

                            isSuccess = result.customActionResult.isSuccess;
                            res.Name = fileName;
                            res.Type = hpf.ContentType;
                            res.Length = hpf.ContentLength;
                            res.isSuccess = isSuccess;
                        }
                        catch (Exception ex)
                        {
                            res.isSuccess = false;
                        }

                        results.Add(res);
                    }

                    // Returns json
                    return Json(results, "text/html", System.Text.Encoding.UTF8);
                    //Content("{\"name\":\"" + results[0].Name + "\",\"type\":\"" + results[0].Type + "\",\"size\":\"" + string.Format("{0} bytes", results[0].Length) + "\"}", "text/plain");
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            // add error message
            return null;
        }

        /// <summary>
        /// Synchronous method for downloading a group's information as a text file
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ActionResult DownloadAttachment(string id)
        {
            try
            {
                var accessToken = Session["access_token"];
                if (accessToken != null)
                {
                    var cs = new CohortService(accessToken.ToString());
                    var cohort = CohortHelper.GetGroupById(accessToken.ToString(), id);
                    var cohortDisplayObj = CohortHelper.GetCohortDisplayObject(cs, cohort).Result;
                    string cohortId = cohort.id;
                    string fileName = cohortDisplayObj.custom.lessonPlan.name;
                    string contentType = cohortDisplayObj.custom.lessonPlan.type;

                    Stream fileStream = FTPHelper.downloadFile("/" + cohortId + "/" + fileName);

                    return File(fileStream,
                         contentType,
                         fileName);
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Log user out of the current session
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Logout()
        {
            var token = Session["access_token"];
            if (token != null)
            {
                try
                {
                    var ss = new SessionService(token.ToString());
                    var result = await ss.Logout();
                    Session.Clear();
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {
                    //logout fail
                    Session.Clear();
                    return Json(new LogOutResult { logout = false, msg = e.Message }, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                //user is already logged out
                return Json(new LogOutResult { logout = true, msg = "There was no access token" }, JsonRequestBehavior.AllowGet);
            }            
        }

        //[OutputCache(Duration = 1200, VaryByParam = "none")]
        public ActionResult MultipleGroupsEdit()
        {
            return View("MultipleGroupsEdit");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GroupSelection()
        {
            return View("GroupSelection");
        }

        /// <summary>
        /// Synchronous method for downloading a group's information as a text file
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public ActionResult DownloadGroup(string id)
        {
            try
            {
                var accessToken = Session["access_token"];
                if (accessToken != null)
                {   
                    var cohort = CohortHelper.GetGroupById(accessToken.ToString(), id);                    
                    StringBuilder sb = new StringBuilder();
                    sb.Append("Name: " + cohort.cohortIdentifier + "\r\n\r\n");
                    sb.Append("Description: " + cohort.cohortDescription + "\r\n\r\n");                    

                    var cs = new CohortService(accessToken.ToString());
                    var group = CohortHelper.GetCohortDisplayObject(cs, cohort);

                    if (group.Result.students.Count() > 0)
                    {
                        sb.Append("Students:\r\n");
                        var st = GetStudents();
                        var allStudents = st.Result;
                        var studentIds = group.Result.students;
                        foreach (string sid in studentIds)
                        {
                            var student = allStudents.First(c => c.id.Equals(sid));
                            sb.Append(student.name.firstName + " " + student.name.lastSurName + "\r\n");
                        }
                    }
                    else
                    {
                        sb.Append("[no students]");
                    }
                    
                    return File(Encoding.UTF8.GetBytes(sb.ToString()),
                         "text/plain",
                          string.Format("{0}.txt", cohort.cohortIdentifier));
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// AJAX to this method to create a brand new group with students
        /// </summary>
        /// <param name="obj">the cohort to delete</param>
        /// <returns>result of the delete</returns>
        public async Task<ActionResult> CreateGroup(CohortActionObject obj)
        {
            try
            {
                var cohortResult = await ProcessOneCohortCreate(obj);
                return Json(cohortResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //handle
                throw;
            }
        }

        /// <summary>
        /// AJAX to this method to create brand new groups with students
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Result>> CreateMultipleGroups(IEnumerable<CohortActionObject> objs)
        {
            try
            {
                var result = await Task.WhenAll(from obj in objs select ProcessOneCohortCreate(obj));
                return result;
            }
            catch (Exception e)
            {
                //handle
                throw;
            }
        }

        /// <summary>
        /// AJAX to this method to update an existing group
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<ActionResult> UpdateGroup(CohortActionObject obj)
        {
            try
            {
                var cohortResult = await ProcessOneCohortUpdate(obj);
                return Json(cohortResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //handle
                throw;
            }
        }

        /// <summary>
        /// AJAX to this method to update multiple groups
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Result>> UpdateMultipleGroups(IEnumerable<CohortActionObject> objs)
        {
            try
            {
                var result = await Task.WhenAll(from obj in objs select ProcessOneCohortUpdate(obj));
                return result;
            }
            catch (Exception e)
            {
                //handle
                throw;
            }
        }

        /// <summary>
        /// AJAX to this method to delete a cohort by passing in a cohort id
        /// </summary>
        /// <param name="id">id of the cohort to delete</param>
        /// <returns>result</returns>
        public async Task<ActionResult> DeleteGroup(string id)
        {
            try
            {
                var cohortResult = await ProcessOneCohortDelete(id);
                return Json(cohortResult, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// AJAX to this method to delete multiple groups
        /// </summary>
        /// <param name="ids">list of cohort ids to delete</param>
        /// <returns>result</returns>
        public async Task<ActionResult> DeleteMultipleGroups(IEnumerable<string> ids)
        {
            try
            {
                var result = await Task.WhenAll(from id in ids select ProcessOneCohortDelete(id));
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                //handle
                throw;
            }
        }


        /// <summary>
        /// AJAX to this method to get Cohort by Id
        /// </summary>
        /// <param name="id">the id of the cohort to get</param>
        /// <returns>return the requested Cohort object</returns>
        public async Task<Cohort> GetGroupById(string id)
        {
            try
            {
                var accessToken = Session["access_token"];
                if (accessToken != null)
                {
                    var cohort = await CohortHelper.GetCohortById(accessToken.ToString(), id);
                    return cohort;
                }
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Get all cohorts from SLI for the current user session
        /// </summary>
        /// <returns>list of all cohorts the current user has access to</returns>
        public async Task<IEnumerable<Cohort>> GetCohorts()
        {
            var c = new CohortService(Session["access_token"].ToString());
            var cohorts = await c.GetAll();

            return cohorts;
        }

        /// <summary>
        /// Get all students from SLI for the current user session
        /// </summary>
        /// <returns>list of all students the current user has access to</returns>
        public async Task<IEnumerable<Student>> GetStudents()
        {
            var s = new StudentService(Session["access_token"].ToString());
            var students = await s.GetAll();

            return students;
        }

        /// <summary>
        /// Get all sections from SLI for the current user session
        /// </summary>
        /// <returns>List of all sections the current user has access to</returns>
        public async Task<IEnumerable<Section>> GetSections()
        {
            var c = new SectionService(Session["access_token"].ToString());
            var list = await c.GetAll();

            return list;
        }

        /// <summary>
        /// Create a single cohort
        /// </summary>
        /// <returns>result of the cohort creation</returns>
        public async Task<Result> CreateCohort(CohortService cs, Cohort cohort) //TODO: modify to accept a cohort argument
        {
            try
            {
                var userSession = (UserSession)Session[INBLOOM_USER_SESSION];

                //set temporary edorgid because it's null from the INBLOOM API
                if (userSession != null && userSession.edOrgId != null && userSession.edOrgId != "")
                    cohort.educationOrgId = userSession.edOrgId;
                else
                    cohort.educationOrgId = CURRENT_ED_ORG_ID;
                cohort.cohortType = InBloomClient.Enum.CohortType.Other;

                var response = await cs.Create(cohort);

                var result = new Result
                {
                    completedSuccessfully = response.StatusCode == HttpStatusCode.Created,
                    objectActionResult = GlobalHelper.GetActionResponseResult("", cohort.cohortIdentifier, response, HttpStatusCode.Created)
                };

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    //another way of getting the Id: result.Headers.Location.AbsolutePath.Substring(result.Headers.Location.AbsolutePath.LastIndexOf("/") + 1)              
                    result.objectActionResult.objectId = response.Headers.Location.Segments[5]; //getting the id from header location
                    result.objectActionResult.objectName = cohort.cohortIdentifier;
                }

                return result;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Update a single cohort
        /// </summary>
        /// <returns>result of the update</returns>
        public async Task<Result> UpdateCohort(CohortService cs, Cohort cohort)
        {
            try
            {
                var userSession = (UserSession)Session[INBLOOM_USER_SESSION];

                //user session has edOrgId == null but we need edOrgId to update a cohort
                if (userSession != null && userSession.edOrgId != null && userSession.edOrgId != "")
                    cohort.educationOrgId = userSession.edOrgId;
                else
                    cohort.educationOrgId = CURRENT_ED_ORG_ID;

                var response = await cs.Update(cohort);

                var result = new Result
                {
                    completedSuccessfully = response.StatusCode == HttpStatusCode.NoContent,
                    objectActionResult = GlobalHelper.GetActionResponseResult(cohort.id, cohort.cohortIdentifier, response, HttpStatusCode.NoContent)
                };

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// Delete a cohort
        /// </summary>
        /// <param name="id">the id of the cohort to delete</param>
        public static async Task<Result> DeleteCohort(CohortService cs, string id)
        {
            try
            {
                var result = new Result { completedSuccessfully = false }; //default to false, set to true later if it's successful
                var response = await cs.DeleteById(id);

                if (response.StatusCode == HttpStatusCode.NoContent)
                    result.completedSuccessfully = true;

                return result;
            }
            catch
            {
                throw;
            }
        }  

        /// <summary>
        /// Update one cohort
        /// </summary>
        /// <param name="obj">data object to update cohort</param>
        /// <returns>result of the action</returns>
        public async Task<Result> ProcessOneCohortUpdate(CohortActionObject obj)
        {
            try
            {
                var accessToken = Session["access_token"];
                if (accessToken != null)
                {
                    var cs = new CohortService(accessToken.ToString());
                    //1) update cohort
                    var cohortResult = await UpdateCohort(cs, obj.cohort);
                    //2) create student cohort association                    
                    var newStudentsAssociations = CohortActionHelper.GetNewStudentCohortAssociations(obj, cs);
                    //3) update cohort custom entity
                    var cohortCustom = CohortActionHelper.UpdateCustom(obj, cs);

                    // if there is no lesson plan, then delete the group's directory from the FTP server
                    if (obj.custom.lessonPlan == null)
                    {
                        FTPHelper.removeDir(obj.cohort.id);
                    }

                    //4) remove students from cohort
                    Task<IEnumerable<ActionResponseResult>> removeStudents;
                    if (obj.studentsToDelete != null && obj.studentsToDelete.Count() > 0)
                        removeStudents = await CohortActionHelper.DeleteStudentCohortAssocations(obj, cs);
                    else
                        removeStudents = null;

                    //contruct a list of tasks we're waiting for
                    var tasksToWaitFor = new List<Task>();
                    if (newStudentsAssociations != null) tasksToWaitFor.Add(newStudentsAssociations);
                    if (cohortCustom != null) tasksToWaitFor.Add(cohortCustom);
                    if (removeStudents != null) tasksToWaitFor.Add(removeStudents);

                    await Task.WhenAll(tasksToWaitFor);

                    if (newStudentsAssociations != null) CohortActionHelper.DetermineFailedToCreateFor(cohortResult, newStudentsAssociations.Result);
                    if (removeStudents != null) CohortActionHelper.DetermineFailedToDeleteFor(cohortResult, removeStudents.Result);

                    //determine whether custom was created successfully
                    CohortActionHelper.ProcessCustomResult(cohortResult, cohortCustom, HttpStatusCode.NoContent, obj.custom, cs);

                    //remove cohort from cache after an update
                    HttpContext.Cache.Remove(obj.cohort.id);

                    return cohortResult;
                }
                else
                {
                    //session expired
                    return GlobalHelper.GetSessionExpiredResult(obj.cohort.id);
                }
            }
            catch (Exception e)
            {
                return GlobalHelper.GetExceptionResult(obj.cohort.id, e);
            }

        }

        /// <summary>
        /// Create one cohort
        /// </summary>
        /// <param name="obj">data object to create cohort</param>
        /// <returns>result of the action</returns>
        public async Task<Result> ProcessOneCohortCreate(CohortActionObject obj)
        {
            try
            {
                var accessToken = Session["access_token"];
                if (accessToken != null)
                {
                    var cs = new CohortService(accessToken.ToString());
                    //create the cohort first
                    var cohortResult = await CreateCohort(cs, obj.cohort);

                    //if cohort was created successfully then continue to create associations
                    if (cohortResult.completedSuccessfully)
                    {
                        var staffId = Session[INBLOOM_USER_ID].ToString();
                        await ProcessASuccessfulCohortCreate(obj, cs, cohortResult, staffId);
                    }

                    return cohortResult;
                }
                else
                {
                    //section expired
                    return GlobalHelper.GetSessionExpiredResult(obj.cohort.cohortIdentifier);
                }

            }
            catch (Exception e)
            {
                return GlobalHelper.GetExceptionResult(obj.cohort.cohortIdentifier, e);
            }
        }

        private static async Task ProcessASuccessfulCohortCreate(CohortActionObject obj, CohortService cs, Result cohortResult, string staffId)
        {
            obj.cohort.id = cohortResult.objectActionResult.objectId;
            obj.custom.cohortId = obj.cohort.id;
            //1) start creating staff/student cohort association
            var newStudentsAssociations = CohortActionHelper.GetNewStudentCohortAssociations(obj, cs);
            var newStaffAssociation = CohortActionHelper.CreateOneStaffCohortAssociation(cs, obj.cohort.id, staffId);
            //2) initial populate of the cohort custom entity        
            var cohortCustom = CohortActionHelper.CreateCustom(obj.custom, cs);

            //contruct a list of tasks we're waiting for
            var tasksToWaitFor = new List<Task>();
            if (newStudentsAssociations != null) tasksToWaitFor.Add(newStudentsAssociations);
            if (cohortCustom != null) tasksToWaitFor.Add(cohortCustom);
            if (newStaffAssociation != null) tasksToWaitFor.Add(newStaffAssociation);

            await Task.WhenAll(tasksToWaitFor);

            if (newStudentsAssociations != null)
                CohortActionHelper.DetermineFailedToCreateFor(cohortResult, newStudentsAssociations.Result);

            //determine whether custom was created successfully
            CohortActionHelper.ProcessCustomResult(cohortResult, cohortCustom, HttpStatusCode.Created);
        }

        /// <summary>
        /// Delete one cohort
        /// </summary>
        /// <param name="obj">data object to deletes cohort</param>
        /// <returns>result of the action</returns>
        public async Task<Result> ProcessOneCohortDelete(string id)
        {
            try
            {
                var accessToken = Session["access_token"];
                if (accessToken != null)
                {
                    var cs = new CohortService(Session["access_token"].ToString());                                 

                    var cohortResult = await DeleteCohort(cs, id);
                    //remove cohort from cache after an update
                    HttpContext.Cache.Remove(id);

                    // delete the group's directory from the FTP server
                    FTPHelper.removeDir(id);

                    return cohortResult;
                }
                else
                {
                    //session has expired
                    return GlobalHelper.GetSessionExpiredResult(id);
                }
            }
            catch (Exception e)
            {
                return GlobalHelper.GetExceptionResult(id, e);
            }
        }

        /// <summary>
        /// AJAX to this method for a master save of all groups
        /// </summary>
        /// <param name="list">list of CohortActionObject to save</param>
        /// <returns>list of result of this action</returns>
        public async Task<ActionResult> SaveAll(IEnumerable<CohortActionObject> list)
        {
            try
            {
                var cohortsToUpdate = from cao in list where cao.cohort.id != null select cao;
                var cohortsToCreate = from cao in list where cao.cohort.id == null select cao;

                var updateCohorts = UpdateMultipleGroups(cohortsToUpdate);
                var createCohorts = CreateMultipleGroups(cohortsToCreate);

                var allTasks = await Task.WhenAll(updateCohorts, createCohorts);

                var resultList = new List<Result>();
                resultList.AddRange(updateCohorts.Result);
                resultList.AddRange(createCohorts.Result);

                return Json(resultList, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Get all necessary data for initial page load
        /// </summary>
        /// <returns>data needed for page load</returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<ActionResult> Group()
        {
            var accessToken = Session["access_token"];
            if (accessToken != null)
            {
                var cs = new CohortService(accessToken.ToString());
                var ss = new StudentService(accessToken.ToString());
                var ses = new SectionService(accessToken.ToString());

                var co = GetCohorts();
                var st = GetStudents();
                var se = GetSections();

                //Get the available data elements and colors
                var dataElements = GlobalHelper.InitializeDataElements();
                var colors = GlobalHelper.InitializeColors();

                await Task.WhenAll(co, st, se);

                var cohorts = Task.WhenAll(from c in co.Result select CohortHelper.GetCohortDisplayObject(cs, c));
                var students = Task.WhenAll(from s in st.Result select StudentHelper.GetStudentDisplayObject(ss, s));
                var sections = Task.WhenAll(from s in se.Result select SectionHelper.GetSectionDisplayObject(ses, s));

                await Task.WhenAll(cohorts, students, se);

                var filters = FilterHelper.InitializeFilters(sections.Result); //contruct filter values to filter students in the app
                await Task.WhenAll(dataElements, colors);

                var data = GetGroupingDisplayObject(dataElements, colors, filters, cohorts, students, sections);

                return Json(data, JsonRequestBehavior.AllowGet);
            }

            //session has expired, refresh page
            return View("Index");
        }

        /// <summary>
        /// Populate a GroupingDisplayObject
        /// </summary>
        /// <param name="dataElements"></param>
        /// <param name="colors"></param>
        /// <param name="filters"></param>
        /// <param name="cohorts"></param>
        /// <param name="students"></param>
        /// <param name="sections"></param>
        /// <returns></returns>
        private static GroupingDisplayObject GetGroupingDisplayObject(Task<IEnumerable<DataElement>> dataElements, Task<IEnumerable<Color>> colors, IEnumerable<Models.Filter> filters, Task<CohortDisplayObject[]> cohorts, Task<StudentDisplayObject[]> students, Task<SectionDisplayObject[]> sections)
        {
            //Construct a master object to for display purpose
            var data = new GroupingDisplayObject();
            data.cohorts = cohorts.Result;

            IEnumerable<StudentDisplayObject> studentDisplayObjects = students.Result;
            List<StudentDisplayObject> listOfStudents = studentDisplayObjects.ToList();
            listOfStudents.Sort();

            data.students = listOfStudents;
            data.sections = sections.Result;
            data.dataElements = dataElements.Result;
            data.colors = colors.Result;
            data.filters = filters;
            return data;
        }

        /// <summary>
        /// Return the LoginError view
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginError()
        {
            Session.Clear();
            return View();
        }

        /// <summary>
        /// Return the Error view
        /// </summary>
        /// <returns></returns>
        public ActionResult Error()
        {
            return View();
        }       

        /// <summary>
        /// INBLOOM OAuth
        /// </summary>
        /// <param name="redirectUrl">url to redirect to after successful authentication</param>       
        private ActionResult GetToken(string redirectUrl)
        {
            try
            {
                // We get a code back from the first leg of OAuth process.  If we don't have one, let's get it.
                if (Request.QueryString["code"] == null || Request.QueryString["code"] == "")
                {
                    // Here the user will log into the INBLOOM.
                    string authorizeUrl = string.Format(INBLOOM_SANDBOX_LOGIN, INBLOOM_CLIENT_ID, INBLOOM_REDIRECT_URL);
                    return Redirect(authorizeUrl);
                }
                else
                {
                    return ProcessSecondPartOfOAuth(redirectUrl);
                }
            }
            catch (Exception e)
            {
                ExceptionHelper.LogCaughtException(e);
                return RedirectToAction("LoginError", new { code = "" });
            }
        }

        /// <summary>
        /// Get inBloom token
        /// </summary>
        /// <param name="redirectUrl">url to redirect to after successful authentication</param>
        private ActionResult ProcessSecondPartOfOAuth(string redirectUrl)
        {
            // Now we have a code, we can run the second leg of OAuth process.
            string code = Request.QueryString["code"];

            // Set the authorization URL
            string sessionUrl = string.Format(INBLOOM_OAUTH_URL, INBLOOM_CLIENT_ID, INBLOOM_SHARED_SECRET, INBLOOM_REDIRECT_URL, code);

            var client = new HttpClient();
            var response = client.GetAsync(sessionUrl).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string access_token = JObject.Parse(response.Content.ReadAsStringAsync().Result)["access_token"].ToString();
                // If we have a valid token, it'll be 38 chars long.  Let's add it to session if so.
                if (access_token.Length == 38)
                {
                    Session.Add("access_token", access_token);

                    //Get the current user session info
                    var ss = new SessionService(access_token);
                    var userSession = ss.Get().Result;
                    var staffId = ss.GetCurrentUserId().Result;

                    //Get edOrg through staff service because inBloom user session service call always comes back with a null edOrg
                    var staffService = new StaffService(access_token);
                    var staffOrg = staffService.GetStaffEducationOrganizationAssociations(staffId).Result;
                    
                    if(staffOrg.FirstOrDefault() != null)
                        userSession.edOrgId = staffOrg.FirstOrDefault().educationOrganizationReference;

                    Session.Add(INBLOOM_USER_SESSION, userSession);
                    Session.Add(INBLOOM_USER_ID, staffId);

                    // Redirect to app main page.
                    return Redirect(redirectUrl);
                }
            }

            //error logging into inBloom                
            return RedirectToAction("LoginError", new { code = "" });
        }
    }
}
