using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
namespace WebApplication1
{
    
    class MyRow
    {
        public DateTime date;
        public double value;
}

    public partial class Index : System.Web.UI.Page
    {
        public Chart Chart1;
        int max = 0;
        List<MyRow> datalist = new List<MyRow>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblError.Items.Clear();
                string query = "select distinct ID,StationName from StationMetaData";
                DataTable dt = GetData(query);
                listboxAvailable.DataSource = dt;
                listboxAvailable.DataTextField = "StationName";
                listboxAvailable.DataValueField = "ID";
                listboxAvailable.DataBind();
            }
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
                        cmd.CommandTimeout = 200;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }

        protected void TabContainer1_ActiveTabChanged1(object sender, EventArgs e)
        {
            int rc = validations();
            if (rc == 1)
                return;
            int index = TabContainer1.ActiveTabIndex;
            datalist.Clear();
            switch (index)
            {
                case 0:
                    Click("Temperature", TabPanel_temp);
                    TabPanel_temp.Controls.Add(Chart1);
                    break;
                case 1:
                    Click("PH", TabPanel_ph);
                    TabPanel_ph.Controls.Add(Chart1);
                    break;
                case 2:
                    Click("SpecificConductivity", TabPanel_SC);
                    TabPanel_SC.Controls.Add(Chart1);
                    break;
                case 3:
                    Click("Turbidity", TabPanel_tur);
                    TabPanel_tur.Controls.Add(Chart1);
                    break;
                case 4:
                    Click("DisolvedOxygen", TabPanel_do);
                    TabPanel_do.Controls.Add(Chart1);
                    break;
            }
        }

        public void Click(string SelectedParameter, TabPanel tp)
        {
            Chart1 = new Chart();
            Chart1.ChartAreas.Add("ChartArea1");
            Panel2.Controls.Clear();
           foreach(ListItem l in listboxAvailable.Items)
           {
               if (l.Selected)
               {
                   datalist.Clear();
                   generateGraph(SelectedParameter,l);
                   generateTable(SelectedParameter, l);
                   max++;
               }
           }
        }


        private void generateTable(string SelectedParameter,ListItem Station)
        {
            double avg=0;
            Panel1.Controls.Add(new LiteralControl("</br></br>"));
    
            Table t1 = new Table();
            t1.GridLines = GridLines.Both;
            t1.BorderWidth = 1;
            t1.BorderStyle = BorderStyle.Solid ;
            TableRow trow ;
            trow= new TableRow();
            TableCell tcell_stationID = new TableCell();
            tcell_stationID.Text = Station.Text;
            tcell_stationID.Wrap = false;
            tcell_stationID.HorizontalAlign = HorizontalAlign.Center;
            tcell_stationID.ColumnSpan = 2;
            trow.Cells.Add(tcell_stationID);
            t1.Rows.Add(trow);
            //Find the minimum value
            trow = new TableRow();
            TableCell tcell_minheader = new TableCell();
            TableCell tcell_min = new TableCell();
            tcell_minheader.Text = "Minimum";
            if (datalist.Count > 0)
            {

                double min= Math.Round(datalist[0].value,2);
                tcell_min.Text =min.ToString();
                tcell_min.Text += "\n On ";
                tcell_min.Text += datalist[0].date.ToString();
                }
                else
                {
                    tcell_min.Text = "  No Data  ";
                }
            
            tcell_min.HorizontalAlign = HorizontalAlign.Center;
            tcell_minheader.HorizontalAlign = HorizontalAlign.Center;
            trow.Cells.Add(tcell_minheader);
            trow.Cells.Add(tcell_min);
            t1.Rows.Add(trow);
            //Find the maximum value
            trow = new TableRow();
            TableCell tcell_max = new TableCell();
            TableCell tcell_maxheader = new TableCell();
            tcell_maxheader.Text = "Maximum";
            if (datalist.Count>0)
            {
               
                    tcell_max.Text =  Math.Round(datalist[datalist.Count-1].value,2).ToString();
                    tcell_max.Text += "\n On ";
                    tcell_max.Text += datalist[datalist.Count-1].date.ToString();
                }

                else
                {
                    tcell_max.Text = "  No Data  ";
                }
            
            tcell_max.HorizontalAlign = HorizontalAlign.Center;
            tcell_maxheader.HorizontalAlign = HorizontalAlign.Center;
            trow.Cells.Add(tcell_maxheader);
            trow.Cells.Add(tcell_max);
            
            t1.Rows.Add(trow);
            //Find the Avg Value
            trow = new TableRow();
            TableCell tcell_avgheader = new TableCell();
            TableCell tcell_avg = new TableCell();
            tcell_avgheader.Text = "Mean";
            if (datalist.Count > 0)
            {
                 avg = 0;
                foreach(MyRow r in datalist)
                {
                    avg += r.value;
                }
                avg /= datalist.Count;
                    tcell_avg.Text =  Math.Round(avg,2).ToString();
                }
                else
                {
                    tcell_avg.Text = "  No Data  ";
                }
            
            tcell_avg.Wrap = false;
            tcell_avg.HorizontalAlign = HorizontalAlign.Center;
            tcell_avgheader.HorizontalAlign = HorizontalAlign.Center;
            trow.Cells.Add(tcell_avgheader);
            trow.Cells.Add(tcell_avg);
            t1.Rows.Add(trow);

            //Find the Standard Deviation
            trow = new TableRow();
            TableCell tcell_stdev = new TableCell();
            TableCell tcell_stdevheader = new TableCell();
            tcell_stdevheader.Text = "Standard Deviation";
            if (datalist.Count > 0)
            {
                double varience=0,stdev=0;
                foreach(MyRow r in datalist)
                {
                    varience+=Math.Pow((r.value-avg),2);
                }
                varience = Math.Sqrt(varience);
                stdev = Math.Sqrt(varience);
                tcell_stdev.Text = Math.Round(stdev,2).ToString();
            }
               
           
            else
            {
                tcell_stdev.Text = "  No Data  ";
            }
            tcell_stdev.HorizontalAlign = HorizontalAlign.Center;
            tcell_stdevheader.HorizontalAlign = HorizontalAlign.Center;
            trow.Cells.Add(tcell_stdevheader);
            trow.Cells.Add(tcell_stdev);
            t1.Rows.Add(trow);

            //Find the Median value
            trow = new TableRow();
            TableCell tcell_medheader = new TableCell();
            tcell_medheader.Text = "Median";
            
            TableCell tcell_median = new TableCell();
            int cnt=datalist.Count;
            if (cnt > 0)
            {
                if (cnt % 2 == 0)
                    tcell_median.Text =  Math.Round(((datalist[cnt/2 - 1].value + datalist[cnt/2].value) / 2),2).ToString();
                else
                    tcell_median.Text = Math.Round(datalist[cnt / 2].value,2).ToString();
            }
                else
                {
                    tcell_median.Text = "  No Data  ";
                }
            
            
            tcell_median.Wrap = false;
            tcell_median.HorizontalAlign = HorizontalAlign.Center;
            tcell_medheader.HorizontalAlign = HorizontalAlign.Center;
            trow.Cells.Add(tcell_medheader);
            trow.Cells.Add(tcell_median);
            t1.Rows.Add(trow);

            //Find Q1
            trow = new TableRow();
            TableCell tcell_q1header = new TableCell();
            tcell_q1header.Text = "Q1";
            TableCell tcell_Q1 = new TableCell();
            cnt = datalist.Count;
            if (cnt > 0)
            {
                int q1=(cnt+1)/4;
                    tcell_Q1.Text =  Math.Round(datalist[q1].value,2).ToString();
                }
              
                else
                {
                    tcell_Q1.Text = "  No Data  ";
                }
            
           tcell_q1header.HorizontalAlign=HorizontalAlign.Center;
            tcell_Q1.HorizontalAlign = HorizontalAlign.Center;
            tcell_Q1.Wrap = false;
            trow.Cells.Add(tcell_q1header);
            trow.Cells.Add(tcell_Q1);
            t1.Rows.Add(trow);
            //Find Q2
            trow = new TableRow();
            TableCell tcell_q2header = new TableCell();
            tcell_q2header.Text = "Q2";
            TableCell tcell_Q2 = new TableCell();
            cnt = datalist.Count;
            if (cnt > 0)
            {
                if (cnt % 2 == 0)
                    tcell_Q2.Text =  Math.Round(((datalist[cnt / 2 - 1].value + datalist[cnt / 2].value) / 2),2).ToString();
                else
                    tcell_Q2.Text =  Math.Round(datalist[cnt / 2].value,2).ToString();
            }
            else
            {
                tcell_Q2.Text = "  No Data  ";
            }
           
            tcell_Q2.Wrap = false;
            tcell_q2header.HorizontalAlign = HorizontalAlign.Center;
            tcell_Q2.HorizontalAlign = HorizontalAlign.Center;
            trow.Cells.Add(tcell_q2header);
            trow.Cells.Add(tcell_Q2);
            t1.Rows.Add(trow);
            //Find Q3
            trow = new TableRow();
            TableCell tcell_q3header = new TableCell();
            tcell_q3header.Text = "Q3";
            TableCell tcell_Q3 = new TableCell();
            if (datalist.Count > 0)
            {
                int q3 = datalist.Count * 3 / 4;
                tcell_Q3.Text = Math.Round(datalist[q3].value,2).ToString();
                }
            
            else
            {
                tcell_Q3.Text = "  No Data  ";
            }
            tcell_Q3.Wrap = false;
            tcell_q3header.HorizontalAlign = HorizontalAlign.Center;
            tcell_Q3.HorizontalAlign = HorizontalAlign.Center;
            trow.Cells.Add(tcell_q3header);
            trow.Cells.Add(tcell_Q3);
            t1.Rows.Add(trow);
            Panel1.Direction = ContentDirection.LeftToRight;
            Panel1.ScrollBars = ScrollBars.Both;
            Panel1.Controls.Add(t1);
        }

        private void generateGraph(string SelectedParameter,ListItem Station)
        {
            Chart1.ChartAreas["ChartArea1"].AxisX.MajorGrid.LineColor = Color.Gray;
            Chart1.ChartAreas["ChartArea1"].AxisY.MajorGrid.LineColor = Color.Gray;
            TableRow trow = new TableRow();
            TableCell tcell_SelectedParameter = new TableCell();
            tcell_SelectedParameter.Text = SelectedParameter;
            tcell_SelectedParameter.HorizontalAlign = System.Web.UI.WebControls.HorizontalAlign.Center;
            trow.Cells.Add(tcell_SelectedParameter);
            Chart1.ChartAreas[0].AxisX.Title = "Sample Time";
            Chart1.ChartAreas[0].AxisX.TitleAlignment = System.Drawing.StringAlignment.Center;
            Chart1.ChartAreas[0].AxisX.TitleFont = new System.Drawing.Font("Arial,Helvetica,sans-serif", 11, FontStyle.Bold);
            Chart1.ChartAreas[0].AxisY.Title = SelectedParameter;
            Chart1.ChartAreas[0].AxisY.TitleFont = new System.Drawing.Font("Arial,Helvetica,sans-serif", 11, FontStyle.Bold);
            Chart1.ChartAreas[0].AxisY.TitleAlignment = System.Drawing.StringAlignment.Center;
            Chart1.Series.Add(Station.Text);
            Chart1.Series[Station.Text].ChartType = SeriesChartType.Line;
            Chart1.Series[Station.Text].IsVisibleInLegend = true;
            Chart1.ApplyPaletteColors();
            Label l1_line = new Label();
            l1_line.Text = "----   ";
            l1_line.ForeColor = Chart1.Series[Station.Text].Color;
            Label l1_name = new Label();
            l1_name.Text = Station.Text;
            string query = string.Format("SELECT SampleTime, {0} from WaterQualityData where StationID = '{1}' and SampleTime >= '{2}' and SampleTime <= '{3}' order by SampleTime asc", SelectedParameter, Station.Value, txtStartDate.Text, txtEndDate.Text);
            DataTable dt = GetData(query);
            DateTime StartDate = Convert.ToDateTime(txtStartDate.Text.ToString());
            decimal []y1=new decimal[dt.Rows.Count];
            string[] x1 = new string[dt.Rows.Count];
            for (int k = 0; k < dt.Rows.Count; k++)
                y1[k] = 0;
            int cnt = 0,i,j=0;
            DateTime d1=StartDate;
            
            for(i=0,j=0;i<dt.Rows.Count;i++)
            {
                DateTime d3 = FinalDatetime(d1);
                if(Convert.ToDateTime(dt.Rows[i][0].ToString())<=FinalDatetime(d1))
                {
                    if (!Convert.IsDBNull(dt.Rows[i][1]))
                    {
                        y1[j] += Convert.ToDecimal(Math.Round(Convert.ToDouble(dt.Rows[i][1]), 2));
                    }
                    cnt++;
                }
                else
                {
                    if (cnt > 0)
                    {
                        i -= 1;
                        DataSet ds = dt.DataSet;
                        
                        x1[j] = dt.Rows[i][0].ToString();
                        y1[j] = y1[j] / cnt;
                        cnt = 0;
                        MyRow r1 = new MyRow();
                        r1.date = Convert.ToDateTime(dt.Rows[i][0]);
                        r1.value = Convert.ToDouble(y1[j]);
                        datalist.Add(r1);
                        j++;
                        
                    }
                    else
                    {
                        i=i-1;
                    }
                    d1 = FinalDatetime(d1);
                }
            }
            if(cnt!=0)
            {
                x1[j] = dt.Rows[i-1][0].ToString();
                y1[j] = y1[j] / cnt;
                MyRow r1 = new MyRow();
                r1.date = Convert.ToDateTime(dt.Rows[i-1][0]);
                r1.value = Convert.ToDouble(y1[j]);
                datalist.Add(r1);
            }
            for ( i = 0; i < j; i++)
            {
                Chart1.Series[Station.Text].Points.AddXY(x1[i], y1[i]);
                Chart1.Series[Station.Text].Font = new System.Drawing.Font("Arial,Helvetica,sans-serif", 11);
            }
            Chart1.Width = 750;
            Chart1.Height = 250;
            Panel2.Controls.Add(l1_line);
            Panel2.Controls.Add(l1_name);
            Panel2.Controls.Add(new LiteralControl("</br>"));
           datalist.Sort((x,y)=> {
                                    if(x.value>y.value) 
                                        return 1;  
                                    else if(x.value==y.value)
                                        return 0;
                                    else return -1;}
                                    );
           Console.WriteLine(datalist);
            
        }
       
        protected void Button1_Click(object sender, EventArgs e)
        {
           int rc= validations();
            if(rc==1)
            {
                return;
            }
            TabContainer1_ActiveTabChanged1(sender, e);
        }

        protected int validations()
        {
            int flag = 0;
            lblError.Items.Clear();
            if (String.IsNullOrEmpty(txtStartDate.Text))
            {
                lblError.Items.Add("Start Date Cannot be Empty");
                flag = 1;
            }
            if (String.IsNullOrEmpty(txtEndDate.Text))
            {
                lblError.Items.Add("End Date Cannot be Empty");
                flag = 1;
            }
            if (String.IsNullOrEmpty(txtbox_timestep.Text))
            {
                lblError.Items.Add("Histogram Time Step Cannot be Empty");
                flag = 1;
            }
            else
            {
                char[] temp=txtbox_timestep.Text.ToCharArray();
                for(int i=0;i<temp.Length;i++)
                {
                    if (temp[i] >= 48 && temp[i] <= 57)
                    {
                    }
                    else
                        flag = 1;
                }

            }
            if (listboxAvailable.SelectedIndex == -1)
            {
                lblError.Items.Add("Atleast One station needs to be selected");
                flag = 1;
            }
            if (flag == 1)
                lblError.Visible = true;
            return flag;
        }
        

        protected DateTime FinalDatetime(DateTime d1)
        {
            DateTime d2=new DateTime();
            String parameter=ddl_histogramstep.SelectedValue.ToString();
            switch(parameter)
            {
                case "Minutes":
                    d2 = d1.AddMinutes(Convert.ToDouble(txtbox_timestep.Text.ToString()));
                    break;
                case "Hours":
                    d2=d1.AddHours((Convert.ToDouble(txtbox_timestep.Text.ToString())));
                    break;
                case "Days":
                    d2=d1.AddDays(Convert.ToDouble(txtbox_timestep.Text.ToString()));
                    break;
                case "Weeks":
                    d2=d1.AddDays(7*(Convert.ToDouble(txtbox_timestep.Text.ToString())));
                    break;
                case "Months":
                    d2=d1.AddMonths(Convert.ToInt16(txtbox_timestep.Text.ToString()));
                    break;
                case "Years":
                    d2=d1.AddYears(Convert.ToInt16(txtbox_timestep.Text.ToString()));
                    break;
            }
            return d2;
        }
    }
}