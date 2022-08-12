using System.Collections;
using System.Windows.Forms;
using Aveva.Core.Database;
using PR = Aveva.Core.Presentation;
using Aveva.Core.Presentation;
using System;
using Aveva.Core.Database.Filters;
using Aveva.Core.PMLNet;

namespace NetGridControl
{
    public partial class NetGridCntrl : UserControl
    {
        PR.NetGridControl netGridControl;
        Hashtable atts;
        Hashtable titles;
        Hashtable items;
        DBElementCollection Specs;
        TypeFilter spcoFilter;
        string tableName = "Spec Viewer";

        public NetGridCntrl()
        {
            InitializeComponent();
            spcoFilter = new TypeFilter(DbElementTypeInstance.SPCOMPONENT);
            items = new Hashtable();

            CollectElements();
            AddGridToForm();               
            InitializeGrid();

        }

        private void CollectElements()
        {
            items = new Hashtable();
            int index = 1;
            TypeFilter typeFilter = new TypeFilter(DbElementTypeInstance.SPECIFICATION);
            AttributeValueFilter attributeFilter = new AttributeValueFilter(DbAttributeInstance.PURP, TestOperator.Equals, "PIPE");
            AndFilter andFilter = new AndFilter(typeFilter, attributeFilter);

            Db[] dbs = MDB.CurrentMDB.GetDBArray(DbType.Catalogue);
            foreach (Db db in dbs)
            {
                DbElement world = db.World;

                Specs = new DBElementCollection(world, andFilter);
                foreach (DbElement spec in Specs)
                {
                    DBElementCollection tempCollection = new DBElementCollection(spec, spcoFilter);
                    foreach (DbElement spco in tempCollection)
                    {
                        items.Add((double)index, spco);
                        index++;
                    }
                }
            }


        }

        private void AddGridToForm()
        {
            this.netGridControl = new PR.NetGridControl();
            this.panel1.Controls.Add(this.netGridControl);
        }

        private void InitializeGrid()
        {
            atts = new Hashtable();
            titles = new Hashtable();
            
            atts[1.0] = "Namn of PRMOWN";
            atts[2.0] = "STYP";
            atts[3.0] = "PPBO 1";
            atts[4.0] = "PPBO 2";
            atts[5.0] = "PPBO 3";
            atts[6.0] = ":IdentCodeENG of PRTREF";
            atts[7.0] = "DTXR";
            atts[8.0] = "MTXX";

            titles[1.0] = "Spec";
            titles[2.0] = "Type";
            titles[3.0] = "Arrive";
            titles[4.0] = "Leave";
            titles[5.0] = "Branch Conn";
            titles[6.0] = "Ident Code";
            titles[7.0] = "Description";
            titles[8.0] = "Material";

            try
            {
                NetDataSource netDataSource = new NetDataSource(tableName, atts, titles, items);
                this.netGridControl.BindToDataSource(netDataSource);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
            this.netGridControl.AfterRowFilterChanged += new PMLNetDelegate.PMLNetEventHandler(FilterChanged);
            
        }

        private void FilterChanged(ArrayList args)
        {
            //Aveva.Core.Utilities.CommandLine.Command.CreateCommand($@"$p changed").RunInPdms();
            //this.netGridControl.AutoFitColumns();
            //double total = this.netGridControl.getNumberRows();
            //Aveva.Core.Utilities.CommandLine.Command.CreateCommand($@"$p total = {total}").RunInPdms();
            //double filteredIn = this.netGridControl.getFilteredInRows().Count;
            //double filteredOut = this.netGridControl.getFilteredOutRows().Count;
            //Aveva.Core.Utilities.CommandLine.Command.CreateCommand($@"$p filteredIn = {filteredIn}").RunInPdms();
            //Aveva.Core.Utilities.CommandLine.Command.CreateCommand($@"$p filteredOut = {filteredOut}").RunInPdms();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            CollectElements();
            InitializeGrid();

        }
    }
}
