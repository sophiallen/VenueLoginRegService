﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "VenueLoginRegService" in code, svc and config file together.
public class VenueLoginRegService : IVenueLoginRegService
{
    ShowTrackerEntities db = new ShowTrackerEntities();
    public bool RegisterVenue(VenueUser v)
    {
        bool result = true;

        int pass = db.usp_RegisterVenue(v.VenueName, v.VenueAddress, v.VenueCity, v.VenueState, v.VenueZipCode, v.VenuePhone, v.VenueEmail, v.VenueWebPage, v.VenueAgeRestriction, v.VenueUsername, v.VenuePassWord);
        if(pass == -1)
        {
            result = false;
        }

        return result;
    }

    public int VenueLogin(string userName, string password)
    {
        int venueKey = 0;
        int result = db.usp_venueLogin(userName, password);
        if(result != -1)
        {
            var key = (from v in db.VenueLogins
                       where v.VenueLoginUserName.Equals(userName)
                       select new {v.VenueLoginKey}).FirstOrDefault();

            venueKey = key.VenueLoginKey;
        }

        return venueKey;
    }
}
