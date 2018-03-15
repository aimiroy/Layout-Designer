using BuisnessLayer.Services;
using LayoutDesigner.Models;
using LayoutDesigner.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuisnessLayer.DTO;
using System.Text;

namespace LayoutDesigner.Controllers
{
    public class HomeController : Controller
    {
        private readonly IControlService _controlService = null;
        public HomeController(IControlService controlService)
        {
            this._controlService = controlService;
        }

        // GET: Home
        public ActionResult Index()
        {
            var controlList = GetStoredControl();
            return View(controlList);
        }

        public ActionResult InsertControls(List<Control> controls)
        { 
            //Assign contol list to dto list
            var dto =
                controls.Select(x => new ControlDTO
                {
                    ControlId = x.ControlId,
                    Label = x.Label,
                    Type = x.Type,
                    IsVisible = x.IsVisible,
                    IsReadOnly = x.IsReadOnly,
                    Order = x.Order
                }).ToList();
            _controlService.StoreControl(dto);
            return RedirectToAction("DisplayControl", "Home");
        }

        public ActionResult DisplayControl()
        {
            var controlToShow = GetStoredControl();
            return View(controlToShow);
        }

        [HttpPost]
        public ActionResult DisplayControl(FormCollection form)
        {
            var controlToShow = GetStoredControl();
            List<UserInput> summary = new List<UserInput>();
            foreach (var control in controlToShow)
            {
                if(control.IsVisible && !control.IsReadOnly)
                {
                    var input = new UserInput()
                    {
                        Label = control.Label,
                        Value = form[control.ControlId].ToString()

                    };
                    summary.Add(input);
                }
            }
            Session["summary"] = summary;
            return RedirectToAction("DisplayControl", "Home");
        }

        public ActionResult DisplaySummary()
        {
            try
            {
            var summary= (List<UserInput>)Session["summary"];
            if(!(summary.Count()==0))
            return PartialView("_Summary", summary);
            }
            catch(Exception message)
            {
                return Json(message.Message);
            }
            return Json("Submit form");
        }
        public ActionResult ShowData()
        {
            var summary = (List<UserInput>)Session["summary"];
            return View("ShowData", summary);
        }
        public ActionResult ClearLayout()
        {
            _controlService.ClearLayout();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Get Currently Controls Data.
        /// </summary>
        /// <returns></returns>
        public List<Control> GetStoredControl()
        {
            var dto = _controlService.GetControlData();
            var controlToShow = dto.Select(x => new Control
            {
                ControlId = x.ControlId,
                Label = x.Label,
                Type = x.Type,
                IsVisible = x.IsVisible,
                IsReadOnly = x.IsReadOnly,
                Order = x.Order
            }).ToList();
            return controlToShow;
        }
    }
}