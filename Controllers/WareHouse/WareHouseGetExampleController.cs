﻿using CRMEngSystem.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace CRMEngSystem.Controllers.WareHouse
{
    public class WareHouseGetExampleController : Controller
    {
        public IActionResult WareHouseGetExample() =>  File(Convert.FromBase64String(ConfigurationSettings.WareHouseExample), "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "Склад_Зразок.xlsx");
    }
}
