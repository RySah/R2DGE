using System;
using Terminal.Gui;

class Program
{
    static void Main(string[] args)
    {
        string userName = Environment.UserName;
        Console.WriteLine("Current User: " + userName);
        //Application.Init();

        //var top = Application.Top;

        //var win = new Window("Mouse Click Demo")
        //{
        //    X = 0,
        //    Y = 1, // Leave one row for the toplevel menu
        //    Width = Dim.Fill(),
        //    Height = Dim.Fill()
        //};

        //var label = new Label("Click inside the terminal window!")
        //{
        //    X = 1,
        //    Y = 1
        //};

        //win.Add(label);


        //MenuBar menuBar = new MenuBar( new MenuBarItem[] {
        //    new MenuBarItem("File", new MenuItem[] { new("Save", "Save the file ig", () => { }) })
        //    }
        //    );

        //win.Add(menuBar);

        //top.Add(win);

        //Application.RootMouseEvent += (MouseEvent me) =>
        //{
        //    label.Text = $"Mouse event at ({me.X}, {me.Y}) - {me.Flags}";
        //    Application.Refresh();
        //};

        //Application.Run();
    }
}
