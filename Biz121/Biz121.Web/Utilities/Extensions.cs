using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Biz121.Web.Utilities
{
    public static class Extensions
    {
        public static IQueryable<T> Sort<T>(this IQueryable<T> query,
                                                 string sortField,
                                                 SortDirection direction)
        {
            if (direction == SortDirection.Ascending)
                return query.OrderBy(s => s.GetType()
                                           .GetProperty(sortField));
            return query.OrderByDescending(s => s.GetType()
                                                 .GetProperty(sortField));

        }

        public static String Filters_AddRP(this String ReceivePortName)
        {
            return "<Filter><Group>" +
                        "<Statement Property='BTS.ReceivePortName' Operator='0' Value='"+ReceivePortName+"'/>" +
                    "</Group></Filter>";
        }

        public static String Filters_AddFailedMsg(this String ReceivePortName, String SendPortName)
        {
            return "<Filter><Group>" +
                        "<Statement Property='ErrorReport.FailureCode' Operator='6' />" +
                        "<Statement Property='ErrorReport.ReceivePortName' Operator='0' Value='" + ReceivePortName + "'/>" +
                    "</Group>" +
                    "<Group>" +
                        "<Statement Property='ErrorReport.FailureCode' Operator='6' />" +
                        "<Statement Property='ErrorReport.SendPortName' Operator='0' Value='" + SendPortName + "'/>" +
                    "</Group></Filter>";
        }
    } 
}