using Microsoft.AspNetCore.Mvc;
using Scope.Models;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Data;
using Microsoft.Win32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Scope.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

         public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }


        public IActionResult Review(Review rev)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Register; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False"))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO Review(Name, YourReview) VALUES (@name, @yourreview)", con))
                        {
                            cmd.Parameters.AddWithValue("@name", rev.Name ?? (object)DBNull.Value);
                            cmd.Parameters.AddWithValue("@yourreview", rev.YourReview ?? (object)DBNull.Value);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    TempData["SuccessMessage"] = "ThankYou for your review!";
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    TempData["ErrorMessage"] = "An error occurred while processing your request.";
                }

            }
            return View();
        }

        public IActionResult Contact(Contact cons)
        {


            SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Register;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("insert into Contact(Name,Email,Subject,Message)Values(@name,@email,@subject,@message)", con);
            SqlParameter nameParams = cmd.Parameters.AddWithValue("@name", cons.Name);
            if (cons.Name == null)
            {
                nameParams.Value = DBNull.Value;
            }
            SqlParameter emailParams = cmd.Parameters.AddWithValue("@email", cons.Email);
            if (cons.Email == null)
            {
                emailParams.Value = DBNull.Value;
            }
            SqlParameter subjectParams = cmd.Parameters.AddWithValue("@subject", cons.Subject);
            if (cons.Subject == null)
            {
                subjectParams.Value = DBNull.Value;
            }
            SqlParameter messageParams = cmd.Parameters.AddWithValue("@message", cons.Message);
            if (cons.Message == null)
            {
                messageParams.Value = DBNull.Value;
            }



            cmd.ExecuteNonQuery();
            con.Close();
            return View();


        }



        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                if (IsValidUser(login.Email, login.Password))
                {

                    return RedirectToAction("Studentdashboard");
                }
                else
                {
                    //ModelState.AddModelError("", "Invalid email or password");
                    ViewBag.ErrorMessage = "Invalid email or password.";

                }
            }
            return View(login);
        }

        private bool IsValidUser(string email, string password)
        {
            using (SqlConnection con = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Register;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False"))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Registerpage WHERE Email = @Email AND Password = @Password", con))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }

		public IActionResult Placement()
		{
			return View();
		}
		
		public IActionResult Logout()
        {
            return View();
        }
       

        [BindProperty]
        public List<string> Hobbies { get; set; }
        string hobbies;

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Success(Registration reg)
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Register;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

                using (SqlConnection con = new SqlConnection("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = Register; Integrated Security = True; Connect Timeout = 30; Encrypt = False; Trust Server Certificate = False; Application Intent = ReadWrite; Multi Subnet Failover = False"))
                {
                    con.Open();

                    if (IsEmailAlreadyRegistered(con, reg.Email))
                    {
                        ModelState.AddModelError("Email", "Email is already registered");
                        return View("Registration");
                    }

                    using (SqlCommand cmd = new SqlCommand("INSERT INTO Registerpage (FirstName, LastName, Gender, DateofBirth, Email, PhoneNumber, Country, State, City, Password, ConfirmPassword, Hobbies) VALUES (@firstname, @lastname, @gender, @dateofbirth, @email, @phoneNumber, @country, @state, @city, @password, @confirmpassword, @Hobbies)", con))
                    {
                        cmd.Parameters.AddWithValue("@firstname", reg.FirstName);
                        cmd.Parameters.AddWithValue("@lastname", reg.LastName);
                        cmd.Parameters.AddWithValue("@gender", reg.Gender);
                        cmd.Parameters.AddWithValue("@dateofbirth", reg.DateofBirth);
                        cmd.Parameters.AddWithValue("@email", reg.Email);
                        cmd.Parameters.AddWithValue("@phonenumber", reg.PhoneNumber);
                        cmd.Parameters.AddWithValue("@country", reg.Country);
                        cmd.Parameters.AddWithValue("@state", reg.State);
                        cmd.Parameters.AddWithValue("@city", reg.City);
                        cmd.Parameters.AddWithValue("@password", reg.Password);
                        cmd.Parameters.AddWithValue("@confirmpassword", reg.ConfirmPassword);
                        string HobbiesMULTI = string.Join(",", reg.Hobbies);
                        cmd.Parameters.AddWithValue("@Hobbies", HobbiesMULTI);

                        cmd.ExecuteNonQuery();
                    }
                }

                return View();
            }
            catch (Exception ex)
            {

                ViewBag.ErrorMessage = "An error occurred while processing your request.";
                return View("Error");
            }
        }

        private bool IsEmailAlreadyRegistered(SqlConnection con, string email)
        {
            using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Registerpage WHERE Email = @email", con))
            {
                cmd.Parameters.AddWithValue("@email", email);
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }


        private static List<Dashboard> courses = new List<Dashboard>
        {
            new Dashboard { Id = 1, Name = "Java Full Stack Course Internship", Fee = 37000, Duration = "", Languages =  "JAVA, MYSQL, React JS, Spring(MVC), jQuery, Git Vertion Control,J2EE"  },
            new Dashboard { Id = 2, Name = "Python Full Stack Course Internship", Fee = 37000, Duration = "", Languages =  "Python 3.10, Django, MYSQL, SQLite, Javascript,Bootstrap 5 / SASS" },
            new Dashboard { Id = 3, Name = "PHP Full Stack Course Internship", Fee = 37000, Duration = "", Languages =  "PHP 8, Codeigniter 4 / Laravel 8, WordPress, MYSQL, React JS,Web Hosting Techniques"  },
            new Dashboard { Id = 4, Name = ".Net Full Stack Course Internship", Fee = 42000, Duration = "", Languages =  ".Net 6 Core MVC, C#.Net, JQuery, JavaScript ES 7, MSSQL,Razor Coding"  },
            new Dashboard { Id = 5, Name = "MERN Full Stack Course Internship", Fee = 41000, Duration = "", Languages =   "MongoDB, Express JS, React JS, Node JS, MYSQL,Regular Expressions" },
            new Dashboard { Id = 6, Name = "MEAN Full Stack Course Internship", Fee = 18200, Duration = "", Languages =  "MongoDB, Express JS, Angular, Node JS, CSS 4 / Bootstrap 5,Logical Reasoning"  },
            new Dashboard { Id = 7, Name = "Android/iOS Mobile App Course in Google Flutter", Fee = 1200, Duration = "", Languages = "SQL, Dart, Flutter, Firebase, SQLite,State Management - Provider"  },
            new Dashboard { Id = 8, Name = "Android/iOS Mobile App Course in IONIC", Fee = 30200, Duration = "", Languages =  "Ionic 5 framework, Cordova/Capacitor, Native API, Angular, App Deployment Tools"  },
            new Dashboard { Id = 9, Name = "Software Testing Advanced Internship", Fee = 32000, Duration = "", Languages =  "Software testing fundamentals based on ISTQB, ISTQB Exam Preparation, SQL, Java/Python, SEO Basics"  },
            new Dashboard { Id = 10, Name = "Networking, Server, & Cloud Administration", Fee = 32000, Duration = "", Languages =  "CCNA (200-301), jQuery, AWS Architect – Associate, MS Azure Administrator, MCSE-Server Infrastructure"  },
            new Dashboard { Id = 11, Name = "AWS Architect Associate Course", Fee = 32000, Duration = "", Languages =  "Introduction of Cloud Computing, Introduction of AWS, Elastic Compute Cloud (Ec2), Managing Ec2 Instances, Management of Elastic Block Storage, Managing Amazon Virtual Private Cloud (VPC)"  },
            new Dashboard { Id = 12, Name = "AMs Azure Cloud Administrator", Fee = 20000, Duration = "", Languages =  "Introduction and Starting with Azure, Deploy and Manage Azure Compute Resources, Configure and Manage Virtual Networking, Implement and Manage Storage, Implement and Manage Storage"  },
            new Dashboard { Id = 13, Name = "DevOps Engineer Course Internship", Fee = 43200, Duration = "", Languages =  "Linux Administration, AWS, Azure, Python, GIT,Docker"  },
            new Dashboard { Id = 14, Name = "Digital Marketing Expert Internship", Fee = 12000, Duration = "", Languages =  "Python 3.10, Regular Expressions, MySQL, Django, SQLite"  },
            new Dashboard { Id = 15, Name = "Data Science Course Internship", Fee = 42200, Duration = "", Languages =  "Python, MySQL, Data Processing using Pandas, Numpy, Matplotlib"  },
            new Dashboard { Id = 16, Name = "Data Analytics Course Internship", Fee = 37000, Duration = "", Languages =  "HTML 6 Basics, Digital Marketing Overview, Advanced Keyword Research, Website Planning and Structuring, WordPress Website Designing"  }
        };



        public IActionResult Studentdashboard()
		{
			return View(courses);
		}
		public IActionResult GetCourseDetails(int courseId)
		{
			Dashboard course = courses.FirstOrDefault(c => c.Id == courseId);

			if (course != null)
			{
				return View(course);
			}
			else
			{
				
				return RedirectToAction("Index");
			}
		}

		public IActionResult Success()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}