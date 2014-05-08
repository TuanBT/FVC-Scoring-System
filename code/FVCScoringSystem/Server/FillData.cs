using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Server
{
    public class FillData
    {
        //private TextBox txtNumberMatch;
        private NumericUpDown nmrNumberMatch;
        private ComboBox cbbSex;
        private ComboBox cbbWeight;

       /* private ComboBox cbbNameRed;
        private ComboBox cbbNameBlue;
        private ComboBox cbbIdRed;
        private ComboBox cbbClassBlue;
        private ComboBox cbbIdBlue;
        private ComboBox cbbClassRed;*/

        private TextBox txtNameRed;
        private TextBox txtNameBlue;
        private TextBox txtIdRed;
        private TextBox txtClassBlue;
        private TextBox txtIdBlue;
        private TextBox txtClassRed;

        public FillData(NumericUpDown nmrNumberMatch, ComboBox cbbSex, ComboBox cbbWeight, 
            TextBox txtNameRed, TextBox txtNameBlue, TextBox txtIdRed, TextBox txtClassBlue, TextBox txtIdBlue, TextBox txtClassRed)
        {
            this.nmrNumberMatch = nmrNumberMatch;
            this.cbbSex = cbbSex;
            this.cbbWeight = cbbWeight;

            /*this.cbbNameRed = cbbNameRed;
            this.cbbNameBlue = cbbNameBlue;
            this.cbbIdRed = cbbIdRed;
            this.cbbIdBlue = cbbIdBlue;
            this.cbbClassRed = cbbClassRed;
            this.cbbClassBlue = cbbClassBlue;*/

            this.txtNameRed = txtNameRed;
            this.txtNameBlue = txtNameBlue;
            this.txtIdRed = txtIdRed;
            this.txtClassBlue = txtClassBlue;
            this.txtIdBlue = txtIdBlue;
            this.txtClassRed = txtClassRed;
        }


        private void ClearTexbox()
        {
            txtNameRed.Text = "";
            txtNameBlue.Text = "";
            txtIdRed.Text = "";
            txtIdBlue.Text = "";
            txtClassRed.Text = "";
            txtClassBlue.Text = "";
        }

     private void setValuesTextboxRed(string figIdRed)
        {
            try
            {
                string sex = "";
                string weight = "";

                string name = "";
                string id = "";
                string classs = "";

                var db = new SQLiteDatabase();
                DataTable recipe;
                String query = "SELECT * FROM Fighter WHERE (FigId='" + figIdRed + "')";
                recipe = db.GetDataTable(query);
                foreach (DataRow r in recipe.Rows)
                {
                    sex = r["FigSex"].ToString();
                    weight = r["FigWeight"].ToString();
                    name = r["FigName"].ToString();
                    id = r["FigId"].ToString();
                    classs = r["FigClass"].ToString();
                }
                cbbSex.Text = sex;
                cbbWeight.Text = weight;

                txtNameRed.Text = name;
                txtIdRed.Text = id;
                txtClassRed.Text = classs;
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }
        }

        private void setValuesTextboxBlue(string figIdBlue)
        {
            try
            {
                //string sex = "";
                //string weight = "";

                string name = "";
                string id = "";
                string classs = "";

                var db = new SQLiteDatabase();
                DataTable recipe;
                String query = "SELECT * FROM Fighter WHERE (FigId='" + figIdBlue + "')";
                recipe = db.GetDataTable(query);
                foreach (DataRow r in recipe.Rows)
                {
                    //sex = r["FigSex"].ToString();
                    //weight = r["FigWeight"].ToString();
                    name = r["FigName"].ToString();
                    id = r["FigId"].ToString();
                    classs = r["FigClass"].ToString();
                }
                //cbbSex.Text = sex;
                //cbbWeight.Text = weight;

                txtNameBlue.Text = name;
                txtIdBlue.Text = id;
                txtClassBlue.Text = classs;
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }
        }

        private string setMSSVFromMath(string NumberMathWin)
        {
            string figIWin = "";
            try
            {
                var db = new SQLiteDatabase();
                DataTable recipe;
                String query = "SELECT * FROM Match WHERE (MatId=" + NumberMathWin + ")";
                recipe = db.GetDataTable(query);
                if (recipe.Rows.Count > 0)
                {
                    foreach (DataRow r in recipe.Rows)
                    {
                        figIWin = r["FigIdWin"].ToString();
                    }
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }

            return figIWin;
        }


        public void FillFromMatch(NumericUpDown nmrNumberMatch)
        {
            //ClearCombobox();
            string figIdRed = "";
            string figIdBlue = "";
            string match = nmrNumberMatch.Value.ToString();
            try
            {
                var db = new SQLiteDatabase();
                DataTable recipe;
                String query = "SELECT * FROM Match WHERE (MatId=" + match + ")";
                recipe = db.GetDataTable(query);
                if (recipe.Rows.Count > 0)
                {
                    foreach (DataRow r in recipe.Rows)
                    {
                        figIdRed = r["FigIdRed"].ToString();
                        figIdBlue = r["FigIdBlue"].ToString();
                    }
                    if (checkIsNumber(figIdRed))
                        figIdRed = setMSSVFromMath(figIdRed);
                    if (checkIsNumber(figIdBlue))
                        figIdBlue = setMSSVFromMath(figIdBlue);

                    setValuesTextboxRed(figIdRed);
                    setValuesTextboxBlue(figIdBlue);
                    //setValuesCombobox(idRed, idblue);
                }
                else
                {
                    //FillFromSexAndWeight(cbbSex,cbbWeight);
                    //ClearCombobox();
                    ClearTexbox();
                }
            }
            catch (Exception fail)
            {
                String error = "The following error has occurred:\n\n";
                error += fail.Message.ToString() + "\n\n";
                MessageBox.Show(error);
            }
        }

        //Kiểm tra MSSV này là số người thắng của trận, hay đơn thuần là MSSV
        //Là số trận thì True.
        private Boolean checkIsNumber(string figId)
        {
            try
            {
                Convert.ToInt32(figId);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Boolean checkMatchExist(string match)
        {
            try
            {
                var db = new SQLiteDatabase();
                DataTable recipe;
                String query = "SELECT * FROM Match WHERE (MatId=" + match + ")";
                recipe = db.GetDataTable(query);
                if (recipe.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public void SaveMath(string winId)
        {
            var db = new SQLiteDatabase();
            //Nếu đã tồn tại Match này thì xóa
            if (checkMatchExist(nmrNumberMatch.Value.ToString()))
            {
                string match = nmrNumberMatch.Value.ToString();
                db.Delete("Match", String.Format("MatId = {0}", match));
            }

            Dictionary<String, String> data = new Dictionary<String, String>();
            data.Add("MatId", nmrNumberMatch.Value.ToString());
            data.Add("FigIdRed", txtIdRed.Text);
            data.Add("FigIdBlue", txtIdBlue.Text);
            data.Add("FigIdWin", winId);
            try
            {
                db.Insert("Match", data);
            }
            catch (Exception crap)
            {
                MessageBox.Show(crap.Message);
            }
        }
    }
}
