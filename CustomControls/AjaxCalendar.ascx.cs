using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WAC_CustomControls
{
    public partial class CustomControls_AjaxCalendar : System.Web.UI.UserControl
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            StartYear = 1900;
            EndYear = DateTime.Today.Year + 5;
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        private string _dateText;
        protected void tbDataBound(object sender, EventArgs e)
        {
            _dateText = tb.Text;
        }
        protected void tbTextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DateTime d = Convert.ToDateTime(tb.Text);
            //    string s = string.Format("{0:MM/dd/yyyy}", d);
            //    this._dateText = s;
            //}
            //catch { 
            //    WACAlert.Show("Invalid Date format!", 0);
            //    this._dateText = "";
            //    tb.Text = "";
            //    this.Text = "";
            //}
            _dateText = tb.Text;
        }
        public string Text
        {
            get { _dateText = tb.Text; return this._dateText; }
            set
            {
                try
                {
                    DateTime d = Convert.ToDateTime(value);
                    string s = string.Format("{0:MM/dd/yyyy}", d);
                    this._dateText = s;
                }
                catch
                {
                    //WACAlert.Show("Invalid Date format!", 0);
                    this._dateText = value;
                }
                tb.Text = _dateText;
            }
        }

        public DateTime? CalDateNullable
        {
            get
            {
                try
                {
                    DateTime d = Convert.ToDateTime(tb.Text);
                    if (!goodYearRange(d))
                        throw new Exception("Year outside of acceptable range, must be between " + StartYear.ToString() + " and " + EndYear.ToString());
                    return d;
                }
                catch { return null; }
            }
        }

        public DateTime CalDateNotNullable
        {
            get
            {
                try
                {
                    DateTime d = Convert.ToDateTime(tb.Text);
                    if (!goodYearRange(d))
                        throw new Exception("Year outside of acceptable range, must be between " + StartYear.ToString() + " and " + EndYear.ToString());
                    return d;
                }
                catch { throw new Exception("Missing Date or Invalid Date format"); }
            }
        }

        private bool goodYearRange(DateTime d)
        {
            return d.Year >= StartYear && d.Year <= EndYear;
        }
    }
}