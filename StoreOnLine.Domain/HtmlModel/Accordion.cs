using System;
using System.Collections.Generic;

namespace StoreOnLine.Domain.HtmlModel
{
    public class Accordion
    {
        public Panel Panel { get; set; }

        public Accordion()
        {
            Panel = new Panel();
        }

        public Panel GetPanel()
        {
            return Panel;
        }

        public List<SubPanel> GetSubPanels()
        {
            return Panel.SubPanels;
        } 
        

        public void AddPanel(int id, string title)
        {
            Panel.Id = id;
            Panel.Title = title;
        }

        public void AddSubPanel(int parentId, int id, string title, string message)
        {
            if (Panel.SubPanels == null)
            {
                Panel.SubPanels = new List<SubPanel>();
            }
            Panel.SubPanels.Add(new SubPanel(parentId, id, title, message));
        }

    }
    public class Panel
    {
        public int Id { get; set; } // 1-> Categorias 2-> Campañas
        public string Title { get; set; }
        public List<SubPanel> SubPanels { get; set; }
    }

    public class SubPanel
    {
        public int IdParent { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public SubPanel(int parentId, int id, string title, string message)
        {
            IdParent = parentId;
            Id = id;
            Title = title;
            Message = message;
        }


    }
}
