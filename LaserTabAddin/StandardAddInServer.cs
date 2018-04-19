using System;
using System.Runtime.InteropServices;
using Inventor;
using Microsoft.Win32;
using stdole;
using System.Windows.Forms;
using System.Resources;
using System.Collections.Generic;

namespace LaserTabAddin
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("0defbf22-e302-4266-9bc9-fb80d5c8eb7e")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {

        // Inventor application object.
        private Inventor.Application m_inventorApplication;

        private ButtonDefinition m_buttonDef;
        private UserInterfaceEvents m_uiEvents;
        private InteractionEvents m_interaction;
        private SelectEvents m_selects; // keeping a reference to the events objects seems to be crucial! (even tough it is part of m_interaction)
        private LaserTabForm m_dialog;

        public StandardAddInServer()
        {
        }
        
        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {
            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            m_inventorApplication = addInSiteObject.Application;

            // TODO: Add ApplicationAddInServer.Activate implementation.
            // e.g. event initialization, command creation etc.

            // Get a reference to the UserInterfaceManager object.
            Inventor.UserInterfaceManager UIManager = m_inventorApplication.UserInterfaceManager;

            // Get a reference to the ControlDefinitions object.
            ControlDefinitions controlDefs = m_inventorApplication.CommandManager.ControlDefinitions;

            // Get the images from the resources.  They are stored as .Net images and the
            // PictureConverter class is used to convert them to IPictureDisp objects, which
            // the Inventor API requires. 
            stdole.IPictureDisp icon_large = PictureConverter.ImageToPictureDisp(Properties.Resources.icon32);
            stdole.IPictureDisp icon_small = PictureConverter.ImageToPictureDisp(Properties.Resources.icon16);

            // Create the button definition.
            m_buttonDef = controlDefs.AddButtonDefinition("make tabs", "UIRibbonSampleOne",
                                                 CommandTypesEnum.kNonShapeEditCmdType,
                                                 "0defbf22-e302-4266-9bc9-fb80d5c8eb7e", "", "", icon_small, icon_large);

            // Call the function to add information to the user-interface.
            if (firstTime)
            {
                CreateUserInterface();
                //PrintRibbonNames();
            }

            // Connect to UI events to be able to handle a UI reset.
            m_uiEvents = m_inventorApplication.UserInterfaceManager.UserInterfaceEvents;
            m_uiEvents.OnResetRibbonInterface += m_uiEvents_OnResetRibbonInterface;

            m_buttonDef.OnExecute += m_buttonDef_OnExecute;
        }

        private void CreateUserInterface()
        {
            // Get a reference to the UserInterfaceManager object. 
            Inventor.UserInterfaceManager UIManager = m_inventorApplication.UserInterfaceManager;

            // Get the zero doc ribbon.
            Inventor.Ribbon zeroRibbon = UIManager.Ribbons["Part"];

            // Get the getting started tab.
            Inventor.RibbonTab startedTab = zeroRibbon.RibbonTabs["id_TabModel"];

            // Get the new features panel.
            Inventor.RibbonPanel newFeaturesPanel = startedTab.RibbonPanels["id_PanelP_ModelModify"];

            // Add a button to the panel, using the previously created button definition.
            newFeaturesPanel.CommandControls.AddButton(m_buttonDef, true);
        }

        private void m_uiEvents_OnResetRibbonInterface(NameValueMap context)
        {
            CreateUserInterface();
        }

        private void m_buttonDef_OnExecute(NameValueMap context)
        {
            if (m_dialog != null)
            {
                m_dialog.Focus();
                return;
            }

            m_interaction = m_inventorApplication.CommandManager.CreateInteractionEvents();
            m_interaction.StatusBarText = "Select the face to add tabs to";
            m_selects = m_interaction.SelectEvents;
            m_selects.ClearSelectionFilter();
            m_selects.AddSelectionFilter(SelectionFilterEnum.kPartFaceFilter);
            m_selects.SingleSelectEnabled = true;
            m_selects.MouseMoveEnabled = false; // recommended for performance if not used
            m_selects.Enabled = true; // ???
            // no idea what these event sinks are and why we need them...
            m_selects.OnSelect += new Inventor.SelectEventsSink_OnSelectEventHandler(this.SelectEvents_OnSelect);
            m_interaction.OnTerminate += new Inventor.InteractionEventsSink_OnTerminateEventHandler(this.M_interaction_OnTerminate);
            

            m_dialog = new LaserTabForm();

            m_dialog.setLabel("select a face");
            m_dialog.Show(new InventorMainFrame(m_inventorApplication.MainFrameHWND));
            m_dialog.FormClosed += M_dialog_FormClosed;

            m_interaction.Start();

        }

        private void M_interaction_OnTerminate()
        {
            stop_selection();
        }

        private void M_dialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            stop_selection();
        }

        private void stop_selection()
        {
            m_interaction.SelectEvents.OnSelect -= new Inventor.SelectEventsSink_OnSelectEventHandler(this.SelectEvents_OnSelect);
            m_interaction.OnTerminate -= new Inventor.InteractionEventsSink_OnTerminateEventHandler(this.M_interaction_OnTerminate);
            m_interaction.Stop();
            m_interaction = null;

            m_dialog.FormClosed -= M_dialog_FormClosed;
            m_dialog.Close();
            m_dialog = null;
        }

        private void SelectEvents_OnSelect(ObjectsEnumerator JustSelectedEntities, SelectionDeviceEnum SelectionDevice, Point ModelPosition, Point2d ViewPosition, Inventor.View View)
        {
            if (JustSelectedEntities.Count != 1)
            {
                m_dialog.setLabel(string.Format("Wut? You selected {} objects.", JustSelectedEntities.Count));
                return;
            }
            
            if (JustSelectedEntities[1] is Face)
            {
                Face f = JustSelectedEntities[1] as Face;
                m_dialog.setLabel(string.Format("selected: {0}, area = {1}", f.InternalName, f.Evaluator.Area));
            }
            else
            {
                m_dialog.setLabel("Please select a face. This should not happen!");
            }
        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            // TODO: Add ApplicationAddInServer.Deactivate implementation

            // Release objects.
            m_inventorApplication = null;

            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        #endregion


        private void PrintRibbonNames()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Samuel Bryner\ribbons.txt"))
            {
                Inventor.UserInterfaceManager UIManager = m_inventorApplication.UserInterfaceManager;

                // Get the zero doc ribbon.
                foreach (Ribbon rib in UIManager.Ribbons)
                {
                    file.WriteLine(rib.InternalName);
                    foreach (RibbonTab tab in rib.RibbonTabs)
                    {
                        file.WriteLine(string.Format("  - Tab: internal name = '{0}', display name = '{1}'", tab.InternalName, tab.DisplayName));
                        foreach (RibbonPanel panel in tab.RibbonPanels)
                        {
                            file.WriteLine(string.Format("      - Panel: {0} = '{1}'", panel.DisplayName, panel.InternalName));
                        }
                    }
                }
            }
        }
    }
}
