using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System.Collections.Generic;

namespace Assignment9
{
    [Activity(Label = "SQLiteDB", MainLauncher = true)]
    public class MainActivity : Activity
    {
        ListView lstViewData;
        List<Shopping> listSource = new List<Shopping>();
        Database db;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource  
            SetContentView(Resource.Layout.activity_main);
            //Create Database  
            db = new Database();
            db.createDatabase();
            lstViewData = FindViewById<ListView>(Resource.Id.listView);
            var edtName = FindViewById<EditText>(Resource.Id.edtName);
            
            var btnAdd = FindViewById<Button>(Resource.Id.btnAdd);
            var btnEdit = FindViewById<Button>(Resource.Id.btnEdit);
            var btnRemove = FindViewById<Button>(Resource.Id.btnRemove);
            //Load Data  
            LoadData();
            //Event  
            btnAdd.Click += delegate
            {
                Shopping shopping = new Shopping()
                {
                    Name = edtName.Text,
                    
                };
                db.insertIntoTable(shopping);
                LoadData();
            };
            btnEdit.Click += delegate
            {
                Shopping shopping = new Shopping()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                    
                };
                db.updateTable(shopping);
                LoadData();
            };
            btnRemove.Click += delegate
            {
                Shopping shopping = new Shopping()
                {
                    Id = int.Parse(edtName.Tag.ToString()),
                    Name = edtName.Text,
                   
                };
                db.removeTable(shopping);
                LoadData();
            };
            lstViewData.ItemClick += (s, e) =>
            {
                //Set Backround for selected item  
                for (int i = 0; i < lstViewData.Count; i++)
                {
                    if (e.Position == i)
                        lstViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Black);
                    else
                        lstViewData.GetChildAt(i).SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
                //Binding Data  
                var txtName = e.View.FindViewById<TextView>(Resource.Id.txtView_Name);
            
                edtName.Tag = e.Id;
                
            };
        }
        private void LoadData()
        {
            listSource = db.selectTable();
            var adapter = new ListViewAdapter(this, listSource);
            lstViewData.Adapter = adapter;
        }
       // public void OnDelete(object sender, EventArgs e)
       // {
       //     var mi = ((MenuItem)sender);
        //    DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "OK");
       // }
    }
}