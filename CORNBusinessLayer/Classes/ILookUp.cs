using System;

namespace CORNBusinessLayer.Classes
{
    public interface ILookUp
    {
        Int32 IntEmployeeCode { set; get; }
        string KeyFieldValue { set; get; }
        string StrCustomFilter { set; get; }
        object gridDataSource { set; get; }
        string setRecordInfoStaus { set; }
        string GetFilterCriteria { get; }
        void SetGridColomnCaption();
        void SetGridRowColor();
        void HideColumn(string strColsToHide);
        void SetGridColomnFormat(string strColsToDate, string strColsToTime, string strColsToDateTime);
        void HideAndFixedColumn(string strColsToHide, string strColsToFixed);
        void SetStatusForNevigationButtons(Int32 intRecCount, Int32 lngTotalRecord);
    }
}
