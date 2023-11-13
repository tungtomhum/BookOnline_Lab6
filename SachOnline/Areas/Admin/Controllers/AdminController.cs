using SachOnline.Models;
using System.Linq;
using System.Web.Mvc;

namespace SachOnline.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private string connection;
        private dbSachOnlineDataContext db;

        public AdminController()
        {
            // Khởi tạo chuỗi kết nối
            connection = "Data Source=LAPTOP-SD6JFUCG\\MSSQLSERVER01;Initial Catalog=SachOnline;Integrated Security=True";
            db = new dbSachOnlineDataContext(connection);
        }

        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            var username = form["your_name"];
            var password = form["your_pass"];

            ADMIN admin = db.ADMINs.SingleOrDefault(n => n.TenDN == username && n.MatKhau == password);
            if (admin != null)
            {
                Session["Admin"] = admin;
                return RedirectToAction("Index", "Admin");
            }
            else
            {
                ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng";
                return View();
            }
        }
    }
}
