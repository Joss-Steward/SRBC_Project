using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;

namespace WebApplication1
{
    public partial class Graphs : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string query = "select distinct ID,StationName from StationMetaData";
                DataTable dt = GetData(query);
                listboxAvailable.DataSource = dt;
                listboxAvailable.DataTextField = "StationName";
                listboxAvailable.DataValueField = "ID";
                listboxAvailable.DataBind();
            }
           // Chart2.Visible = false;
           
        }
        private static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }
        
        protected void ShowGraph_Click(object sender, ImageClickEventArgs e)
        {

            //Do validations
            bool isvalidated = true;
            lblError.Visible = true;
            lblError.Items.Clear();
            int max=0;
            if (listboxSelected.Items.Count == 0)
            {
                lblError.Items.Add(new ListItem("Please select atleast one Station", "Please select atleast one Station"));
                isvalidated = false;
            }
            if (String.IsNullOrEmpty(txtStartDate.Text))
            {
                lblError.Items.Add(new ListItem("Enter Start date", "Enter Start date"));
                isvalidated = false;
            }
            if (String.IsNullOrEmpty(txtEndDate.Text))
            {
                lblError.Items.Add(new ListItem("Enter End date", "Enter End date"));
                isvalidated = false;
            }
            if (CheckBoxList2.SelectedItem == null)
            {
                lblError.Items.Add(new ListItem("Select a Parameter", "Select a Parameter"));
                isvalidated = false;
            }

            if (isvalidated)
            {
                foreach (ListItem l in CheckBoxList2.Items)
                {
                    if (!(l.Selected == true))
                    {
                        continue;
                    }
                    Chart Chart2 = new Chart();
                    Chart2.ChartAreas.Add("ChartArea1");
                    Chart2.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gray;
                    Chart2.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gray;
                    String SelectedParameter = l.Value.ToString();
                    
                    //Add the stationName
                    
                    foreach (ListItem item in listboxSelected.Items)
                    {
                        TableRow trow = new TableRow();
                        TableCell tcell_SelectedParameter = new TableCell();
                        tcell_SelectedParameter.Text = SelectedParameter;
                        /*tcell_SelectedParameter.Font.Bold = true; ;
                        tcell_SelectedParameter.Font.Underline = true;
                        tcell_SelectedParameter.Font.Size = 14;
                        tcell_SelectedParameter.Wrap = false;
                        tcell_SelectedParameter.ColumnSpan = 8;*/
                        tcell_SelectedParameter.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
                        trow.Cells.Add(tcell_SelectedParameter);

                        tbl_Summary.Rows.Add(trow);
                        string query = string.Format("SELECT SampleTime, {0} from WaterQualityData where StationID = '{1}' and SampleTime >= '{2}' and SampleTime <= '{3}' order by SampleTime asc", SelectedParameter, item.Value, txtStartDate.Text, txtEndDate.Text);
                        DataTable dt = GetData(query);
                        Chart2.ChartAreas[0].AxisX.Title = "Date";
                        Chart2.ChartAreas[0].AxisX.TitleAlignment = System.Drawing.StringAlignment.Center;
                        Chart2.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial,Helvetica,sans-serif", 11, FontStyle.Bold);

                        Chart2.ChartAreas[0].AxisY.Title = SelectedParameter;
                        Chart2.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial,Helvetica,sans-serif", 11, FontStyle.Bold);
                        Chart2.ChartAreas[0].AxisY.TitleAlignment = System.Drawing.StringAlignment.Center;
                        Chart2.Series.Add(item.Text);
                        Chart2.Series[item.Text].ChartType = SeriesChartType.Line;

                        Chart2.Series[item.Text].IsVisibleInLegend = true;
                        Chart2.Legends.Add(item.Text);
                        String[] x = new String[dt.Rows.Count];
                        decimal[] y = new decimal[dt.Rows.Count];
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            x[i] = (Convert.ToDateTime(dt.Rows[i][0].ToString())).ToString("MM/dd/yy");
                            y[i] = Convert.ToDecimal(Math.Round(Convert.ToDouble(dt.Rows[i][1]), 2));
                            Chart2.Series[item.Text].Points.AddXY(x[i], y[i]);
                            Chart2.Series[item.Text].Font = new System.Drawing.Font("Arial,Helvetica,sans-serif", 11);
                        }
                        if (max < x.Length)     //to modify the width of the chart
                            max = x.Length;
                        //trow = new TableRow();
                        //Add the stationName
                        TableCell tcell_stationID = new TableCell();
                        tcell_stationID.Text = item.Text;
                        tcell_stationID.Wrap = false;
                        tcell_stationID.HorizontalAlign = HorizontalAlign.Center;
                        trow.Cells.Add(tcell_stationID);

                        //Find the minimum value
                        query = string.Format("Select {0},SampleTime from WaterQualityData where {0}=( SELECT Min({0}) from WaterQualityData where StationID = '{1}' and SampleTime >= '{2}' and SampleTime <= '{3}')", SelectedParameter, item.Value, txtStartDate.Text, txtEndDate.Text);
                        //query = string.Format("SELECT * from WaterQualityData ");//where StationID = '{1}' and SampleTime >= '{2}' and SampleTime <= '{3}'", SelectedParameter, item.Value, txtStartDate.Text, txtEndDate.Text);
                        dt = GetData(query);
                        TableCell tcell_min = new TableCell();
                        TableCell tcell_minDate = new TableCell();
                        if (dt.Rows.Count > 0)
                        {
                            tcell_min.Text = dt.Rows[0][0].ToString();
                           // tcell_minDate.Text = (Convert.ToDateTime(dt.Rows[0][1].ToString()).ToShortTimeString());
                            tcell_minDate.Text = (Convert.ToDateTime(dt.Rows[0][1].ToString()).ToLongDateString());
                            tcell_minDate.Text += "  " + (Convert.ToDateTime(dt.Rows[0][1].ToString()).ToLongTimeString());
                        }
                        else
                        {
                            tcell_min.Text = "  No Data  ";
                            tcell_minDate.Text = "  No Data  ";
                        }
                        tcell_min.HorizontalAlign = HorizontalAlign.Center;
                        tcell_minDate.HorizontalAlign = HorizontalAlign.Center;
                        trow.Cells.Add(tcell_min);
                        trow.Cells.Add(tcell_minDate);
                        //Find the maximum value
                        query = string.Format("Select {0},SampleTime from WaterQualityData where {0}=(SELECT Max({0}) from WaterQualityData where StationID = '{1}' and SampleTime >= '{2}' and SampleTime <= '{3}')", SelectedParameter, item.Value, txtStartDate.Text, txtEndDate.Text);
                        dt = GetData(query);
                        TableCell tcell_max = new TableCell();
                        TableCell tcell_maxDate = new TableCell();
                        if (dt.Rows.Count > 0)
                        {
                            tcell_max.Text = dt.Rows[0][0].ToString();
//                            tcell_maxDate.Text = (Convert.ToDateTime(dt.Rows[0][1].ToString()).ToShorTimeString());
                            tcell_maxDate.Text = (Convert.ToDateTime(dt.Rows[0][1].ToString()).ToLongDateString());
                            tcell_maxDate.Text +="  " + (Convert.ToDateTime(dt.Rows[0][1].ToString()).ToLongTimeString());
                        }
                        else
                        {
                            tcell_max.Text = "  No Data  ";
                            tcell_maxDate.Text = "  No Data  ";
                        }
                        tcell_max.HorizontalAlign = HorizontalAlign.Center;
                        tcell_maxDate.HorizontalAlign = HorizontalAlign.Center;
                        trow.Cells.Add(tcell_max);
                        trow.Cells.Add(tcell_maxDate);
                        //Find the Avg Value
                        query = string.Format("SELECT  ROUND(avg(CAST({0} AS FLOAT)),4) from WaterQualityData where StationID = '{1}' and SampleTime >= '{2}' and SampleTime <= '{3}'", SelectedParameter, item.Value, txtStartDate.Text, txtEndDate.Text);
                        dt = GetData(query);
                        TableCell tcell_avg = new TableCell();
                        if (dt.Rows[0][0].ToString()!="")
                        {
                            tcell_avg.Text = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            tcell_avg.Text = "  No Data  ";
                        }
                        trow.Cells.Add(tcell_avg);
                        tbl_Summary.Rows.Add(trow);
                        //Find the Median value

                        query = string.Format(@"(select * from(select WaterQualityData.{0},row_number() over(order by WaterQualityData.{0} asc) as 'row' from WaterQualityData where WaterQualityData.StationID={1}) as temp,
                        (select count(*) as cnt from WaterQualityData where WaterQualityData.StationID={1}) as temp1
                        where temp.row =temp1.cnt/2)", SelectedParameter, item.Value);
                        dt = GetData(query);
                        TableCell tcell_median = new TableCell();
                        if (dt.Rows.Count > 0)
                        {
                            tcell_median.Text = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            tcell_median.Text = "  No Data  ";
                        }
                        tcell_median.Wrap = false;
                        tcell_median.HorizontalAlign = HorizontalAlign.Center;
                        trow.Cells.Add(tcell_median);
                        tbl_Summary.Rows.Add(trow);
                        
                        //Find the Standard Deviation
                        query = string.Format("SELECT  ROUND(stdev(CAST({0} AS FLOAT)),3) from WaterQualityData where StationID = '{1}' and SampleTime >= '{2}' and SampleTime <= '{3}'", SelectedParameter, item.Value, txtStartDate.Text, txtEndDate.Text);
                        dt = GetData(query);
                        TableCell tcell_stdev = new TableCell();
                        if (dt.Rows[0][0].ToString() != "")
                        {
                            tcell_stdev.Text = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            tcell_stdev.Text = "  No Data  ";
                        }
                        tcell_stdev.HorizontalAlign = HorizontalAlign.Center;
                        trow.Cells.Add(tcell_stdev);
                        tbl_Summary.Rows.Add(trow);

                        //Find Q1
                        query = string.Format(@"(select round(avg(temp.{0}),3) from(select WaterQualityData.{0},row_number() over(order by WaterQualityData.{0} asc) as 'row' from WaterQualityData where WaterQualityData.StationID={1}) as temp,
                        (select count(*) as cnt from WaterQualityData where WaterQualityData.StationID={1}) as temp1
                        where temp.row <= temp1.cnt/4)", SelectedParameter, item.Value);
                        dt = GetData(query);
                        TableCell tcell_Q1 = new TableCell();
                        if (dt.Rows[0][0].ToString() != "")
                        {
                            tcell_Q1.Text = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            tcell_Q1.Text = "  No Data  ";
                        }
                        tcell_Q1.HorizontalAlign = HorizontalAlign.Center;
                        tcell_Q1.Wrap = false;
                        trow.Cells.Add(tcell_Q1);
                        tbl_Summary.Rows.Add(trow);
                        //Find Q2
                        query = string.Format(@"(select round(avg(temp.{0}),3) from(select WaterQualityData.{0},row_number() over(order by WaterQualityData.{0} asc) as 'row' from WaterQualityData where WaterQualityData.StationID={1}) as temp,
                        (select count(*) as cnt from WaterQualityData where WaterQualityData.StationID={1}) as temp1
                        where temp.row > temp1.cnt/4 and temp.row <= temp1.cnt/2)", SelectedParameter, item.Value);
                        dt = GetData(query);
                        TableCell tcell_Q2 = new TableCell();
                        if (dt.Rows[0][0].ToString() != "")
                        {
                            tcell_Q2.Text = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            tcell_Q2.Text = "  No Data  ";
                        }
                        tcell_Q2.Wrap = false;
                        tcell_Q2.HorizontalAlign = HorizontalAlign.Center;
                        trow.Cells.Add(tcell_Q2);
                        tbl_Summary.Rows.Add(trow);
                        //Find Q3
                        query = string.Format(@"(select round(avg(temp.{0}),3) from(select WaterQualityData.{0},row_number() over(order by WaterQualityData.{0} asc) as 'row' from WaterQualityData where WaterQualityData.StationID={1}) as temp,
                        (select count(*) as cnt from WaterQualityData where WaterQualityData.StationID={1}) as temp1
                        where temp.row > temp1.cnt/2 and temp.row <= temp1.cnt*3/4)", SelectedParameter, item.Value);
                        dt = GetData(query);
                        TableCell tcell_Q3 = new TableCell();
                        if (dt.Rows[0][0].ToString() != "")
                        {
                            tcell_Q3.Text = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            tcell_Q3.Text = "  No Data  ";
                        }
                        tcell_Q3.Wrap = false;
                        tcell_Q3.HorizontalAlign = HorizontalAlign.Center;
                        trow.Cells.Add(tcell_Q3);
                        tbl_Summary.Rows.Add(trow);
                        //Find Q4
                        query = string.Format(@"(select round(avg(temp.{0}),4) from(select WaterQualityData.{0},row_number() over(order by WaterQualityData.{0} asc) as 'row' from WaterQualityData where WaterQualityData.StationID={1}) as temp,
                        (select count(*) as cnt from WaterQualityData where WaterQualityData.StationID={1}) as temp1
                        where temp.row > temp1.cnt*3/2 and temp.row <= temp1.cnt)", SelectedParameter, item.Value);
                        dt = GetData(query);
                        TableCell tcell_Q4 = new TableCell();
                        if (dt.Rows[0][0].ToString() != "")
                        {
                            tcell_Q4.Text = dt.Rows[0][0].ToString();
                        }
                        else
                        {
                            tcell_Q4.Text = "  No Data  ";
                        }
                        tcell_Q4.Wrap = false;
                        tcell_Q4.HorizontalAlign = HorizontalAlign.Center;
                        trow.Cells.Add(tcell_Q4);
                        tbl_Summary.Rows.Add(trow);

                        ////Find the Mode value
                        //query = string.Format("SELECT {0},count({0}) from WaterQualityData where StationID = '{1}' and SampleTime >= '{2}' and SampleTime <= '{3}' group by {0} having count({0})>1 order by count({0})", SelectedParameter, item.Value, txtStartDate.Text, txtEndDate.Text);
                        //dt = GetData(query);
                        //int rowcnt = dt.Rows.Count;
                        //TableCell tcell_mode = new TableCell();
                        //int row = 0;
                        //if (rowcnt == 0)
                        //    tcell_mode.Text = "NO MODE";
                        //while (rowcnt > 0)
                        //{   //To do
                        //    if (rowcnt == 1)
                        //        tcell_mode.Text = dt.Rows[row][0].ToString();
                        //    else
                        //    {
                        //        if (row == rowcnt - 1)
                        //        {
                        //            if (dt.Rows[row][1].ToString() == dt.Rows[row - 1][1].ToString())
                        //                tcell_mode.Text += dt.Rows[row][0].ToString();
                        //            break;
                        //        }
                        //        else
                        //        {
                        //            if (dt.Rows[row][1].ToString() == dt.Rows[row + 1][1].ToString())
                        //                tcell_mode.Text += dt.Rows[row][0].ToString() + ",";
                        //            else
                        //            {
                        //                break;
                        //            }
                        //        }
                        //    }
                        //    row++;
                        //    rowcnt--;
                        //}
                        //tcell_mode.Wrap = false;
                        //trow.Cells.Add(tcell_mode);
                        //tbl_Summary.Rows.Add(trow);

                    }
                    if (max > 3)
                    {
                        Chart2.Width = 150 * max;
                    }
                    Panel1.Controls.Add(Chart2);
                }
            }
            
            tbl_Summary.BorderColor = System.Drawing.Color.Black;
            tbl_Summary.BorderStyle = BorderStyle.Solid;
            
        }

        protected void btn_Remove_Click(object sender, ImageClickEventArgs e)
        {
            if (listboxSelected.Items.Count == 0)

            {
                tbl_Summary.Rows.Clear();
            }
            for (int i = listboxSelected.Items.Count - 1; i >= 0; i--)
            {
                if (listboxSelected.Items[i].Selected)
                {
                    listboxAvailable.Items.Add(listboxSelected.Items[i]);
                    listboxSelected.Items.Remove(listboxSelected.Items[i]);
                }

            }


        }

        protected void btn_Add_Click(object sender, ImageClickEventArgs e)
        {
               for (int i = listboxAvailable.Items.Count - 1; i >= 0; i--)
                {

                    if (listboxAvailable.Items[i].Selected)
                    {
                        listboxSelected.Items.Add(listboxAvailable.Items[i]);
                        listboxAvailable.Items.Remove(listboxAvailable.Items[i]);
                    }

                }
        }

    }
}
