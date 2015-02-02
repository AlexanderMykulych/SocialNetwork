using Social.Domain.DataBase;
using Social.WebUI.ViewModel.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Social.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IUser_Db _db;

        public HomeController(IUser_Db db)
        {
            _db = db;
        }

        [Authorize]
        public ActionResult Index()
        {

            return View(new VM_Messages() 
                { 
                    Messages = _db.THistory.ToList()
                });
        }

        
        public ActionResult NewMessage(UserMessage model)
        {
            if (model.UserName != null)
                _db.AddMessage(model.UserName, model.Message);

            return RedirectToAction("Index");
        }
    }
}
