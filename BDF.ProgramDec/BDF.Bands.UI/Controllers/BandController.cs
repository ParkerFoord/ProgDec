﻿using BDF.Bands.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BDF.Bands.UI.Controllers
{
    public class BandController : Controller
    {
        BandModel[] bands = new BandModel[]
        {
            new BandModel{Id = 1, Name="Eric Clapton", Genre = "Blues", YearFounded = 1978},
            new BandModel{Id = 2, Name="Motley Crue", Genre = "Rock", YearFounded = 1982},
            new BandModel{Id = 3, Name="Norah Jones", Genre = "Jazz", YearFounded = 2005}
        };

        // GET: Band
        public ActionResult Index()
        {
            GetBands();

            return View(bands);
        }

        private void GetBands()
        {
            if (Session["bands"] != null)
                bands = (BandModel[])Session["bands"];
        }

        public ActionResult Details(int id)
        {
            GetBands();

            BandModel band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        [HttpGet]
        public ActionResult Create()
        {
            BandModel newband = new BandModel();
            return View(newband);
        }

        [HttpPost]
        public ActionResult Create (BandModel band)
        {
            // add the new band here
            Array.Resize(ref bands, bands.Length + 1);

            band.Id = bands.Length;
            bands[bands.Length - 1] = band;

            Session["bands"] = bands;

            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            GetBands();
            BandModel band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        [HttpPost]
        public ActionResult Edit(int id, BandModel band)
        {
            bands[id - 1] = band;

            Session["bands"] = bands;
            return RedirectToAction("Index");
        }

        [HttpGet]

        public ActionResult Delete(int id)
        {
            GetBands();
            BandModel band = bands.FirstOrDefault(b => b.Id == id);
            return View(band);
        }

        [HttpPost]
        public ActionResult Delete(int id, BandModel band)
        {
            GetBands();

            //Delete one by adding all the other ones to the bands.
            var newbands = bands.Where(b => b.Id != id);
            bands = newbands.ToArray();
            Session["bands"] = bands;
            return RedirectToAction("Index");
        }
        


    }
}