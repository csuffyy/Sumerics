﻿namespace Sumerics.Api
{
    using YAMP;

    [Description("This function is useful if you want to undo the the undock function. Docking a plot displays the last undocked plot into the visualization tab.")]
    [Kind("UI")]
    sealed class DockFunction : ArgumentFunction
    {
        readonly IApplication _application;

        public DockFunction(IApplication application)
        {
            _application = application;
        }

        [Description("Docks the last undocked plot to the main window.")]
        [Example("dock()", "Returns the last undocked plot to the visualization tab.")]
        public void Function()
        {
            var visualizer = _application.Get<IVisualizer>();
            visualizer.DockLast();
        }

        [Description("Docks the given plot to the main window.")]
        [Example("a=cplot(sin); undock(); sleep(5000); dock(a)", "Returns the complex plot of the sinus to the visualization tab after 5s.")]
        public void Function(PlotValue plot)
        {
            var visualizer = _application.Get<IVisualizer>();
            visualizer.Dock(plot);
        }
    }
}
