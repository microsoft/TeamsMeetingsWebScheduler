/* 
*  Copyright (c) Microsoft. All rights reserved. Licensed under the MIT license. 
*  See LICENSE in the source repository root for complete license information. 
*/

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using MicrosoftTeamsSchedulerWebsite.Services;
using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MicrosoftTeamsSchedulerWebsite.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IGraphServiceClientFactory _graphServiceClientFactory;

        public HomeController(IWebHostEnvironment hostingEnvironment, IGraphServiceClientFactory graphServiceClientFactory)
        {
            _env = hostingEnvironment;
            _graphServiceClientFactory = graphServiceClientFactory;
        }

        public class Meetings
        {
            public string Subject { get; set; }
            public string BodyPreview { get; set; }
            public string WebLink { get; set; }
            public string Attendees { get; set; }
            public string OnlineMeetingUrl { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }



        public class JsonMeetingsCLass
        {
            [JsonProperty("Subject")]
            public string Subject { get; set; }
            public string BodyPreview { get; set; }
            public string Start { get; set; }
            public string End { get; set; }
            public string WebLink{ get; set; }
            [JsonProperty("Attendees")]
            public string[] Attendees { get; set; }
            public string OnlineMeetingUrl { get; set; }
        }



        [AllowAnonymous]
        // Load user's profile.
        public async Task<IActionResult> Index(string email)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Get users's email.
                email ??= User.FindFirst("preferred_username")?.Value;
                ViewData["Email"] = email.Replace("@teams.", "@");

                

                // Initialize the GraphServiceClient.
                var graphClient = _graphServiceClientFactory.GetAuthenticatedGraphClient((ClaimsIdentity)User.Identity);

                //ViewData["Response"] = await GraphService.GetUserJson(graphClient, email, HttpContext);

                //   ViewData["Response"] = await GraphService.GetUserMeetings(graphClient, email, HttpContext);
               /* string meetingsjson = await GraphService.GetUserMeetings(graphClient, email, HttpContext);


                var meetingsobject = JsonConvert.DeserializeObject<List<JsonMeetingsCLass>>(meetingsjson, new JsonSerializerSettings()
                {
                    Error = (sender, error) => error.ErrorContext.Handled = true
                });



                var response = "";
                
                foreach (var temp in meetingsobject)
                {
                    var meeting = temp;
                    response += "Meeting Subject: " + meeting.Subject + "</br>";
                    response += "Meeting Preview: " + meeting.BodyPreview + "</br>";
                    response += "Meeting Start Time: " + meeting.Start + "</br>";
                    response += "Meeting End Time: " + meeting.End + "</br>";
                    response += "Meeting Join URL: " + meeting.OnlineMeetingUrl + "</br>";
                //    response += "Meeting Attendees: " + meeting.Attendees + "</br>";
                    response += "</br></br></br>";
                }

                */

                ViewData["Response"] = await GraphService.GetUserMeetings(graphClient, email, HttpContext);

                ViewData["Picture"] = await GraphService.GetPictureBase64(graphClient, email, HttpContext);
            }

            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateMeeting(string recipients, DateTime date, string duration, string title, string myselfemail)
        {
            if (string.IsNullOrEmpty(recipients))
            {
                TempData["Message"] = "Please add a valid email address to the recipients list!";
                return RedirectToAction("Index");
            }

            try
            {
                // Initialize the GraphServiceClient.
                var graphClient = _graphServiceClientFactory.GetAuthenticatedGraphClient((ClaimsIdentity)User.Identity);

                // Send the email.
                await GraphService.CreateMeeting(graphClient, _env, recipients, date, duration, HttpContext, title, myselfemail);

                // Reset the current user's email address and the status to display when the page reloads.
                TempData["Message"] = "Success! Meeting created.";
                return RedirectToAction("Index");
            }
            catch (ServiceException se)
            {
                if (se.Error.Code == "Caller needs to authenticate.") return new EmptyResult();
                return RedirectToAction("Error", "Home", new { message = "Error: " + se.Error.Message });
            }
        }



        [AllowAnonymous]
        public IActionResult About()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
