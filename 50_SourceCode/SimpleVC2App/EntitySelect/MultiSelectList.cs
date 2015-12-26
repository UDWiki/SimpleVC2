using System;
using System.Collections.Generic;
using Telossoft.SimpleVC.Model;
using SGLibrary.Framework.GridBind;

namespace Telossoft.SimpleVC.WinFormApp.EntitySelect
{
    public class MultiSelectList : List<CheckSelectEty>
    {
        public void SetOldSelectList(IList<BaseEntity> OldSelectList)
        {
            Clear();

            foreach (BaseEntity ety in OldSelectList)
                this.Add(new CheckSelectEty(ety));

            foreach(CheckSelectEty chkEty in this)
                chkEty.IsChecked = true;
        }

        public void ClearAllChecked()
        {
            foreach (CheckSelectEty chkEty in this)
                chkEty.IsChecked = false;
        }

        public void CheckAll()
        {
            foreach(CheckSelectEty chkEty in this)
                chkEty.IsChecked = true;
        }

        public void Update(IList<BaseEntity> Values)
        {
            for (Int32 i = Count - 1; i >= 0; i--)
                if (!this[i].IsChecked)
                    this.RemoveAt(i);

            IList<BaseEntity> Slted = GetSelected();

            foreach (BaseEntity ety in Values)
                if (!Slted.Contains(ety))
                    this.Add(new CheckSelectEty(ety));
        }

        public IList<BaseEntity> GetSelected()
        {
            IList<BaseEntity> Result = new List<BaseEntity>();
            foreach(CheckSelectEty chkEty in this)
                if (chkEty.IsChecked)
                    Result.Add(chkEty.Obj);
            
            return Result;

        }

        public GridBindColumnAndPropertyMap[] GetColumnPropertis()
        {
            GridBindColumnAndPropertyMap[] Result = new GridBindColumnAndPropertyMap[2];
            Result[0] = new GridBindColumnAndPropertyMap() { PropertyName = "IsChecked", ColumnAttribute = new GridBindColumnAttribute { Title = "Ñ¡Ôñ" } };
            Result[1] = new GridBindColumnAndPropertyMap() { PropertyName = "Name", ColumnAttribute = new GridBindColumnAttribute { Title = "Ãû³Æ", ReadOnly = true } };

            return Result;
        }
    }
}
