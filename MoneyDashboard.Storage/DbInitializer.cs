﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Migrations;

namespace MoneyDashboard.Storage
{
    public class DbInitializer : DropCreateDatabaseAlways<UserRegistrationStore> { }
}
