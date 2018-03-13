using BuisnessLayer.Services;
using LayoutDesigner.Models;
using LayoutDesigner.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BuisnessLayer.DTO;
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
            var controlList = new List<Control>();
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
            return View(controlToShow);
        }

    }
}