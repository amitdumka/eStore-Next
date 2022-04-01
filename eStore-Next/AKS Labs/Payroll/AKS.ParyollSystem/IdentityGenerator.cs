﻿using AKS.Shared.Payroll.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKS.ParyollSystem
{
    public  class IdentityGenerator
    {
        public static string GenerateAttendanceId(Attendance att) {

            string id = $"{att.OnDate.Year}/{att.OnDate.Month}/{att.OnDate.Day}/{att.EmployeeId.Split("/")[3].Trim()}";
            return id;
        }
        public static string GenerateEmployeeId(Employee emp)
        {
            string id = $"{emp.StoreId}/{emp.JoiningDate.Year}/";
            switch (emp.Category)
            {
                case EmpType.Salesman:
                    id+="SLM";
                    break;

                case EmpType.StoreManager:
                   id+= "SM";
                    break;

                case EmpType.HouseKeeping:
                   id+= "HK";
                    break;

                case EmpType.Owner:
                   id+= "OWN";
                    break;

                case EmpType.Accounts:
                   id+= "ACC";
                    break;

                case EmpType.TailorMaster:
                   id+= "TM";
                    break;

                case EmpType.Tailors:
                   id+= "TLS";
                    break;

                case EmpType.TailoringAssistance:
                   id+= "TLA";
                    break;

                case EmpType.Others:
                   id+= "OTH";
                    break;

                default:
                    break;
            }
            return id;
        }

        public static string GenerateMonthlyAttendance(DateTime on, string empid)
        {
            string id = $"{on.Year}/{on.Month}/{empid}";
            return id;
        }
    }
}
