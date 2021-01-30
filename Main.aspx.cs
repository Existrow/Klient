using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Text;
using System.Security.Cryptography;

namespace Klient
{
    public static class Variables
    {
        public static int Pager { get; set; }
        public static int? Auth = null;

    }
    public partial class WebForm1 : Page
    {
        Service.WebService1 service1 = new Service.WebService1();

        protected void Page_Load(object sender, EventArgs e)
        {
            NameBox.Visible = false;
            RoomsBox.Visible = false;
            ExponatsBox.Visible = false;

            LogutButton.Visible = Panel1.Visible = Variables.Auth != null ? true : false;
            AuthPanel.Visible = Variables.Auth != null ? false : true;

            RoomsBox.Items.Clear();
            foreach (var i in service1.GetRooms())
                RoomsBox.Items.Add(new ListItem(i.Name, i.Id.ToString()));

            ExponatsBox.Items.Clear();
            foreach (var i in service1.GetExponats())
                ExponatsBox.Items.Add(new ListItem(i.Name, i.Id.ToString()));
        }

        protected void roomsView_Click(object sender, EventArgs e)
        {
            Variables.Pager = 0;
            NameBox.Visible = true;
            RoomsBox.Visible = false;
            ExponatsBox.Visible = false;
            GridView1.DataSource = service1.GetRooms()
                .Select(p => new
                {
                    ID = p.Id,
                    Имя = p.Name
                });
            GridView1.DataBind();
        }

        protected void exponatsView_Click(object sender, EventArgs e)
        {
            Variables.Pager = 1;
            NameBox.Visible = true;
            RoomsBox.Visible = false;
            ExponatsBox.Visible = false;
            GridView1.DataSource = service1.GetExponats()
                .Select(p => new
                {
                    ID = p.Id,
                    Имя = p.Name
                });
            GridView1.DataBind();
        }

        protected void migratView_Click(object sender, EventArgs e)
        {
            Variables.Pager = 2;
            NameBox.Visible = false;
            RoomsBox.Visible = true;
            ExponatsBox.Visible = true;
            GridView1.DataSource = service1.GetMigrats()
                .Select(p => new
                {
                    ID = p.Id,
                    Дата = p.Date1.ToShortDateString(),
                    Комната = service1.GetRooms()
                        .Where(room => room.Id == p.RoomId)
                        .Select(el => el.Name).First(),
                    Экспонат = service1.GetExponats()
                        .Where(exponat => exponat.Id == p.ExponatId)
                        .Select(el => el.Name).First()
                });
            GridView1.DataBind();
        }

        protected void ButtonDel_Click(object sender, EventArgs e)
        {
            foreach(GridViewRow i in GridView1.Rows)
            {
                if (((CheckBox)i.Cells[0].FindControl("checker")).Checked)
                {
                    switch (Variables.Pager)
                    {
                        case 0:
                            service1.DelRoom(int.Parse(i.Cells[1].Text));
                            break;
                        case 1:
                            service1.DelExponat(int.Parse(i.Cells[1].Text));
                            break;
                        case 2:
                            service1.DelMigrat(int.Parse(i.Cells[1].Text));
                            break;
                    }
                }
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            switch(Variables.Pager)
            {
                case 0:
                    service1.AddRoom(NameBox.Text);
                    break;
                case 1:
                    service1.AddExponat(NameBox.Text);
                    break;
                case 2:
                    service1.AddMigrat(DateTime.Now, int.Parse(ExponatsBox.SelectedItem.Value), int.Parse(RoomsBox.SelectedItem.Value));
                    break;
            }
        }

        protected void AuthButton_Click(object sender, EventArgs e)
        {
            var hasher = MD5.Create();
            var hash = Convert.ToBase64String(hasher.ComputeHash(Encoding.UTF8.GetBytes(UserPassword.Text)));
            Variables.Auth = service1.Auth(UserName.Text, hash);
            Page_Load(null, e);
        }

        protected void RegButton_Click(object sender, EventArgs e) =>
            service1.Register(UserName.Text, UserPassword.Text);

        protected void LogutButton_Click(object sender, EventArgs e)
        {
            Variables.Auth = null;
            Page_Load(null, e);
        }
    }
}