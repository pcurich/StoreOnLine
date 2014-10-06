using System;
using System.Collections.Generic;

namespace StoreOnLine.Domain.HtmlModel
{
    public class Accordion
    {
        public Panel Panel { get; set; }

        private static volatile Accordion _instance;
        private static readonly object SyncRoot = new Object();

        public Accordion()
        {
            Panel = new Panel();
        }

        public static Accordion Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new Accordion();
                    }
                }
                return _instance;
            }
        }

        public void AddPanel(int id, string title)
        {
            Instance.Panel.Id = id;
            Instance.Panel.Title = title;
        }

        public void AddSubPanel(int parentId, int id, string title, string message)
        {
            if (Instance.Panel.SubPanels == null)
            {
                Instance.Panel.SubPanels = new List<SubPanel>();
            }
            Instance.Panel.SubPanels.Add(new SubPanel(parentId, id, title, message));
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
