using MongoDB.Bson;
using MongoDB.Driver;
using ProjectThesisStack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectThesisStack.Controllers
{
    public class HomeController : Controller
    {
        static IMongoDatabase db = (new MongoClient()).GetDatabase("ThesisStack");

        //---------------------- Login Logout Starts ---------------------------
        public ActionResult Logout()
        {
            Session["usertype"] = null;
            Session["user"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult Login()
        {
            ViewBag.LoginFailed = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            string email = user.email;
            string password = user.password;
            var userType = user.UserType;
            ViewBag.LoginFailed = null;


            if (userType.ToString() == "Supervisor")
            {
                var coll = db.GetCollection<Supervisor>("Supervisor");
                var supervisor = coll.Find(b => b.email == email && b.password == password).ToList<Supervisor>();

                if (supervisor.Count == 0)
                {
                    ViewBag.LoginFailed = "Invalid Email or Password. Please Try Again!";
                    return View();
                }
                else
                {
                    Session["userType"] = "Supervisor";
                    Session["user"] = supervisor[0];
                    ViewBag.LoginFailed = null;
                    return RedirectToAction("Index");
                }
            }
            else if (userType.ToString() == "Student")
            {
                var coll = db.GetCollection<Student>("Student");
                var student = coll.Find(b => b.email == email && b.password == password).ToList<Student>();

                if (student.Count == 0)
                {
                    ViewBag.LoginFailed = "Invalid Email or Password. Please Try Again!";
                    return View();
                }
                else
                {
                    Session["userType"] = "Student";
                    Session["user"] = student[0];
                    ViewBag.LoginFailed = null;
                    return RedirectToAction("Index");
                }
            }
            else if (userType.ToString() == "Admin")
            {
                var coll = db.GetCollection<Admin>("Admin");
                var admins = coll.Find(b => b.email == email && b.password == password).ToList<Admin>();
             
                if (admins.Count==0)
                {
                    ViewBag.LoginFailed = "Invalid Email or Password. Please Try Again!";
                    return View();
                }
                else
                {
                    Session["userType"] = "Admin";
                    Session["user"] = admins[0];
                    ViewBag.LoginFailed = null;
                    return RedirectToAction("AdminNotice");
                }
            }
            else
            {
                ViewBag.LoginFailed = "Someting Went Wrong. Please Try Again Later!";
                return View();
            }
        }

        //---------------------- Login Logout Ends ---------------------------

        //---------------------- Admin Pages Starts -------------------------------
        public ActionResult AdminSupervisors()
        {
            if ((string)Session["userType"] == "Admin")
                return View();
            else
                return RedirectToAction("SessionNotFound");
        }
        [HttpPost]
        public ActionResult AdminSupervisors(Supervisor newSupervisor)
        {
            if ((string)Session["userType"] == "Admin")
            {
                var coll = db.GetCollection<Supervisor>("Supervisor");
                var supervisors = coll.Find(x => x.email == newSupervisor.email).ToList<Supervisor>();
                if (supervisors.Count == 0)
                {
                    coll.InsertOne(newSupervisor);
                }
                return RedirectToAction("AdminSupervisors");
            }
            else 
                return RedirectToAction("SessionNotFound");
        }
        public ActionResult AdminNotice()
        {
            if ((string)Session["userType"] == "Admin")
            {
                var coll = db.GetCollection<Notice>("Notice");
                var sort = Builders<Notice>.Sort.Descending("date");
                var notices = coll.Find(b => true).Sort(sort).ToList<Notice>();
                ViewData["notices"] = notices;
                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        [HttpPost]
        public ActionResult AdminNotice(Notice newNotice)
        {
            if ((string)Session["userType"] == "Admin")
            {
                var coll = db.GetCollection<Notice>("Notice");
                var now = DateTime.Now;
                now = DateTime.SpecifyKind(now, DateTimeKind.Utc);
                newNotice.date = DateTime.SpecifyKind(now, DateTimeKind.Utc);
                coll.InsertOne(newNotice);
                var sort = Builders<Notice>.Sort.Descending("date");
                var notices = coll.Find(b => true).Sort(sort).ToList<Notice>();
                ViewData["notices"] = notices;
                return RedirectToAction("AdminNotice");
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        public ActionResult AdminStudents()
        {
            if ((string)Session["userType"] == "Admin")
            {
                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        [HttpPost]
        public ActionResult AdminStudents(Student newStudent)
        {
            if ((string)Session["userType"] == "Admin")
            {
                var coll = db.GetCollection<Student>("Student");
                var students = coll.Find(x => x.studentID == newStudent.studentID).ToList<Student>();
                if(students.Count == 0)
                    coll.InsertOne(newStudent);
                return RedirectToAction("AdminStudents"); 
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        public ActionResult AdminCG()
        {
            if ((string)Session["userType"] == "Admin")
            {
                var coll = db.GetCollection<MedianCGPA>("MedianCGPA");
                ViewData["cgpaList"] = coll.Find(b => true).ToList<MedianCGPA>();
                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        [HttpPost]
        public ActionResult AdminCG(MedianCGPA cg)
        {
            if ((string)Session["userType"] == "Admin")
            {
                var coll = db.GetCollection<MedianCGPA>("MedianCGPA");
                var filter = Builders<MedianCGPA>.Filter.Eq("department", cg.department) &
                             Builders<MedianCGPA>.Filter.Eq("semester", cg.semester);

                var update = Builders<MedianCGPA>.Update.Set("session", cg.session)
                                                        .Set("totalStudents", cg.totalStudents)
                                                        .Set("medianCG", cg.medianCG)
                                                        .Set("maxCG", cg.maxCG);

                coll.UpdateOne(filter, update);
                ViewData["cgpaList"] = coll.Find(b => true).ToList<MedianCGPA>() ;
                return RedirectToAction("AdminCG");
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        public ActionResult AdminList()
        {
            if ((string)Session["userType"] == "Admin")
            {
                var coll = db.GetCollection<Admin>("Admin");
                ViewData["adminList"] = coll.Find(b => true).ToList<Admin>();
                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        [HttpPost]
        public ActionResult AdminList(Admin newAdmin)
        {
            if ((string)Session["userType"] == "Admin")
            {
                var coll = db.GetCollection<Admin>("Admin");
                coll.InsertOne(newAdmin);
                ViewData["adminList"] = coll.Find(b => true).ToList<Admin>();
                return RedirectToAction("AdminList");
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        //---------------------- Admin Pages Ends -------------------------------

        //---------------------- Supervisor Pages Starts -------------------------------
        public ActionResult SupervisorTeam()
        {
            if ((string)Session["userType"] == "Supervisor")
            {
                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        [HttpPost]
        public ActionResult SupervisorTeam(NewTeamForm newTeam)
        {
            if ((string)Session["userType"] == "Supervisor")
            {
                var coll = db.GetCollection<Team>("Team");
                var collStud = db.GetCollection<Student>("Student");

                List<ObjectId> studentIds = new List<ObjectId>();
                var flag = true;

                if (!string.IsNullOrEmpty(newTeam.student1))
                {
                    var studentId = collStud.Find(x => x.studentID == newTeam.student1).ToList<Student>();
                    if (studentId.Count == 0)
                        flag = false;
                    else
                        studentIds.Add(studentId[0].id);
                }
                if (!string.IsNullOrEmpty(newTeam.student2))
                {
                    var studentId = collStud.Find(x => x.studentID == newTeam.student2).ToList<Student>();
                    if (studentId.Count == 0)
                        flag = false;
                    else
                        studentIds.Add(studentId[0].id);
                }
                if (!string.IsNullOrEmpty(newTeam.student3))
                {
                    var studentId = collStud.Find(x => x.studentID == newTeam.student3).ToList<Student>();
                    if (studentId.Count == 0)
                        flag = false;
                    else
                        studentIds.Add(studentId[0].id);
                }
                if (!string.IsNullOrEmpty(newTeam.student4))
                {
                    var studentId = collStud.Find(x => x.studentID == newTeam.student4).ToList<Student>();
                    if (studentId.Count == 0)
                        flag = false;
                    else
                        studentIds.Add(studentId[0].id);
                }

                if (flag)
                {
                    Team team = new Team();
                    team.studentIDs = studentIds.ToArray();
                    team.teamName = newTeam.teamName;
                    team.dp = newTeam.dp;
                    team.supervisorID = ((Supervisor)Session["user"]).id;
                    coll.InsertOne(team);
                }
                return RedirectToAction("SupervisorTeam");
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        //---------------------- Supervisor Pages Ends -------------------------------

        //---------------------- Common Pages Starts -------------------------------
        public ActionResult Index()
        {
            if (Session["userType"] != null)
            {
                var coll = db.GetCollection<Paper>("Paper");
                var sort = Builders<Paper>.Sort.Descending("date");
                var papers = coll.Find(b => true).Sort(sort).ToList<Paper>();
                ViewData["papers"] = papers;

                var collTeams = db.GetCollection<Team>("Team");
                var collStudents = db.GetCollection<Student>("Student");
                var collSupervisor = db.GetCollection<Supervisor>("Supervisor");
                ViewData["collTeams"] = collTeams;
                ViewData["collStudents"] = collStudents;
                ViewData["collSupervisor"] = collSupervisor;
                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        public ActionResult Supervisors()
        {
            if (Session["userType"] != null)
            {
                var coll = db.GetCollection<Supervisor>("Supervisor");
                var supervisors = coll.Find(b => true).ToList<Supervisor>();
                ViewData["supervisors"] = supervisors;
                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        public ActionResult Teams()
        {
            if (Session["userType"] != null)
            {
                var collTeams = db.GetCollection<Team>("Team");
                var teams = collTeams.Find(b => true).ToList<Team>();
                ViewData["teams"] = teams;

                var collStudent = db.GetCollection<Student>("Student");
                var collSupervisor = db.GetCollection<Supervisor>("Supervisor");

                var studentMap = new Dictionary<ObjectId, Student>();
                var supervisorMap = new Dictionary<ObjectId, Supervisor>();

                foreach (Team team in teams)
                {
                    foreach (ObjectId studentId in team.studentIDs)
                    {

                        var student = collStudent.Find(b => b.id == studentId).ToList<Student>()[0];
                        if (!studentMap.ContainsKey(studentId))
                            studentMap.Add(studentId, student);
                    }
                    var supervisor = collSupervisor.Find(b => b.id == team.supervisorID).ToList<Supervisor>()[0];
                    if (!supervisorMap.ContainsKey(team.supervisorID))
                        supervisorMap.Add(team.supervisorID, supervisor);
                }
                ViewData["studentMap"] = studentMap;
                ViewData["supervisorMap"] = supervisorMap;

                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        public ActionResult Notice()
        {
            if (Session["userType"] != null)
            {
                var coll = db.GetCollection<Notice>("Notice");

                var sort = Builders<Notice>.Sort.Descending("date");
                var notices = coll.Find(b => true).ToList<Notice>();
                var notices2 = coll.Find(b => b.featured==true).ToList<Notice>();
                notices2.Reverse();
                notices.Reverse();
                ViewData["notices"] = notices;
                ViewData["notices2"] = notices2;

                var collCG = db.GetCollection<MedianCGPA>("MedianCGPA");
                ViewData["collCG"] = collCG;

                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }
        [HttpPost]
        public ActionResult NewsFeed( NewPaper newpaper)
        {
            if (Session["userType"] != null)
            {
                var coll = db.GetCollection<Paper>("Paper");
                var papers = coll.Find(b => true).ToList<Paper>();

                var teamNameMap = new Dictionary<ObjectId, Team>();
                var coll2 = db.GetCollection<Team>("Team");

                var coll3 = db.GetCollection<Interraction>("Interraction");
                var interraction = new Interraction();
                interraction.id = ObjectId.Empty;
                coll3.InsertOne(interraction);
                var insertedEntity = coll3.Find(b => b.id ==interraction.id).ToList<Interraction>()[0];
                var teams = coll2.Find(b => b.teamName==newpaper.teamName).ToList<Team>();
                
                var paperX = new Paper();
                paperX.interractionID = insertedEntity.id;
                var flag = 1;
                if(teams.Count==0)
                {
                    flag = 0;
                }
                else
                {
                    paperX.teamID = teams[0].id;
                }

              
                paperX.title = newpaper.paperTitle;

                paperX.tags = newpaper.tags.Split(new string[] { ", ",","," , "," ," }, StringSplitOptions.None);

                paperX.fileLink = newpaper.PDFLink;
                paperX.body = newpaper.paperBody;
                paperX.publicationLinks = newpaper.publicationsLink.Split(new string[] { ", ", ",", " , ", " ," }, StringSplitOptions.None);

                
                paperX.status = newpaper.status;
                coll.InsertOne(paperX);
                return RedirectToAction("NewsFeed");
            }
            else
                return RedirectToAction("SessionNotFound");


        }
        public ActionResult NewsFeed()
        {
            if (Session["userType"] != null)
            {
                var coll = db.GetCollection<Paper>("Paper");
                var papers = coll.Find(b => true).ToList<Paper>();

                var teamNameMap = new Dictionary<ObjectId, Team>();
                var coll2 = db.GetCollection<Team>("Team");

                var teams = coll2.Find(b => true).ToList<Team>();

                foreach (var team in teams)
                {
                    teamNameMap.Add(team.id, team);
                }
                ViewData["papers"] = coll.Find(b => true).ToList<Paper>();
                ViewData["teamNameMap"] = teamNameMap;
                return View();
            }
            else
                return RedirectToAction("SessionNotFound");


        }
        public ActionResult MyProfile()
        {
            if (Session["userType"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("SessionNotFound");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SessionNotFound()
        {
            return View();
        }
    }
}